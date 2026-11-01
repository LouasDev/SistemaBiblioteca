using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Unit
{
    [TestClass]
    [TestCategory("P1")]
    public class CodigoMestreArquivoTests
    {
        private const string Codigo = "0123456789ABCDEF0123456789ABCDEF0123456789ABCDEF";

        [TestMethod]
        public void Ler_FormatoExportado_RetornaCodigoNormalizado()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt",
                    "Código de recuperação do SUB" + Environment.NewLine +
                    "Instituição: Escola Teste" + Environment.NewLine +
                    "Código: " + Codigo.ToLowerInvariant() + Environment.NewLine);
                Assert.AreEqual(Codigo, CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_ArquivoSomenteComCodigo_RetornaCodigo()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt", Codigo.ToLowerInvariant());
                Assert.AreEqual(Codigo, CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_VariosCodigos_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt",
                    "Código: " + Codigo + Environment.NewLine +
                    "Código: " + Codigo + Environment.NewLine);
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_TamanhoInvalido_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho47 = workspace.CreateFile("codigo47.txt", Codigo.Substring(0, 47));
                string caminho49 = workspace.CreateFile("codigo49.txt", Codigo + "0");
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho47));
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho49));
            }
        }

        [TestMethod]
        public void Ler_ExtensaoErrada_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.dat", Codigo);
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_ArquivoVazio_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt", string.Empty);
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_ArquivoMaiorQue16Kb_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt", new string('a', 16 * 1024 + 1));
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho));
            }
        }

        [TestMethod]
        public void Ler_CaracteresNaoHexadecimais_Rejeita()
        {
            using (var workspace = new TestTempWorkspace())
            {
                string caminho = workspace.CreateFile("codigo.txt", new string('Z', 48));
                Assert.ThrowsException<FormatException>(() => CodigoMestreArquivo.Ler(caminho));
            }
        }
    }
}