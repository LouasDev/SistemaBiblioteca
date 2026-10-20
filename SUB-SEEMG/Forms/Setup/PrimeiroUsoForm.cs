using SeuProjeto;
using SUB.Models;
using SUB.Utils;
using System;
using System.Collections.Generic;
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
        private readonly List<string> _turmasIniciais = new List<string>();



        public PrimeiroUsoForm()
        {

            //aredondar cantos e desenhar borda cinza

            int meuRaio = 15;
            this.ArredondarCantos(meuRaio);
            this.DesenharBorda(Color.FromArgb(31, 72, 126), meuRaio, 1);


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
            btnAvancarEtapa2.BaseColor = primaria;
            btnVoltar.ForeColor = primaria;
            btnVoltarEtapa3.ForeColor = primaria;
            btnVoltar.FlatAppearance.MouseOverBackColor = tema.AccentColor;
            btnVoltar.FlatAppearance.MouseDownBackColor = ThemeManager.Lighten(primaria, 0.86f);
            exportarCodigo.ForeColor = primaria;
            exportarCodigo.FlatAppearance.BorderColor = primaria;
            exportarCodigo.FlatAppearance.MouseOverBackColor = tema.AccentColor;
            exportarCodigo.FlatAppearance.MouseDownBackColor = ThemeManager.Lighten(primaria, 0.86f);
            btnAdicionarTurma.BackColor = primaria;
            btnAdicionarTurma.ForeColor = ThemeManager.GetContrastColor(primaria);
            btnAdicionarTurma.FlatAppearance.BorderColor = ThemeManager.Darken(primaria, 0.12f);
            btnRemoverTurma.BackColor = Color.White;
            btnRemoverTurma.ForeColor = ThemeManager.GetLabelColor(primaria);
            btnRemoverTurma.FlatAppearance.BorderColor = ThemeManager.Lighten(primaria, 0.55f);
            lstTurmasIniciais.BackColor = Color.White;
            lstTurmasIniciais.ForeColor = Color.FromArgb(29, 39, 53);

            RoundedTextBox[] campos =
            {
                _nomeInstituicao,
                _nomeBibliotecaria,
                _emailBibliotecaria,
                _senhaBibliotecaria,
                _confirmarSenhaBibliotecaria,
                txtNovaTurma
            };

            foreach (RoundedTextBox campo in campos)
            {
                campo.BorderColor = borda;
                campo.BorderFocusColor = primaria;
                campo.HoverBackColor = hover;
                campo.Invalidate();
            }

            _telefoneBibliotecaria.BorderColor = borda;
            _telefoneBibliotecaria.BorderFocusColor = primaria;
            _telefoneBibliotecaria.HoverBackColor = hover;
            _telefoneBibliotecaria.HoverBorderColor = primaria;
            _telefoneBibliotecaria.Invalidate();

            _tema.BorderColor = primaria;
            _tema.Invalidate();
            AtualizarIndicadorEtapa();
            AtualizarEstadoConclusao();
            SubLogoManager.SetImage(picLogo, SubLogoVariant.Square, primaria, pnlLogo.BackColor);
        }

        private void MostrarEtapa(int etapa)
        {
            _etapaAtual = etapa;
            pnlEtapa1.Visible = etapa == 1;
            pnlEtapa2.Visible = etapa == 2;
            pnlEtapa3.Visible = etapa == 3;

            if (etapa == 1) pnlEtapa1.BringToFront();
            else if (etapa == 2) pnlEtapa2.BringToFront();
            else pnlEtapa3.BringToFront();

            lblEtapaAtual.Text = "Etapa " + etapa + " de 3";
            AcceptButton = etapa == 1 ? (IButtonControl)btnAvancar :
                etapa == 2 ? (IButtonControl)btnAvancarEtapa2 : salvar;
            AtualizarIndicadorEtapa();
            OcultarErroGeral();

            if (etapa == 1)
                _nomeInstituicao.Focus();
            else if (etapa == 2)
                _nomeBibliotecaria.Focus();
            else
                txtNovaTurma.Focus();
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
            else if (_etapaAtual == 2)
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
                    btnAvancarEtapa2.Focus();
                    return true;
                }
            }
            else if (_etapaAtual == 3 && txtNovaTurma.ContainsFocus)
            {
                btnAdicionarTurma.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AtualizarIndicadorEtapa()
        {
            Color primaria = TemaSelecionado.PrimaryColor;
            indicadorEtapa1.BackColor = primaria;
            indicadorEtapa2.BackColor = _etapaAtual >= 2 ? primaria : Color.FromArgb(222, 227, 234);
            indicadorEtapa3.BackColor = _etapaAtual >= 3 ? primaria : Color.FromArgb(222, 227, 234);

            bool primeiraEtapa = _etapaAtual == 1;
            bool segundaEtapa = _etapaAtual == 2;
            bool terceiraEtapa = _etapaAtual == 3;
            lblPasso1Numero.BackColor = primeiraEtapa ? Color.White : ThemeManager.Lighten(primaria, 0.34f);
            lblPasso2Numero.BackColor = segundaEtapa ? Color.White : ThemeManager.Lighten(primaria, 0.34f);
            lblPasso3Numero.BackColor = terceiraEtapa ? Color.White : ThemeManager.Lighten(primaria, 0.34f);
            lblPasso1Numero.ForeColor = primeiraEtapa ? primaria : Color.White;
            lblPasso2Numero.ForeColor = segundaEtapa ? primaria : Color.White;
            lblPasso3Numero.ForeColor = terceiraEtapa ? primaria : Color.White;
            lblPasso1Texto.Font = new Font("Segoe UI", 10F,
                primeiraEtapa ? FontStyle.Bold : FontStyle.Regular);
            lblPasso2Texto.Font = new Font("Segoe UI", 10F,
                segundaEtapa ? FontStyle.Bold : FontStyle.Regular);
            lblPasso3Texto.Font = new Font("Segoe UI", 10F,
                terceiraEtapa ? FontStyle.Bold : FontStyle.Regular);
            linhaRoteiro.BackColor = ThemeManager.Lighten(primaria, 0.42f);
            linhaRoteiro2.BackColor = ThemeManager.Lighten(primaria, 0.42f);
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            if (!ValidarEtapa1())
                return;

            MostrarEtapa(2);
        }

        private void btnAvancarEtapa2_Click(object sender, EventArgs e)
        {
            if (!ValidarEtapa2())
                return;

            MostrarEtapa(3);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            MostrarEtapa(1);
        }

        private void btnVoltarEtapa3_Click(object sender, EventArgs e)
        {
            MostrarEtapa(2);
        }

        private void btnAdicionarTurma_Click(object sender, EventArgs e)
        {
            string turma = (txtNovaTurma.Text ?? string.Empty).Trim().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(turma))
            {
                ExibirErro(lblErroTurmas, "Digite o nome da turma antes de adicionar.");
                txtNovaTurma.Focus();
                return;
            }

            if (_turmasIniciais.Contains(turma, StringComparer.OrdinalIgnoreCase))
            {
                ExibirErro(lblErroTurmas, "Esta turma já foi adicionada.");
                txtNovaTurma.SelectAll();
                txtNovaTurma.Focus();
                return;
            }

            _turmasIniciais.Add(turma);
            txtNovaTurma.Text = string.Empty;
            AtualizarListaTurmasIniciais();
            ExibirErro(lblErroTurmas, null);
            AtualizarEstadoConclusao();
            txtNovaTurma.Focus();
        }

        private void btnRemoverTurma_Click(object sender, EventArgs e)
        {
            string turma = lstTurmasIniciais.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(turma))
                return;

            _turmasIniciais.RemoveAll(item => string.Equals(item, turma, StringComparison.OrdinalIgnoreCase));
            AtualizarListaTurmasIniciais();
            AtualizarEstadoConclusao();
        }

        private void txtNovaTurma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAdicionarTurma.PerformClick();
            }
        }

        private void lstTurmasIniciais_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoverTurma.Enabled = lstTurmasIniciais.SelectedIndex >= 0;
        }

        private void AtualizarListaTurmasIniciais()
        {
            lstTurmasIniciais.BeginUpdate();
            lstTurmasIniciais.Items.Clear();
            foreach (string turma in _turmasIniciais.OrderBy(item => item, StringComparer.CurrentCultureIgnoreCase))
                lstTurmasIniciais.Items.Add(turma);
            lstTurmasIniciais.EndUpdate();
            btnRemoverTurma.Enabled = false;
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

        private bool ValidarEtapa3()
        {
            string erro = _turmasIniciais.Count == 0
                ? "Adicione ao menos uma turma para concluir a configuração."
                : null;
            ExibirErro(lblErroTurmas, erro);

            if (erro == null)
                return true;

            ExibirErroGeral("Cadastre pelo menos uma turma da instituição antes de concluir.");
            txtNovaTurma.Focus();
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
            salvar.Enabled = codigoDisponivel && _turmasIniciais.Count > 0 && !_concluindo;
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

            if (!ValidarEtapa3())
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

                foreach (string turma in _turmasIniciais)
                {
                    using (SqlCeCommand inserirTurma = conexao.CreateCommand())
                    {
                        inserirTurma.Transaction = transacao;
                        inserirTurma.CommandText = "INSERT INTO Turmas (Nome) VALUES (@Nome)";
                        inserirTurma.Parameters.AddWithValue("@Nome", turma);
                        inserirTurma.ExecuteNonQuery();
                    }
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

        private void pnlInstitucional_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTituloEtapa2_Click(object sender, EventArgs e)
        {

        }
    }
}
