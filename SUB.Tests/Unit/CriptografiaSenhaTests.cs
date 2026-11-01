using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Utils;

namespace SUB.Tests.Unit
{
    [TestClass]
    [TestCategory("P1")]
    public class CriptografiaSenhaTests
    {
        [TestMethod]
        public void CriarHash_GeraSaltEHashEmBase64ComTamanhosEsperados()
        {
            CriptografiaSenha.CriarHash("Senha#123", out string hash, out string salt);

            Assert.AreEqual(16, Convert.FromBase64String(salt).Length);
            Assert.AreEqual(32, Convert.FromBase64String(hash).Length);
        }

        [TestMethod]
        public void CriarHash_MesmaSenhaComSaltsDiferentes_GeraHashesDiferentes()
        {
            CriptografiaSenha.CriarHash("Senha#123", out string hash1, out string salt1);
            CriptografiaSenha.CriarHash("Senha#123", out string hash2, out string salt2);

            Assert.AreNotEqual(salt1, salt2);
            Assert.AreNotEqual(hash1, hash2);
            Assert.IsTrue(CriptografiaSenha.VerificarSenha("Senha#123", hash1, salt1));
            Assert.IsTrue(CriptografiaSenha.VerificarSenha("Senha#123", hash2, salt2));
        }

        [TestMethod]
        public void VerificarSenha_SenhaErrada_RetornaFalse()
        {
            CriptografiaSenha.CriarHash("Senha#123", out string hash, out string salt);
            Assert.IsFalse(CriptografiaSenha.VerificarSenha("Senha#124", hash, salt));
        }

        [TestMethod]
        public void VerificarSenha_Unicode_RetornaTrue()
        {
            const string senha = "çãõé 汉字 🔐";
            CriptografiaSenha.CriarHash(senha, out string hash, out string salt);
            Assert.IsTrue(CriptografiaSenha.VerificarSenha(senha, hash, salt));
        }

        [TestMethod]
        public void VerificarSenha_SenhaVazia_RetornaTrue()
        {
            CriptografiaSenha.CriarHash(string.Empty, out string hash, out string salt);
            Assert.IsTrue(CriptografiaSenha.VerificarSenha(string.Empty, hash, salt));
        }

        [TestMethod]
        public void VerificarSenha_HashNulo_RetornaFalse()
        {
            Assert.IsFalse(CriptografiaSenha.VerificarSenha("Senha#123", null, "AAAAAAAAAAAAAAAAAAAAAA=="));
        }

        [TestMethod]
        [TestCategory("BugConhecido")]
        public void VerificarSenha_SaltMalformado_DeveRetornarFalse()
        {
            Assert.IsFalse(CriptografiaSenha.VerificarSenha("Senha#123", "hash-invalido", "salt-invalido"));
        }
    }
}