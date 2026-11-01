using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Integration
{
    [TestClass]
    public class ExclusaoCadastroTests : DatabaseTestBase
    {
        private SeedContext ctx;
        protected override void OnSeed(SqlCeTestDatabase db)
        {
            using (var c = db.Open()) ctx = SeedData.SeedBasic(c);
        }

        private ResultadoExclusaoCadastro Excluir(bool usuario)
        {
            using (var c = Db.Open())
                return usuario ? ExclusaoCadastroService.ExcluirUsuario(c, ctx.AlunoId)
                    : ExclusaoCadastroService.ExcluirLivro(c, ctx.LivroNormalId);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void SemVinculos_Exclui(bool usuario)
        {
            Assert.AreEqual(ResultadoExclusaoCadastro.Excluido, Excluir(usuario));
            Assert.AreEqual(0, Db.Int("SELECT COUNT(*) FROM " + (usuario ? "usuarios" : "Livros") +
                " WHERE Id = " + (usuario ? ctx.AlunoId : ctx.LivroNormalId)));
        }

        [DataTestMethod]
        [DataRow("Ativo", true)]
        [DataRow("Ativo", false)]
        [DataRow("Atrasado", true)]
        [DataRow("Atrasado", false)]
        [DataRow(null, true)]
        [DataRow(null, false)]
        [DataRow("Devolvido", true)]
        [DataRow("Devolvido", false)]
        public void NormalPendenteOuSemDataReal_Bloqueia(string status, bool usuario)
        {
            using (var c = Db.Open())
                SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, DateTime.Today, DateTime.Today, status ?? "Ativo");
            if (status == null) Db.Execute("UPDATE Emprestimo SET Status = NULL");
            Assert.ThrowsException<InvalidOperationException>(() => Excluir(usuario));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(1, Db.Int("SELECT Ativo FROM usuarios WHERE Id = " + ctx.AlunoId));
            Assert.AreEqual(1, Db.Int("SELECT Ativo FROM Livros WHERE Id = " + ctx.LivroNormalId));
            Assert.AreEqual(3, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void NormalDevolvido_DesativaSemApagarHistorico(bool usuario)
        {
            using (var c = Db.Open())
                SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, DateTime.Today, DateTime.Today, "Devolvido");
            Db.Execute("UPDATE Emprestimo SET DataRealDevolucao = GETDATE()");
            Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(usuario));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(0, Db.Int("SELECT Ativo FROM " + (usuario ? "usuarios" : "Livros") +
                " WHERE Id = " + (usuario ? ctx.AlunoId : ctx.LivroNormalId)));
            Assert.AreEqual(3, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [DataTestMethod]
        [DataRow("Ativo", 2, true)]
        [DataRow("Faltando", 1, true)]
        [DataRow("Devolvido", 1, true)]
        [DataRow("Devolvido", 0, false)]
        public void RapidoPendenteOuInconsistente_Bloqueia(string status, int saldo, bool dataReal)
        {
            using (var c = Db.Open())
                SeedData.InsertQuickLoan(c, ctx.ProfessorId, ctx.LivroNormalId, "Livro", "6A", saldo, DateTime.Today, status);
            if (dataReal) Db.Execute("UPDATE EmprestimoRapido SET DataHoraDevolucaoReal = GETDATE()");
            using (var c = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() => ExclusaoCadastroService.ExcluirUsuario(c, ctx.ProfessorId));
                Assert.ThrowsException<InvalidOperationException>(() => ExclusaoCadastroService.ExcluirLivro(c, ctx.LivroNormalId));
            }
            Assert.AreEqual(saldo, Db.Int("SELECT Quantidade FROM EmprestimoRapido"));
        }

        [TestMethod]
        public void RapidoDevolvido_DesativaProfessorELivro()
        {
            using (var c = Db.Open())
                SeedData.InsertQuickLoan(c, ctx.ProfessorId, ctx.LivroNormalId, "Livro", "6A", 0, DateTime.Today, "Devolvido");
            Db.Execute("UPDATE EmprestimoRapido SET DataHoraDevolucaoReal = GETDATE()");
            using (var c = Db.Open())
            {
                Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, ExclusaoCadastroService.ExcluirUsuario(c, ctx.ProfessorId));
                Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, ExclusaoCadastroService.ExcluirLivro(c, ctx.LivroNormalId));
            }
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM EmprestimoRapido"));
        }

        [DataTestMethod]
        [DataRow("UsuarioId")]
        [DataRow("Responsavel")]
        public void VinculosAlternativos_Bloqueiam(string coluna)
        {
            using (var c = Db.Open())
                SeedData.InsertLoan(c, ctx.ProfessorId, ctx.LivroNormalId, ctx.BibliotecariaId, DateTime.Today, DateTime.Today, "Ativo");
            Db.Execute("UPDATE Emprestimo SET " + coluna + " = " + ctx.AlunoId);
            Assert.ThrowsException<InvalidOperationException>(() => Excluir(true));
        }

        [TestMethod]
        public void OutrosHistoricos_DesativamSemApagar()
        {
            Db.Execute("INSERT INTO NotificacoesDisponibilidade (UsuarioId, LivroId, Ativo) VALUES (" + ctx.AlunoId + "," + ctx.LivroNormalId + ",1)");
            Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(true));
            Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(false));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM NotificacoesDisponibilidade"));
        }

        [DataTestMethod]
        [DataRow("LogProrrogacoes", "INSERT INTO LogProrrogacoes (UsuarioId, DataHora) VALUES ({0}, GETDATE())")]
        [DataRow("MapeamentoAnualItens", "INSERT INTO MapeamentoAnualItens (Ano, UsuarioId, TurmaOrigem, TurmaDestino) VALUES (2026,{0},'6A','7A')")]
        [DataRow("ComunicadosAtraso", "INSERT INTO ComunicadosAtraso (UsuarioId, Instituicao, GeradoEm, VersaoModelo) VALUES ({0},'Teste',GETDATE(),1)")]
        public void HistoricoIndependenteDeEmprestimo_PreservaUsuario(string tabela, string sql)
        {
            Db.Execute(string.Format(sql, ctx.AlunoId));
            Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(true));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM " + tabela));
        }

        [TestMethod]
        public void ComunicadoPreservaLivro()
        {
            Db.Execute("INSERT INTO ComunicadosAtrasoItens (ComunicadoId, EmprestimoId, LivroId, Titulo, DataDevolucaoPrevista, DiasAtraso) VALUES (1,1," + ctx.LivroNormalId + ",'Livro',GETDATE(),2)");
            Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(false));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM ComunicadosAtrasoItens"));
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void DesativacaoImpedeNovoEmprestimo(bool usuario)
        {
            using (var c = Db.Open())
                SeedData.InsertLoan(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, DateTime.Today, DateTime.Today, "Devolvido");
            Db.Execute("UPDATE Emprestimo SET DataRealDevolucao = GETDATE()");
            Excluir(usuario);
            using (var c = Db.Open())
                Assert.ThrowsException<InvalidOperationException>(() => CirculacaoService.EmprestarNormal(c, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId, DateTime.Today.AddDays(7), DateTime.Today));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo"));
            Assert.AreEqual(3, Db.Int("SELECT Quantidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void LivroLegadoSemId_PreservaVinculoPorNome(bool devolvido)
        {
            using (var c = Db.Open())
                SeedData.InsertQuickLoan(c, ctx.ProfessorId, ctx.LivroNormalId, "Livro Normal", "6A", devolvido ? 0 : 1, DateTime.Today, devolvido ? "Devolvido" : "Ativo");
            Db.Execute("UPDATE EmprestimoRapido SET LivroId = NULL" + (devolvido ? ", DataHoraDevolucaoReal = GETDATE()" : ""));
            if (devolvido)
                Assert.AreEqual(ResultadoExclusaoCadastro.Desativado, Excluir(false));
            else
                Assert.ThrowsException<InvalidOperationException>(() => Excluir(false));
            Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM EmprestimoRapido"));
        }

        [TestMethod]
        public void CadastroAusente_NaoAlteraOutrosCadastros()
        {
            using (var c = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() => ExclusaoCadastroService.ExcluirLivro(c, 999999));
                Assert.ThrowsException<InvalidOperationException>(() => ExclusaoCadastroService.ExcluirUsuario(c, 999999));
                Assert.ThrowsException<InvalidOperationException>(() => ExclusaoCadastroService.ExcluirUsuario(c, 0));
            }
            Assert.AreEqual(4, Db.Int("SELECT COUNT(*) FROM usuarios"));
        }
    }
}