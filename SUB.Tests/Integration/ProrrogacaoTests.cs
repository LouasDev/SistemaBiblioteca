using System;
using System.Data.SqlServerCe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class ProrrogacaoTests : DatabaseTestBase
    {
        private SeedContext ctx;
        private static readonly DateTime Agora = new DateTime(2026, 3, 10, 10, 0, 0);
        private static readonly DateTime Prazo = Agora.Date.AddDays(7);

        protected override void OnSeed(SqlCeTestDatabase db)
        {
            using (var conexao = db.Open())
                ctx = SeedData.SeedBasic(conexao);
        }

        [TestMethod]
        public void Prorrogar_Sucesso_AtualizaDatasEStatusERegistraLog()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                ProrrogacaoService.Prorrogar(conexao, id, 7, Prazo, "Bibliotecária Teste", Agora.AddDays(1));

                Assert.AreEqual(Prazo.AddDays(7), Convert.ToDateTime(Db.Scalar(
                    "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id)));
                Assert.AreEqual(Prazo.AddDays(7), Convert.ToDateTime(Db.Scalar(
                    "SELECT DataProrrogacao FROM Emprestimo WHERE Id = " + id)));
                Assert.AreEqual("Ativo", Db.Scalar("SELECT Status FROM Emprestimo WHERE Id = " + id).ToString());
                Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM LogProrrogacoes WHERE EmprestimoId = " + id));
                Assert.AreEqual("Bibliotecária Teste", Db.Scalar(
                    "SELECT BibliotecariaNome FROM LogProrrogacoes").ToString());
                Assert.AreEqual(Prazo.AddDays(7), Convert.ToDateTime(Db.Scalar(
                    "SELECT NovaDataDevolucao FROM LogProrrogacoes")));
            }
        }

        [TestMethod]
        public void Prorrogar_UmDia_AtualizaPrazo()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                ProrrogacaoService.Prorrogar(conexao, id, 1, Prazo, "Bibliotecária Teste", Agora);

                Assert.AreEqual(Prazo.AddDays(1), Convert.ToDateTime(Db.Scalar(
                    "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id)));
            }
        }

        [TestMethod]
        public void Prorrogar_QuatorzeDias_AtualizaPrazo()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                ProrrogacaoService.Prorrogar(conexao, id, 14, Prazo, "Bibliotecária Teste", Agora);

                Assert.AreEqual(Prazo.AddDays(14), Convert.ToDateTime(Db.Scalar(
                    "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id)));
            }
        }

        [TestMethod]
        public void Prorrogar_ZeroOuQuinzeDias_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 0, Prazo, "Bibliotecária Teste", Agora));
                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 15, Prazo, "Bibliotecária Teste", Agora));
                Assert.AreEqual(0, Db.Int("SELECT COUNT(*) FROM LogProrrogacoes"));
            }
        }

        [TestMethod]
        public void Prorrogar_EmprestimoDevolvido_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));
                CirculacaoService.DevolverNormal(conexao, id, Agora.AddDays(1));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 7, Prazo, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void Prorrogar_PrazoDivergente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 7, Prazo.AddDays(1), "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void Prorrogar_EmprestimoInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, 999999, 7, Prazo, "Bibliotecária Teste", Agora));
            }
        }

        [TestMethod]
        public void Prorrogar_BibliotecariaVazia_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 7, Prazo, " ", Agora));
            }
        }

        [TestMethod]
        public void Prorrogar_EmprestimoAtrasado_VoltaParaAtivo()
        {
            using (var conexao = Db.Open())
            {
                int id = SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Agora.AddDays(-14), Agora.AddDays(-7), "Atrasado");

                ProrrogacaoService.Prorrogar(conexao, id, 7, Agora.AddDays(-7), "Bibliotecária Teste", Agora);

                Assert.AreEqual("Ativo", Db.Scalar("SELECT Status FROM Emprestimo WHERE Id = " + id).ToString());
                Assert.AreEqual(Agora, Convert.ToDateTime(Db.Scalar(
                    "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id)));
            }
        }

        [TestMethod]
        public void Prorrogar_EstouroDeData_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                var prazoLimite = new DateTime(9999, 12, 30);
                int id = SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Agora, prazoLimite, "Ativo");

                Assert.ThrowsException<InvalidOperationException>(() =>
                    ProrrogacaoService.Prorrogar(conexao, id, 14, prazoLimite, "Bibliotecária Teste", Agora));
            }
        }
    }
}