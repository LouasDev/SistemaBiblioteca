using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Utils;
using System;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm : Form
    {
        private const int TamanhoMaximoNome = 150;

        private TextBox txtNome;
        private ComboBox cbTemas;
        private Panel pnlCorPreview;
        private Label lblCorHex;
        private Button btnSalvar;
        private Button btnCancelar;
        private Color corSelecionada = ThemeManager.DefaultColor;
        private string corOriginalHtml;
        private bool valoresCarregados;

        public ConfiguracaoForm()
        {
            InitializeComponent();
            ConstruirUi();
            CarregarValoresAtuais();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // Recarrega uma única vez já com o form parentado/tematizado pelo MDI.
            // Protege contra o caso em que a carga do construtor ficou com valores vazios.
            if (!valoresCarregados)
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

            cbTemas = new ComboBox
            {
                Location = new Point(24, 152),
                Size = new Size(240, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbTemas.SelectedIndexChanged += (s, e) =>
            {
                var preset = cbTemas.SelectedItem as ThemePreset;
                if (preset != null)
                {
                    corSelecionada = preset.PrimaryColor;
                    AtualizarPreviewCor();
                }
            };

            pnlCorPreview = new Panel
            {
                Location = new Point(24, 196),
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

            lblCorHex = new Label
            {
                AutoSize = true,
                Location = new Point(216, 205),
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
            Controls.Add(cbTemas);
            Controls.Add(pnlCorPreview);
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

            txtNome.Text = configuracao != null ? (configuracao.Nome ?? string.Empty) : string.Empty;
            corOriginalHtml = configuracao != null ? configuracao.CorPrimaria : null;

            corSelecionada = ThemeManager.DefaultColor;
            if (!string.IsNullOrWhiteSpace(corOriginalHtml))
            {
                try
                {
                    Color cor = ColorTranslator.FromHtml(corOriginalHtml);
                    if (!cor.IsEmpty)
                        corSelecionada = cor;
                }
                catch
                {
                    // cor inválida no arquivo: mantém a padrão
                }
            }

            // Cores predefinidas — mesma lista usada no PrimeiroUsoForm (fonte única: ThemeManager.Presets).
            cbTemas.Items.Clear();
            foreach (ThemePreset preset in ThemeManager.Presets)
                cbTemas.Items.Add(preset);

            ThemePreset selecionado = null;
            foreach (ThemePreset preset in ThemeManager.Presets)
            {
                if (string.Equals(ColorTranslator.ToHtml(preset.PrimaryColor),
                        ColorTranslator.ToHtml(corSelecionada), StringComparison.OrdinalIgnoreCase))
                {
                    selecionado = preset;
                    break;
                }
            }

            if (selecionado == null)
            {
                // Config salva antes desta mudança (cor personalizada):
                // exibe como "Atual (personalizada)" e não permite criar novas.
                selecionado = new ThemePreset("Atual (personalizada)",
                    corSelecionada, corSelecionada, corSelecionada);
                cbTemas.Items.Add(selecionado);
            }

            cbTemas.SelectedItem = selecionado;
            AtualizarPreviewCor();
            valoresCarregados = true;
        }

        private void AtualizarPreviewCor()
        {
            pnlCorPreview.BackColor = corSelecionada;
            lblCorHex.Text = ColorTranslator.ToHtml(corSelecionada);
            pnlCorPreview.Invalidate();
        }

        private bool ConfirmarSenhaUsuarioLogado()
        {
            if (!Sessao.UsuarioAutenticado)
            {
                MessageBox.Show("Nenhum usuário está logado.", "Configurações",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (var passwordForm = new PasswordForm())
            {
                passwordForm.Titulo = "Confirmação de senha";
                passwordForm.Mensagem = "Digite sua senha para confirmar a alteração:";
                if (passwordForm.ShowDialog(this) != DialogResult.OK)
                    return false;

                try
                {
                    using (var conexao = Conexao.ObterConexao())
                    {
                        conexao.Open();
                        using (var comando = new SqlCeCommand(
                            "SELECT Senha_Hash, Senha_Salt FROM usuarios WHERE Id = @id", conexao))
                        {
                            comando.Parameters.AddWithValue("@id", Sessao.UsuarioId.Value);
                            using (var leitor = comando.ExecuteReader())
                            {
                                if (leitor.Read() && CriptografiaSenha.VerificarSenha(
                                        passwordForm.SenhaDigitada,
                                        leitor["Senha_Hash"].ToString(),
                                        leitor["Senha_Salt"].ToString()))
                                    return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar a senha: " + ex.Message, "Configurações",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            MessageBox.Show("Senha incorreta. Nada foi salvo.", "Configurações",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
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

            ThemePreset presetSelecionado = cbTemas.SelectedItem as ThemePreset;
            if (presetSelecionado == null)
            {
                MessageBox.Show("Selecione uma das cores predefinidas.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // A cor salva só pode ser: uma das predefinidas, ou a cor atual já gravada
            // (entrada "Atual (personalizada)"). Nenhum outro valor é aceito.
            bool ehPredefinida = ThemeManager.Presets.Contains(presetSelecionado);
            string corHtml;
            if (ehPredefinida)
            {
                corHtml = ColorTranslator.ToHtml(presetSelecionado.PrimaryColor);
            }
            else
            {
                bool corIgualAAtual =
                    string.Equals(ColorTranslator.ToHtml(corSelecionada),
                        ColorTranslator.ToHtml(ThemeManager.Normalize(
                            string.IsNullOrWhiteSpace(corOriginalHtml)
                                ? ThemeManager.DefaultColor
                                : ColorTranslator.FromHtml(corOriginalHtml))),
                        StringComparison.OrdinalIgnoreCase);
                if (!corIgualAAtual)
                {
                    MessageBox.Show("Selecione uma das cores predefinidas.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                corHtml = corOriginalHtml;
            }

            if (!ConfirmarSenhaUsuarioLogado())
                return;

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
            configuracao.CorPrimaria = corHtml;

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

            MessageBox.Show("Configurações salvas com sucesso.", "Configurações",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Fecha antes de avisar o MainForm: o FormClosed zera o campo no MainForm
            // e o evento então abre o dashboard sem depender deste form.
            Close();
            EventosGlobais.OnConfiguracaoSalva();
        }
    }
}
