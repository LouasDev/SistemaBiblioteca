using Newtonsoft.Json;
using SUB.Models;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SUB.Utils
{
    public static class AtualizacaoService
    {
        public const long MaxTamanhoPacoteBytes = 300L * 1024 * 1024;
        private const long MargemEspacoLivreBytes = 20L * 1024 * 1024;

        private static readonly string[] hostsPermitidos =
        {
            "github.com",
            "objects.githubusercontent.com",
            "release-assets.githubusercontent.com",
            "raw.githubusercontent.com"
        };

        private const string ChavePublicaAssinaturaXml =
            "<RSAKeyValue><Modulus>sRFREn6nY1ybpGJgg6uSw1w1/SDxlX4ffVlte1AyLIo4WQvTbLjqaFOCJM4lut9AHRrfnJ4GL+4jXacX4dObBAXFGdt8aUfvQc2JzlxhRMwr7Y4aupSGWo1bfagVMSkbVP5dgiARoM64lY5Bkeckf47+srP+j28PSha+aZnk8UE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        private static readonly HttpClient http;
        private static readonly HttpClient httpDownload;

        static AtualizacaoService()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            http = new HttpClient();
            httpDownload = new HttpClient();
            httpDownload.Timeout = Timeout.InfiniteTimeSpan;
        }

        public static string UrlManifestoConfigurada =>
            (ConfigurationManager.AppSettings["ManifestoAtualizacaoUrl"] ?? string.Empty).Trim();

        public static string VersaoAtual()
        {
            var local = Assembly.GetExecutingAssembly().Location;
            return FileVersionInfo.GetVersionInfo(local).FileVersion ?? string.Empty;
        }

        public static async Task<ResultadoVerificacaoAtualizacao> VerificarAsync(string urlManifesto, int timeoutSegundos)
        {
            var resultado = new ResultadoVerificacaoAtualizacao
            {
                VersaoAtual = VersaoAtual(),
                Mensagem = string.Empty
            };

            urlManifesto = string.IsNullOrWhiteSpace(urlManifesto) ? UrlManifestoConfigurada : urlManifesto.Trim();

            if (string.IsNullOrEmpty(urlManifesto))
            {
                resultado.Mensagem = "Endereço do manifesto de atualização não configurado.";
                return resultado;
            }

            byte[] bytesManifesto;
            byte[] bytesAssinatura;
            try
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSegundos)))
                {
                    using (var resposta = await http.GetAsync(urlManifesto, HttpCompletionOption.ResponseContentRead, cts.Token).ConfigureAwait(false))
                    {
                        if (!resposta.IsSuccessStatusCode)
                        {
                            resultado.Mensagem = string.Format(
                                "Não foi possível obter o manifesto de atualização (HTTP {0}).",
                                (int)resposta.StatusCode);
                            return resultado;
                        }

                        bytesManifesto = await resposta.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    }

                    using (var respostaSig = await http.GetAsync(urlManifesto + ".sig", HttpCompletionOption.ResponseContentRead, cts.Token).ConfigureAwait(false))
                    {
                        if (!respostaSig.IsSuccessStatusCode)
                        {
                            resultado.AssinaturaInvalida = true;
                            resultado.Mensagem = "Assinatura do manifesto não encontrada. Atualização não confiável.";
                            return resultado;
                        }

                        bytesAssinatura = await respostaSig.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                resultado.SemInternet = true;
                resultado.Mensagem = "Tempo esgotado ao consultar atualizações. Verifique a conexão com a internet.";
                return resultado;
            }
            catch (HttpRequestException)
            {
                resultado.SemInternet = true;
                resultado.Mensagem = "Sem conexão com a internet. Não foi possível verificar atualizações.";
                return resultado;
            }

            if (!AssinaturaValida(bytesManifesto, bytesAssinatura))
            {
                resultado.AssinaturaInvalida = true;
                resultado.Mensagem = "Assinatura do manifesto inválida. Atualização não confiável.";
                return resultado;
            }

            AtualizacaoManifesto manifesto;
            try
            {
                manifesto = JsonConvert.DeserializeObject<AtualizacaoManifesto>(RemoverBom(bytesManifesto));
            }
            catch (JsonException)
            {
                resultado.ManifestoInvalido = true;
                resultado.Mensagem = "Manifesto de atualização inválido.";
                return resultado;
            }

            var erroManifesto = ValidarManifesto(manifesto);
            if (erroManifesto != null)
            {
                resultado.ManifestoInvalido = true;
                resultado.Mensagem = erroManifesto;
                return resultado;
            }

            Version atual;
            Version nova;
            if (!Version.TryParse(NormalizarVersao(resultado.VersaoAtual), out atual) ||
                !Version.TryParse(NormalizarVersao(manifesto.Versao), out nova))
            {
                resultado.ManifestoInvalido = true;
                resultado.Mensagem = "Versão informada no manifesto é inválida.";
                return resultado;
            }

            resultado.Manifesto = manifesto;
            resultado.VersaoNova = manifesto.Versao;
            resultado.AtualizacaoDisponivel = nova > atual;
            resultado.Sucesso = true;
            resultado.Mensagem = resultado.AtualizacaoDisponivel
                ? string.Empty
                : string.Format("Você já está usando a versão mais recente ({0}).", resultado.VersaoAtual);
            return resultado;
        }

        public static async Task<ResultadoDownloadPacote> BaixarPacoteAsync(
            AtualizacaoManifesto manifesto,
            IProgress<ProgressoDownload> progresso,
            CancellationToken cancelamento,
            int timeoutSegundos)
        {
            var resultado = new ResultadoDownloadPacote { Mensagem = string.Empty };

            if (manifesto == null || string.IsNullOrWhiteSpace(manifesto.Url))
            {
                resultado.Mensagem = "Pacote de atualização não informado.";
                return resultado;
            }

            Uri uri;
            if (!Uri.TryCreate(manifesto.Url.Trim(), UriKind.Absolute, out uri) || !UrlPacoteAceitavel(uri))
            {
                resultado.HostNaoPermitido = true;
                resultado.Mensagem = "Download de host não permitido. Só é aceito HTTPS em hosts do GitHub.";
                return resultado;
            }

            if (manifesto.Tamanho <= 0 || manifesto.Tamanho > MaxTamanhoPacoteBytes)
            {
                resultado.Mensagem = "Tamanho do pacote inválido ou acima do limite permitido.";
                return resultado;
            }

            var pastaUpdate = Path.Combine(AppPaths.AppDataFolder, "update");
            Directory.CreateDirectory(pastaUpdate);

            var caminhoFinal = Path.Combine(pastaUpdate, "SUB-" + NormalizarVersao(manifesto.Versao) + ".zip");
            var caminhoParcial = caminhoFinal + ".part";

            if (!EspacoSuficiente(pastaUpdate, manifesto.Tamanho))
            {
                resultado.EspacoInsuficiente = true;
                resultado.Mensagem = "Espaço insuficiente em disco para baixar a atualização.";
                return resultado;
            }

            if (File.Exists(caminhoParcial))
                File.Delete(caminhoParcial);

            try
            {
                using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancelamento))
                {
                    cts.CancelAfter(TimeSpan.FromSeconds(timeoutSegundos));

                    try
                    {
                        using (var resposta = await httpDownload.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cts.Token).ConfigureAwait(false))
                        {
                            if (!resposta.IsSuccessStatusCode)
                            {
                                resultado.Mensagem = string.Format(
                                    "Falha no download da atualização (HTTP {0}).",
                                    (int)resposta.StatusCode);
                                return resultado;
                            }

                            using (var fonte = await resposta.Content.ReadAsStreamAsync().ConfigureAwait(false))
                            using (var destino = new FileStream(caminhoParcial, FileMode.Create, FileAccess.Write, FileShare.None, 81920, true))
                            {
                                var buffer = new byte[81920];
                                long total = 0;
                                int lidos;

                                while ((lidos = await fonte.ReadAsync(buffer, 0, buffer.Length, cts.Token).ConfigureAwait(false)) > 0)
                                {
                                    total += lidos;
                                    if (total > manifesto.Tamanho)
                                    {
                                        resultado.Mensagem = "O pacote recebido é maior que o tamanho informado no manifesto.";
                                        return resultado;
                                    }

                                    await destino.WriteAsync(buffer, 0, lidos, cts.Token).ConfigureAwait(false);

                                    if (progresso != null)
                                    {
                                        progresso.Report(new ProgressoDownload
                                        {
                                            BytesBaixados = total,
                                            TotalBytes = manifesto.Tamanho
                                        });
                                    }
                                }

                                if (total != manifesto.Tamanho)
                                {
                                    resultado.Mensagem = "O tamanho do pacote baixado não corresponde ao informado no manifesto.";
                                    return resultado;
                                }
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        if (cancelamento.IsCancellationRequested)
                        {
                            resultado.Cancelado = true;
                            resultado.Mensagem = "Download cancelado.";
                        }
                        else
                        {
                            resultado.Mensagem = "Tempo esgotado durante o download da atualização.";
                        }
                        return resultado;
                    }
                    catch (HttpRequestException)
                    {
                        resultado.Mensagem = "Conexão interrompida durante o download da atualização.";
                        return resultado;
                    }
                    catch (IOException)
                    {
                        resultado.Mensagem = "Conexão interrompida durante o download da atualização.";
                        return resultado;
                    }
                }

                var hash = CalcularSha256(caminhoParcial);
                if (!string.Equals(hash, (manifesto.Sha256 ?? string.Empty).Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    resultado.HashDivergente = true;
                    resultado.Mensagem = "O arquivo baixado não confere com o hash SHA-256 publicado. Atualização descartada.";
                    return resultado;
                }

                if (File.Exists(caminhoFinal))
                    File.Delete(caminhoFinal);
                File.Move(caminhoParcial, caminhoFinal);

                resultado.Sucesso = true;
                resultado.CaminhoArquivo = caminhoFinal;
                resultado.Tamanho = manifesto.Tamanho;
                return resultado;
            }
            finally
            {
                if (!resultado.Sucesso && File.Exists(caminhoParcial))
                {
                    try { File.Delete(caminhoParcial); } catch (IOException) { }
                }
            }
        }

        internal static bool UrlPacoteAceitavel(Uri uri)
        {
            if (uri == null)
                return false;

            var ehLoopback = uri.Host.Equals("127.0.0.1", StringComparison.Ordinal) ||
                             uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase);
            if (ehLoopback && uri.Scheme == Uri.UriSchemeHttp)
                return true;

            return uri.Scheme == Uri.UriSchemeHttps &&
                   Array.Exists(hostsPermitidos, h => string.Equals(uri.Host, h, StringComparison.OrdinalIgnoreCase));
        }

        internal static bool EspacoSuficiente(string caminhoDiretorio, long tamanhoNecessario)
        {
            try
            {
                var raiz = Path.GetPathRoot(Path.GetFullPath(caminhoDiretorio));
                if (string.IsNullOrEmpty(raiz))
                    return true;

                var disco = new DriveInfo(raiz);
                return disco.AvailableFreeSpace >= tamanhoNecessario + MargemEspacoLivreBytes;
            }
            catch (ArgumentException)
            {
                return true;
            }
            catch (IOException)
            {
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
        }

        private static string CalcularSha256(string caminho)
        {
            using (var sha = SHA256.Create())
            using (var fs = new FileStream(caminho, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true))
            {
                var hash = sha.ComputeHash(fs);
                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private static bool AssinaturaValida(byte[] dados, byte[] assinatura)
        {
            try
            {
                var base64 = Encoding.ASCII.GetString(assinatura).Trim();
                var bytesAssinatura = Convert.FromBase64String(base64);

                using (var rsa = RSA.Create())
                {
                    rsa.FromXmlString(ChavePublicaAssinaturaXml);
                    return rsa.VerifyData(dados, bytesAssinatura, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
            catch (FormatException)
            {
                return false;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }

        private static string RemoverBom(byte[] bytes)
        {
            if (bytes != null && bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);

            return Encoding.UTF8.GetString(bytes ?? new byte[0]);
        }

        private static string ValidarManifesto(AtualizacaoManifesto manifesto)
        {
            if (manifesto == null || string.IsNullOrWhiteSpace(manifesto.Versao))
                return "Manifesto de atualização sem a versão.";

            var url = (manifesto.Url ?? string.Empty).Trim();
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                return "Manifesto sem endereço de download válido.";

            var sha = (manifesto.Sha256 ?? string.Empty).Trim();
            if (sha.Length != 64 || !EhHexadecimal(sha))
                return "Manifesto sem hash SHA-256 válido.";

            if (manifesto.Tamanho <= 0 || manifesto.Tamanho > MaxTamanhoPacoteBytes)
                return "Manifesto sem tamanho válido do pacote.";

            return null;
        }

        private static bool EhHexadecimal(string valor)
        {
            foreach (var c in valor)
            {
                var ehDigito = c >= '0' && c <= '9';
                var ehLetra = (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
                if (!ehDigito && !ehLetra)
                    return false;
            }

            return true;
        }

        internal static string NormalizarVersao(string versao)
        {
            if (string.IsNullOrWhiteSpace(versao))
                return versao;

            var cortado = versao.Trim();
            var corte = cortado.IndexOfAny(new[] { '-', '+' });
            if (corte >= 0)
                cortado = cortado.Substring(0, corte);
            return cortado;
        }
    }
}
