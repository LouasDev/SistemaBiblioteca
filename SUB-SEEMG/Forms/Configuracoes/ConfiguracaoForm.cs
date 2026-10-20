using SUB.Models;
using SUB.Utils;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm : Form
    {
        private const int TamanhoMaximoNome = 150;

        private TextBox txtNome;
        private Panel pnlCorPreview;
        private Label lblCorHex;
        private Button btnEscolherCor;
        private Button btnSalvar;
        private Button btnCancelar;
        private Color corSelecionada = ThemeManager.DefaultColor;

        public ConfiguracaoForm()
        {
            InitializeComponent();
            ConstruirUi();
            CarregarValoresAtuais();
        }

        private void ConstruirUi()
        {
            Text = "Configurações";

            var lblTitulo = new Label
            {
                Text = "Configurações da instituição",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(24, 20)
            };

            var lblNome = new Label
            {
                Text = "Nome da instituição:",
                AutoSize = true,
                Location = new Point(24, 64)
            };

            txtNome = new TextBox
            {
                Location = new Point(24, 88),
                Size = new Size(360, 27),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                MaxLength = TamanhoMaximoNome
            };

            var lblCor = new Label
            {
                Text = "Cor principal da identidade visual:",
                AutoSize = true,
                Location = new Point(24, 128)
            };

            pnlCorPreview = new Panel
            {
                Location = new Point(24, 152),
                Size = new Size(180, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            pnlCorPreview.Paint += (s, e) =>
            {
                using (var fonte = new Font("Segoe UI", 10F, FontStyle.Bold))
                using (var pincel = new SolidBrush(ThemeManager.GetContrastColor(corSelecionada)))
                {
                    const string texto = "Pré-visualização";
                    SizeF medida = e.Graphics.MeasureString(texto, fonte);
                    e.Graphics.DrawString(texto, fonte, pincel,
                        (pnlCorPreview.Width - medida.Width) / 2f,
                        (pnlCorPreview.Height - medida.Height) / 2f);
                }
            };

            btnEscolherCor = new Button
            {
                Text = "Escolher cor...",
                Location = new Point(216, 152),
                Size = new Size(120, 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                FlatStyle = FlatStyle.Flat
            };
            btnEscolherCor.Click += btnEscolherCor_Click;

            lblCorHex = new Label
            {
                AutoSize = true,
                Location = new Point(348, 161),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Text = ""
            };

            btnSalvar = new Button
            {
                Name = "btnSalvar",
                Text = "Salvar",
                Location = new Point(520, 380),
                Size = new Size(100, 36),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat
            };
            btnSalvar.Click += btnSalvar_Click;

            btnCancelar = new Button
            {
                Name = "btnCancelar",
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(630, 380),
                Size = new Size(100, 36),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.Click += (s, e) => Close();

            CancelButton = btnCancelar;

            Controls.Add(lblTitulo);
            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblCor);
            Controls.Add(pnlCorPreview);
            Controls.Add(btnEscolherCor);
            Controls.Add(lblCorHex);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        private void CarregarValoresAtuais()
        {
            InstituicaoConfiguracao configuracao = null;
            try
            {
                configuracao = InstituicaoConfiguracaoStore.Carregar();
            }
            catch
            {
                // JSON ilegível: mostra os valores padrão; o salvamento aborta
                // (não sobrescreve um arquivo possivelmente recuperável).
            }

            if (configuracao != null)
            {
                txtNome.Text = configuracao.Nome ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(configuracao.CorPrimaria))
                {
                    try
                    {
                        Color cor = ColorTranslator.FromHtml(configuracao.CorPrimaria);
                        if (!cor.IsEmpty)
                            corSelecionada = cor;
                    }
                    catch
                    {
                        // cor inválida no arquivo: mantém a padrão
                    }
                }
            }

            AtualizarPreviewCor();
        }

        private void btnEscolherCor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = corSelecionada;
                dialog.FullOpen = true;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    corSelecionada = dialog.Color;
                    AtualizarPreviewCor();
                }
            }
        }

        private void AtualizarPreviewCor()
        {
            pnlCorPreview.BackColor = corSelecionada;
            lblCorHex.Text = ColorTranslator.ToHtml(corSelecionada);
            pnlCorPreview.Invalidate();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = (txtNome.Text ?? string.Empty).Trim();

            if (nome.Length == 0)
            {
                MessageBox.Show("Informe o nome da instituição.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (nome.Length > TamanhoMaximoNome)
            {
                MessageBox.Show(string.Format("O nome da instituição pode ter no máximo {0} caracteres.",
                    TamanhoMaximoNome), "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (Regex.IsMatch(nome, @"[\x00-\x1F\x7F]"))
            {
                MessageBox.Show("O nome da instituição contém caracteres inválidos.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            InstituicaoConfiguracao configuracao;
            try
            {
                configuracao = InstituicaoConfiguracaoStore.Carregar();
            }
            catch
            {
                MessageBox.Show(
                    "Não foi possível ler a configuração atual (arquivo ausente ou corrompido). " +
                    "Nada foi salvo para não sobrescrever o arquivo existente.",
                    "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (configuracao == null)
                configuracao = new InstituicaoConfiguracao();

            // Carregar devolve o objeto completo (inclui CodigoMestreHash/Salt);
            // alteramos somente os campos editáveis.
            configuracao.Nome = nome;
            configuracao.CorPrimaria = ColorTranslator.ToHtml(corSelecionada);

            try
            {
                InstituicaoConfiguracaoStore.Salvar(configuracao);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível salvar a configuração: " + ex.Message,
                    "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EventosGlobais.OnConfiguracaoSalva();
            MessageBox.Show("Configurações salvas com sucesso.", "Configurações",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
