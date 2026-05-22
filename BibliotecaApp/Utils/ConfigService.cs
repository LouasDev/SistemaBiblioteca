using BibliotecaApp.Utils;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Newtonsoft.Json;


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

            string json = JsonConvert.SerializeObject(dados, Formatting.Indented);
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
            return JsonConvert.DeserializeObject<ConfiguracaoApp>(json) ?? new ConfiguracaoApp();
        }
        catch { return new ConfiguracaoApp(); }
    }

    public static string ObterCaminhoMidia()
    {
        // Criamos uma pasta 'Media' dentro da pasta de dados do App
        string pastaMedia = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Imagens");

        if (!Directory.Exists(pastaMedia))
            Directory.CreateDirectory(pastaMedia);

        return pastaMedia;
    }
}
