using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using SUB.Models;
using SUB.Utils;
namespace SUB.Forms.Configuracoes
{
    public partial class PoliticaEmprestimosForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PoliticaEmprestimos Regras { get; private set; }
        public PoliticaEmprestimosForm()
        {
            InitializeComponent();
            editorRegras.ValidadeAlterada += valido => btnAplicar.Enabled = valido;
            ThemeManager.Apply(this, ThemeManager.DefaultColor);
            editorRegras.AplicarTema(ThemeManager.DefaultColor);
        }
        public PoliticaEmprestimosForm(PoliticaEmprestimos regras, Color cor) : this()
        {
            editorRegras.Preencher(regras);
            ThemeManager.Apply(this, cor);
            editorRegras.AplicarTema(cor);
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            if (!editorRegras.ValidarValores()) return;
            Regras = editorRegras.LerRegras();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
