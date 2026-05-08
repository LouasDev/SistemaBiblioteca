using BibliotecaApp.Utils;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;


    public static class ConfigService
    {
        // Local profissional: C:\Users\Usuario\AppData\Roaming\NomeDoSeuApp
        private static string pasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BibliotecaLouas");
        private static string arquivo = Path.Combine(pasta, "appsettings.json");

        public static void Salvar(ConfiguracaoApp dados)
        {
            try
            {
                if (!Directory.Exists(pasta)) Directory.CreateDirectory(pasta);

                string json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(arquivo, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro técnico ao salvar arquivo: " + ex.Message);
            }
        }

        public static ConfiguracaoApp Carregar()
        {
            if (!File.Exists(arquivo)) return new ConfiguracaoApp();

            try
            {
                string json = File.ReadAllText(arquivo);
                return JsonSerializer.Deserialize<ConfiguracaoApp>(json);
            }
            catch { return new ConfiguracaoApp(); }
        }
    }