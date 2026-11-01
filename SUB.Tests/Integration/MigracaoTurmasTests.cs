using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class MigracaoTurmasTests
    {
        private SqlCeTestDatabase db;

        [TestInitialize]
        public void Inicializar()
        {
            db = new SqlCeTestDatabase();
            db.CreateSchema();
        }

        [TestCleanup]
        public void Limpar()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        [TestMethod]
        public void CreateSchema_MigraTurmasDistintasDosUsuarios()
        {
            using (var conexao = db.Open())
            {
                SeedData.InsertUser(conexao, "Aluno A", "Aluno(a)", "6º Ano A", null, true);
                SeedData.InsertUser(conexao, "Aluno B", "Aluno(a)", "6º Ano A ", null, true);
                SeedData.InsertUser(conexao, "Aluno C", "Aluno(a)", "7º Ano B", null, true);
                SeedData.InsertUser(conexao, "Aluno D", "Aluno(a)", "6º ano a", null, true);
                SeedData.InsertUser(conexao, "Aluno E", "Aluno(a)", "   ", null, true);
                SeedData.InsertUser(conexao, "Aluno F", "Aluno(a)", null, null, true);
                using (var limpar = new SqlCeCommand("DELETE FROM Turmas", conexao))
                    limpar.ExecuteNonQuery();
            }

            db.CreateSchema();

            var turmas = new List<string>();
            using (var conexao = db.Open())
            using (var comando = new SqlCeCommand("SELECT Nome FROM Turmas ORDER BY Nome", conexao))
            using (var leitor = comando.ExecuteReader())
            {
                while (leitor.Read())
                    turmas.Add(leitor.GetString(0));
            }

            CollectionAssert.Contains(turmas, "6º Ano A");
            CollectionAssert.Contains(turmas, "7º Ano B");
            Assert.AreEqual(2, turmas.Count);
        }

        [TestMethod]
        public void CreateSchema_MigracaoDeTurmas_EIdempotente()
        {
            using (var conexao = db.Open())
            {
                SeedData.InsertUser(conexao, "Aluno A", "Aluno(a)", "8º Ano C", null, true);
                using (var limpar = new SqlCeCommand("DELETE FROM Turmas", conexao))
                    limpar.ExecuteNonQuery();
            }

            db.CreateSchema();
            int depoisDaPrimeira = db.Int("SELECT COUNT(*) FROM Turmas");
            db.CreateSchema();
            int depoisDaSegunda = db.Int("SELECT COUNT(*) FROM Turmas");

            Assert.AreEqual(1, depoisDaPrimeira);
            Assert.AreEqual(depoisDaPrimeira, depoisDaSegunda);
        }

        [TestMethod]
        public void CreateSchema_PreservaTurmaJaCadastrada()
        {
            using (var conexao = db.Open())
            {
                SeedData.InsertTurma(conexao, "6º Ano A");
                SeedData.InsertUser(conexao, "Aluno A", "Aluno(a)", "6º Ano A", null, true);
            }

            db.CreateSchema();

            Assert.AreEqual(1, db.Int("SELECT COUNT(*) FROM Turmas WHERE Nome = '6º Ano A'"));
        }
    }
}