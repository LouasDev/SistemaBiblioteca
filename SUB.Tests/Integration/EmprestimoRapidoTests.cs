using System;
using System.Data.SqlServerCe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class EmprestimoRapidoTests : DatabaseTestBase
    {
        private SeedContext ctx;
        private static readonly DateTime Agora = new DateTime(2026, 3, 10, 10, 0, 0);

        protected override void OnSeed(SqlCeTestDatabase db)
        {
            using (var conexao = db.Open())
                ctx = SeedData.SeedBasic(conexao);
        }

        [TestMethod]
        public void EmprestarRapido_Sucesso_DecrementaEstoque()
        {
            using (var conexao = Db.Open())
            {
                int antes = SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId);
                CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                    "6º Ano A", 2, "Bibliotecária Teste", Agora);

                Assert.AreEqual(antes - 2, SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId));
                Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM EmprestimoRapido WHERE Status = 'Ativo'"));
                Assert.AreEqual(2, Db.Int("SELECT Quantidade FROM EmprestimoRapido"));
                Assert.AreEqual("6º Ano A", Db.Scalar("SELECT Turma FROM EmprestimoRapido").ToString());
            }
        }

        [TestMethod]
        public void EmprestarRapido_LivroNaoDidatico_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Normal",
                        "6º Ano A", 1, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void EmprestarRapido_ProfessorInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Fantasma", "Livro Didático",
                        "6º Ano A", 1, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void EmprestarRapido_LivroInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Fantasma",
                        "6º Ano A", 1, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void EmprestarRapido_QuantidadeInvalida_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                        "6º Ano A", 0, "Bibliotecária Teste", Agora));
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                        "6º Ano A", -1, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void EmprestarRapido_TurmaOuBibliotecariaVazia_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                        " ", 1, "Bibliotecária Teste", Agora));
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                        "6º Ano A", 1, " ", Agora));
            }
        }

        [TestMethod]
        public void EmprestarRapido_ProfessorComDuasPendencias_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                SeedData.InsertQuickLoan(conexao, ctx.ProfessorId, ctx.LivroDidaticoId,
                    "Livro Didático", "6º Ano A", 1, Agora.AddDays(-2), "Faltando");
                SeedData.InsertQuickLoan(conexao, ctx.ProfessorId, ctx.LivroDidaticoDoisId,
                    "Livro Didático Dois", "6º Ano A", 1, Agora.AddDays(-1), "Faltando");

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                        "6º Ano A", 1, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void DevolverRapido_Parcial_MarcaFaltandoERepoeParte()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                    "6º Ano A", 2, "Bibliotecária Teste", Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM EmprestimoRapido"));
                int antes = SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId);

                CirculacaoService.DevolverRapido(conexao, id, 2, 1, Agora.AddDays(1));

                Assert.AreEqual("Faltando", Db.Scalar("SELECT Status FROM EmprestimoRapido").ToString());
                Assert.AreEqual(1, Db.Int("SELECT Quantidade FROM EmprestimoRapido"));
                Assert.AreEqual(antes + 1, SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId));
            }
        }

        [TestMethod]
        public void DevolverRapido_Total_MarcaDevolvidoERepoeTudo()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                    "6º Ano A", 2, "Bibliotecária Teste", Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM EmprestimoRapido"));
                int antes = SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId);

                CirculacaoService.DevolverRapido(conexao, id, 2, 2, Agora.AddDays(1));

                Assert.AreEqual("Devolvido", Db.Scalar("SELECT Status FROM EmprestimoRapido").ToString());
                Assert.AreEqual(0, Db.Int("SELECT Quantidade FROM EmprestimoRapido"));
                Assert.AreEqual(antes + 2, SeedData.ReadQuantity(conexao, ctx.LivroDidaticoId));
            }
        }

        [TestMethod]
        public void DevolverRapido_SaldoDivergente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                    "6º Ano A", 2, "Bibliotecária Teste", Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM EmprestimoRapido"));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverRapido(conexao, id, 3, 1, Agora));
            }
        }

        [TestMethod]
        public void DevolverRapido_EmprestimoInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverRapido(conexao, 999999, 1, 1, Agora));
            }
        }

        [TestMethod]
        public void DevolverRapido_JaDevolvido_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarRapido(conexao, "Professor Teste", "Livro Didático",
                    "6º Ano A", 2, "Bibliotecária Teste", Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM EmprestimoRapido"));
                CirculacaoService.DevolverRapido(conexao, id, 2, 2, Agora.AddDays(1));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverRapido(conexao, id, 1, 1, Agora.AddDays(2)));
            }
        }
    }
}