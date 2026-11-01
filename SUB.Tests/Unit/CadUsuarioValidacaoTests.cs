using System;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Forms.Usuario;

namespace SUB.Tests.Unit
{
    [TestClass]
    [TestCategory("P1")]
    public class CadUsuarioValidacaoTests
    {
        private static CadUsuario CriarValidador()
        {
            return (CadUsuario)FormatterServices.GetUninitializedObject(typeof(CadUsuario));
        }

        [TestMethod]
        public void ValidarCPF_AceitaCpfValido()
        {
            Assert.IsTrue(CriarValidador().ValidarCPF("111.444.777-35"));
            Assert.IsTrue(CriarValidador().ValidarCPF("11144477735"));
        }

        [TestMethod]
        public void ValidarCPF_RejeitaCpfInvalido()
        {
            var validador = CriarValidador();
            Assert.IsFalse(validador.ValidarCPF("111.111.111-11"));
            Assert.IsFalse(validador.ValidarCPF("123.456.789-00"));
            Assert.IsFalse(validador.ValidarCPF("123"));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void ValidarCPF_Nulo_DeveRetornarFalse()
        {
            Assert.IsFalse(CriarValidador().ValidarCPF(null));
        }

        [TestMethod]
        public void ValidarTelefone_AceitaOnzeDigitos()
        {
            Assert.IsTrue(CriarValidador().ValidarTelefone("(11) 91234-5678"));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void ValidarTelefone_AceitaDezDigitos_FixoComDdd()
        {
            Assert.IsTrue(CriarValidador().ValidarTelefone("(11) 1234-5678"));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void ValidarTelefone_RejeitaDozeDigitos()
        {
            Assert.IsFalse(CriarValidador().ValidarTelefone("551191234567"));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void ValidarTelefone_Nulo_DeveRetornarFalse()
        {
            Assert.IsFalse(CriarValidador().ValidarTelefone(null));
        }

        [TestMethod]
        public void ValidarEmail_AceitaEmailValido()
        {
            Assert.IsTrue(CriarValidador().ValidarEmail("pessoa@instituicao.gov.br"));
            Assert.IsTrue(CriarValidador().ValidarEmail("a@b.co"));
        }

        [TestMethod]
        public void ValidarEmail_RejeitaEmailInvalido()
        {
            var validador = CriarValidador();
            Assert.IsFalse(validador.ValidarEmail("sem-arroba"));
            Assert.IsFalse(validador.ValidarEmail("a@b"));
            Assert.IsFalse(validador.ValidarEmail("a b@c.com"));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void ValidarEmail_Nulo_DeveRetornarFalse()
        {
            Assert.IsFalse(CriarValidador().ValidarEmail(null));
        }
    }
}