using SUB.Models;
using SUB.Utils;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SUB.Forms.Setup
{
    [System.ComponentModel.DesignerCategory("Form")]
    public partial class PrimeiroUsoForm : Form
    {
        private readonly string _codigoMestreGerado = GerarCodigoMestre();
        private readonly Color _corErro = Color.FromArgb(184, 50, 50);
        private readonly Color _corSucesso = Color.FromArgb(31, 122, 73);
        private readonly Color _corNeutra = Color.FromArgb(105, 114, 126);
        private string _caminhoCodigoMestre;
        private int _etapaAtual = 1;
        private bool _concluindo;

        public PrimeiroUsoForm()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            _tema.SelectedIndexChanged += Tema_SelectedIndexChanged;
            foreach (var preset in ThemeManager.Presets)
                _tema.Items.Add(preset);

            _codigoMestre.Text = _codigoMestreGerado;
            ConectarValidacaoProgressiva();
            _tema.SelectedIndex = 0;
            MostrarEtapa(1);
            AtualizarEstadoConclusao();
        }

        private ThemePreset TemaSelecionado
        {
            get { return _tema.SelectedItem as ThemePreset ?? ThemeManager.Presets[0]; }
        }

        private void ConectarValidacaoProgressiva()
        {
            _nomeInstituicao.TextChanged += (s, e) => RevalidarSeVisivel(lblErroNomeInstituicao, ValidarNomeInstituicao);
            _nomeBibliotecaria.TextChanged += (s, e) => RevalidarSeVisivel(lblErroNomeBibliotecaria, ValidarNomeBibliotecaria);
            _emailBibliotecaria.TextChanged += (s, e) => RevalidarSeVisivel(lblErroEmailBibliotecaria, ValidarEmail);
            _telefoneBibliotecaria.TextChanged += (s, e) => RevalidarSeVisivel(lblErroTelefoneBibliotecaria, ValidarTelefone);
            _senhaBibliotecaria.TextChanged += (s, e) =>
            {
                RevalidarSeVisivel(lblErroSenhaBibliotecaria, ValidarSenha);
                RevalidarSeVisivel(lblErroConfirmarSenhaBibliotecaria, ValidarConfirmacaoSenha);
            };
            _confirmarSenhaBibliotecaria.TextChanged += (s, e) => RevalidarSeVisivel(lblErroConfirmarSenhaBibliotecaria, ValidarConfirmacaoSenha);
        }

        private void RevalidarSeVisivel(Label label, Func<string> validar)
        {
            if (!label.Visible)
                return;

            ExibirErro(label, validar());
            if (!TemErrosVisiveis())
                OcultarErroGeral();
        }

        private void Tema_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExibirErro(lblErroTema, ValidarTema());
            AtualizarVisualizacaoTema();
        }

        private void AtualizarVisualizacaoTema()
        {
            ThemePreset tema = TemaSelecionado;
            Color primaria = tema.PrimaryColor;
            Color secundaria = tema.SecondaryColor;
            Color borda = ThemeManager.Lighten(primaria, 0.42f);
            Color hover = ThemeManager.Lighten(primaria, 0.94f);

            BackColor = tema.AccentColor;
            pnlInstitucional.Gradientetop = primaria;
            pnlInstitucional.Gradientebottom = secundaria;
            pnlInstitucional.Invalidate();
            pnlCodigoRecuperacao.BackColor = tema.AccentColor;
            _visualizadorCor.BackColor = primaria;
            lblEtapaAtual.ForeColor = primaria;
            btnAvancar.BaseColor = primaria;
            btnVoltar.ForeColor = primaria;
            btnVoltar.FlatAppearance.MouseOverBackColor = tema.AccentColor;
            btnVoltar.FlatAppearance.MouseDownBackColor = ThemeManager.Lighten(primaria, 0.86f);
            exportarCodigo.ForeColor = primaria;
            exportarCodigo.FlatAppearance.BorderColor = primaria;
            exportarCodigo.FlatAppearance.MouseOverBackColor = tema.AccentColor;
            exportarCodigo.FlatAppearance.MouseDownBackColor = ThemeManager.Lighten(primaria, 0.86f);

            RoundedTextBox[] campos =
            {
                _nomeInstituicao,
                _nomeBibliotecaria,
                _emailBibliotecaria,
                _telefoneBibliotecaria,
                _senhaBibliotecaria,
                _confirmarSenhaBibliotecaria
            };

            foreach (RoundedTextBox campo in campos)
            {
                campo.BorderColor = borda;
                campo.BorderFocusColor = primaria;
                campo.HoverBackColor = hover;
                campo.Invalidate();
            }

            _tema.BorderColor = primaria;
            _tema.Invalidate();
            AtualizarIndicadorEtapa();
            AtualizarEstadoConclusao();
            SubLogoManager.SetImage(picLogo, SubLogoVariant.Square, primaria, pnlLogo.BackColor);
        }

        private void MostrarEtapa(int etapa)
        {
            _etapaAtual = etapa;
            bool primeira = etapa == 1;
            pnlEtapa1.Visible = primeira;
            pnlEtapa2.Visible = !primeira;
            pnlEtapa1.BringToFront();
            if (!primeira)
                pnlEtapa2.BringToFront();

            lblEtapaAtual.Text = primeira ? "Etapa 1 de 2" : "Etapa 2 de 2";
            AcceptButton = primeira ? (IButtonControl)btnAvancar : salvar;
            AtualizarIndicadorEtapa();
            OcultarErroGeral();

            if (primeira)
                _nomeInstituicao.Focus();
            else
                _nomeBibliotecaria.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData != Keys.Enter)
                return base.ProcessCmdKey(ref msg, keyData);

            if (_etapaAtual == 1)
            {
                if (_nomeInstituicao.ContainsFocus)
                {
                    _tema.Focus();
                    return true;
                }

                if (_tema.Focused && !_tema.DroppedDown)
                {
                    btnAvancar.PerformClick();
                    return true;
                }
            }
            else
            {
                if (_nomeBibliotecaria.ContainsFocus)
                {
                    _emailBibliotecaria.Focus();
                    return true;
                }

                if (_emailBibliotecaria.ContainsFocus)
                {
                    _telefoneBibliotecaria.Focus();
                    return true;
                }

                if (_telefoneBibliotecaria.ContainsFocus)
                {
                    _senhaBibliotecaria.Focus();
                    return true;
                }

                if (_senhaBibliotecaria.ContainsFocus)
                {
                    _confirmarSenhaBibliotecaria.Focus();
                    return true;
                }

                if (_confirmarSenhaBibliotecaria.ContainsFocus)
                {
                    exportarCodigo.Focus();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AtualizarIndicadorEtapa()
        {
            Color primaria = TemaSelecionado.PrimaryColor;
            indicadorEtapa1.BackColor = primaria;
            indicadorEtapa2.BackColor = _etapaAtual == 2 ? primaria : Color.FromArgb(222, 227, 234);

            bool primeiraEtapa = _etapaAtual == 1;
            lblPasso1Numero.BackColor = primeiraEtapa ? Color.White : ThemeManager.Lighten(primaria, 0.34f);
            lblPasso2Numero.BackColor = primeiraEtapa ? ThemeManager.Lighten(primaria, 0.34f) : Color.White;
            lblPasso1Numero.ForeColor = primeiraEtapa ? primaria : Color.White;
            lblPasso2Numero.ForeColor = primeiraEtapa ? Color.White : primaria;
            lblPasso1Texto.Font = new Font("Segoe UI", 10F,
                primeiraEtapa ? FontStyle.Bold : FontStyle.Regular);
            lblPasso2Texto.Font = new Font("Segoe UI", 10F,
                primeiraEtapa ? FontStyle.Regular : FontStyle.Bold);
            linhaRoteiro.BackColor = ThemeManager.Lighten(primaria, 0.42f);
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            if (!ValidarEtapa1())
                return;

            MostrarEtapa(2);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            MostrarEtapa(1);
        }

        private void chkMostrarSenhas_CheckedChanged(object sender, EventArgs e)
        {
            bool ocultar = !chkMostrarSenhas.Checked;
            _senhaBibliotecaria.UseSystemPasswordChar = ocultar;
            _confirmarSenhaBibliotecaria.UseSystemPasswordChar = ocultar;
        }

        private bool ValidarEtapa1()
        {
            string erroNome = ValidarNomeInstituicao();
            string erroTema = ValidarTema();
            ExibirErro(lblErroNomeInstituicao, erroNome);
            ExibirErro(lblErroTema, erroTema);

            if (erroNome == null && erroTema == null)
            {
                OcultarErroGeral();
                return true;
            }

            ExibirErroGeral("Revise os campos destacados antes de avançar.");
            if (erroNome != null)
                _nomeInstituicao.Focus();
            else
                _tema.Focus();
            return false;
        }

        private bool ValidarEtapa2()
        {
            string erroNome = ValidarNomeBibliotecaria();
            string erroEmail = ValidarEmail();
            string erroTelefone = ValidarTelefone();
            string erroSenha = ValidarSenha();
            string erroConfirmacao = ValidarConfirmacaoSenha();

            ExibirErro(lblErroNomeBibliotecaria, erroNome);
            ExibirErro(lblErroEmailBibliotecaria, erroEmail);
            ExibirErro(lblErroTelefoneBibliotecaria, erroTelefone);
            ExibirErro(lblErroSenhaBibliotecaria, erroSenha);
            ExibirErro(lblErroConfirmarSenhaBibliotecaria, erroConfirmacao);

            if (erroNome == null && erroEmail == null && erroTelefone == null && erroSenha == null && erroConfirmacao == null)
            {
                OcultarErroGeral();
                return true;
            }

            ExibirErroGeral("Revise os campos destacados antes de concluir.");
            if (erroNome != null) _nomeBibliotecaria.Focus();
            else if (erroEmail != null) _emailBibliotecaria.Focus();
            else if (erroTelefone != null) _telefoneBibliotecaria.Focus();
            else if (erroSenha != null) _senhaBibliotecaria.Focus();
            else _confirmarSenhaBibliotecaria.Focus();
            return false;
        }

        private string ValidarNomeInstituicao()
        {
            return string.IsNullOrWhiteSpace(_nomeInstituicao.Text) ? "Informe o nome da instituição." : null;
        }

        private string ValidarTema()
        {
            return _tema.SelectedItem is ThemePreset ? null : "Selecione um tema.";
        }

        private string ValidarNomeBibliotecaria()
        {
            string nome = (_nomeBibliotecaria.Text ?? string.Empty).Trim();
            if (nome.Length == 0)
                return "Informe o nome completo da bibliotecária.";

            string[] partes = nome.Split(
                new[] { ' ', '\t', '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);

            return partes.Length >= 2
                ? null
                : "Digite o nome e o sobrenome da bibliotecária.";
        }

        private string ValidarEmail()
        {
            string email = _emailBibliotecaria.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
                return "Informe o e-mail.";
            return EmailValido(email) ? null : "Informe um e-mail válido.";
        }

        private string ValidarTelefone()
        {
            string telefone = ApenasDigitos(_telefoneBibliotecaria.Text);
            if (telefone.Length == 0)
                return "Informe o telefone.";
            return telefone.Length == 10 || telefone.Length == 11 ? null : "Use DDD + 8 ou 9 dígitos.";
        }

        private string ValidarSenha()
        {
            string senha = _senhaBibliotecaria.Text;
            if (string.IsNullOrWhiteSpace(senha))
                return "Informe a senha.";
            return senha.Length >= 6 && senha.Length <= 25 ? null : "Use 6 a 25 caracteres.";
        }

        private string ValidarConfirmacaoSenha()
        {
            if (string.IsNullOrEmpty(_confirmarSenhaBibliotecaria.Text))
                return "Confirme a senha.";
            return _senhaBibliotecaria.Text == _confirmarSenhaBibliotecaria.Text ? null : "As senhas não coincidem.";
        }

        private static string ApenasDigitos(string valor)
        {
            return new string((valor ?? string.Empty).Where(char.IsDigit).ToArray());
        }

        private void ExibirErro(Label label, string mensagem)
        {
            label.Text = mensagem ?? string.Empty;
            label.Visible = !string.IsNullOrEmpty(mensagem);
        }

        private bool TemErrosVisiveis()
        {
            return lblErroNomeInstituicao.Visible || lblErroTema.Visible || lblErroNomeBibliotecaria.Visible ||
                   lblErroEmailBibliotecaria.Visible || lblErroTelefoneBibliotecaria.Visible ||
                   lblErroSenhaBibliotecaria.Visible || lblErroConfirmarSenhaBibliotecaria.Visible;
        }

        private void ExibirErroGeral(string mensagem)
        {
            lblErroGeral.Text = mensagem;
            lblErroGeral.Visible = true;
        }

        private void OcultarErroGeral()
        {
            lblErroGeral.Text = string.Empty;
            lblErroGeral.Visible = false;
        }

        private void exportarCodigo_Click(object sender, EventArgs e)
        {
            using (var dialogo = new SaveFileDialog
            {
                Filter = "Arquivo de texto (*.txt)|*.txt",
                DefaultExt = "txt",
                AddExtension = true,
                FileName = "codigo-recuperacao-SUB.txt",
                Title = "Salvar código de recuperação do SUB"
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    string conteudo =
                        "Código de recuperação do SUB" + Environment.NewLine +
                        "Instituição: " + (_nomeInstituicao.Text.Trim().Length == 0 ? "Não informada" : _nomeInstituicao.Text.Trim()) + Environment.NewLine +
                        "Código: " + _codigoMestreGerado + Environment.NewLine + Environment.NewLine +
                        "Guarde este arquivo em local seguro. O código permite recuperar o acesso administrativo.";

                    File.WriteAllText(dialogo.FileName, conteudo, new UTF8Encoding(true));
                    _caminhoCodigoMestre = dialogo.FileName;
                    statusCodigo.Text = "Salvo em: " + _caminhoCodigoMestre;
                    statusCodigo.ForeColor = _corSucesso;
                    AtualizarEstadoConclusao();
                }
                catch (Exception ex)
                {
                    statusCodigo.Text = "Não foi possível salvar. Escolha outro local. " + ex.Message;
                    statusCodigo.ForeColor = _corErro;
                    AtualizarEstadoConclusao();
                }
            }
        }

        private void AtualizarEstadoConclusao()
        {
            bool codigoDisponivel = !string.IsNullOrWhiteSpace(_caminhoCodigoMestre) && File.Exists(_caminhoCodigoMestre);
            salvar.Enabled = codigoDisponivel && !_concluindo;
            salvar.BaseColor = salvar.Enabled ? TemaSelecionado.PrimaryColor : Color.FromArgb(130, 139, 150);
            salvar.Cursor = salvar.Enabled ? Cursors.Hand : Cursors.Default;
            salvar.Invalidate();
        }

        private static string GerarCodigoMestre()
        {
            byte[] bytes = new byte[24];
            using (RandomNumberGenerator gerador = RandomNumberGenerator.Create())
                gerador.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", string.Empty);
        }

        private void salvar_Click(object sender, EventArgs e)
        {
            if (_concluindo)
                return;

            if (string.IsNullOrWhiteSpace(_caminhoCodigoMestre) || !File.Exists(_caminhoCodigoMestre))
            {
                _caminhoCodigoMestre = null;
                statusCodigo.Text = "Arquivo não encontrado. Salve o código novamente para concluir.";
                statusCodigo.ForeColor = _corErro;
                AtualizarEstadoConclusao();
                return;
            }

            if (!ValidarEtapa2())
                return;

            ConcluirConfiguracao();
        }

        private void ConcluirConfiguracao()
        {
            string caminhoBanco = Conexao.CaminhoBanco;
            string pastaBanco = Path.GetDirectoryName(caminhoBanco);
            string caminhoConfiguracao = Path.Combine(AppPaths.AppDataFolder, "instituicao.json");
            string caminhoConfiguracaoTemporaria = caminhoConfiguracao + ".tmp";

            bool bancoExistia = File.Exists(caminhoBanco);
            bool pastaBancoExistia = Directory.Exists(pastaBanco);
            bool pastaAppExistia = Directory.Exists(AppPaths.AppDataFolder);
            bool pastaMapeamentoExistia = Directory.Exists(AppPaths.MappingFolder);
            bool pastaRelatoriosExistia = Directory.Exists(AppPaths.ReportsFolder);
            bool configuracaoExistia = File.Exists(caminhoConfiguracao);
            bool temporarioExistia = File.Exists(caminhoConfiguracaoTemporaria);

            SqlCeTransaction transacao = null;
            SqlCeConnection conexao = null;
            DefinirEstadoOcupado(true);

            try
            {
                string email = _emailBibliotecaria.Text.Trim().ToLowerInvariant();
                ThemePreset tema = TemaSelecionado;
                CriptografiaSenha.CriarHash(_senhaBibliotecaria.Text, out string senhaHash, out string senhaSalt);
                CriptografiaSenha.CriarHash(_codigoMestreGerado, out string codigoHash, out string codigoSalt);

                DatabaseInitializer.EnsureCreated();
                conexao = Conexao.ObterConexao();
                conexao.Open();
                transacao = conexao.BeginTransaction();

                using (SqlCeCommand verificarEmail = conexao.CreateCommand())
                {
                    verificarEmail.Transaction = transacao;
                    verificarEmail.CommandText = "SELECT COUNT(*) FROM usuarios WHERE Email = @Email";
                    verificarEmail.Parameters.AddWithValue("@Email", email);
                    if (Convert.ToInt32(verificarEmail.ExecuteScalar()) > 0)
                    {
                        transacao.Rollback();
                        transacao.Dispose();
                        transacao = null;
                        ExibirErro(lblErroEmailBibliotecaria, "Este e-mail já está cadastrado.");
                        ExibirErroGeral("Use outro e-mail para a primeira bibliotecária.");
                        _emailBibliotecaria.Focus();
                        return;
                    }
                }

                using (SqlCeCommand inserir = conexao.CreateCommand())
                {
                    inserir.Transaction = transacao;
                    inserir.CommandText = @"INSERT INTO usuarios
(Nome, Email, Senha_Hash, Senha_Salt, CPF, DataNascimento, Turma, Telefone, TipoUsuario)
VALUES
(@Nome, @Email, @Senha_Hash, @Senha_Salt, @CPF, @DataNascimento, @Turma, @Telefone, @TipoUsuario)";
                    inserir.Parameters.AddWithValue("@Nome", _nomeBibliotecaria.Text.Trim());
                    inserir.Parameters.AddWithValue("@Email", email);
                    inserir.Parameters.AddWithValue("@Senha_Hash", senhaHash);
                    inserir.Parameters.AddWithValue("@Senha_Salt", senhaSalt);
                    inserir.Parameters.AddWithValue("@CPF", DBNull.Value);
                    inserir.Parameters.AddWithValue("@DataNascimento", DateTime.Today);
                    inserir.Parameters.AddWithValue("@Turma", DBNull.Value);
                    inserir.Parameters.AddWithValue("@Telefone", _telefoneBibliotecaria.Text.Trim());
                    inserir.Parameters.AddWithValue("@TipoUsuario", "Bibliotecário(a)");
                    inserir.ExecuteNonQuery();
                }

                InstituicaoConfiguracaoStore.Salvar(new InstituicaoConfiguracao
                {
                    Nome = _nomeInstituicao.Text.Trim(),
                    CorPrimaria = ColorTranslator.ToHtml(tema.PrimaryColor),
                    CodigoMestreHash = codigoHash,
                    CodigoMestreSalt = codigoSalt
                });

                transacao.Commit();
                transacao.Dispose();
                transacao = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (transacao != null)
                {
                    try { transacao.Rollback(); } catch { }
                }

                if (conexao != null)
                {
                    try { conexao.Close(); } catch { }
                }

                string falhasLimpeza = DesfazerInstalacaoParcial(
                    caminhoBanco,
                    pastaBanco,
                    caminhoConfiguracao,
                    caminhoConfiguracaoTemporaria,
                    bancoExistia,
                    pastaBancoExistia,
                    pastaAppExistia,
                    pastaMapeamentoExistia,
                    pastaRelatoriosExistia,
                    configuracaoExistia,
                    temporarioExistia);

                string mensagem = "Não foi possível concluir. As alterações da instalação foram desfeitas. Tente novamente.";
                if (!string.IsNullOrEmpty(falhasLimpeza))
                    mensagem = "Não foi possível concluir. Feche o SUB e verifique: " + falhasLimpeza;
                else if (!string.IsNullOrWhiteSpace(ex.Message))
                    mensagem += " Motivo: " + ex.Message;

                ExibirErroGeral(mensagem);
            }
            finally
            {
                if (transacao != null)
                    transacao.Dispose();
                if (conexao != null)
                    conexao.Dispose();
                DefinirEstadoOcupado(false);
            }
        }

        private string DesfazerInstalacaoParcial(
            string caminhoBanco,
            string pastaBanco,
            string caminhoConfiguracao,
            string caminhoConfiguracaoTemporaria,
            bool bancoExistia,
            bool pastaBancoExistia,
            bool pastaAppExistia,
            bool pastaMapeamentoExistia,
            bool pastaRelatoriosExistia,
            bool configuracaoExistia,
            bool temporarioExistia)
        {
            var falhas = new System.Collections.Generic.List<string>();

            ExcluirArquivoCriado(caminhoConfiguracao, configuracaoExistia, falhas);
            ExcluirArquivoCriado(caminhoConfiguracaoTemporaria, temporarioExistia, falhas);
            ExcluirArquivoCriado(caminhoBanco, bancoExistia, falhas);

            ExcluirDiretorioVazio(AppPaths.MappingFolder, pastaMapeamentoExistia, falhas);
            ExcluirDiretorioVazio(AppPaths.ReportsFolder, pastaRelatoriosExistia, falhas);
            ExcluirDiretorioVazio(pastaBanco, pastaBancoExistia, falhas);
            ExcluirDiretorioVazio(AppPaths.AppDataFolder, pastaAppExistia, falhas);

            return string.Join("; ", falhas);
        }

        private static void ExcluirArquivoCriado(string caminho, bool existiaAntes, System.Collections.Generic.ICollection<string> falhas)
        {
            if (existiaAntes || string.IsNullOrWhiteSpace(caminho) || !File.Exists(caminho))
                return;

            try
            {
                File.Delete(caminho);
            }
            catch
            {
                falhas.Add(caminho);
            }
        }

        private static void ExcluirDiretorioVazio(string caminho, bool existiaAntes, System.Collections.Generic.ICollection<string> falhas)
        {
            if (existiaAntes || string.IsNullOrWhiteSpace(caminho) || !Directory.Exists(caminho))
                return;

            try
            {
                if (!Directory.EnumerateFileSystemEntries(caminho).Any())
                    Directory.Delete(caminho, false);
            }
            catch
            {
                falhas.Add(caminho);
            }
        }

        private void DefinirEstadoOcupado(bool ocupado)
        {
            _concluindo = ocupado;
            btnVoltar.Enabled = !ocupado;
            exportarCodigo.Enabled = !ocupado;
            btnFechar.Enabled = !ocupado;
            salvar.Text = ocupado ? "Concluindo..." : "Concluir configuração";
            UseWaitCursor = ocupado;
            AtualizarEstadoConclusao();
        }

        private static bool EmailValido(string email)
        {
            try
            {
                var endereco = new MailAddress(email);
                return string.Equals(endereco.Address, email, StringComparison.OrdinalIgnoreCase);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            const string mensagem = "Tem certeza de que deseja fechar o SUB? A configuração ainda não foi concluída.";
            const string titulo = "Sair da configuração inicial";
            DialogResult confirma = MessageBox.Show(this, mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma == DialogResult.Yes)
                Application.Exit();
        }
    }
}
