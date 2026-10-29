using System;
using System.IO;
using Newtonsoft.Json;
using SUB.Models;

namespace SUB.Utils
{
    public static class InstituicaoConfiguracaoStore
    {
        // Uma única tentativa de backup por execução do app, já que
        // Carregar tem vários chamadores.
        private static bool backupCorrompidoJaTentado;
        /// <summary>
        /// Indica que a última leitura encontrou o arquivo corrompido.
        /// Quem chama (ex.: MainForm) pode exibir um aviso — o Store não abre UI.
        /// </summary>
        public static bool CorrupcaoDetectada { get; private set; }
        /// <summary>
        /// Caminho do backup criado quando a corrupção foi detectada (null se não houve backup).
        /// </summary>
        public static string BackupCorrompidoPath { get; private set; }
        private static string ArquivoConfiguracao => Path.Combine(AppPaths.AppDataFolder, "instituicao.json");

        public static bool Existe()
        {
            return File.Exists(ArquivoConfiguracao);
        }

        public static InstituicaoConfiguracao Carregar()
        {
            if (!Existe())
                return null;
            string json = null;
            try
            {
                json = File.ReadAllText(ArquivoConfiguracao);
                return JsonConvert.DeserializeObject<InstituicaoConfiguracao>(json);
            }
            catch
            {
                RegistrarCorrupcao(json);
                return null;
            }
        }

        // Nunca apaga nem sobrescreve o arquivo original; apenas o copia.
        private static void RegistrarCorrupcao(string conteudoLido)
        {
            CorrupcaoDetectada = true;
            if (backupCorrompidoJaTentado)
                return;
            backupCorrompidoJaTentado = true;
            try
            {
                if (!File.Exists(ArquivoConfiguracao))
                    return;
                string conteudoAtual = conteudoLido;
                if (conteudoAtual == null)
                    conteudoAtual = File.ReadAllText(ArquivoConfiguracao);
                // Se já existe um backup com o mesmo conteúdo, reutiliza.
                foreach (string existente in Directory.GetFiles(AppPaths.AppDataFolder, "instituicao.json.corrompido*"))
                {
                    try
                    {
                        if (File.ReadAllText(existente) == conteudoAtual)
                        {
                            BackupCorrompidoPath = existente;
                            return;
                        }
                    }
                    catch
                    {
                    // não conseguimos ler esse backup; tenta o próximo/cópia nova
                    }
                }

                string destino = string.Format("{0}.corrompido-{1:yyyyMMdd-HHmmss}", ArquivoConfiguracao, DateTime.Now);
                File.Copy(ArquivoConfiguracao, destino, false);
                BackupCorrompidoPath = destino;
            }
            catch
            {
            // Falha ao copiar não pode derrubar o app.
            }
        }

        public static void Salvar(InstituicaoConfiguracao configuracao)
        {
            if (configuracao == null)
                throw new ArgumentNullException(nameof(configuracao));
            if (string.IsNullOrWhiteSpace(configuracao.Nome))
                throw new ArgumentException("O nome da instituição é obrigatório.", nameof(configuracao));
            AppPaths.EnsureFolders();
            configuracao.AtualizadaEm = DateTime.Now;
            if (configuracao.ConfiguradaEm == default(DateTime))
                configuracao.ConfiguradaEm = configuracao.AtualizadaEm;
            string temporario = ArquivoConfiguracao + ".tmp";
            // NullValueHandling.Ignore: não materializa CodigoMestreHash/Salt nulos
            // em configurações antigas que não os possuem (ver comentário no model).
            string json = JsonConvert.SerializeObject(configuracao, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            File.WriteAllText(temporario, json);
            if (File.Exists(ArquivoConfiguracao))
                File.Replace(temporario, ArquivoConfiguracao, null);
            else
                File.Move(temporario, ArquivoConfiguracao);
        }
    }
}
