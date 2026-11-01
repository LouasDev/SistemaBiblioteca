using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;
using SUB.Utils;

namespace SUB.Tests.Infrastructure
{
    public sealed class SqlCeTestDatabase : IDisposable
    {
        private const string Senha = "SubTest#123";
        private readonly TestTempWorkspace workspace;

        public string DatabasePath { get; }
        public string ConnectionString { get; }
        public string Root => workspace.Root;

        public SqlCeTestDatabase()
        {
            workspace = new TestTempWorkspace();
            string pasta = workspace.CreateFolder("db");
            DatabasePath = Path.Combine(pasta, "sub.tests.sdf");
            ConnectionString = string.Format("Data Source={0}; Password={1};", DatabasePath, Senha);
            using (var engine = new SqlCeEngine(ConnectionString))
                engine.CreateDatabase();
        }

        public void CreateSchema()
        {
            using (var conexao = Open())
            {
                MethodInfo metodo = typeof(DatabaseInitializer).GetMethod(
                    "CreateSchema",
                    BindingFlags.NonPublic | BindingFlags.Static);
                if (metodo == null)
                    throw new InvalidOperationException("DatabaseInitializer.CreateSchema não encontrado.");
                try
                {
                    metodo.Invoke(null, new object[] { conexao });
                }
                catch (TargetInvocationException ex) when (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
            }
        }

        public SqlCeConnection Open()
        {
            var conexao = new SqlCeConnection(ConnectionString);
            conexao.Open();
            return conexao;
        }

        public object Scalar(string sql)
        {
            using (var conexao = Open())
            using (var comando = new SqlCeCommand(sql, conexao))
                return comando.ExecuteScalar();
        }

        public int Int(string sql)
        {
            object valor = Scalar(sql);
            return valor == null || valor == DBNull.Value ? 0 : Convert.ToInt32(valor);
        }

        public int Execute(string sql)
        {
            using (var conexao = Open())
            using (var comando = new SqlCeCommand(sql, conexao))
                return comando.ExecuteNonQuery();
        }

        public void Dispose()
        {
            workspace.Dispose();
        }
    }
}