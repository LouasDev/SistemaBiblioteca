using System;
using System.Configuration;
using System.Data.SqlServerCe;
using System.IO;
using System.Text;

namespace SUB.Utils
{
    public static class Conexao
    {
        public static string CaminhoBanco => Path.Combine(AppPaths.AppDataFolder, "SUB", "sub.sdf");

        public static string Conectar
        {
            get
            {
                var connStrTemplate = ConfigurationManager.ConnectionStrings["SUBDB"]?.ConnectionString;
                if (string.IsNullOrWhiteSpace(connStrTemplate))
                    throw new InvalidOperationException("ConnectionString 'SUBDB' não encontrada em App.config.");
                var senha = GetDatabasePasswordProtected();
                if (string.IsNullOrEmpty(senha))
                    throw new InvalidOperationException("Senha do banco não encontrada em AppSettings['DBPasswordProtected'].");
                var connStr = connStrTemplate.Replace("CAMINHO", CaminhoBanco).Replace("{PASSWORD}", senha);
                return connStr;
            }
        }

        public static SqlCeConnection ObterConexao()
        {
            return new SqlCeConnection(Conectar);
        }

        // Lê AppSettings["DBPasswordProtected"], decodifica Base64 e descriptografa com DPAPI (escopo máquina)
        private static string GetDatabasePasswordProtected()
        {
            var base64 = ConfigurationManager.AppSettings["DBPasswordProtected"];
            if (string.IsNullOrWhiteSpace(base64))
                return null;
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}
