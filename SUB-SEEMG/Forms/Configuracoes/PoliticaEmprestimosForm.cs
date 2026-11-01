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
            Preencher(new PoliticaEmprestimos());
            ThemeManager.Apply(this, ThemeManager.DefaultColor);
        }

        public PoliticaEmprestimosForm(PoliticaEmprestimos regras, Color cor) : this()
        {
            Preencher(regras);
            ThemeManager.Apply(this, cor);
        }

        private void Preencher(PoliticaEmprestimos regras)
        {
            regras.Validar();
            numAluno.Value = Math.Max(0, regras.LimiteAluno);
            chkSemLimiteAluno.Checked = regras.LimiteAluno < 0;
            numProfessor.Value = Math.Max(0, regras.LimiteProfessor);
            chkSemLimiteProfessor.Checked = regras.LimiteProfessor < 0;
            numBibliotecario.Value = Math.Max(0, regras.LimiteBibliotecario);
            chkSemLimiteBibliotecario.Checked = regras.LimiteBibliotecario < 0;
            numPrazo.Value = regras.PrazoDias;
            numProrrogacoes.Value = Math.Max(0, regras.MaximoProrrogacoes);
            chkSemLimiteProrrogacoes.Checked = regras.MaximoProrrogacoes < 0;
        }

        private void SemLimite_CheckedChanged(object sender, EventArgs e)
        {
            numAluno.Enabled = !chkSemLimiteAluno.Checked;
            numProfessor.Enabled = !chkSemLimiteProfessor.Checked;
            numBibliotecario.Enabled = !chkSemLimiteBibliotecario.Checked;
            numProrrogacoes.Enabled = !chkSemLimiteProrrogacoes.Checked;
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var regras = new PoliticaEmprestimos {
                LimiteAluno = chkSemLimiteAluno.Checked ? -1 : (int)numAluno.Value, LimiteProfessor = chkSemLimiteProfessor.Checked ? -1 : (int)numProfessor.Value,
                LimiteBibliotecario = chkSemLimiteBibliotecario.Checked ? -1 : (int)numBibliotecario.Value, PrazoDias = (int)numPrazo.Value,
                MaximoProrrogacoes = chkSemLimiteProrrogacoes.Checked ? -1 : (int)numProrrogacoes.Value
            };
            regras.Validar();
            Regras = regras;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
