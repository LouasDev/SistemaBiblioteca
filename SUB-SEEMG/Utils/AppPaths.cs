// Utils/AppPaths.cs
using System;
using System.IO;

namespace SUB.Utils
{
    public static class AppPaths
    {
        public static string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SUB");
        public static string MappingFolder => Path.Combine(AppDataFolder, "mapeamentoanual");
        public static string ReportsFolder => Path.Combine(AppDataFolder, "Relatorios");
        public static string UpdateFolder => Path.Combine(AppDataFolder, "update");
        public static string BackupsFolder => Path.Combine(AppDataFolder, "backups");

        public static void EnsureFolders()
        {
            try
            {
                Directory.CreateDirectory(AppDataFolder);
                Directory.CreateDirectory(ReportsFolder);
            }
            catch
            {
            // Não levantar aqui — quem chamar pode tratar. Mas tentamos garantir diretórios.
            }
        }
    }
}
