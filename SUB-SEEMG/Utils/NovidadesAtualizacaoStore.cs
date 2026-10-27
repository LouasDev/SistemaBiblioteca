using Newtonsoft.Json;
using SUB.Models;
using System;
using System.IO;
using System.Text;

namespace SUB.Utils
{
    internal sealed class NovidadesAtualizacaoStore
    {
        private readonly string caminho;

        internal sealed class Registro
        {
            public AtualizacaoManifesto Manifesto { get; set; }
            public bool Exibida { get; set; }
        }

        internal NovidadesAtualizacaoStore(string pastaUpdate)
        {
            caminho = Path.Combine(pastaUpdate, "novidades-atualizacao.json");
        }

        internal void Preparar(AtualizacaoManifesto manifesto)
        {
            if (manifesto == null || !VersaoAplicativo.FormatoValido(manifesto.Versao))
                throw new ArgumentException("Versão inválida para as novidades da atualização.");

            Salvar(new Registro
            {
                Manifesto = new AtualizacaoManifesto
                {
                    Versao = manifesto.Versao,
                    Data = manifesto.Data,
                    Notas = manifesto.Notas != null && manifesto.Notas.Length > 4000
                        ? manifesto.Notas.Substring(0, 4000) + Environment.NewLine + "[notas truncadas]"
                        : manifesto.Notas
                }
            });
        }

        internal Registro ObterDaVersao(string versao)
        {
            try
            {
                if (!File.Exists(caminho) || new FileInfo(caminho).Length > 65536)
                    return null;
                var registro = JsonConvert.DeserializeObject<Registro>(File.ReadAllText(caminho));
                return registro?.Manifesto != null && VersaoAplicativo.FormatoValido(versao) &&
                    VersaoAplicativo.Formatar(registro.Manifesto.Versao) == versao
                    ? registro : null;
            }
            catch (IOException) { return null; }
            catch (UnauthorizedAccessException) { return null; }
            catch (JsonException) { return null; }
        }

        internal void MarcarExibida(string versao)
        {
            var registro = ObterDaVersao(versao);
            if (registro == null)
                return;
            registro.Exibida = true;
            Salvar(registro);
        }

        private void Salvar(Registro registro)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(caminho));
            string temporario = caminho + ".tmp";
            try
            {
                File.WriteAllText(temporario, JsonConvert.SerializeObject(registro), new UTF8Encoding(false));
                if (File.Exists(caminho))
                    File.Replace(temporario, caminho, null);
                else
                    File.Move(temporario, caminho);
            }
            finally
            {
                if (File.Exists(temporario))
                    File.Delete(temporario);
            }
        }
    }
}
