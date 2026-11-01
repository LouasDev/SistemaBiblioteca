using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Utils;

namespace SUB.Tests.Unit
{
    [TestClass]
    [TestCategory("P1")]
    public class RegrasMapeamentoTurmasTests
    {
        private static RegrasMapeamentoTurmas Criar()
        {
            return new RegrasMapeamentoTurmas(new[]
            {
                "1º Ano 1", "2º Ano 1", "3º Ano 1",
                "6º Ano 1", "6º Ano 2", "7º Ano 1", "7º Ano 2",
                "8º Ano 2", "9º Ano 1"
            });
        }

        [TestMethod]
        public void Sugerir_SerieIntermediaria_AvancaParaProximoAno()
        {
            Assert.AreEqual("7º Ano 1", Criar().Sugerir("6º Ano 1"));
        }

        [TestMethod]
        public void Sugerir_NonoAno_VoltaParaPrimeiroAno()
        {
            Assert.AreEqual("1º Ano 1", Criar().Sugerir("9º Ano 1"));
        }

        [TestMethod]
        public void Sugerir_TerceiroAno_RetornaEgresso()
        {
            Assert.AreEqual(RegrasMapeamentoTurmas.Egresso, Criar().Sugerir("3º Ano 1"));
        }

        [TestMethod]
        public void OpcoesDeProgressao_StatusEspecial_RetornaProprioStatus()
        {
            var regras = Criar();
            CollectionAssert.AreEqual(new[] { "EGRESSO" }, regras.OpcoesDeProgressao("egresso"));
            CollectionAssert.AreEqual(new[] { "TRANSFERIDO" }, regras.OpcoesDeProgressao("Transferido"));
            CollectionAssert.AreEqual(new[] { "DESISTENTE" }, regras.OpcoesDeProgressao("desistente"));
        }

        [TestMethod]
        public void OpcoesDeAjusteIndividual_IncluiProgressaoMesmoAnoEStatusEspeciais()
        {
            var opcoes = Criar().OpcoesDeAjusteIndividual("6º Ano 1");
            CollectionAssert.Contains(opcoes, "7º Ano 1");
            CollectionAssert.Contains(opcoes, "6º Ano 1");
            CollectionAssert.Contains(opcoes, "6º Ano 2");
            CollectionAssert.Contains(opcoes, RegrasMapeamentoTurmas.Desistente);
            CollectionAssert.Contains(opcoes, RegrasMapeamentoTurmas.Transferido);
        }

        [TestMethod]
        public void EhEscolhaDeProgressaoValida_SoAceitaSerieSeguinte()
        {
            var regras = Criar();
            Assert.IsTrue(regras.EhEscolhaDeProgressaoValida("6º Ano 1", "7º Ano 1"));
            Assert.IsTrue(regras.EhEscolhaDeProgressaoValida("6º Ano 1", " 7º ano 1 "));
            Assert.IsFalse(regras.EhEscolhaDeProgressaoValida("6º Ano 1", "8º Ano 2"));
            Assert.IsFalse(regras.EhEscolhaDeProgressaoValida("6º Ano 1", "6º Ano 1"));
        }

        [TestMethod]
        public void EhEscolhaIndividualValida_AceitaStatusEspecial()
        {
            Assert.IsTrue(Criar().EhEscolhaIndividualValida("6º Ano 1", RegrasMapeamentoTurmas.Transferido));
            Assert.IsFalse(Criar().EhEscolhaIndividualValida("6º Ano 1", "9º Ano 1"));
        }

        [TestMethod]
        public void ExtrairSerie_ExtraiNumeroOuZero()
        {
            var regras = Criar();
            Assert.AreEqual(8, regras.ExtrairSerie("8º Ano 2"));
            Assert.AreEqual(9, regras.ExtrairSerie("9 ano 1"));
            Assert.AreEqual(0, regras.ExtrairSerie("EGRESSO"));
            Assert.AreEqual(0, regras.ExtrairSerie("6º Ano A"));
            Assert.AreEqual(0, regras.ExtrairSerie(null));
        }

        [TestMethod]
        public void OpcoesDeProgressao_TurmaInvalida_RetornaVazio()
        {
            Assert.AreEqual(0, Criar().OpcoesDeProgressao("6º Ano A").Count);
        }

        [TestMethod]
        public void OpcoesDeProgressao_SemSerieSeguinte_RetornaVazio()
        {
            var regras = new RegrasMapeamentoTurmas(new[] { "6º Ano 1" });
            Assert.AreEqual(0, regras.OpcoesDeProgressao("6º Ano 1").Count);
        }

        [TestMethod]
        public void Construtor_ListaNula_NaoLanca()
        {
            var regras = new RegrasMapeamentoTurmas(null);
            Assert.AreEqual(0, regras.OpcoesDeProgressao("6º Ano 1").Count);
        }
    }
}