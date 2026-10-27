using SUB.Models;
using SUB.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SUB.Forms.Configuracoes
{
    public partial class ResetTermoForm : Form
    {
        private string arquivoSelecionado;
        internal Func<string, string> ValidarArquivo;

        [Browsable(false)]
        public ResumoReset Resumo { get; set; }

        internal string CodigoMestre { get; private set; }

        public ResetTermoForm()
        {
            InitializeComponent();
            ValidarArquivo = ResetService.ValidarArquivoCodigo;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                AplicarTema(ThemeManager.DefaultColor);
        }

        public void AplicarTema(Color corPrimaria)
        {
            Color cor = ThemeManager.Normalize(corPrimaria);
            ThemeManager.Apply(this, cor);
            pnlHeader.BackColor = cor;
            lblTitulo.ForeColor = ThemeManager.GetContrastColor(cor);
            lblSubtitulo.ForeColor = ThemeManager.GetContrastColor(cor);
            chkConcordo.ForeColor = ThemeManager.GetLabelColor(cor);
            pnlRodape.BackColor = ThemeManager.Escuro ? ThemeManager.Dark.Superficie : ThemeManager.Lighten(cor, 0.94F);
        }

        private void ResetTermoForm_Load(object sender, EventArgs e)
        {
            if (Resumo == null)
                return;
            lblUsuariosValor.Text = Resumo.Usuarios.ToString("N0", CultureInfo.CurrentCulture);
            lblTitulosValor.Text = Resumo.Titulos.ToString("N0", CultureInfo.CurrentCulture);
            lblEmprestimosValor.Text = Resumo.Emprestimos.ToString("N0", CultureInfo.CurrentCulture);
            lblRapidosValor.Text = Resumo.EmprestimosRapidos.ToString("N0", CultureInfo.CurrentCulture);
            lblTurmasValor.Text = Resumo.Turmas.ToString("N0", CultureInfo.CurrentCulture);
        }

        private void btnSelecionarTxt_Click(object sender, EventArgs e)
        {
            if (dlgCodigoMestre.ShowDialog(this) == DialogResult.OK)
                ImportarArquivo(dlgCodigoMestre.FileName);
        }

        internal bool ImportarArquivo(string caminho)
        {
            CodigoMestre = null;
            arquivoSelecionado = null;
            btnContinuar.Enabled = false;
            try
            {
                lblArquivo.Text = Path.GetFileName(caminho);
                string codigo = ValidarArquivo(caminho);
                if (string.IsNullOrWhiteSpace(codigo))
                    throw new InvalidOperationException("Não foi possível validar o arquivo de recuperação.");
                CodigoMestre = codigo;
                arquivoSelecionado = caminho;
                lblStatusArquivo.Text = "Arquivo validado. O código não será exibido na tela.";
                lblStatusArquivo.ForeColor = Color.FromArgb(31, 122, 73);
                AtualizarContinuar();
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                ex is FormatException || ex is InvalidOperationException || ex is ArgumentException)
            {
                lblStatusArquivo.Text = ex.Message;
                lblStatusArquivo.ForeColor = Color.FromArgb(178, 34, 34);
                return false;
            }
        }

        private void chkConcordo_CheckedChanged(object sender, EventArgs e)
        {
            AtualizarContinuar();
        }

        private void AtualizarContinuar()
        {
            btnContinuar.Enabled = chkConcordo.Checked && !string.IsNullOrEmpty(CodigoMestre);
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            if (!chkConcordo.Checked || string.IsNullOrEmpty(arquivoSelecionado))
                return;
            // Confere novamente o arquivo caso tenha sido trocado ou removido
            // enquanto o termo estava aberto.
            if (!ImportarArquivo(arquivoSelecionado))
                return;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
