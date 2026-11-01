using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class SchemaTests : DatabaseTestBase
    {
        [TestMethod]
        public void CreateSchema_CriaTodasAsTabelasEsperadas()
        {
            var esperadas = new[]
            {
                "SchemaInfo", "usuarios", "Livros", "Emprestimo", "EmprestimoRapido",
                "LogProrrogacoes", "NotificacoesDisponibilidade", "Turmas",
                "MapeamentoAnualControle", "MapeamentosAnuais", "MapeamentoAnualItens",
                "ComunicadosAtraso", "ComunicadosAtrasoItens"
            };

            var existentes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var conexao = Db.Open())
            using (var comando = new SqlCeCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", conexao))
            using (var leitor = comando.ExecuteReader())
            {
                while (leitor.Read())
                    existentes.Add(leitor.GetString(0));
            }

            CollectionAssert.AreEquivalent(esperadas, existentes.ToArray());
        }

        [TestMethod]
        public void CreateSchema_RegistraVersaoSete()
        {
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM SchemaInfo WHERE Versao = 7"));
        }

        [TestMethod]
        public void CreateSchema_EIdempotente()
        {
            int tabelasAntes = Db.Int("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES");
            int versoesAntes = Db.Int("SELECT COUNT(*) FROM SchemaInfo");

            Db.CreateSchema();

            Assert.AreEqual(tabelasAntes, Db.Int("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"));
            Assert.AreEqual(versoesAntes, Db.Int("SELECT COUNT(*) FROM SchemaInfo"));
        }

        [TestMethod]
        public void CreateSchema_ColunasDeMigracaoExistem()
        {
            Assert.AreEqual(1, ColumnCount("Emprestimo", "Responsavel"));
            Assert.AreEqual(1, ColumnCount("Emprestimo", "LivroId"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "ProfessorId"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "Quantidade"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "LivroNome"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "Turma"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "DataHoraDevolucaoReal"));
            Assert.AreEqual(1, ColumnCount("EmprestimoRapido", "Bibliotecaria"));
            Assert.AreEqual(1, ColumnCount("LogProrrogacoes", "DataDaAcao"));
            Assert.AreEqual(1, ColumnCount("LogProrrogacoes", "NovaDataDevolucao"));
            Assert.AreEqual(1, ColumnCount("LogProrrogacoes", "BibliotecariaNome"));
        }

        [TestMethod]
        public void CreateSchema_TurmasTemNomeUnico()
        {
            using (var conexao = Db.Open())
            {
                SeedData.InsertTurma(conexao, "6º Ano A");
                Assert.ThrowsException<SqlCeException>(() => SeedData.InsertTurma(conexao, "6º Ano A"));
            }
        }

        [TestMethod]
        public void CreateSchema_CamposObrigatoriosSaoNotNull()
        {
            using (var conexao = Db.Open())
            using (var comando = new SqlCeCommand(
                "INSERT INTO usuarios (Nome, TipoUsuario) VALUES (NULL, 'Aluno(a)')", conexao))
            {
                Assert.ThrowsException<SqlCeException>(() => comando.ExecuteNonQuery());
            }

            using (var conexao = Db.Open())
            using (var comando = new SqlCeCommand(
                "INSERT INTO Livros (Nome, Quantidade) VALUES (NULL, 1)", conexao))
            {
                Assert.ThrowsException<SqlCeException>(() => comando.ExecuteNonQuery());
            }
        }

        [TestMethod]
        public void CreateSchema_EmailCpfETurmaSaoOpcionais()
        {
            using (var conexao = Db.Open())
            {
                int id = SeedData.InsertUser(conexao, "Sem Contato", "Aluno(a)", null, null, true);
                Assert.IsTrue(id > 0);
            }
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void Integridade_EmprestimoDeveTerForeignKeyParaUsuarioELivro()
        {
            int fks = Db.Int(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                               WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'");
            Assert.IsTrue(fks > 0, "O schema deveria declarar chaves estrangeiras para impedir registros órfãos.");
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void Integridade_InsertDeEmprestimoComUsuarioInexistente_DeveFalhar()
        {
            using (var conexao = Db.Open())
            using (var comando = new SqlCeCommand(@"
INSERT INTO Emprestimo (UsuarioId, LivroId, Alocador, Status)
VALUES (999999, 999999, 999999, 'Ativo')", conexao))
            {
                Assert.ThrowsException<SqlCeException>(() => comando.ExecuteNonQuery());
            }
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void Integridade_ExcluirUsuarioComEmprestimo_DeveSerBloqueado()
        {
            using (var conexao = Db.Open())
            {
                var ctx = SeedData.SeedBasic(conexao);
                SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId,
                    DateTime.Today, DateTime.Today.AddDays(7), "Ativo");

                using (var comando = new SqlCeCommand("DELETE FROM usuarios WHERE Id = @Id", conexao))
                {
                    comando.Parameters.AddWithValue("@Id", ctx.AlunoId);
                    Assert.ThrowsException<SqlCeException>(() => comando.ExecuteNonQuery());
                }
            }
        }

        private int ColumnCount(string tabela, string coluna)
        {
            using (var conexao = Db.Open())
            using (var comando = new SqlCeCommand(
                "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @t AND COLUMN_NAME = @c", conexao))
            {
                comando.Parameters.AddWithValue("@t", tabela);
                comando.Parameters.AddWithValue("@c", coluna);
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }
    }
}