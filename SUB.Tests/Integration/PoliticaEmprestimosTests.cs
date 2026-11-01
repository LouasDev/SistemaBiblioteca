using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Models;
using SUB.Utils;
using SUB.Tests.Infrastructure;

namespace SUB.Tests.Integration
{
    [TestClass]
    public class PoliticaEmprestimosTests : DatabaseTestBase
    {
        private SeedContext ctx;
        private readonly DateTime hoje = new DateTime(2026, 10, 31);
        protected override void OnSeed(SqlCeTestDatabase db)
        {
            using (var c = db.Open()) ctx = SeedData.SeedBasic(c);
        }
        private int Pessoa(string perfil) => perfil == "Aluno" ? ctx.AlunoId : perfil == "Professor" ? ctx.ProfessorId : ctx.BibliotecariaId;
        private void Emprestar(int usuario, PoliticaEmprestimos regras, int dias = 7)
        {
            using (var c = Db.Open())
                CirculacaoService.EmprestarNormal(c, usuario, ctx.LivroNormalId, ctx.BibliotecariaId, hoje.AddDays(dias), hoje, hoje, regras);
        }
        private void SeedLoan(int usuario, string status)
        {
            using (var c = Db.Open())
                SeedData.InsertLoan(c, usuario, ctx.LivroNormalId, ctx.BibliotecariaId, hoje.AddDays(-10), hoje.AddDays(-3), status);
        }

        [DataTestMethod]
        [DataRow("Aluno", "Ativo")]
        [DataRow("Aluno", "Atrasado")]
        [DataRow("Professor", "Ativo")]
        [DataRow("Professor", "Atrasado")]
        [DataRow("Bibliotecario", "Ativo")]
        [DataRow("Bibliotecario", "Atrasado")]
        public void LimiteDoPerfil_ContaAtivosEAtrasados(string perfil, string status)
        {
            int usuario = Pessoa(perfil);
            SeedLoan(usuario, status);
            var regras = new PoliticaEmprestimos { LimiteAluno = 1, LimiteProfessor = 1, LimiteBibliotecario = 1 };
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(usuario, regras));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(3, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [TestMethod]
        public void LimiteDois_PermiteSegundoEBloqueiaTerceiro()
        {
            SeedLoan(ctx.AlunoId, "Atrasado");
            var regras = new PoliticaEmprestimos { LimiteAluno = 2 };
            Emprestar(ctx.AlunoId, regras);
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(ctx.AlunoId, regras));
            Assert.AreEqual(2, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(2, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [TestMethod]
        public void LimitesIndependentes_ZeroBloqueiaSomenteOPerfil()
        {
            var regras = new PoliticaEmprestimos { LimiteAluno = 0, LimiteProfessor = 2 };
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(ctx.AlunoId, regras));
            Emprestar(ctx.ProfessorId, regras);
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
        }

        [TestMethod]
        public void SemLimite_NaoBloqueiaPorEmprestimosAnteriores()
        {
            SeedLoan(ctx.AlunoId, "Ativo");
            SeedLoan(ctx.AlunoId, "Atrasado");
            Emprestar(ctx.AlunoId, new PoliticaEmprestimos { LimiteAluno = -1 });
            Assert.AreEqual(3, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
        }

        [TestMethod]
        public void Devolvidos_NaoConsomemLimite()
        {
            SeedLoan(ctx.AlunoId, "Devolvido");
            Emprestar(ctx.AlunoId, new PoliticaEmprestimos { LimiteAluno = 1 });
            Assert.AreEqual(2, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(21)]
        [DataRow(60)]
        public void PrazoConfigurado_AceitaNoLimite(int dias)
        {
            Emprestar(ctx.AlunoId, new PoliticaEmprestimos { PrazoDias = dias }, dias);
            Assert.AreEqual(hoje.AddDays(dias), Convert.ToDateTime(Db.Scalar("SELECT DataDevolucao FROM Emprestimo")));
        }

        [TestMethod]
        public void PrazoConfigurado_RejeitaExcessoSemAlterarEstoque()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(ctx.AlunoId, new PoliticaEmprestimos { PrazoDias = 5 }, 6));
            Assert.AreEqual(0, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(3, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [TestMethod]
        public void ReduzirLimite_NaoAlteraContratosExistentes()
        {
            SeedLoan(ctx.AlunoId, "Ativo");
            SeedLoan(ctx.AlunoId, "Atrasado");
            var prazo = Db.Scalar("SELECT MIN(DataDevolucao) FROM Emprestimo");
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(ctx.AlunoId, new PoliticaEmprestimos { LimiteAluno = 1 }));
            Assert.AreEqual(prazo, Db.Scalar("SELECT MIN(DataDevolucao) FROM Emprestimo"));
            Assert.AreEqual(2, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        public void MaximoProrrogacoes_EfetivoPorEmprestimo(int maximo)
        {
            int id;
            using (var c = Db.Open())
                id = SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, hoje, hoje.AddDays(7), "Ativo");
            var regras = new PoliticaEmprestimos { MaximoProrrogacoes = maximo };
            DateTime prazo = hoje.AddDays(7);
            using (var c = Db.Open())
            {
                for (int i = 0; i < maximo; i++)
                {
                    ProrrogacaoService.Prorrogar(c, id, 1, prazo, "Teste", hoje, regras);
                    prazo = prazo.AddDays(1);
                }
                Assert.ThrowsException<InvalidOperationException>(() => ProrrogacaoService.Prorrogar(c, id, 1, prazo, "Teste", hoje, regras));
            }
            Assert.AreEqual(maximo, Db.Int("SELECT COUNT(*) FROM LogProrrogacoes"));
            Assert.AreEqual(prazo, Convert.ToDateTime(Db.Scalar("SELECT DataDevolucao FROM Emprestimo")));
        }

        [TestMethod]
        public void ProrrogacoesAnteriores_ContinuamContando()
        {
            int id;
            using (var c = Db.Open())
            {
                id = SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, hoje, hoje.AddDays(7), "Ativo");
                ProrrogacaoService.Prorrogar(c, id, 1, hoje.AddDays(7), "Teste", hoje);
                Assert.ThrowsException<InvalidOperationException>(() => ProrrogacaoService.Prorrogar(c, id, 1, hoje.AddDays(8), "Teste", hoje,
                    new PoliticaEmprestimos { MaximoProrrogacoes = 1 }));
            }
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM LogProrrogacoes"));
        }

        [TestMethod]
        public void ProrrogacoesSemLimite_PermitemOperacoesSucessivas()
        {
            using (var c = Db.Open())
            {
                int id = SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, hoje, hoje.AddDays(7), "Ativo");
                for (int i = 0; i < 3; i++)
                    ProrrogacaoService.Prorrogar(c, id, 1, hoje.AddDays(7+i), "Teste", hoje, new PoliticaEmprestimos { MaximoProrrogacoes = -1 });
            }
            Assert.AreEqual(3, Db.Int("SELECT COUNT(*) FROM LogProrrogacoes"));
        }

        [TestMethod]
        public void EmprestimoRapido_PermaneceSeparado()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Emprestar(ctx.ProfessorId, new PoliticaEmprestimos { LimiteProfessor = 0 }));
            using (var c = Db.Open())
                CirculacaoService.EmprestarRapido(c, "Professor Teste", "Livro Didático", "6A", 1, "Bibliotecária Teste", hoje);
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM EmprestimoRapido"));
        }
    }
}