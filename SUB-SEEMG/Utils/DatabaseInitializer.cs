using System;
using System.Data.SqlServerCe;
using System.IO;

namespace SUB.Utils
{
    public static class DatabaseInitializer
    {
        public static void EnsureCreated()
        {
            string directory = Path.GetDirectoryName(Conexao.CaminhoBanco);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(Conexao.CaminhoBanco))
            {
                var engine = new SqlCeEngine(Conexao.Conectar);
                engine.CreateDatabase();
            }

            using (var connection = Conexao.ObterConexao())
            {
                connection.Open();
                CreateSchema(connection);
            }
        }

        private static void CreateSchema(SqlCeConnection connection)
        {
            Execute(connection, "CREATE TABLE SchemaInfo (Id int IDENTITY(1,1) PRIMARY KEY, Versao int NOT NULL, AplicadoEm datetime NOT NULL)", "SchemaInfo");
            Execute(connection, @"CREATE TABLE usuarios (
                Id int IDENTITY(1,1) PRIMARY KEY, Nome nvarchar(200) NOT NULL, Email nvarchar(200) NULL,
                Senha_Hash nvarchar(500) NULL, Senha_Salt nvarchar(500) NULL, CPF nvarchar(30) NULL,
                DataNascimento datetime NULL, Turma nvarchar(100) NULL, Telefone nvarchar(30) NULL,
                TipoUsuario nvarchar(80) NOT NULL, Ativo bit NULL, CriadoEm datetime NULL,
                AtualizadoEm datetime NULL, UltimoLoginEm datetime NULL)", "usuarios");
            Execute(connection, @"CREATE TABLE Livros (
                Id int IDENTITY(1,1) PRIMARY KEY, Nome nvarchar(250) NOT NULL, Autor nvarchar(200) NULL,
                Genero nvarchar(120) NULL, Quantidade int NOT NULL, CodigoBarras nvarchar(80) NULL,
                Disponibilidade int NULL, Ativo bit NULL, CriadoEm datetime NULL,
                AtualizadoEm datetime NULL)", "Livros");
            Execute(connection, @"CREATE TABLE Emprestimo (
                Id int IDENTITY(1,1) PRIMARY KEY, UsuarioId int NULL, LivroId int NULL, Alocador int NULL, Responsavel int NULL,
                DataEmprestimo datetime NULL, DataDevolucao datetime NULL, DataDevolucaoPrevista datetime NULL,
                DataRealDevolucao datetime NULL, DataProrrogacao datetime NULL, Status nvarchar(50) NULL,
                Observacao nvarchar(500) NULL)", "Emprestimo");
            Execute(connection, @"CREATE TABLE EmprestimoRapido (
                Id int IDENTITY(1,1) PRIMARY KEY, UsuarioId int NULL, LivroId int NULL,
                DataHoraEmprestimo datetime NULL, DataDevolucao datetime NULL, Status nvarchar(50) NULL)", "EmprestimoRapido");
            Execute(connection, @"CREATE TABLE LogProrrogacoes (
                Id int IDENTITY(1,1) PRIMARY KEY, EmprestimoId int NULL, UsuarioId int NULL,
                DataHora datetime NOT NULL, Observacao nvarchar(500) NULL)", "LogProrrogacoes");
            Execute(connection, @"CREATE TABLE NotificacoesDisponibilidade (
                Id int IDENTITY(1,1) PRIMARY KEY, UsuarioId int NULL, LivroId int NULL,
                DataSolicitacao datetime NULL, Ativo bit NOT NULL)", "NotificacoesDisponibilidade");
            MigrarEmprestimo(connection);
            GarantirVersaoSchema(connection, 2);
        }

        private static void MigrarEmprestimo(SqlCeConnection connection)
        {
            if (!TableExists(connection, "Emprestimo"))
                return;

            AddColumnIfMissing(connection, "Emprestimo", "Responsavel", "int NULL");
            AddColumnIfMissing(connection, "Emprestimo", "LivroId", "int NULL");

            if (ColumnExists(connection, "Emprestimo", "Livro"))
            {
                Execute(connection, "UPDATE Emprestimo SET LivroId = Livro WHERE LivroId IS NULL AND Livro IS NOT NULL", null);
            }
        }

        private static void AddColumnIfMissing(SqlCeConnection connection, string tableName, string columnName, string definition)
        {
            if (!ColumnExists(connection, tableName, columnName))
                Execute(connection, string.Format("ALTER TABLE {0} ADD {1} {2}", tableName, columnName, definition), null);
        }

        private static bool ColumnExists(SqlCeConnection connection, string tableName, string columnName)
        {
            using (var command = new SqlCeCommand(
                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table AND COLUMN_NAME = @column", connection))
            {
                command.Parameters.AddWithValue("@table", tableName);
                command.Parameters.AddWithValue("@column", columnName);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        private static void GarantirVersaoSchema(SqlCeConnection connection, int versao)
        {
            using (var command = new SqlCeCommand("SELECT COUNT(*) FROM SchemaInfo WHERE Versao = @versao", connection))
            {
                command.Parameters.AddWithValue("@versao", versao);
                if (Convert.ToInt32(command.ExecuteScalar()) > 0)
                    return;
            }

            using (var command = new SqlCeCommand("INSERT INTO SchemaInfo (Versao, AplicadoEm) VALUES (@versao, GETDATE())", connection))
            {
                command.Parameters.AddWithValue("@versao", versao);
                command.ExecuteNonQuery();
            }
        }

        private static void Execute(SqlCeConnection connection, string sql, string tableName)
        {
            if (tableName != null && TableExists(connection, tableName))
                return;
            using (var command = new SqlCeCommand(sql, connection))
                command.ExecuteNonQuery();
        }

        private static bool TableExists(SqlCeConnection connection, string tableName)
        {
            using (var command = new SqlCeCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table", connection))
            {
                command.Parameters.AddWithValue("@table", tableName);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
    }
}

