using System;
using System.Drawing;
using System.Windows.Forms;
using SUB.Utils;

namespace SUB.Forms.Usuario
{
    public partial class PasswordForm : Form
    {
        public string SenhaDigitada { get; private set; }
        public string Titulo { get; set; } = "Confirmação de Senha";
        public string Mensagem { get; set; } = "Digite sua senha para confirmar a operação:";
        public string Placeholder { get; set; } = "Digite sua senha...";
        public string MensagemCampoObrigatorio { get; set; } = "Digite uma senha!";

        public PasswordForm()
        {
            InitializeComponent();
            AcceptButton = btnConfirmar;
            ThemeManager.ApplyConfiguredTheme(this);
            this.StartPosition = FormStartPosition.CenterParent;
            Shown += PasswordForm_Shown;
            CancelButton = btnCancelar;
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = Titulo;
            lblMensagem.Text = Mensagem;
            txtSenha.PlaceholderText = Placeholder;
            txtSenha.Focus();
        }

        private void PasswordForm_Shown(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (IsDisposed)
                    return;

                ActiveControl = txtSenha;
                txtSenha.Focus();
            }));
        }

        

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                lblErro.Text = MensagemCampoObrigatorio;
                lblErro.Visible = true;
                txtSenha.Focus();
                return;
            }

            SenhaDigitada = txtSenha.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            lblErro.Visible = false;
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConfirmar.PerformClick();
                e.SuppressKeyPress = true;
            }
        }

        
    }
}
