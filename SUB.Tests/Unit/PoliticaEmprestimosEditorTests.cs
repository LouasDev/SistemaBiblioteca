using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Forms.Configuracoes;
using SUB.Models;

namespace SUB.Tests.Unit
{
    [TestClass]
    public class PoliticaEmprestimosEditorTests
    {
        [DataTestMethod]
        [DataRow("numAluno")]
        [DataRow("numProfessor")]
        [DataRow("numBibliotecario")]
        public void LimiteDeLivros_ZeroViraUmEDezContinuaDigitavel(string nomeCampo)
        {
            using (var editor = new PoliticaEmprestimosEditor())
            {
                var campo = ObterCampo(editor, nomeCampo);
                campo.Text = "0";
                Assert.AreEqual("1", campo.Text);

                var entrada = campo.Controls.OfType<TextBox>().Single();
                entrada.Text = "1";
                entrada.AppendText("0");
                Assert.AreEqual("10", campo.Text);
                Assert.IsTrue(editor.ValidarValores());
            }
        }

        [TestMethod]
        public void SemLimite_ExibeUmNoCampoDesabilitado()
        {
            using (var editor = new PoliticaEmprestimosEditor())
            {
                editor.Preencher(new PoliticaEmprestimos { LimiteBibliotecario = -1 });
                Assert.AreEqual("1", ObterCampo(editor, "numBibliotecario").Text);
                Assert.IsFalse(ObterCampo(editor, "numBibliotecario").Enabled);
                Assert.IsTrue(ObterCheck(editor, "chkSemLimiteBibliotecario").Checked);
                Assert.IsTrue(editor.ValidarValores());
            }
        }

        private static RoundedTextBox ObterCampo(PoliticaEmprestimosEditor editor, string nome)
        {
            return (RoundedTextBox)typeof(PoliticaEmprestimosEditor)
                .GetField(nome, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(editor);
        }

        private static CheckBox ObterCheck(PoliticaEmprestimosEditor editor, string nome)
        {
            return (CheckBox)typeof(PoliticaEmprestimosEditor)
                .GetField(nome, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(editor);
        }
    }
}
