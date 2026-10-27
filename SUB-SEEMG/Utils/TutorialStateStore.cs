using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SUB.Utils
{
    public static class TutorialStateStore
    {
        public const int VersaoAtual = 3;

        private sealed class RegistroTutorial
        {
            public Dictionary<int, int> VersaoVistaPorUsuario { get; set; } =
                new Dictionary<int, int>();
        }

        private static string CaminhoArquivo => Path.Combine(AppPaths.AppDataFolder, "tutorial.json");

        public static bool DeveExibir(int? usuarioId)
        {
            if (!usuarioId.HasValue)
                return false;

            RegistroTutorial registro = Carregar();
            int versaoVista;
            return !registro.VersaoVistaPorUsuario.TryGetValue(usuarioId.Value, out versaoVista) ||
                versaoVista < VersaoAtual;
        }

        public static void MarcarComoVisto(int? usuarioId)
        {
            if (!usuarioId.HasValue)
                return;

            try
            {
                RegistroTutorial registro = Carregar();
                registro.VersaoVistaPorUsuario[usuarioId.Value] = VersaoAtual;
                Salvar(registro);
            }
            catch
            {
                // Falhar ao gravar não pode impedir a operação do sistema.
                // Neste caso, o tutorial será exibido novamente no próximo login.
            }
        }

        private static RegistroTutorial Carregar()
        {
            try
            {
                if (!File.Exists(CaminhoArquivo))
                    return new RegistroTutorial();

                RegistroTutorial registro = JsonConvert.DeserializeObject<RegistroTutorial>(File.ReadAllText(CaminhoArquivo));
                if (registro == null)
                    return new RegistroTutorial();

                if (registro.VersaoVistaPorUsuario == null)
                    registro.VersaoVistaPorUsuario = new Dictionary<int, int>();

                return registro;
            }
            catch
            {
                return new RegistroTutorial();
            }
        }

        private static void Salvar(RegistroTutorial registro)
        {
            AppPaths.EnsureFolders();
            string temporario = CaminhoArquivo + ".tmp";
            File.WriteAllText(temporario, JsonConvert.SerializeObject(registro, Formatting.Indented));

            if (File.Exists(CaminhoArquivo))
                File.Replace(temporario, CaminhoArquivo, null);
            else
                File.Move(temporario, CaminhoArquivo);
        }
    }
}
