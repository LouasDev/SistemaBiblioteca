using System;
using System.Windows.Forms;
using SUB.Utils;
using static Usuarios;

namespace SUB.Forms.Livros
{
    public partial class FichaAlunoForm : Form
    {
        public FichaAlunoForm()
        {
            InitializeComponent();
            LayoutAdaptavel.Aplicar(this);
            ThemeManager.ApplyConfiguredTheme(this);
        }

        public void PreencherAluno(Aluno aluno)
        {
            txtNomeAluno.Text = aluno.Nome;
            txtEmail.Text = aluno.Email;
            txtTurma.Text = aluno.Turma;
            mtxTelefone.Text = aluno.Telefone;
            mtxCPF.Text = aluno.CPF;
            dtpDataNasc.Value = aluno.DataNascimento;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
