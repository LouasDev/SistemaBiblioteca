using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm : Form
    {
        private const int TamanhoMaximoNome = 150;
        private Color corSelecionada = ThemeManager.DefaultColor;
        private string corOriginalHtml;
        private string nomeOriginal;
        private ModoAparencia aparenciaOriginal;
        private bool valoresCarregados;
        private readonly Dictionary<Control, Rectangle> _designerBounds = new Dictionary<Control, Rectangle>();
        private Size _designerClientSize;
        private void CapturarLayoutDesigner(Control container)
        {
            foreach (Control controle in container.Controls)
            {
                _designerBounds[controle] = controle.Bounds;
                CapturarLayoutDesigner(controle);
            }
        }

        private bool operacaoEmAndamento;
        private readonly Font fonteRotuloCompacto = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        private readonly Font fonteStatusCompacto = new Font("Segoe UI", 10F);
        public ConfiguracaoForm()
        {
            InitializeComponent();
            // Captura as medidas definidas no Designer.
            _designerClientSize = ClientSize;
            CapturarLayoutDesigner(this);
            // Atualiza o botão Salvar quando algum campo é alterado.
            c.TextChanged += (sender, args) => AtualizarEstadoSalvar();
            cbAparencia.SelectedIndexChanged += (sender, args) => AtualizarEstadoSalvar();
        }

        private void btnReverTutorial_Click(object sender, EventArgs e)
        {
            if (!operacaoEmAndamento)
                EventosGlobais.OnTutorialSolicitado();
        }

        private void linkSuporteConfig_Click(object sender, EventArgs e)
        {
            FormHelper.AbrirSuporte("subgvsuporte@gmail.com");
        }

      

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Recarrega uma única vez já com o form parentado/tematizado pelo MDI.
            // Nenhum acesso a arquivo/banco acontece no construtor nem no InitializeComponent.
            if (!valoresCarregados)
                CarregarValoresAtuais();
            // OnShown dispara apenas na primeira exibição; a versão do executável
            // não muda durante a vida do processo.
            lblVersaoAtual.Text = "Versão atual: " + AtualizacaoService.VersaoAtual();
            CarregarNovidadesInstaladas();
            AjustarLayout();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Cancela o download em andamento de forma segura (o serviço apaga o .part).
            // Também cobre o fechamento do MainForm no logout, que fecha os filhos MDI.
            CancelarDownloadSeNecessario();
            base.OnFormClosing(e);
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

            c.Text = configuracao != null ? (configuracao.Nome ?? string.Empty) : string.Empty;
            nomeOriginal = (c.Text ?? string.Empty).Trim();
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
                if (string.Equals(ColorTranslator.ToHtml(preset.PrimaryColor), ColorTranslator.ToHtml(corSelecionada), StringComparison.OrdinalIgnoreCase))
                {
                    selecionado = preset;
                    break;
                }
            }

            if (selecionado == null)
            {
                // Config salva antes desta mudança (cor personalizada):
                // exibe como "Atual (personalizada)" e não permite criar novas.
                selecionado = new ThemePreset("Atual (personalizada)", corSelecionada, corSelecionada, corSelecionada);
                cbTemas.Items.Add(selecionado);
            }

            cbTemas.SelectedItem = selecionado;
            AtualizarPreviewCor();
            cbAparencia.Items.Clear();
            cbAparencia.Items.Add("Claro (Padrão)");
            cbAparencia.Items.Add("Escuro");
            cbAparencia.Items.Add("Sistema");
            aparenciaOriginal = ThemeManager.ObterModoAparencia(configuracao);
            cbAparencia.SelectedIndex = (int)aparenciaOriginal;

            valoresCarregados = true;
            AtualizarEstadoSalvar();
        }

        private void cbTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var preset = cbTemas.SelectedItem as ThemePreset;
            if (preset != null)
            {
                corSelecionada = preset.PrimaryColor;
                AtualizarPreviewCor();
                AtualizarEstadoSalvar();
            }
        }

        private void AtualizarPreviewCor()
        {
            pnlCorPreview.BackColor = corSelecionada;
            pnlCorPreview.Invalidate();
        }

        private Color ObterCorTemaSalva()
        {
            if (!string.IsNullOrWhiteSpace(corOriginalHtml))
            {
                try
                {
                    return ThemeManager.Normalize(ColorTranslator.FromHtml(corOriginalHtml));
                }
                catch
                {
                // Usa a cor padrão quando a configuração persistida for inválida.
                }
            }

            return ThemeManager.DefaultColor;
        }

        private bool HaAlteracoesPendentes()
        {
            if (!valoresCarregados)
                return false;
            bool nomeAlterado = !string.Equals((c.Text ?? string.Empty).Trim(), nomeOriginal ?? string.Empty, StringComparison.Ordinal);
            bool corAlterada = corSelecionada.ToArgb() != ObterCorTemaSalva().ToArgb();
            bool aparenciaAlterada = cbAparencia.SelectedIndex != (int)aparenciaOriginal;

            return nomeAlterado || corAlterada || aparenciaAlterada;
        }

        private void AtualizarEstadoSalvar()
        {
            bool habilitado = !operacaoEmAndamento && HaAlteracoesPendentes();
            btnSalvar.Enabled = habilitado;
            btnSalvar.Cursor = habilitado ? Cursors.Hand : Cursors.Default;
        }

        private bool ConfirmarSenhaUsuarioLogado(string mensagem = "Digite sua senha para confirmar a alteração:")
        {
            if (!Sessao.UsuarioAutenticado)
            {
                MessageBox.Show("Nenhum usuário está logado.", "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            using (var passwordForm = new PasswordForm())
            {
                passwordForm.Titulo = "Confirmação de senha";
                passwordForm.Mensagem = mensagem;
                if (passwordForm.ShowDialog(this) != DialogResult.OK)
                    return false;
                try
                {
                    using (var conexao = Conexao.ObterConexao())
                    {
                        conexao.Open();
                        using (var comando = new SqlCeCommand("SELECT Senha_Hash, Senha_Salt FROM usuarios WHERE Id = @id", conexao))
                        {
                            comando.Parameters.AddWithValue("@id", Sessao.UsuarioId.Value);
                            using (var leitor = comando.ExecuteReader())
                            {
                                if (leitor.Read() && CriptografiaSenha.VerificarSenha(passwordForm.SenhaDigitada, leitor["Senha_Hash"].ToString(), leitor["Senha_Salt"].ToString()))
                                    return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao verificar a senha: " + ex.Message, "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            MessageBox.Show("Senha incorreta. Nada foi salvo.", "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!HaAlteracoesPendentes())
            {
                AtualizarEstadoSalvar();
                return;
            }

            string nome = (c.Text ?? string.Empty).Trim();
            if (nome.Length == 0)
            {
                MessageBox.Show("Informe o nome da instituição.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
                return;
            }

            if (nome.Length > TamanhoMaximoNome)
            {
                MessageBox.Show(string.Format("O nome da instituição pode ter no máximo {0} caracteres.", TamanhoMaximoNome), "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
                return;
            }

            if (Regex.IsMatch(nome, @"[\x00-\x1F\x7F]"))
            {
                MessageBox.Show("O nome da instituição contém caracteres inválidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
                return;
            }

            ThemePreset presetSelecionado = cbTemas.SelectedItem as ThemePreset;
            if (presetSelecionado == null)
            {
                MessageBox.Show("Selecione uma das cores predefinidas.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                bool corIgualAAtual = string.Equals(ColorTranslator.ToHtml(corSelecionada), ColorTranslator.ToHtml(ThemeManager.Normalize(string.IsNullOrWhiteSpace(corOriginalHtml) ? ThemeManager.DefaultColor : ColorTranslator.FromHtml(corOriginalHtml))), StringComparison.OrdinalIgnoreCase);
                if (!corIgualAAtual)
                {
                    MessageBox.Show("Selecione uma das cores predefinidas.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Não foi possível ler a configuração atual (arquivo ausente ou corrompido). " + "Nada foi salvo para não sobrescrever o arquivo existente.", "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (configuracao == null)
                configuracao = new InstituicaoConfiguracao();
            // Carregar devolve o objeto completo (inclui CodigoMestreHash/Salt);
            // alteramos somente os campos editáveis.
            configuracao.Nome = nome;
            configuracao.CorPrimaria = corHtml;
            configuracao.Aparencia = (ModoAparencia)cbAparencia.SelectedIndex;
            // Mantém a chave antiga para leitores de versões anteriores do SUB.
            configuracao.TemaEscuro = ThemeManager.ResolverModoEscuro(configuracao.Aparencia.Value);

            try
            {
                InstituicaoConfiguracaoStore.Salvar(configuracao);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível salvar a configuração: " + ex.Message, "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Configurações salvas com sucesso.", "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Fecha antes de avisar o MainForm: o FormClosed zera o campo no MainForm
            // e o evento então abre o dashboard sem depender deste form.
            Close();
            EventosGlobais.OnConfiguracaoSalva();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfiguracaoForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && !IsDisposed)
                AjustarLayout();
        }

        private void AjustarLayout()
        {
            if (WindowState == FormWindowState.Minimized || _designerBounds.Count == 0)
                return;
            SuspendLayout();
            try
            {
                bool compacto = ClientSize.Height < 600;
                Rectangle inst = _designerBounds[grpInstituicao];
                Rectangle atual = _designerBounds[grpAtualizacao];
                Rectangle manut = _designerBounds[grpManutencao];
                Rectangle salvar = _designerBounds[btnSalvar];
                int margemEsquerda = inst.Left;
                int margemDireita = _designerClientSize.Width - inst.Right;
                int largura = Math.Max(360, ClientSize.Width - margemEsquerda - margemDireita);
                int diferenca = largura - inst.Width;
                int metade = diferenca / 2;
                // Cabeçalho
                lblSubtitulo.Visible = !compacto;
                pnlLinhaCabecalho.Visible = !compacto;
                // Grupo: Instituição e aparência
                grpInstituicao.MaximumSize = Size.Empty;
                int topoInstituicao = compacto ? Math.Max(16, inst.Top - 38) : inst.Top;
                grpInstituicao.SetBounds(inst.Left, topoInstituicao, largura, inst.Height);
                // Nome da instituição
                Rectangle nome = _designerBounds[c];
                c.SetBounds(nome.X, nome.Y, Math.Max(150, nome.Width + metade), nome.Height);
                // Cor principal
                Rectangle rotuloCor = _designerBounds[lblCor];
                lblCor.Location = new Point(rotuloCor.X + metade, rotuloCor.Y);
                Rectangle temas = _designerBounds[cbTemas];
                cbTemas.SetBounds(temas.X + metade, temas.Y, Math.Max(150, temas.Width + metade), temas.Height);
                // Pré-visualização da cor
                Rectangle preview = _designerBounds[pnlCorPreview];
                pnlCorPreview.SetBounds(preview.X + diferenca, preview.Y, preview.Width, preview.Height);
                // Campo de aparência
                Rectangle aparencia = _designerBounds[cbAparencia];
                cbAparencia.SetBounds(aparencia.X, aparencia.Y, Math.Max(150, aparencia.Width + metade), aparencia.Height);
                // Grupo: Atualização
                grpAtualizacao.MaximumSize = Size.Empty;
                int deslocamentoVertical = topoInstituicao - inst.Top;
                int topoAtualizacao = Math.Max(atual.Top + deslocamentoVertical, grpInstituicao.Bottom + 12);
                grpAtualizacao.SetBounds(atual.Left, topoAtualizacao, largura, atual.Height);
                // Texto de status
                Rectangle status = _designerBounds[lblStatusAtualizacao];
                lblStatusAtualizacao.SetBounds(status.X, status.Y, Math.Max(200, status.Width + diferenca), status.Height);
                // Barras de progresso
                Rectangle verificando = _designerBounds[pbVerificando];
                pbVerificando.SetBounds(verificando.X, verificando.Y, Math.Max(200, verificando.Width + diferenca), verificando.Height);
                Rectangle download = _designerBounds[pbDownload];
                pbDownload.SetBounds(download.X, download.Y, Math.Max(200, download.Width + diferenca), download.Height);
                // Botão Salvar: preserva distância das bordas do Designer
                int distanciaDireita = _designerClientSize.Width - salvar.Right;
                int distanciaInferior = _designerClientSize.Height - salvar.Bottom;
                btnSalvar.SetBounds(ClientSize.Width - distanciaDireita - salvar.Width, ClientSize.Height - distanciaInferior - salvar.Height, salvar.Width, salvar.Height);
                // Grupo: Manutenção
                int topoManutencao = Math.Max(manut.Top + deslocamentoVertical, grpAtualizacao.Bottom + 12);
                int larguraManutencao = largura;
                if (topoManutencao + manut.Height + 12 > btnSalvar.Top)
                {
                    larguraManutencao = Math.Max(360, largura - salvar.Width - 12);
                }

                grpManutencao.SetBounds(manut.Left, topoManutencao, larguraManutencao, manut.Height);
            }
            finally
            {
                ResumeLayout(false);
            }
        }
    }
}
