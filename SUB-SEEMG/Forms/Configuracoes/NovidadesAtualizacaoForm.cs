using SUB.Models;
using SUB.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SUB.Forms.Configuracoes
{
    public partial class NovidadesAtualizacaoForm : Form
    {
        public NovidadesAtualizacaoForm()
        {
            InitializeComponent();
        }

        public void CarregarNovidades(AtualizacaoManifesto manifesto, bool permitirAtualizacao = false,
            bool instalada = false)
        {
            if (manifesto == null)
                throw new ArgumentNullException(nameof(manifesto));

            lblVersao.Text = "SUB " + (VersaoAplicativo.Formatar(manifesto.Versao) ?? "—");
            lblData.Text = string.IsNullOrWhiteSpace(manifesto.Data)
                ? string.Empty : "Publicada em " + manifesto.Data.Trim();
            lblSubtitulo.Text = instalada ? "Atualização instalada. Confira o que mudou."
                : "Confira as melhorias e correções desta versão.";
            rtbNovidades.Text = string.IsNullOrWhiteSpace(manifesto.Notas)
                ? "Não foram informadas notas para esta versão."
                : ConfiguracaoForm.TruncarNotas(manifesto.Notas);
            btnContinuar.Visible = permitirAtualizacao;
            btnFechar.Text = permitirAtualizacao ? "Agora não" : "Entendi";
            btnFechar.DialogResult = permitirAtualizacao ? DialogResult.Cancel : DialogResult.OK;
            AcceptButton = permitirAtualizacao ? btnContinuar : btnFechar;
        }

        public void AplicarTema(Color corPrimaria)
        {
            Color cor = ThemeManager.Normalize(corPrimaria);
            ThemeManager.Apply(this, cor);
            pnlHeader.BackColor = cor;
            lblTitulo.ForeColor = ThemeManager.GetContrastColor(cor);
            lblSubtitulo.ForeColor = ThemeManager.GetContrastColor(cor);
            lblVersao.ForeColor = ThemeManager.GetLabelColor(cor);
            pnlRodape.BackColor = ThemeManager.Lighten(cor, 0.94F);
        }
    }
}
