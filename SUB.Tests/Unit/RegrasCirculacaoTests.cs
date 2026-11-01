using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Utils;

namespace SUB.Tests.Unit
{
    [TestClass]
    [TestCategory("P1")]
    public class RegrasCirculacaoTests
    {
        [TestMethod]
        public void PrazoPadrao_AdicionaSeteDias()
        {
            var data = new DateTime(2026, 3, 10);
            Assert.AreEqual(new DateTime(2026, 3, 17), RegrasCirculacao.PrazoPadrao(data));
        }

        [TestMethod]
        public void PrazoValido_IgualOuPosterior_Valido()
        {
            var emprestimo = new DateTime(2026, 3, 10);
            Assert.IsTrue(RegrasCirculacao.PrazoValido(emprestimo, emprestimo));
            Assert.IsTrue(RegrasCirculacao.PrazoValido(emprestimo, emprestimo.AddDays(1)));
            Assert.IsFalse(RegrasCirculacao.PrazoValido(emprestimo, emprestimo.AddDays(-1)));
        }

        [TestMethod]
        public void AlunoAtingiuLimite_SomenteAlunoComEmprestimoAtivo()
        {
            Assert.IsTrue(RegrasCirculacao.AlunoAtingiuLimite(RegrasCirculacao.PerfilAluno, 1));
            Assert.IsFalse(RegrasCirculacao.AlunoAtingiuLimite(RegrasCirculacao.PerfilAluno, 0));
            Assert.IsFalse(RegrasCirculacao.AlunoAtingiuLimite(RegrasCirculacao.PerfilProfessor, 5));
        }

        [TestMethod]
        public void AtingiuLimitePendencias_LimiteEhDois()
        {
            Assert.IsFalse(RegrasCirculacao.AtingiuLimitePendencias(1));
            Assert.IsTrue(RegrasCirculacao.AtingiuLimitePendencias(2));
            Assert.IsTrue(RegrasCirculacao.AtingiuLimitePendencias(3));
        }

        [TestMethod]
        public void EmprestimoNormalAberto_AtivoEAtrasado()
        {
            Assert.IsTrue(RegrasCirculacao.EmprestimoNormalAberto("Ativo"));
            Assert.IsTrue(RegrasCirculacao.EmprestimoNormalAberto("Atrasado"));
            Assert.IsFalse(RegrasCirculacao.EmprestimoNormalAberto("Devolvido"));
            Assert.IsFalse(RegrasCirculacao.EmprestimoNormalAberto(null));
        }

        [TestMethod]
        public void EmprestimoRapidoAberto_ExigeSaldoESituacaoAberta()
        {
            Assert.IsTrue(RegrasCirculacao.EmprestimoRapidoAberto("Ativo", 1));
            Assert.IsTrue(RegrasCirculacao.EmprestimoRapidoAberto("Faltando", 2));
            Assert.IsFalse(RegrasCirculacao.EmprestimoRapidoAberto("Ativo", 0));
            Assert.IsFalse(RegrasCirculacao.EmprestimoRapidoAberto("Devolvido", 1));
        }

        [TestMethod]
        public void Quantidades_ValidamLimites()
        {
            Assert.IsTrue(RegrasCirculacao.QuantidadeRetiradaValida(1));
            Assert.IsFalse(RegrasCirculacao.QuantidadeRetiradaValida(0));
            Assert.IsFalse(RegrasCirculacao.QuantidadeRetiradaValida(-1));

            Assert.IsTrue(RegrasCirculacao.QuantidadeDevolvidaValida(2, 1));
            Assert.IsTrue(RegrasCirculacao.QuantidadeDevolvidaValida(2, 0));
            Assert.IsFalse(RegrasCirculacao.QuantidadeDevolvidaValida(2, 3));
            Assert.IsFalse(RegrasCirculacao.QuantidadeDevolvidaValida(0, 0));
        }

        [TestMethod]
        public void SaldoAposDevolucao_CalculaOuLanca()
        {
            Assert.AreEqual(1, RegrasCirculacao.SaldoAposDevolucao(2, 1));
            Assert.ThrowsException<InvalidOperationException>(() => RegrasCirculacao.SaldoAposDevolucao(2, 3));
        }

        [TestMethod]
        public void SituacaoSaldoRapido_ZeroEhDevolvido()
        {
            Assert.AreEqual("Devolvido", RegrasCirculacao.SituacaoSaldoRapido(0));
            Assert.AreEqual("Faltando", RegrasCirculacao.SituacaoSaldoRapido(1));
        }

        [TestMethod]
        public void SituacaoPrazo_ComparaPorDia()
        {
            var agora = new DateTime(2026, 3, 10, 23, 59, 0);
            Assert.AreEqual("Ativo", RegrasCirculacao.SituacaoPrazo(new DateTime(2026, 3, 10, 0, 0, 0), agora));
            Assert.AreEqual("Atrasado", RegrasCirculacao.SituacaoPrazo(new DateTime(2026, 3, 9, 23, 59, 0), agora));
            Assert.AreEqual("Ativo", RegrasCirculacao.SituacaoPrazo(new DateTime(2026, 3, 11), agora));
        }

        [TestMethod]
        public void ErroProrrogacao_ValidaIntervaloEOverflow()
        {
            var prazo = new DateTime(2026, 3, 10);
            Assert.IsNull(RegrasCirculacao.ErroProrrogacao(prazo, 1));
            Assert.IsNull(RegrasCirculacao.ErroProrrogacao(prazo, 14));
            Assert.IsNotNull(RegrasCirculacao.ErroProrrogacao(prazo, 0));
            Assert.IsNotNull(RegrasCirculacao.ErroProrrogacao(prazo, 15));
            Assert.IsNotNull(RegrasCirculacao.ErroProrrogacao(DateTime.MaxValue.AddDays(-1), 2));
        }

        [TestMethod]
        public void CalcularProrrogacao_AdicionaOuLanca()
        {
            var prazo = new DateTime(2026, 3, 10);
            Assert.AreEqual(prazo.AddDays(14), RegrasCirculacao.CalcularProrrogacao(prazo, 14));
            Assert.ThrowsException<InvalidOperationException>(() => RegrasCirculacao.CalcularProrrogacao(prazo, 0));
        }
    }
}