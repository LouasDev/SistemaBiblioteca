using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SUB.Models;
namespace SUB.Tests.Unit
{
    [TestClass]
    public class PoliticaEmprestimosModelTests
    {
        [TestMethod]
        public void ConfiguracaoAntiga_UsaPadroesSemModificarOriginal()
        {
            var config = JsonConvert.DeserializeObject<InstituicaoConfiguracao>(JsonConvert.SerializeObject(new { Nome = "Escola" }));
            var regras = PoliticaEmprestimos.Obter(config);
            Assert.AreEqual(1, regras.LimiteAluno);
            Assert.AreEqual(-1, regras.LimiteProfessor);
            Assert.AreEqual(-1, regras.LimiteBibliotecario);
            Assert.AreEqual(-1, regras.MaximoProrrogacoes);
            Assert.AreEqual(7, regras.PrazoDias);
            Assert.IsNull(config.RegrasEmprestimo);
        }

        [TestMethod]
        public void Json_PreservaRegrasEOutrosCampos()
        {
            var original = new InstituicaoConfiguracao { Nome = "Escola", CodigoMestreHash = "ficticio", CorPrimaria = "#123456",
                RegrasEmprestimo = new PoliticaEmprestimos { LimiteAluno=2, LimiteProfessor=5, LimiteBibliotecario=3, PrazoDias=20, MaximoProrrogacoes=2 } };
            var copy = JsonConvert.DeserializeObject<InstituicaoConfiguracao>(JsonConvert.SerializeObject(original));
            Assert.AreEqual("ficticio", copy.CodigoMestreHash);
            Assert.AreEqual("#123456", copy.CorPrimaria);
            Assert.AreEqual(20, copy.RegrasEmprestimo.PrazoDias);
            Assert.AreEqual(2, copy.RegrasEmprestimo.LimiteAluno);
            Assert.AreEqual(5, copy.RegrasEmprestimo.LimiteProfessor);
            Assert.AreEqual(3, copy.RegrasEmprestimo.LimiteBibliotecario);
            Assert.AreEqual(2, copy.RegrasEmprestimo.MaximoProrrogacoes);
        }

        [DataTestMethod]
        [DataRow("LimiteAluno", -2)]
        [DataRow("LimiteProfessor", 1001)]
        [DataRow("LimiteBibliotecario", -2)]
        [DataRow("PrazoDias", 0)]
        [DataRow("PrazoDias", 3651)]
        [DataRow("MaximoProrrogacoes", -2)]
        [DataRow("MaximoProrrogacoes", 1001)]
        public void ConfiguracaoInvalida_Rejeita(string campo, int valor)
        {
            var regras = new PoliticaEmprestimos();
            typeof(PoliticaEmprestimos).GetProperty(campo).SetValue(regras, valor);
            Assert.ThrowsException<InvalidOperationException>(() => regras.Validar());
        }

        [TestMethod]
        public void Copia_NaoAlteraRegraOriginal()
        {
            var regras = new PoliticaEmprestimos();
            var copy = regras.Copiar();
            copy.LimiteAluno = 10;
            Assert.AreEqual(1, regras.LimiteAluno);
        }
    }
}