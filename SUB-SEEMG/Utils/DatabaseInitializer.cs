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
            Execute(connection, @"CREATE TABLE Turmas (
                Id int IDENTITY(1,1) PRIMARY KEY, Nome nvarchar(100) NOT NULL UNIQUE)", "Turmas");
            MigrarEmprestimo(connection);
            MigrarEmprestimoRapido(connection);
            MigrarTurmasDosUsuariosExistentes(connection);
            GarantirVersaoSchema(connection, 4);
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

        private static void MigrarEmprestimoRapido(SqlCeConnection connection)
        {
            if (!TableExists(connection, "EmprestimoRapido"))
                return;

            // Versões anteriores guardavam somente UsuarioId e DataDevolucao.
            // O fluxo de empréstimo rápido usa estes campos adicionais desde então.
            AddColumnIfMissing(connection, "EmprestimoRapido", "ProfessorId", "int NULL");
            AddColumnIfMissing(connection, "EmprestimoRapido", "LivroNome", "nvarchar(250) NULL");
            AddColumnIfMissing(connection, "EmprestimoRapido", "Turma", "nvarchar(100) NULL");
            AddColumnIfMissing(connection, "EmprestimoRapido", "Quantidade", "int NULL");
            AddColumnIfMissing(connection, "EmprestimoRapido", "DataHoraDevolucaoReal", "datetime NULL");
            AddColumnIfMissing(connection, "EmprestimoRapido", "Bibliotecaria", "nvarchar(200) NULL");

            if (ColumnExists(connection, "EmprestimoRapido", "UsuarioId"))
                Execute(connection, "UPDATE EmprestimoRapido SET ProfessorId = UsuarioId WHERE ProfessorId IS NULL AND UsuarioId IS NOT NULL", null);

            if (ColumnExists(connection, "EmprestimoRapido", "DataDevolucao"))
                Execute(connection, "UPDATE EmprestimoRapido SET DataHoraDevolucaoReal = DataDevolucao WHERE DataHoraDevolucaoReal IS NULL AND DataDevolucao IS NOT NULL", null);

            Execute(connection, "UPDATE EmprestimoRapido SET Quantidade = 1 WHERE Quantidade IS NULL", null);
        }

        private static void MigrarTurmasDosUsuariosExistentes(SqlCeConnection connection)
        {
            if (!TableExists(connection, "Turmas") || !TableExists(connection, "usuarios") ||
                !ColumnExists(connection, "usuarios", "Turma"))
                return;

            var conhecidas = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var consulta = new SqlCeCommand("SELECT Nome FROM Turmas", connection))
            using (var leitor = consulta.ExecuteReader())
            {
                while (leitor.Read())
                {
                    string nome = leitor.GetString(0).Trim();
                    if (nome.Length > 0)
                        conhecidas.Add(nome);
                }
            }

            var turmas = new System.Collections.Generic.List<string>();
            using (var consulta = new SqlCeCommand("SELECT DISTINCT Turma FROM usuarios WHERE Turma IS NOT NULL AND Turma <> ''", connection))
            using (var leitor = consulta.ExecuteReader())
            {
                while (leitor.Read())
                {
                    string turma = leitor.GetString(0).Trim();
                    if (turma.Length > 0)
                        turmas.Add(turma);
                }
            }

            foreach (string turma in turmas)
            {
                if (!conhecidas.Add(turma))
                    continue;

                using (var inserir = new SqlCeCommand("INSERT INTO Turmas (Nome) VALUES (@nome)", connection))
                {
                    inserir.Parameters.AddWithValue("@nome", turma);
                    inserir.ExecuteNonQuery();
                }
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

