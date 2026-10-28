using SUB.Forms.Utils;
using SUB.Models;
using SUB.Utils;
using SeuProjeto;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUB.Forms.Login
{
    public partial class LoginForm : Form
    {
        #region Propriedades
        // Variável para controle externo de login
        public static bool cancelar = false;

        // Indica se o login atual foi realizado por uma bibliotecária
        public static bool UsuarioBibliotecaria { get; set; } = false;

        // Evita reentrância no clique (duplo Enter/click)
        private bool _isAuthenticating = false;
        private Color _corPrimariaAtual = ThemeManager.DefaultColor;
        // Definidas em AplicarTema conforme o modo claro/escuro da instalação.
        private Color _corBordaPadrao = Color.FromArgb(174, 188, 204);
        private Color _corErro = Color.FromArgb(184, 50, 50);
        private Color _corFocoCampo = ThemeManager.DefaultColor;
        #endregion

        #region Construtor
        public LoginForm()
        {
            InitializeComponent();

            //aredondar cantos e desenhar borda cinza

            int meuRaio = 15;
            this.ArredondarCantos(meuRaio);
            this.DesenharBorda(Color.FromArgb(188, 199, 212), meuRaio, 1);
            this.AcceptButton = BtnEntrar;

            txtEmail.TextChanged += (s, e) => LimparErroCampo(txtEmail, lblErroEmail);
            txtSenha.TextChanged += (s, e) => LimparErroCampo(txtSenha, lblErroSenha);
        }

        //Fecharo form se o login for cancelado externamente
        private void LoginForm_Load(object sender, EventArgs e)
        {
            var configuracao = InstituicaoConfiguracaoStore.Carregar();
            lblNomeEscola.Text = configuracao == null || string.IsNullOrWhiteSpace(configuracao.Nome)
                ? "Biblioteca da instituição"
                : configuracao.Nome.Trim();

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

            ThemeManager.DefinirModo(configuracao);
            AplicarTema(corPrimaria);
            ThemeManager.TemaDoSistemaAlterado += ThemeManager_TemaDoSistemaAlterado;
        }

        private void ThemeManager_TemaDoSistemaAlterado(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(ThemeManager_TemaDoSistemaAlterado), sender, e);
                return;
            }

            AplicarTema(_corPrimariaAtual);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            ThemeManager.TemaDoSistemaAlterado -= ThemeManager_TemaDoSistemaAlterado;
            base.OnFormClosed(e);
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => txtEmail.Focus()));
        }

        private void AplicarTema(Color corPrimaria)
        {
            _corPrimariaAtual = ThemeManager.Normalize(corPrimaria);
            SubLogoManager.ApplyWindowIcon(this, _corPrimariaAtual);
            ThemePreset tema = ThemeManager.FindPreset(ColorTranslator.ToHtml(_corPrimariaAtual));
            bool escuro = ThemeManager.Escuro;
            Color superficie = escuro ? ThemeManager.Dark.Fundo : tema.AccentColor;
            Color focoSuave = ThemeManager.Lighten(_corPrimariaAtual, 0.94f);
            _corBordaPadrao = escuro ? ThemeManager.Dark.Borda : Color.FromArgb(174, 188, 204);
            _corErro = escuro ? ThemeManager.CorTextoParaEscuro(Color.FromArgb(184, 50, 50)) : Color.FromArgb(184, 50, 50);
            _corFocoCampo = escuro ? ThemeManager.Lighten(_corPrimariaAtual, 0.35f) : _corPrimariaAtual;

            BackColor = superficie;
            pnInicial.BackColor = superficie;
            panel1.BackColor = _corPrimariaAtual;
            gradientPanel2.BackColor = Color.FromArgb(250, 252, 255);
            gradientPanel2.Gradientetop = gradientPanel2.BackColor;
            gradientPanel2.Gradientebottom = gradientPanel2.BackColor;
            pnlLogo.BackColor = ThemeManager.Lighten(_corPrimariaAtual, 0.88f);
            linhaMarca.BackColor = ThemeManager.Lighten(_corPrimariaAtual, 0.48f);

            lblAcessoInstitucional.ForeColor = _corPrimariaAtual;
            lblEsqueceuSenha.LinkColor = _corPrimariaAtual;
            lblEsqueceuSenha.ActiveLinkColor = ThemeManager.Darken(_corPrimariaAtual, 0.18f);
            lblEsqueceuSenha.VisitedLinkColor = _corPrimariaAtual;

            foreach (RoundedTextBox campo in new[] { txtEmail, txtSenha })
            {
                campo.BackColor = escuro ? ThemeManager.Dark.Entrada : Color.White;
                campo.TextColor = escuro ? ThemeManager.Dark.Texto : Color.FromArgb(29, 39, 53);
                campo.PlaceholderColor = escuro ? ThemeManager.Dark.TextoSuave : Color.Gray;
                campo.BorderColor = _corBordaPadrao;
                campo.BorderFocusColor = _corFocoCampo;
                campo.HoverBackColor = focoSuave;
                campo.Invalidate();
            }

            if (!escuro)
            {
                // AplicarModoEscuro converte os rótulos do painel. Ao voltar ao claro,
                // estas são as cores intencionais do layout original do login.
                label2.ForeColor = Color.FromArgb(23, 36, 52);
                lblSubtituloLogin.ForeColor = Color.FromArgb(91, 104, 120);
                label3.ForeColor = Color.FromArgb(43, 55, 69);
                label4.ForeColor = Color.FromArgb(43, 55, 69);
                lblErroEmail.ForeColor = Color.FromArgb(184, 50, 50);
                lblErroSenha.ForeColor = Color.FromArgb(184, 50, 50);
                cbMostrarSenha.ForeColor = Color.FromArgb(73, 86, 102);
                lblAcessoRestrito.ForeColor = Color.FromArgb(105, 116, 130);
                this.AlterarCorDaBorda(Color.FromArgb(188, 199, 212));
            }

            BtnEntrar.BaseColor = _corPrimariaAtual;
            BtnEntrar.BackgroundColor = _corPrimariaAtual;
            BtnEntrar.BorderColor = _corPrimariaAtual;
            BtnEntrar.Invalidate();
            SubLogoManager.SetImage(picLogoLogin, SubLogoVariant.Square, _corPrimariaAtual, pnlLogo.BackColor);

            // O painel da marca já é escuro (cor da instituição); muda só o lado do formulário.
            if (escuro)
            {
                ThemeManager.AplicarModoEscuro(gradientPanel2, _corPrimariaAtual);
                this.AlterarCorDaBorda(ThemeManager.Dark.Borda);
            }
        }
        #endregion

        #region Métodos de Login

        private void BtnEntrar_MouseLeave(object sender, EventArgs e)
        {
            Color cor = ThemeManager.Normalize(BtnEntrar.BaseColor);
            BtnEntrar.BackColor = ThemeManager.Darken(cor, 0.08f);
            BtnEntrar.Refresh();
        }

        private void BtnEntrar_MouseEnter(object sender, EventArgs e)
        {
            BtnEntrar.BackColor = ThemeManager.Lighten(ThemeManager.Normalize(BtnEntrar.BaseColor), 0.12f);
            BtnEntrar.Refresh();
        }
        #endregion

        #region Métodos de Atualização
        private async Task AtualizarStatusEmprestimosAsync()
        {
            using (var progressForm = new frmProgresso())
            {
                progressForm.Show();

                await Task.Run(() =>
                {
                    AtualizarEmprestimos(progressForm);
                });
            }
        }

        private void AtualizarEmprestimos(frmProgresso progressForm)
        {
            try
            {
                using (var connection = Conexao.ObterConexao())
                {
                    connection.Open();

                    string selectQuery = @"
                    SELECT Id, DataDevolucao, DataProrrogacao, DataRealDevolucao
                    FROM Emprestimo
                    WHERE Status <> 'Devolvido'";

                    using (var selectCommand = new SqlCeCommand(selectQuery, connection))
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        var emprestimos = new List<Tuple<int, DateTime, DateTime?, DateTime?>>();
                        while (reader.Read())
                        {
                            emprestimos.Add(Tuple.Create(
                                reader.GetInt32(0),
                                reader.GetDateTime(1),
                                reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                                reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)));
                        }

                        if (emprestimos.Count == 0)
                        {
                            progressForm.AtualizarProgresso(100, "Nenhum empréstimo para atualizar.");
                            return;
                        }

                        for (int i = 0; i < emprestimos.Count; i++)
                        {
                            var emprestimo = emprestimos[i];
                            string novoStatus = CalcularStatus(emprestimo.Item2, emprestimo.Item3, emprestimo.Item4);

                            using (var updateCommand = new SqlCeCommand(
                                "UPDATE Emprestimo SET Status = @Status WHERE Id = @Id", connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Status", novoStatus);
                                updateCommand.Parameters.AddWithValue("@Id", emprestimo.Item1);
                                updateCommand.ExecuteNonQuery();
                            }

                            int progresso = (int)((double)(i + 1) / emprestimos.Count * 100);
                            progressForm.AtualizarProgresso(progresso,
                                $"Atualizando empréstimo {i + 1} de {emprestimos.Count}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar empréstimos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string CalcularStatus(DateTime dataDevolucao, DateTime? dataProrrogacao, DateTime? dataRealDevolucao)
        {
            DateTime dataReferencia = dataProrrogacao ?? dataDevolucao;

            if (dataRealDevolucao.HasValue)
            {
                return "Devolvido";
            }
            else if (DateTime.Now > dataReferencia)
            {
                return "Atrasado";
            }
            else
            {
                return "Ativo";
            }
        }
        #endregion

        #region Eventos de Teclado
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && txtEmail.ContainsFocus)
            {
                txtSenha.Focus();
                return true;
            }

            if (keyData == Keys.Enter && txtSenha.ContainsFocus)
            {
                BtnEntrar.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void lblEsqueceuSenha_Click(object sender, EventArgs e)
        {
            var configuracao = InstituicaoConfiguracaoStore.Carregar();
            if (configuracao == null)
            {
                MostrarStatusLogin("A configuração da instituição não foi encontrada. Reinicie o SUB e tente novamente.");
                return;
            }

            int idBibliotecaria;
            if (!ConfirmarEmailRecuperacao(out idBibliotecaria))
                return;

            if (!ConfirmarCodigoRecuperacao(configuracao))
                return;

            if (DefinirNovaSenha(idBibliotecaria))
                MostrarStatusLogin("Senha atualizada. Você já pode entrar com a nova senha.", true);
        }

        private bool ConfirmarEmailRecuperacao(out int idBibliotecaria)
        {
            idBibliotecaria = 0;
            bool confirmado = false;
            int idConfirmado = 0;

            using (var dialogo = CriarDialogoRecuperacao("Recuperar acesso", new Size(440, 260)))
            using (var email = new TextBox
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F),
                Location = new Point(30, 116),
                Size = new Size(380, 34)
            })
            using (var erro = CriarLabelErro(new Point(30, 158), new Size(380, 38)))
            using (var cancelar = CriarBotaoSecundario("Cancelar", new Point(202, 208), new Size(96, 36)))
            using (var continuar = CriarBotaoPrimario("Continuar", new Point(306, 208), new Size(104, 36)))
            {
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(23, 36, 52),
                    Location = new Point(27, 20),
                    Text = "Recuperar acesso"
                });
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(91, 104, 120),
                    Location = new Point(30, 60),
                    Text = "Informe o e-mail usado para entrar no SUB."
                });
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(43, 55, 69),
                    Location = new Point(30, 94),
                    Text = "E-mail de acesso"
                });
                dialogo.Controls.Add(email);
                dialogo.Controls.Add(erro);
                dialogo.Controls.Add(cancelar);
                dialogo.Controls.Add(continuar);

                email.TextChanged += (s, e) => { erro.Text = string.Empty; erro.Visible = false; };
                cancelar.Click += (s, e) => dialogo.Close();
                continuar.Click += (s, e) =>
                {
                    string emailInformado = (email.Text ?? string.Empty).Trim().ToLowerInvariant();
                    if (!Regex.IsMatch(emailInformado, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        erro.Text = "Informe um e-mail válido.";
                        erro.Visible = true;
                        email.Focus();
                        return;
                    }

                    try
                    {
                        using (var conexao = Conexao.ObterConexao())
                        using (var comando = new SqlCeCommand(
                            "SELECT TOP (1) Id FROM usuarios WHERE Email = @Email AND TipoUsuario = 'Bibliotecário(a)'", conexao))
                        {
                            comando.Parameters.AddWithValue("@Email", emailInformado);
                            conexao.Open();
                            object resultado = comando.ExecuteScalar();
                            if (resultado == null || resultado == DBNull.Value)
                            {
                                erro.Text = "Não há uma conta de bibliotecária com este e-mail.";
                                erro.Visible = true;
                                email.Focus();
                                email.SelectAll();
                                return;
                            }

                            idConfirmado = Convert.ToInt32(resultado);
                            confirmado = true;
                            dialogo.DialogResult = DialogResult.OK;
                            dialogo.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        erro.Text = "Não foi possível verificar o e-mail. " + ex.Message;
                        erro.Visible = true;
                    }
                };

                dialogo.AcceptButton = continuar;
                dialogo.CancelButton = cancelar;
                dialogo.Shown += (s, e) => email.Focus();
                dialogo.ShowDialog(this);
            }

            idBibliotecaria = idConfirmado;
            return confirmado;
        }

        private bool ConfirmarCodigoRecuperacao(InstituicaoConfiguracao configuracao)
        {
            bool confirmado = false;

            using (var dialogo = CriarDialogoRecuperacao("Recuperar acesso", new Size(440, 250)))
            using (var codigo = new TextBox
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F),
                Location = new Point(30, 91),
                Size = new Size(380, 34),
                UseSystemPasswordChar = true
            })
            using (var mostrar = new CheckBox
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(30, 133),
                Text = "Mostrar código"
            })
            using (var erro = CriarLabelErro(new Point(30, 158), new Size(380, 34)))
            using (var cancelar = CriarBotaoSecundario("Cancelar", new Point(202, 202), new Size(96, 36)))
            using (var validar = CriarBotaoPrimario("Validar código", new Point(306, 202), new Size(104, 36)))
            {
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(23, 36, 52),
                    Location = new Point(27, 20),
                    Text = "Recuperar acesso"
                });
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(91, 104, 120),
                    Location = new Point(30, 60),
                    Text = "Informe o código de recuperação salvo na configuração inicial."
                });
                dialogo.Controls.Add(codigo);
                dialogo.Controls.Add(mostrar);
                dialogo.Controls.Add(erro);
                dialogo.Controls.Add(cancelar);
                dialogo.Controls.Add(validar);

                mostrar.CheckedChanged += (s, e) => codigo.UseSystemPasswordChar = !mostrar.Checked;
                codigo.TextChanged += (s, e) => { erro.Text = string.Empty; erro.Visible = false; };
                cancelar.Click += (s, e) => dialogo.Close();
                validar.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(codigo.Text))
                    {
                        erro.Text = "Informe o código de recuperação.";
                        erro.Visible = true;
                        codigo.Focus();
                        return;
                    }

                    if (!CriptografiaSenha.VerificarSenha(codigo.Text.Trim(), configuracao.CodigoMestreHash, configuracao.CodigoMestreSalt))
                    {
                        erro.Text = "O código não confere. Revise o arquivo salvo e tente novamente.";
                        erro.Visible = true;
                        codigo.Focus();
                        codigo.SelectAll();
                        return;
                    }

                    confirmado = true;
                    dialogo.DialogResult = DialogResult.OK;
                    dialogo.Close();
                };

                dialogo.AcceptButton = validar;
                dialogo.CancelButton = cancelar;
                dialogo.Shown += (s, e) => codigo.Focus();
                dialogo.ShowDialog(this);
            }

            return confirmado;
        }

        private bool DefinirNovaSenha(int idBibliotecaria)
        {
            bool atualizada = false;

            using (var dialogo = CriarDialogoRecuperacao("Nova senha da bibliotecária", new Size(440, 360)))
            using (var senha = new TextBox
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F),
                Location = new Point(30, 116),
                MaxLength = 25,
                Size = new Size(380, 34),
                UseSystemPasswordChar = true
            })
            using (var confirmar = new TextBox
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 10.5F),
                Location = new Point(30, 186),
                MaxLength = 25,
                Size = new Size(380, 34),
                UseSystemPasswordChar = true
            })
            using (var mostrar = new CheckBox
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(30, 230),
                Text = "Mostrar senhas"
            })
            using (var erro = CriarLabelErro(new Point(30, 255), new Size(380, 40)))
            using (var cancelar = CriarBotaoSecundario("Cancelar", new Point(202, 307), new Size(96, 38)))
            using (var salvar = CriarBotaoPrimario("Salvar senha", new Point(306, 307), new Size(104, 38)))
            {
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(23, 36, 52),
                    Location = new Point(27, 20),
                    Text = "Crie uma nova senha"
                });
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.5F),
                    ForeColor = Color.FromArgb(91, 104, 120),
                    Location = new Point(30, 60),
                    Text = "Use entre 6 e 25 caracteres."
                });
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(43, 55, 69),
                    Location = new Point(30, 94),
                    Text = "Nova senha"
                });
                dialogo.Controls.Add(senha);
                dialogo.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(43, 55, 69),
                    Location = new Point(30, 164),
                    Text = "Confirmar senha"
                });
                dialogo.Controls.Add(confirmar);
                dialogo.Controls.Add(mostrar);
                dialogo.Controls.Add(erro);
                dialogo.Controls.Add(cancelar);
                dialogo.Controls.Add(salvar);

                mostrar.CheckedChanged += (s, e) =>
                {
                    bool ocultar = !mostrar.Checked;
                    senha.UseSystemPasswordChar = ocultar;
                    confirmar.UseSystemPasswordChar = ocultar;
                };
                senha.TextChanged += (s, e) => { erro.Text = string.Empty; erro.Visible = false; };
                confirmar.TextChanged += (s, e) => { erro.Text = string.Empty; erro.Visible = false; };
                senha.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        confirmar.Focus();
                    }
                };
                cancelar.Click += (s, e) => dialogo.Close();
                salvar.Click += (s, e) =>
                {
                    if (senha.Text.Length < 6 || senha.Text.Length > 25)
                    {
                        erro.Text = "A senha deve conter entre 6 e 25 caracteres.";
                        erro.Visible = true;
                        senha.Focus();
                        return;
                    }

                    if (senha.Text != confirmar.Text)
                    {
                        erro.Text = "As senhas não coincidem.";
                        erro.Visible = true;
                        confirmar.Focus();
                        confirmar.SelectAll();
                        return;
                    }

                    salvar.Enabled = false;
                    salvar.Text = "Salvando...";

                    CriptografiaSenha.CriarHash(senha.Text, out string hash, out string salt);
                    try
                    {
                        using (SqlCeConnection conexao = Conexao.ObterConexao())
                        {
                            conexao.Open();
                            using (SqlCeCommand comando = conexao.CreateCommand())
                            {
                                comando.CommandText = @"UPDATE usuarios
SET Senha_Hash = @Hash, Senha_Salt = @Salt
WHERE Id = @Id AND TipoUsuario = 'Bibliotecário(a)'";
                                comando.Parameters.AddWithValue("@Hash", hash);
                                comando.Parameters.AddWithValue("@Salt", salt);
                                comando.Parameters.AddWithValue("@Id", idBibliotecaria);
                                if (comando.ExecuteNonQuery() == 0)
                                    throw new InvalidOperationException("Não foi encontrada uma conta de bibliotecária para redefinir.");
                            }
                        }

                        atualizada = true;
                        dialogo.DialogResult = DialogResult.OK;
                        dialogo.Close();
                    }
                    catch (Exception ex)
                    {
                        erro.Text = "Não foi possível salvar a nova senha. " + ex.Message;
                        erro.Visible = true;
                        salvar.Enabled = true;
                        salvar.Text = "Salvar senha";
                    }
                };

                dialogo.AcceptButton = salvar;
                dialogo.CancelButton = cancelar;
                dialogo.Shown += (s, e) => senha.Focus();
                dialogo.ShowDialog(this);
            }

            return atualizada;
        }

        private Form CriarDialogoRecuperacao(string titulo, Size tamanho)
        {
            var dialogo = new Form
            {
                BackColor = Color.FromArgb(250, 252, 255),
                ClientSize = tamanho,
                Font = new Font("Segoe UI", 9F),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.CenterParent,
                Text = titulo
            };
            // No Load todos os campos já foram adicionados pelo chamador.
            dialogo.Load += (s, e) =>
            {
                ThemeManager.AplicarModoEscuro(dialogo, _corPrimariaAtual);
                ThemeManager.AplicarBarraDeTitulo(dialogo);
            };
            return dialogo;
        }

        private CustomButton CriarBotaoPrimario(string texto, Point local, Size tamanho)
        {
            return new CustomButton
            {
                BaseColor = _corPrimariaAtual,
                BorderRadius = 8,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Location = local,
                Size = tamanho,
                Text = texto
            };
        }

        private Button CriarBotaoSecundario(string texto, Point local, Size tamanho)
        {
            var botao = new Button
            {
                BackColor = Color.FromArgb(250, 252, 255),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = _corPrimariaAtual,
                Location = local,
                Size = tamanho,
                Text = texto,
                UseVisualStyleBackColor = false
            };
            botao.FlatAppearance.BorderColor = ThemeManager.Lighten(_corPrimariaAtual, 0.42f);
            botao.FlatAppearance.MouseOverBackColor = ThemeManager.Lighten(_corPrimariaAtual, 0.92f);
            if (ThemeManager.Escuro)
            {
                botao.BackColor = ThemeManager.Dark.SuperficieAlt;
                botao.ForeColor = ThemeManager.Dark.Texto;
                botao.FlatAppearance.BorderColor = ThemeManager.Dark.Borda;
                botao.FlatAppearance.MouseOverBackColor = ThemeManager.Dark.EntradaHover;
            }
            return botao;
        }

        private Label CriarLabelErro(Point local, Size tamanho)
        {
            return new Label
            {
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = _corErro,
                Location = local,
                Size = tamanho,
                Visible = false
            };
        }

        private void lblVersion_Click(object sender, EventArgs e)
        {
            Hide();

            try
            {
                using (AboutForm popup = new AboutForm())
                    popup.ShowDialog();
            }
            finally
            {
                if (!IsDisposed)
                {
                    Show();
                    Activate();
                }
            }
        }

        private void lblVersion_MouseEnter(object sender, EventArgs e)
        {
            lblVersion.LinkColor = ThemeManager.Lighten(Color.White, 0.08f);
        }

        private void lblVersion_MouseLeave(object sender, EventArgs e)
        {
            lblVersion.LinkColor = Color.White;
        }


        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            const string msg = "Tem certeza de que quer fechar a Aplicação?";
            const string box = "Confirmação de Encerramento";
            var confirma = MessageBox.Show(msg, box, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void cbMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !cbMostrarSenha.Checked;
        }

        private bool ValidarCamposLogin()
        {
            bool valido = true;
            string email = txtEmail.Text.Trim();

            LimparStatusLogin();

            if (string.IsNullOrWhiteSpace(email))
            {
                MostrarErroCampo(txtEmail, lblErroEmail, "Informe seu e-mail.");
                valido = false;
            }
            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MostrarErroCampo(txtEmail, lblErroEmail, "Informe um e-mail válido.");
                valido = false;
            }

            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                MostrarErroCampo(txtSenha, lblErroSenha, "Informe sua senha.");
                valido = false;
            }

            if (!valido)
            {
                if (lblErroEmail.Visible)
                    txtEmail.Focus();
                else
                    txtSenha.Focus();
            }

            return valido;
        }

        private void MostrarErroCampo(RoundedTextBox campo, Label label, string mensagem)
        {
            campo.BorderColor = _corErro;
            campo.BorderFocusColor = _corErro;
            campo.Invalidate();
            label.Text = mensagem;
            label.Visible = true;
        }

        private void LimparErroCampo(RoundedTextBox campo, Label label)
        {
            campo.BorderColor = _corBordaPadrao;
            campo.BorderFocusColor = _corFocoCampo;
            campo.Invalidate();
            label.Text = string.Empty;
            label.Visible = false;
            LimparStatusLogin();
        }

        private void MostrarStatusLogin(string mensagem, bool sucesso = false)
        {
            lblStatusLogin.Text = mensagem;
            if (ThemeManager.Escuro)
            {
                lblStatusLogin.BackColor = sucesso ? Color.FromArgb(27, 62, 43) : Color.FromArgb(74, 36, 38);
                lblStatusLogin.ForeColor = sucesso ? Color.FromArgb(134, 214, 156) : Color.FromArgb(240, 142, 142);
            }
            else
            {
                lblStatusLogin.BackColor = sucesso ? Color.FromArgb(237, 247, 241) : Color.FromArgb(253, 240, 240);
                lblStatusLogin.ForeColor = sucesso ? Color.FromArgb(31, 122, 73) : Color.FromArgb(143, 38, 38);
            }
            lblStatusLogin.Visible = true;
        }

        private void LimparStatusLogin()
        {
            lblStatusLogin.Text = string.Empty;
            lblStatusLogin.Visible = false;
        }

        private void DefinirEstadoAutenticacao(bool autenticando)
        {
            _isAuthenticating = autenticando;
            BtnEntrar.Enabled = !autenticando;
            BtnEntrar.Text = autenticando ? "Entrando..." : "Entrar";
            BtnEntrar.BaseColor = autenticando ? Color.FromArgb(130, 139, 150) : _corPrimariaAtual;
            BtnEntrar.Cursor = autenticando ? Cursors.WaitCursor : Cursors.Hand;
            UseWaitCursor = autenticando;
            BtnEntrar.Invalidate();
        }

        private async void BtnEntrar_Click(object sender, EventArgs e)
        {
            if (_isAuthenticating || !ValidarCamposLogin())
                return;

            DefinirEstadoAutenticacao(true);

            try
            {
                string email = txtEmail.Text.Trim().ToLowerInvariant();
                string senha = txtSenha.Text;

                try
                {
                    using (SqlCeConnection conexao = Conexao.ObterConexao())
                    {
                        conexao.Open();
                        string query = @"SELECT Id, Nome, Senha_hash, Senha_salt FROM usuarios
                    WHERE Email = @email AND TipoUsuario = 'Bibliotecário(a)' AND (Ativo IS NULL OR Ativo = 1)";

                        using (SqlCeCommand comando = new SqlCeCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@email", email);

                            using (SqlCeDataReader reader = comando.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string hashSalvo = reader["Senha_hash"].ToString();
                                    string saltSalvo = reader["Senha_salt"].ToString();
                                    string nomeBibliotecario = reader["Nome"].ToString();

                                    bool senhaCorreta = CriptografiaSenha.VerificarSenha(senha, hashSalvo, saltSalvo);

                                    if (senhaCorreta)
                                    {
                                        Sessao.UsuarioId = Convert.ToInt32(reader["Id"]);
                                          Sessao.NomeUsuario = nomeBibliotecario;
                                         Sessao.Perfil = "Bibliotecário(a)";
                                        Sessao.NomeBibliotecariaLogada = nomeBibliotecario;
                                        LoginForm.UsuarioBibliotecaria = true;

                                        await AtualizarStatusEmprestimosAsync();

                                        cancelar = true;
                                        this.DialogResult = DialogResult.OK;
                                        this.Close();
                                        return;
                                    }
                                    else
                                    {
                                        MostrarErroCampo(txtEmail, lblErroEmail, string.Empty);
                                        MostrarErroCampo(txtSenha, lblErroSenha, string.Empty);
                                        MostrarStatusLogin("E-mail ou senha não conferem. Revise os dados e tente novamente.");
                                        txtSenha.Focus();
                                        txtSenha.SelectAll();
                                        return;
                                    }
                                }
                                else
                                {
                                    MostrarErroCampo(txtEmail, lblErroEmail, string.Empty);
                                    MostrarErroCampo(txtSenha, lblErroSenha, string.Empty);
                                    MostrarStatusLogin("E-mail ou senha não conferem. Revise os dados e tente novamente.");
                                    txtSenha.Focus();
                                    txtSenha.SelectAll();
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MostrarStatusLogin("Não foi possível entrar agora. Verifique o banco de dados e tente novamente.");
                }
            }
            finally
            {
                if (!IsDisposed && BtnEntrar != null)
                    DefinirEstadoAutenticacao(false);
            }
        }




        private void BtnTeste_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            const string msg = "Tem certeza de que quer fechar a Aplicação?";
            const string box = "Confirmação de Encerramento";
            var confirma = MessageBox.Show(msg, box, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
