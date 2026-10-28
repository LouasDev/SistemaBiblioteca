using SeuProjeto;
using System;
using SUB.Models;
using SUB.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SUB.Forms.Login
{
    public partial class AboutForm : Form
    {
        private Color _corPrimariaAtual = ThemeManager.DefaultColor;
        private Bitmap _iconeFecharTema;
        private Bitmap _iconeGithubTema;
        private Bitmap _logoCompletaTema;

        public AboutForm()
        {
            InitializeComponent();
            label5.Text = "Versão " + AtualizacaoService.VersaoAtual();

            int meuRaio = 15;
            this.ArredondarCantos(meuRaio);
            this.DesenharBorda(Color.FromArgb(188, 199, 212), meuRaio, 1);
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            var configuracao = InstituicaoConfiguracaoStore.Carregar();
            Color corPrimaria = ThemeManager.DefaultColor;

            if (configuracao != null)
            {
                try
                {
                    corPrimaria = ColorTranslator.FromHtml(configuracao.CorPrimaria ?? "#1F487E");
                }
                catch
                {
                    corPrimaria = ThemeManager.DefaultColor;
                }
            }

            AplicarTema(corPrimaria);

            label7.Text = string.Format("© {0} Sistema Único Bibliotecário · SEE/MG", DateTime.Now.Year);
            BtnOk.Focus();
        }

        private void AplicarTema(Color corPrimaria)
        {
            Color primaria = ThemeManager.Normalize(corPrimaria);
            SubLogoManager.ApplyWindowIcon(this, primaria);
            ThemePreset tema = ThemeManager.FindPreset(ColorTranslator.ToHtml(primaria));
            _corPrimariaAtual = primaria;
            bool escuro = ThemeManager.Escuro;
            Color divisoria = escuro ? ThemeManager.Dark.Borda : ThemeManager.Lighten(primaria, 0.72f);

            BackColor = escuro ? ThemeManager.Dark.Fundo : tema.AccentColor;
            panel5.BackColor = escuro ? ThemeManager.Dark.Fundo : Color.FromArgb(250, 252, 255);
            pnlMarcaAbout.BackColor = primaria;
            pnlLogoAbout.BackColor = primaria;
            panel2.BackColor = divisoria;
            linhaRodape.BackColor = divisoria;

            BtnOk.BaseColor = primaria;
            BtnOk.Invalidate();
            foreach (LinkLabel link in new[] { btnTerm, linkRepositorio, linkSuporte })
            {
                link.LinkColor = primaria;
                link.ActiveLinkColor = ThemeManager.Darken(primaria, 0.18f);
                link.VisitedLinkColor = primaria;
            }

            foreach (LinkLabel link in new[] { label4, label8, label9, })
            {
                link.LinkColor = Color.FromArgb(43, 55, 69);
                link.ActiveLinkColor = primaria;
                link.VisitedLinkColor = Color.FromArgb(43, 55, 69);
            }

            AtualizarIconesGithub(escuro ? ThemeManager.Lighten(primaria, 0.5f) : primaria);
            AtualizarIconeFechar(ThemeManager.GetContrastColor(primaria));
            picExit.BackColor = Color.Transparent;
            AtualizarLogoCompleta(primaria);
            pnlLogoAbout.BringToFront();

            // O cabeçalho na cor da instituição (com o selo da SEE em fundo branco) fica como está.
            if (escuro)
            {
                foreach (Control filho in panel5.Controls)
                {
                    if (filho != pnlMarcaAbout && filho != panel2 && filho != linhaRodape)
                        ThemeManager.AplicarModoEscuro(filho, primaria);
                }
                this.AlterarCorDaBorda(ThemeManager.Dark.Borda);
            }
        }

        private void AtualizarIconesGithub(Color cor)
        {
            Bitmap origem = Properties.Resources.icons8_github_30;
            Bitmap novoIcone = new Bitmap(
                origem.Width,
                origem.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int y = 0; y < origem.Height; y++)
            {
                for (int x = 0; x < origem.Width; x++)
                {
                    Color pixel = origem.GetPixel(x, y);
                    if (pixel.A > 0)
                        novoIcone.SetPixel(x, y, Color.FromArgb(pixel.A, cor));
                }
            }

            Bitmap iconeAnterior = _iconeGithubTema;
            _iconeGithubTema = novoIcone;

            foreach (PictureBox icone in new[] { GitRenato, GitLuis, GitIthalo})
                icone.Image = novoIcone;

            if (iconeAnterior != null)
                iconeAnterior.Dispose();
        }

        private void AtualizarLogoCompleta(Color primaria)
        {
            Bitmap origem = SubLogoManager.Get(
                SubLogoVariant.Full,
                primaria,
                Color.FromArgb(250, 252, 255),
                new Size(picLogoAbout.Width * 2, picLogoAbout.Height * 2));
            Bitmap logo = new Bitmap(origem);

            Bitmap logoAnterior = _logoCompletaTema;
            _logoCompletaTema = logo;
            picLogoAbout.Image = logo;
            if (logoAnterior != null)
                logoAnterior.Dispose();
        }

        private void AtualizarIconeFechar(Color cor)
        {
            Bitmap novoIcone = new Bitmap(20, 20);

            using (Graphics grafico = Graphics.FromImage(novoIcone))
            using (Pen caneta = new Pen(cor, 2.2F))
            {
                grafico.SmoothingMode = SmoothingMode.AntiAlias;
                caneta.StartCap = LineCap.Round;
                caneta.EndCap = LineCap.Round;
                grafico.DrawLine(caneta, 5F, 5F, 15F, 15F);
                grafico.DrawLine(caneta, 15F, 5F, 5F, 15F);
            }

            Bitmap iconeAnterior = _iconeFecharTema;
            _iconeFecharTema = novoIcone;
            picExit.Image = novoIcone;
            if (iconeAnterior != null)
                iconeAnterior.Dispose();
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picExit_MouseEnter(object sender, EventArgs e)
        {
            picExit.BackColor = ThemeManager.Darken(_corPrimariaAtual, 0.12F);
        }

        private void picExit_MouseLeave(object sender, EventArgs e)
        {
            picExit.BackColor = Color.Transparent;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            foreach (PictureBox icone in new[] { GitRenato, GitLuis, GitIthalo, })
                icone.Image = null;
            if (_iconeGithubTema != null)
            {
                _iconeGithubTema.Dispose();
                _iconeGithubTema = null;
            }

            picExit.Image = null;
            if (_iconeFecharTema != null)
            {
                _iconeFecharTema.Dispose();
                _iconeFecharTema = null;
            }

            picLogoAbout.Image = null;
            if (_logoCompletaTema != null)
            {
                _logoCompletaTema.Dispose();
                _logoCompletaTema = null;
            }

            base.OnFormClosed(e);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void btnTerm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Este projeto é distribuído sob a licença MIT.",
                "Licença MIT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GitRenato_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/renato0x");
        }

        private void GitLuis_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/louasdev");
        }

        private void GitIthalo_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/Ithaloluzdepanela");
        }

        private void GitMatheus_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/MatheusAlmeida10");
        }

        private void linkRepositorio_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/LouasDev/SistemaBiblioteca");
        }

        private void linkSuporte_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("mailto:subgvsuporte@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir o aplicativo de e-mail: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir o link: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picLogoSee_Click(object sender, EventArgs e)
        {
            OpenUrl("https://www.educacao.mg.gov.br/");
        }
    }
}

