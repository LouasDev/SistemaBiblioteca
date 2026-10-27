using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Utils;
using System;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm : Form
    {
        private const int TamanhoMaximoNome = 150;
        private const int TimeoutVerificacaoSegundos = 20;
        private const int TimeoutDownloadSegundos = 600;
        internal const int LimiteNotasCaracteres = 4000;

        private Color corSelecionada = ThemeManager.DefaultColor;
        private string corOriginalHtml;
        private bool valoresCarregados;

        private AtualizacaoManifesto manifestoDisponivel;
        private AtualizacaoManifesto notasDisponiveis;
        private CancellationTokenSource ctsDownload;
        private bool downloadEmAndamento;
        private bool operacaoEmAndamento;
        private readonly Font fonteRotuloCompacto = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
        private readonly Font fonteStatusCompacto = new Font("Segoe UI", 10F);

        public ConfiguracaoForm()
        {
            InitializeComponent();
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

        #region Instituição (nome e cor)

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

        private void cbTemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var preset = cbTemas.SelectedItem as ThemePreset;
            if (preset != null)
            {
                corSelecionada = preset.PrimaryColor;
                AtualizarPreviewCor();
            }
        }



        private void AtualizarPreviewCor()
        {
            pnlCorPreview.BackColor = corSelecionada;
            pnlCorPreview.Invalidate();
        }

        private bool ConfirmarSenhaUsuarioLogado(string mensagem = "Digite sua senha para confirmar a alteração:")
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
                passwordForm.Mensagem = mensagem;
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
            string nome = (c.Text ?? string.Empty).Trim();

            if (nome.Length == 0)
            {
                MessageBox.Show("Informe o nome da instituição.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
                return;
            }

            if (nome.Length > TamanhoMaximoNome)
            {
                MessageBox.Show(string.Format("O nome da instituição pode ter no máximo {0} caracteres.",
                    TamanhoMaximoNome), "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
                return;
            }

            if (Regex.IsMatch(nome, @"[\x00-\x1F\x7F]"))
            {
                MessageBox.Show("O nome da instituição contém caracteres inválidos.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                c.Focus();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void btnApagarDados_Click(object sender, EventArgs e)
        {
            if (operacaoEmAndamento)
                return;
            if (!Sessao.UsuarioAutenticado || Sessao.Perfil != "Bibliotecário(a)")
            {
                MessageBox.Show("Entre com uma conta de bibliotecário para resetar o aplicativo.", "Reset",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DefinirModoOperacao(true);
            try
            {
                string codigo;
                using (var dialogo = new ResetTermoForm
                {
                    Resumo = ResetService.ObterResumo()
                })
                {
                    dialogo.AplicarTema(corSelecionada);
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;
                    codigo = dialogo.CodigoMestre;
                }
                string senha;
                using (var dialogo = new PasswordForm
                {
                    Titulo = "Reset: etapa 2 de 2",
                    Mensagem = "TXT validado. Digite a senha da bibliotecária logada."
                })
                {
                    ThemeManager.Apply(dialogo, corSelecionada);
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;
                    senha = dialogo.SenhaDigitada;
                }
                ResetService.Autorizacao autorizacao;
                string erro;
                if (!ResetService.TentarAutorizar(codigo, senha, out autorizacao, out erro))
                {
                    MessageBox.Show(erro, "Reset não autorizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show(
                    "As duas verificações foram concluídas.\n\n" +
                    "Apagar definitivamente todos os dados locais e voltar ao primeiro uso?\n\n" +
                    "Os backups locais também serão apagados. Esta ação não pode ser desfeita.",
                    "Confirmar reset definitivo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                ResetService.Iniciar(autorizacao);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível iniciar o reset.\n\n" + ex.Message, "Reset",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    DefinirModoOperacao(false);
            }
        }

        #region Atualização

        private void CarregarNovidadesInstaladas()
        {
            notasDisponiveis = new NovidadesAtualizacaoStore(AppPaths.UpdateFolder)
                .ObterDaVersao(AtualizacaoService.VersaoAtual())?.Manifesto;
            btnNovidades.Enabled = notasDisponiveis != null;
        }

        private void btnNovidades_Click(object sender, EventArgs e)
        {
            if (operacaoEmAndamento || notasDisponiveis == null)
                return;
            using (var novidades = new NovidadesAtualizacaoForm())
            {
                novidades.CarregarNovidades(notasDisponiveis);
                novidades.AplicarTema(corSelecionada);
                novidades.ShowDialog(this);
            }
        }

        private async void btnVerificar_Click(object sender, EventArgs e)
        {
            if (downloadEmAndamento)
                return;

            manifestoDisponivel = null;
            btnAtualizar.Visible = false;
            CarregarNovidadesInstaladas();

            DefinirModoOperacao(true);
            pbVerificando.Visible = true;
            lblStatusAtualizacao.Text = "Verificando atualizações...";

            try
            {
                ResultadoVerificacaoAtualizacao resultado =
                    await AtualizacaoService.VerificarAsync(null, TimeoutVerificacaoSegundos);

                if (IsDisposed)
                    return;

                ApresentarResultadoVerificacao(resultado);
                if (!resultado.Sucesso || !resultado.AtualizacaoDisponivel)
                    RegistrarLog("verificar: " + resultado.Mensagem);
            }
            catch (Exception ex)
            {
                RegistrarLog("verificar: " + ex.Message + " | " + ex.StackTrace);
                if (!IsDisposed)
                    lblStatusAtualizacao.Text = "Não foi possível verificar atualizações. Tente novamente.";
            }
            finally
            {
                if (!IsDisposed)
                {
                    pbVerificando.Visible = false;
                    DefinirModoOperacao(false);
                }
            }
        }

        internal void ApresentarResultadoVerificacao(ResultadoVerificacaoAtualizacao resultado)
        {
            manifestoDisponivel = resultado.Sucesso && resultado.AtualizacaoDisponivel
                ? resultado.Manifesto : null;
            if (resultado.Sucesso && resultado.Manifesto != null)
                notasDisponiveis = resultado.Manifesto;
            btnNovidades.Enabled = notasDisponiveis != null;
            btnAtualizar.Visible = manifestoDisponivel != null;
            lblStatusAtualizacao.Text = manifestoDisponivel != null
                ? MontarMensagemVersaoNova(resultado) : resultado.Mensagem;
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (downloadEmAndamento)
                return;

            if (manifestoDisponivel == null)
            {
                MessageBox.Show(
                    "Nenhuma versão nova foi identificada. Clique em Verificar atualizações primeiro.",
                    "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var novidades = new NovidadesAtualizacaoForm())
            {
                novidades.CarregarNovidades(manifestoDisponivel, permitirAtualizacao: true);
                novidades.AplicarTema(corSelecionada);
                if (novidades.ShowDialog(this) != DialogResult.OK)
                    return;
            }

            if (!ConfirmarSenhaUsuarioLogado("Digite sua senha para confirmar a atualização do SUB:"))
                return;

            var confirmacao = MessageBox.Show(
                "O SUB será fechado e abrirá sozinho para aplicar a versão " +
                manifestoDisponivel.Versao + ".\n\n" +
                "Os dados (banco e configurações) serão preservados com backup automático.\n\n" +
                "Deseja continuar?",
                "Atualização", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacao != DialogResult.Yes)
                return;

            if (!AtualizacaoAplicador.PastaInstalacaoGravavel())
            {
                MessageBox.Show(
                    "A pasta do programa não permite gravação. A atualização não pode ser aplicada aqui.",
                    "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(AtualizacaoAplicador.CaminhoUpdaterInstalado))
            {
                MessageBox.Show(
                    "O atualizador (SUB-UPDATER.exe) não foi encontrado na pasta do programa.",
                    "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            downloadEmAndamento = true;
            DefinirModoOperacao(true);
            btnAtualizar.Visible = false;
            btnCancelarDownload.Visible = true;
            btnCancelarDownload.Enabled = true;
            pbDownload.Visible = true;
            pbDownload.Style = ProgressBarStyle.Marquee;
            pbDownload.Value = 0;
            lblStatusAtualizacao.Text = "Baixando a atualização...";

            ctsDownload = new CancellationTokenSource();
            var progresso = new Progress<ProgressoDownload>(p =>
            {
                if (IsDisposed)
                    return;

                int percentual = CalcularPercentual(p.BytesBaixados, p.TotalBytes);
                if (pbDownload.Style != ProgressBarStyle.Blocks)
                    pbDownload.Style = ProgressBarStyle.Blocks;
                pbDownload.Value = percentual;
                lblStatusAtualizacao.Text = "Baixando a atualização... " + percentual + "%";
            });

            try
            {
                ResultadoDownloadPacote resultado = await AtualizacaoService.BaixarPacoteAsync(
                    manifestoDisponivel, progresso, ctsDownload.Token, TimeoutDownloadSegundos);

                if (IsDisposed)
                    return;

                if (resultado.Cancelado)
                {
                    lblStatusAtualizacao.Text = "Download cancelado. Nada foi aplicado.";
                    return;
                }

                if (!resultado.Sucesso)
                {
                    RegistrarLog("download: " + resultado.Mensagem);
                    lblStatusAtualizacao.Text = resultado.Mensagem;
                    return;
                }

                pbDownload.Style = ProgressBarStyle.Blocks;
                pbDownload.Value = 100;
                lblStatusAtualizacao.Text = "Validando o pacote baixado...";

                // Revalida tamanho e SHA-256 do arquivo em disco antes de aplicar.
                string erro = AtualizacaoAplicador.ValidarPacoteParaAplicar(
                    resultado.CaminhoArquivo, manifestoDisponivel);
                if (erro != null)
                {
                    MessageBox.Show(erro, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusAtualizacao.Text = "O pacote baixado não pôde ser aplicado.";
                    return;
                }

                lblStatusAtualizacao.Text = "Fazendo backup dos dados...";
                erro = AtualizacaoAplicador.FazerBackupDados();
                if (erro != null)
                {
                    MessageBox.Show(erro + "\n\nA atualização não será aplicada sem backup.",
                        "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusAtualizacao.Text = "Falha no backup dos dados. Atualização não aplicada.";
                    return;
                }

                var aplicar = MessageBox.Show(
                    "Download e backup concluídos.\n\nO SUB fechará agora e reabrirá sozinho na versão " +
                    manifestoDisponivel.Versao + ".\n\nDeseja aplicar a atualização agora?",
                    "Atualização", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (aplicar != DialogResult.Yes)
                {
                    lblStatusAtualizacao.Text =
                        "Atualização não aplicada. O pacote continua disponível para aplicar depois.";
                    btnAtualizar.Visible = true;
                    return;
                }

                // As notas ficam locais para abrir uma vez após entrar na versão instalada.
                new NovidadesAtualizacaoStore(AppPaths.UpdateFolder).Preparar(manifestoDisponivel);
                erro = AtualizacaoAplicador.IniciarUpdater(manifestoDisponivel, resultado.CaminhoArquivo);
                if (erro != null)
                {
                    MessageBox.Show(erro, "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusAtualizacao.Text = "Falha ao iniciar o atualizador.";
                    btnAtualizar.Visible = true;
                    return;
                }

                lblStatusAtualizacao.Text = "Atualização em andamento. O SUB fechará em instantes...";
                Application.Exit();
            }
            catch (Exception ex)
            {
                RegistrarLog("atualizar: " + ex.Message + " | " + ex.StackTrace);
                if (!IsDisposed)
                {
                    MessageBox.Show("Falha inesperada durante a atualização. Tente novamente.",
                        "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusAtualizacao.Text = "Falha inesperada durante a atualização.";
                    btnAtualizar.Visible = manifestoDisponivel != null;
                }
            }
            finally
            {
                if (ctsDownload != null)
                {
                    ctsDownload.Dispose();
                    ctsDownload = null;
                }

                downloadEmAndamento = false;

                if (!IsDisposed)
                {
                    pbDownload.Visible = false;
                    pbDownload.Style = ProgressBarStyle.Blocks;
                    pbDownload.Value = 0;
                    btnCancelarDownload.Visible = false;
                    btnCancelarDownload.Enabled = true;
                    btnAtualizar.Visible = manifestoDisponivel != null;
                    DefinirModoOperacao(false);
                }
            }
        }

        private void btnCancelarDownload_Click(object sender, EventArgs e)
        {
            if (!downloadEmAndamento || ctsDownload == null)
                return;

            btnCancelarDownload.Enabled = false;
            lblStatusAtualizacao.Text = "Cancelando o download...";
            CancelarDownloadSeNecessario();
        }

        private void CancelarDownloadSeNecessario()
        {
            try
            {
                if (ctsDownload != null)
                    ctsDownload.Cancel();
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void DefinirModoOperacao(bool ocupado)
        {
            operacaoEmAndamento = ocupado;
            btnApagarDados.Enabled = !ocupado;
            btnVerificar.Enabled = !ocupado;
            btnAtualizar.Enabled = !ocupado;
            btnNovidades.Enabled = !ocupado && notasDisponiveis != null;
            btnSalvar.Enabled = !ocupado;
            c.Enabled = !ocupado;
            cbTemas.Enabled = !ocupado;
        }

        internal static string MontarMensagemVersaoNova(ResultadoVerificacaoAtualizacao resultado)
        {
            var manifesto = resultado != null ? resultado.Manifesto : null;
            if (manifesto == null)
                return "Nova versão disponível.";

            var mensagem = "Nova versão disponível: " + (manifesto.Versao ?? "?").Trim() + ".";
            if (!string.IsNullOrWhiteSpace(manifesto.Data))
                mensagem += " Publicada em " + manifesto.Data.Trim() + ".";
            return mensagem;
        }

        internal static string TruncarNotas(string notas)
        {
            if (string.IsNullOrEmpty(notas))
                return string.Empty;

            if (notas.Length > LimiteNotasCaracteres)
                return notas.Substring(0, LimiteNotasCaracteres) + Environment.NewLine + "[notas truncadas]";

            return notas;
        }

        internal static int CalcularPercentual(long baixados, long total)
        {
            if (total <= 0 || baixados <= 0)
                return 0;
            if (baixados >= total)
                return 100;
            return (int)(baixados * 100L / total);
        }

        private void RegistrarLog(string mensagem)
        {
            try
            {
                var pasta = Path.Combine(Application.StartupPath, "logs");
                Directory.CreateDirectory(pasta);
                File.AppendAllText(
                    Path.Combine(pasta, "configuracao_atualizacao.log"),
                    DateTime.Now + " - " + mensagem + Environment.NewLine);
            }
            catch
            {
                // log é melhor esforço; nunca derruba a interface
            }
        }

        #endregion

        private void ConfiguracaoForm_SizeChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated && !IsDisposed)
                AjustarLayout();
        }

        private void AjustarLayout()
        {
            if (WindowState == FormWindowState.Minimized)
                return;
            SuspendLayout();
            bool compacto = ClientSize.Height < 600;
            int margem = 20;
            int largura = Math.Max(360, ClientSize.Width - margem * 2);
            lblSubtitulo.Visible = !compacto;
            pnlLinhaCabecalho.Visible = !compacto;
            grpAtualizacao.MaximumSize = Size.Empty;
            grpInstituicao.MaximumSize = Size.Empty;
            grpInstituicao.SetBounds(margem, compacto ? 68 : 106, largura, 112);
            lblNome.Font = fonteRotuloCompacto;
            lblNome.Location = new Point(16, 25);
            c.SetBounds(16, 54, largura / 2 - 30, 34);
            int colunaCor = largura / 2 + 6;
            lblCor.Text = "Cor principal:";
            lblCor.Font = fonteRotuloCompacto;
            lblCor.Location = new Point(colunaCor, 25);
            cbTemas.SetBounds(colunaCor, 54, largura - colunaCor - 66, 34);
            pnlCorPreview.SetBounds(largura - 50, 54, 34, 34);
            int topoAtualizacao = grpInstituicao.Bottom + 12;
            int alturaAtualizacao = 184;
            grpAtualizacao.SetBounds(margem, topoAtualizacao, largura, alturaAtualizacao);
            lblVersaoAtual.Location = new Point(16, 23);
            lblVersaoAtual.Font = fonteRotuloCompacto;
            lblStatusAtualizacao.AutoSize = false;
            lblStatusAtualizacao.MaximumSize = Size.Empty;
            lblStatusAtualizacao.Font = fonteStatusCompacto;
            lblStatusAtualizacao.SetBounds(16, 50, largura - 32, 38);
            btnVerificar.SetBounds(16, 96, 190, 34);
            btnNovidades.SetBounds(214, 96, 144, 34);
            btnAtualizar.SetBounds(366, 96, 160, 34);
            btnCancelarDownload.SetBounds(366, 96, 160, 34);
            pbVerificando.SetBounds(16, 142, largura - 32, 24);
            pbDownload.SetBounds(16, 142, largura - 32, 24);
            btnApagarDados.SetBounds(margem, ClientSize.Height - 52, 172, 36);
            btnSalvar.SetBounds(ClientSize.Width - margem - 124, ClientSize.Height - 52, 124, 36);
            ResumeLayout(false);
        }
    }
}
