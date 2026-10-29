using System;
using System.Windows.Forms;
using SUB.Utils;

namespace SUB.Forms.Livros
{
    public partial class DevoluçãoRapidaForm : Form
    {
        public int QuantidadeDevolvida { get; private set; }

        public DevoluçãoRapidaForm(int emprestimoId, string professor, string livro, string turma, int quantidadeEmprestada, DateTime dataEmprestimo, string codigoBarras) : this()
        {
            // Nome do Professor
            txtProfessor.Text = professor;
            //Livro
            txtLivro.Text = livro;
            // Turma
            txtTurma.Text = turma;
            txtTurma.Enabled = false;
            // Quantidade emprestada
            numQuantidadeEmprestado.Text = quantidadeEmprestada.ToString();
            numQuantidadeEmprestado.Enabled = false;
            // Hora do empréstimo
            roundedTextBox2.Text = dataEmprestimo.ToString("HH:mm");
            roundedTextBox2.Enabled = false;
            // Hora da devolução (agora)
            roundedTextBox3.Text = DateTime.Now.ToString("HH:mm");
            roundedTextBox3.Enabled = false;
            // Quantidade devolvida (editável)
            numQuantidadeDevolvidos.Text = quantidadeEmprestada.ToString();
            numQuantidadeDevolvidos.Enabled = true;
            // Preencher o campo de código de barras
            mtxCodigoBarras.Text = codigoBarras;
            mtxCodigoBarras.Enabled = false;
        }

        public DevoluçãoRapidaForm()
        {
            InitializeComponent();
            ThemeManager.ApplyConfiguredTheme(this);
            numQuantidadeDevolvidos.Text = "1"; // valor inicial
            numQuantidadeDevolvidos.KeyPress += numQuantidade_KeyPress;
            numQuantidadeDevolvidos.TextChanged += numQuantidade_TextChanged;
            btnConfirmarDevolucao.Click += btnConfirmar_Click;
            AcceptButton = btnConfirmarDevolucao;
        }

        // Esc fecha sem devolver, como o X da janela.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void numQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true; // cancela caracteres não numéricos
        }

        private void numQuantidade_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(numQuantidadeDevolvidos.Text, out int valor) || valor < 1)
            {
                numQuantidadeDevolvidos.SelectionStart = numQuantidadeDevolvidos.Text.Length; // cursor no final
            }
        }

        private void btnMais_Click(object sender, EventArgs e)
        {
            int valor;
            int limite;
            if (!int.TryParse(numQuantidadeDevolvidos.Text, out valor))
                valor = 0;
            if (!int.TryParse(numQuantidadeEmprestado.Text, out limite) || valor >= limite)
                return;
            valor++;
            numQuantidadeDevolvidos.Text = valor.ToString();
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {
            int valor;
            if (!int.TryParse(numQuantidadeDevolvidos.Text, out valor))
                valor = 0;
            if (valor > 0)
                valor--;
            numQuantidadeDevolvidos.Text = valor.ToString();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            int qtdDevolvida;
            int qtdEmprestada;
            if (!int.TryParse(numQuantidadeDevolvidos.Text, out qtdDevolvida) || !int.TryParse(numQuantidadeEmprestado.Text, out qtdEmprestada) || !RegrasCirculacao.QuantidadeDevolvidaValida(qtdEmprestada, qtdDevolvida))
            {
                MessageBox.Show(RegrasCirculacao.ErroQuantidadeDevolvida, "Quantidade inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (qtdDevolvida < qtdEmprestada)
            {
                var result = MessageBox.Show("A quantidade devolvida é menor que a emprestada. Deseja realmente registrar como devolução faltando?", "Confirmação de devolução faltando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes)
                    return;
            }

            QuantidadeDevolvida = qtdDevolvida;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
