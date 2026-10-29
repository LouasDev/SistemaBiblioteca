using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SUB.Utils
{
    internal enum LivroBuscaStatus
    {
        Encontrado,
        IsbnInvalido,
        NaoEncontrado,
        Indisponivel
    }

    internal sealed class LivroBuscaResultado
    {
        internal LivroBuscaStatus Status { get; set; }
        internal string Nome { get; set; }
        internal string Autor { get; set; }
        internal string Genero { get; set; }
        internal string Fonte { get; set; }
    }

    internal static class LivroMetadataService
    {
        private static readonly HttpClient http;
        static LivroMetadataService()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            http.DefaultRequestHeaders.UserAgent.ParseAdd("SUB-SistemaBiblioteca/1.0");
        }

        internal static bool EhIsbn13Valido(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn) || isbn.Length != 13 || isbn.Any(c => c < '0' || c > '9'))
                return false;
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
            int digitoEsperado = (10 - (soma % 10)) % 10;
            return isbn[12] - '0' == digitoEsperado;
        }

        internal static async Task<LivroBuscaResultado> BuscarPorIsbn13Async(string isbn)
        {
            if (!EhIsbn13Valido(isbn))
                return new LivroBuscaResultado
                {
                    Status = LivroBuscaStatus.IsbnInvalido
                };
            var openLibrary = await BuscarOpenLibraryAsync(isbn);
            if (openLibrary.Livro != null)
                return openLibrary.Livro;
            var brasilApi = await BuscarBrasilApiAsync(isbn);
            if (brasilApi.Livro != null)
                return brasilApi.Livro;
            var googleBooks = await BuscarGoogleBooksAsync(isbn);
            if (googleBooks.Livro != null)
                return googleBooks.Livro;
            return new LivroBuscaResultado
            {
                Status = openLibrary.Respondeu || brasilApi.Respondeu || googleBooks.Respondeu ? LivroBuscaStatus.NaoEncontrado : LivroBuscaStatus.Indisponivel
            };
        }

        private static async Task<ConsultaFonteResultado> BuscarOpenLibraryAsync(string isbn)
        {
            string url = "https://openlibrary.org/api/books?bibkeys=ISBN:" + isbn + "&format=json&jscmd=data";
            try
            {
                using (var resposta = await http.GetAsync(url).ConfigureAwait(false))
                {
                    if (!resposta.IsSuccessStatusCode)
                        return ConsultaFonteResultado.Indisponivel();
                    var raiz = JObject.Parse(await resposta.Content.ReadAsStringAsync().ConfigureAwait(false));
                    var livro = raiz["ISBN:" + isbn];
                    if (livro == null)
                        return ConsultaFonteResultado.SemResultado();
                    return ConsultaFonteResultado.ComLivro(CriarResultado(livro["title"]?.ToString(), livro["authors"]?.First?["name"]?.ToString(), livro["subjects"]?.First?["name"]?.ToString(), "Open Library"));
                }
            }
            catch (HttpRequestException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (TaskCanceledException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (JsonException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
        }

        private static async Task<ConsultaFonteResultado> BuscarBrasilApiAsync(string isbn)
        {
            string url = "https://brasilapi.com.br/api/isbn/v1/" + isbn;
            try
            {
                using (var resposta = await http.GetAsync(url).ConfigureAwait(false))
                {
                    if (!resposta.IsSuccessStatusCode)
                        return ConsultaFonteResultado.Indisponivel();
                    var livro = JObject.Parse(await resposta.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return ConsultaFonteResultado.ComLivro(CriarResultado(livro["title"]?.ToString(), livro["authors"]?.First?.ToString(), livro["subjects"]?.First?.ToString(), "BrasilAPI (CBL)"));
                }
            }
            catch (HttpRequestException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (TaskCanceledException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (JsonException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
        }

        private static async Task<ConsultaFonteResultado> BuscarGoogleBooksAsync(string isbn)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn + "&maxResults=1";
            try
            {
                using (var resposta = await http.GetAsync(url).ConfigureAwait(false))
                {
                    if (!resposta.IsSuccessStatusCode)
                        return ConsultaFonteResultado.Indisponivel();
                    var raiz = JObject.Parse(await resposta.Content.ReadAsStringAsync().ConfigureAwait(false));
                    var informacoes = raiz["items"]?.First?["volumeInfo"];
                    if (informacoes == null)
                        return ConsultaFonteResultado.SemResultado();
                    return ConsultaFonteResultado.ComLivro(CriarResultado(informacoes["title"]?.ToString(), informacoes["authors"]?.First?.ToString(), informacoes["categories"]?.First?.ToString(), "Google Books"));
                }
            }
            catch (HttpRequestException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (TaskCanceledException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
            catch (JsonException)
            {
                return ConsultaFonteResultado.Indisponivel();
            }
        }

        private static LivroBuscaResultado CriarResultado(string nome, string autor, string genero, string fonte)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return null;
            return new LivroBuscaResultado
            {
                Status = LivroBuscaStatus.Encontrado,
                Nome = nome,
                Autor = autor,
                Genero = genero,
                Fonte = fonte
            };
        }

        private sealed class ConsultaFonteResultado
        {
            internal bool Respondeu { get; private set; }
            internal LivroBuscaResultado Livro { get; private set; }

            internal static ConsultaFonteResultado SemResultado()
            {
                return new ConsultaFonteResultado
                {
                    Respondeu = true
                };
            }

            internal static ConsultaFonteResultado Indisponivel()
            {
                return new ConsultaFonteResultado();
            }

            internal static ConsultaFonteResultado ComLivro(LivroBuscaResultado livro)
            {
                return livro == null ? SemResultado() : new ConsultaFonteResultado
                {
                    Respondeu = true,
                    Livro = livro
                };
            }
        }
    }
}
