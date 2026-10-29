using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm
    {
        private const int TimeoutVerificacaoSegundos = 20;
        private const int TimeoutDownloadSegundos = 600;
        internal const int LimiteNotasCaracteres = 4000;
        private AtualizacaoManifesto manifestoDisponivel;
        private AtualizacaoManifesto notasDisponiveis;
        private CancellationTokenSource ctsDownload;
        private bool downloadEmAndamento;
        private void CarregarNovidadesInstaladas()
        {
            notasDisponiveis = new NovidadesAtualizacaoStore(AppPaths.UpdateFolder).ObterDaVersao(AtualizacaoService.VersaoAtual())?.Manifesto;
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
                ResultadoVerificacaoAtualizacao resultado = await AtualizacaoService.VerificarAsync(null, TimeoutVerificacaoSegundos);
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
            manifestoDisponivel = resultado.Sucesso && resultado.AtualizacaoDisponivel ? resultado.Manifesto : null;
            if (resultado.Sucesso && resultado.Manifesto != null)
                notasDisponiveis = resultado.Manifesto;
            btnNovidades.Enabled = notasDisponiveis != null;
            btnAtualizar.Visible = manifestoDisponivel != null;
            lblStatusAtualizacao.Text = manifestoDisponivel != null ? MontarMensagemVersaoNova(resultado) : resultado.Mensagem;
        }

        private async void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (downloadEmAndamento)
                return;
            if (manifestoDisponivel == null)
            {
                MessageBox.Show("Nenhuma versão nova foi identificada. Clique em Verificar atualizações primeiro.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            var confirmacao = MessageBox.Show("O SUB será fechado e abrirá sozinho para aplicar a versão " + manifestoDisponivel.Versao + ".\n\n" + "Os dados (banco e configurações) serão preservados com backup automático.\n\n" + "Deseja continuar?", "Atualização", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacao != DialogResult.Yes)
                return;
            if (!AtualizacaoAplicador.PastaInstalacaoGravavel())
            {
                MessageBox.Show("A pasta do programa não permite gravação. A atualização não pode ser aplicada aqui.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(AtualizacaoAplicador.CaminhoUpdaterInstalado))
            {
                MessageBox.Show("O atualizador (SUB-UPDATER.exe) não foi encontrado na pasta do programa.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ResultadoDownloadPacote resultado = await AtualizacaoService.BaixarPacoteAsync(manifestoDisponivel, progresso, ctsDownload.Token, TimeoutDownloadSegundos);
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
                string erro = AtualizacaoAplicador.ValidarPacoteParaAplicar(resultado.CaminhoArquivo, manifestoDisponivel);
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
                    MessageBox.Show(erro + "\n\nA atualização não será aplicada sem backup.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatusAtualizacao.Text = "Falha no backup dos dados. Atualização não aplicada.";
                    return;
                }

                var aplicar = MessageBox.Show("Download e backup concluídos.\n\nO SUB fechará agora e reabrirá sozinho na versão " + manifestoDisponivel.Versao + ".\n\nDeseja aplicar a atualização agora?", "Atualização", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (aplicar != DialogResult.Yes)
                {
                    lblStatusAtualizacao.Text = "Atualização não aplicada. O pacote continua disponível para aplicar depois.";
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
                    MessageBox.Show("Falha inesperada durante a atualização. Tente novamente.", "Atualização", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            AtualizarEstadoSalvar();
            c.Enabled = !ocupado;
            cbTemas.Enabled = !ocupado;
            cbAparencia.Enabled = !ocupado;
            btnReverTutorial.Enabled = !ocupado;
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
                File.AppendAllText(Path.Combine(pasta, "configuracao_atualizacao.log"), DateTime.Now + " - " + mensagem + Environment.NewLine);
            }
            catch
            {
            // log é melhor esforço; nunca derruba a interface
            }
        }
    }
}
