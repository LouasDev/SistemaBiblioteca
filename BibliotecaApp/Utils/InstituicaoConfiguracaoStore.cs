using SUB.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace SUB.Utils
{
    public static class InstituicaoConfiguracaoStore
    {
        private static string ArquivoConfiguracao =>
            Path.Combine(AppPaths.AppDataFolder, "instituicao.json");

        public static bool Existe()
        {
            return File.Exists(ArquivoConfiguracao);
        }

        public static InstituicaoConfiguracao Carregar()
        {
            if (!Existe())
                return null;

            try
            {
                var json = File.ReadAllText(ArquivoConfiguracao);
                return JsonConvert.DeserializeObject<InstituicaoConfiguracao>(json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Não foi possível carregar a configuração da instituição.", ex);
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
            string json = JsonConvert.SerializeObject(configuracao, Formatting.Indented);
            File.WriteAllText(temporario, json);

            if (File.Exists(ArquivoConfiguracao))
                File.Replace(temporario, ArquivoConfiguracao, null);
            else
                File.Move(temporario, ArquivoConfiguracao);
        }
    }
}

