using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeuProjeto;
using SUB.Forms.Configuracoes;
using SUB.Forms.Livros;
using SUB.Forms.Login;
using SUB.Forms.Relatorio;
using SUB.Forms.Usuario;
using SUB.Froms.Usuario;
using SUB.Models;
using SUB.Properties;
using SUB.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToggleSwitch;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SUB.Forms.Inicio
{
    public partial class MainForm : Form
    {
        private bool menuAnimating = false;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip notifyMenu;
        private Color corPrimaria = ThemeManager.DefaultColor;
        private Button navAtiva;
        private GuidedTourOverlay tutorialOverlay;
        private bool tutorialInicialAgendado;
        private readonly List<Form> formulariosCriadosPeloTutorial = new List<Form>();

        private enum TelaDoTutorial
        {
            Inicio,
            Pessoas,
            CadastroUsuario,
            EditarUsuario,
            Acervo,
            CadastroLivro,
            Emprestimos,
            EmprestimoRapido,
            Devolucao,
            Relatorios,
            Configuracao
        }

        public MainForm()
        {
            InitializeComponent();
            mdiProp();

            var configuracaoInstituicao = InstituicaoConfiguracaoStore.Carregar();
            if (configuracaoInstituicao != null)
            {
                try
                {
                    corPrimaria = ColorTranslator.FromHtml(configuracaoInstituicao.CorPrimaria ?? "#1F487E");
                    if (corPrimaria.IsEmpty)
                        corPrimaria = Color.FromArgb(31, 72, 126);
                }
                catch
                {
                    corPrimaria = Color.FromArgb(31, 72, 126);
                }
                menu.BackColor = corPrimaria;
                Text = configuracaoInstituicao.Nome;

                SubLogoManager.SetImage(picLogoMenu, SubLogoVariant.Square, corPrimaria, corPrimaria);
            }

            if (InstituicaoConfiguracaoStore.CorrupcaoDetectada)
            {
                MessageBox.Show(
                    "O arquivo de configuração da instituição estava danificado e não pôde ser lido.\n" +
                    "O tema padrão foi aplicado.\n\n" +
                    "Uma cópia do arquivo foi guardada em:\n" +
                    (InstituicaoConfiguracaoStore.BackupCorrompidoPath ?? "(cópia não disponível)"),
                    "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            menu.BackColor = corPrimaria;
            ThemeManager.Apply(this, corPrimaria);
            VincularEventosNavegacao();
            AplicarTemaDaNavegacao();
            EventosGlobais.ConfiguracaoSalva += MainForm_ConfiguracaoSalva;
            EventosGlobais.TutorialSolicitado += MainForm_TutorialSolicitado;



            //aredondar cantos e desenhar borda cinza

            int meuRaio = 15;
            this.ArredondarCantos(meuRaio);
            this.DesenharBorda(Color.FromArgb(31, 72, 126), meuRaio, 1);



            // Inicializa NotifyIcon e menu de contexto
            notifyMenu = new ContextMenuStrip();
            var abrirItem = new ToolStripMenuItem("Abrir");
            var sairItem = new ToolStripMenuItem("Sair");

            abrirItem.Click += (s, e) => RestoreFromTray();
            sairItem.Click += (s, e) =>
            {
                // remove o ícone antes de sair para não deixar resíduo
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
                Application.Exit();
            };

            notifyMenu.Items.Add(abrirItem);
            notifyMenu.Items.Add(new ToolStripSeparator());
            notifyMenu.Items.Add(sairItem);

            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = false,
                Text = configuracaoInstituicao?.Nome ?? "Biblioteca Escolar",
                ContextMenuStrip = notifyMenu
            };
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            
        }

        private void VincularEventosNavegacao()
        {
            btnInicio.Click += (s, e) => DefinirNavegacaoAtiva(btnInicio);
            btnUsuario.Click += (s, e) => DefinirNavegacaoAtiva(btnUsuario);
            btnLivro.Click += (s, e) => DefinirNavegacaoAtiva(btnLivro);
            btnRel.Click += (s, e) => DefinirNavegacaoAtiva(btnRel);
            btnConfiguracao.Click += (s, e) => DefinirNavegacaoAtiva(btnConfiguracao);
            VincularItemDeNavegacao(btnUser, btnUsuario);
            VincularItemDeNavegacao(btnUserCad, btnUsuario);
            VincularItemDeNavegacao(btnUserEdit, btnUsuario);
            VincularItemDeNavegacao(btnLivros, btnLivro);
            VincularItemDeNavegacao(btnEmprestimo, btnLivro);
            VincularItemDeNavegacao(btnEmprestimoRap, btnLivro);
            VincularItemDeNavegacao(btnLivroCad, btnLivro);
            VincularItemDeNavegacao(btnDev, btnLivro);
        }

        private void VincularItemDeNavegacao(Button item, Button grupo)
        {
            item.Click += (s, e) =>
            {
                DefinirNavegacaoAtiva(grupo);
            };
        }

        private void AplicarTemaDaNavegacao()
        {
            Color baseMenu = corPrimaria;
            Color itemFilho = ThemeManager.Darken(baseMenu, 0.08F);
            Color hover = ThemeManager.Lighten(baseMenu, 0.12F);
            Color texto = ThemeManager.GetContrastColor(baseMenu);

            menu.BackColor = baseMenu;
            panel1.BackColor = baseMenu;
            lblMarcaMenu.ForeColor = texto;
            lblContextoMenu.ForeColor = ThemeManager.Lighten(texto, 0.18F);

            foreach (var botao in new[] { btnInicio, btnUsuario, btnLivro, btnRel, btnConfiguracao })
                EstilizarBotaoMenu(botao, baseMenu, hover, texto);

            foreach (var botao in new[] { btnUser, btnUserCad, btnUserEdit, btnLivros, btnEmprestimo, btnEmprestimoRap, btnLivroCad, btnDev })
                EstilizarBotaoMenu(botao, itemFilho, hover, texto);

            EstilizarBotaoMenu(btnSair, ThemeManager.Darken(baseMenu, 0.14F), hover, texto);
            if (navAtiva != null)
                DefinirNavegacaoAtiva(navAtiva);
        }

        private static void EstilizarBotaoMenu(Button botao, Color baseColor, Color hoverColor, Color foreColor)
        {
            botao.BackColor = baseColor;
            botao.ForeColor = foreColor;
            botao.FlatAppearance.MouseOverBackColor = hoverColor;
            botao.FlatAppearance.MouseDownBackColor = ThemeManager.Darken(baseColor, 0.08F);
        }

        private void DefinirNavegacaoAtiva(Button botao)
        {
            if (botao == null)
                return;

            navAtiva = botao;
            AplicarTemaDaNavegacaoSemRecursao();
        }

        private void AplicarTemaDaNavegacaoSemRecursao()
        {
            Color baseMenu = corPrimaria;
            Color itemFilho = ThemeManager.Darken(baseMenu, 0.08F);
            Color hover = ThemeManager.Lighten(baseMenu, 0.12F);
            Color texto = ThemeManager.GetContrastColor(baseMenu);

            foreach (var botao in new[] { btnInicio, btnUsuario, btnLivro, btnRel, btnConfiguracao })
                EstilizarBotaoMenu(botao, botao == navAtiva ? ThemeManager.Lighten(baseMenu, 0.10F) : baseMenu, hover, texto);
            foreach (var botao in new[] { btnUser, btnUserCad, btnUserEdit, btnLivros, btnEmprestimo, btnEmprestimoRap, btnLivroCad, btnDev })
                EstilizarBotaoMenu(botao, itemFilho, hover, texto);
            EstilizarBotaoMenu(btnSair, ThemeManager.Darken(baseMenu, 0.14F), hover, texto);
        }

        #region Componentes de inicialização

        private void tamanho()
        {
            this.Width = 1440; this.Height = 800;
        }
        private Size tamanhoOriginal;
        private Point localOriginal;
        private bool maximizado = false;
        

        //Funções da API para movimentar a aba
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        //Nome dos Forms
        InicioForm inicio;
        UsuarioForm usuario;
        LivrosForm livros;
        RelForm rel;
        EmprestimoForm emprestimo;
        EmprestimoRapidoForm emprestimoRap;
        CadastroLivroForm cadastroLivro;
        DevoluçãoForm devolução;
        CadUsuario usuarioCad; 
        public EditarUsuarioForm usuarioEdit;
        ConfiguracaoForm configuracao;

        //Proporção dos Forms
        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(232, 234, 237);
        }

        //Variável criada para inicialização do Form de inicio no MDI
        private void btnInicio_Click(object sender, EventArgs e)
        {
            btnIn();
        }

        //Função de maximizar/restaurar o Form
        private void AlternarMaximizado()
        {
            
            if (!maximizado)
            {
                tamanhoOriginal = this.Size;
                localOriginal = this.Location;

                Rectangle areaTrabalho = Screen.FromHandle(this.Handle).WorkingArea; // exclui barra de tarefas
                this.Location = areaTrabalho.Location;
                this.Size = areaTrabalho.Size;

                maximizado = true;
            }
            else
            {
                this.Size = tamanhoOriginal;
                this.Location = localOriginal;

                maximizado = false;
            }
        }

        private void PanelControl_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChecarMapeamentoPendenteAoInicializar()
        {
            try
            {
                AppPaths.EnsureFolders();
                string pasta = AppPaths.MappingFolder;
                string arquivoMap = Path.Combine(pasta, $"mapeamento_{DateTime.Now.Year}.txt");
                string marcador = Path.Combine(pasta, "app_instalado_em.txt");

                // primeira execução: criar marcador e não abrir mapeamento
                if (!File.Exists(marcador))
                {
                    File.WriteAllText(marcador, DateTime.Now.Year.ToString());
                    return;
                }

                // se existe arquivo de mapeamento e está pendente -> perguntar
                if (File.Exists(arquivoMap))
                {
                    try
                    {
                        var json = File.ReadAllText(arquivoMap);
                        var jObj = JObject.Parse(json);
                        var status = (string)jObj["Status"];

                        if (string.Equals(status, "pendente", StringComparison.OrdinalIgnoreCase))
                        {
                            var msg = $"Existe um mapeamento pendente para o ano {DateTime.Now.Year}.\nDeseja executar o mapeamento agora (Sim) ou lembrar depois (Não)?";
                            var resp = MessageBox.Show(msg, "Mapeamento pendente", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (resp == DialogResult.Yes)
                            {
                                AbrirMapeamentoModal();
                            }
                            // se escolher "Não", nada acontece; aparecerá novamente no próximo startup
                        }
                    }
                    catch
                    {
                        // ignora erro de leitura para não bloquear app
                    }
                    return;
                }

                // se não existe arquivo de mapeamento e app_instalado_em existe com ano < anoAtual -> primeiro ano após instalação: abrir e gerar automaticamente
                var instaladoStr = File.ReadAllText(marcador).Trim();
                if (int.TryParse(instaladoStr, out int anoInstalado))
                {
                    var anoAtual = DateTime.Now.Year;
                    if (anoInstalado < anoAtual)
                    {
                        var msg = $"É necessário realizar o mapeamento de turmas para o ano {anoAtual}.\nDeseja executar agora (Sim) ou mais tarde (Não)?";
                        var resp = MessageBox.Show(msg, "Mapeamento Obrigatório", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        if (resp == DialogResult.Yes)
                        {   
                            AbrirMapeamentoModal();
                        }
                        // se escolher "Não", aparecerá novamente no próximo startup
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Erro checar mapeamento: {ex.Message}");
            }
        }
        private void AbrirMapeamentoModal()
        {
            var mapeamentoForm = new SUB.Forms.Usuario.MapeamentoDeTurmasWizardForm();

            // Configurar como modal dialog
            mapeamentoForm.StartPosition = FormStartPosition.CenterParent;
            mapeamentoForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            mapeamentoForm.MaximizeBox = false;
            mapeamentoForm.MinimizeBox = false;
            mapeamentoForm.ShowInTaskbar = false;

            // Abrir como modal - não permite clicar fora
            var resultado = mapeamentoForm.ShowDialog(this);

            if (resultado == DialogResult.OK)
            {
                
                MessageBox.Show(
                    "Mapeamento de turmas concluído com sucesso!",
                    "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (resultado == DialogResult.Cancel)
            {
                MessageBox.Show(
                    "Mapeamento cancelado. Será solicitado novamente na próxima inicialização.",
                    "Mapeamento Cancelado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Botões
        //Form Inicio
        public void btnIn()
        {
            // evita criar múltiplas instâncias do Inicio
            if (inicio == null || inicio.IsDisposed)
            {
                inicio = new InicioForm();
                inicio.FormClosed += Inicio_FormClosed;
            }

            OpenChild(inicio, keepPreviousHidden: false);
        }

        private void Inicio_FormClosed(object sender, FormClosedEventArgs e)
        {
            inicio = null;
            if (activeChild == null) activeChild = null;
        }

        #region Form do Usuários

        
        //Form usuário
        private async void btnUsuario_Click(object sender, EventArgs e)
        {

            if (menuAnimating) return;      // já estamos animando -> ignora
            menuAnimating = true;
            btnUsuario.Enabled = false;
            btnLivro.Enabled = true;

            if (livroContainer.Height > 60)
            {
                livroTransition.Start();
                await Task.Delay(150);
            }

            userTransition.Start();

            await Task.Delay(120);
            menuAnimating = false;
            btnUsuario.Enabled = true;
            // após a animação, garanta que o menu receba foco para não ativar outro MDI child
            menu.Focus();


        }

        // MainForm.cs
        public void SetUserButtonsEnabled(bool userEnabled, bool userEditEnabled)
        {
            btnUser.Enabled = userEnabled;
            btnUserEdit.Enabled = userEditEnabled;
        }

       

        //Botão de cadastro do usuário
        public void btnUser_Click(object sender, EventArgs e)
        {
            if (usuario == null || usuario.IsDisposed)
            {
                usuario = new UsuarioForm();
                usuario.FormClosed += Usuario_FormClosed;
            }
            OpenChild(usuario, keepPreviousHidden: true); // keepPreviousHidden se quiser manter estado do anterior (opcional)

        }

        //Botão de cadastro do usuário
        private void btnUserCad_Click(object sender, EventArgs e)
        {
            if (usuarioCad == null || usuarioCad.IsDisposed)
            {
                usuarioCad = new CadUsuario();

                // evitar múltiplos +=
                usuarioCad.UsuarioCriado -= UsuarioCad_UsuarioCriado;
                usuarioCad.UsuarioCriado += UsuarioCad_UsuarioCriado;

                usuarioCad.FormClosed += UsuarioCad_FormClosed;
            }

            OpenChild(usuarioCad, keepPreviousHidden: true);
        }

        private void UsuarioCad_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (usuarioCad != null)
            {
                usuarioCad.UsuarioCriado -= UsuarioCad_UsuarioCriado;
            }
            usuarioCad = null;
        }

        private void UsuarioCad_UsuarioCriado(object sender, EventArgs e)
        {
            // Procura se o UsuarioForm já está aberto como MDI child
            var usuarioForm = this.MdiChildren.OfType<SUB.Forms.Usuario.UsuarioForm>().FirstOrDefault();
            if (usuarioForm != null)
            {
                // chama método público para recarregar a grid
                usuarioForm.RefreshGrid();
            }
        }

        //Botão de edição do usuário
        public void btnUserEdit_Click(object sender, EventArgs e)
        {
            if (usuarioEdit == null || usuarioEdit.IsDisposed)
            {
                usuarioEdit = new EditarUsuarioForm();
                usuarioEdit.FormClosed += UsuarioEdit_FormClosed;
            }
            OpenChild(usuarioEdit, keepPreviousHidden: true);
        }

        private void UsuarioEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResetarUsuarioEdit();
        }


        public void ResetarUsuarioEdit()
        {
            usuarioEdit = null;
        }

        //Botão Usuário(Biblioteca)

        #endregion

        private void Usuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            usuario = null;
        }

        #region Form dos Livros
        //Botão Expansão do Livro
        public async void btnLivro_Click(object sender, EventArgs e)
        {
            if (menuAnimating) return;
            btnLivro.Enabled = false;
            menuAnimating = true;
            
            btnUsuario.Enabled = true;

            if (userContainer.Height > 60)
            {
                userTransition.Start();
                await Task.Delay(120);
            }

            livroTransition.Start();
            await Task.Delay(150);
            
            menuAnimating = false;
            btnLivro.Enabled = true;
            menu.Focus();
        }

        //Botão Livro(Bilbioteca)
        private void btnLivros_Click(object sender, EventArgs e)
        {
            if (livros == null || livros.IsDisposed)
            {
                livros = new LivrosForm();
                livros.FormClosed += Livros_FormClosed;
            }
            OpenChild(livros, keepPreviousHidden: true);
           livros.btnProcurar_Click(null, null); // Carrega lista inicial
        }
        private void Livros_FormClosed(object sender, FormClosedEventArgs e)
        {
            livros = null;
        }

        //Botão de empréstimo do livro
        private void btnEmprestimo_Click(object sender, EventArgs e)
        {
            if (emprestimo == null || emprestimo.IsDisposed)
            {
                emprestimo = new EmprestimoForm();
                emprestimo.FormClosed += Emprestimo_FormClosed;
            }
            OpenChild(emprestimo, keepPreviousHidden: true);
        }

        private void Emprestimo_FormClosed(object sender, FormClosedEventArgs e)
        {
            emprestimo = null;
        }

        //Botão Empréstimo Rápido
        public void btnEmprestimoRap_Click(object sender, EventArgs e)
        {
            if (emprestimoRap == null || emprestimoRap.IsDisposed)
            {
                emprestimoRap = new EmprestimoRapidoForm();
                emprestimoRap.FormClosed += EmprestimoRap_FormClosed;
            }
            OpenChild(emprestimoRap, keepPreviousHidden: true);


        }

        //Botão de empréstimo rápido
        private void EmprestimoRap_FormClosed(object sender, FormClosedEventArgs e)
        {
            emprestimoRap = null; // corrigido
        }
        private void btnLivroCad_Click(object sender, EventArgs e)
        {
            if (cadastroLivro == null || cadastroLivro.IsDisposed)
            {
                cadastroLivro = new CadastroLivroForm();
                cadastroLivro.FormClosed += CadastroLivro_FormClosed;
            }
            OpenChild(cadastroLivro, keepPreviousHidden: true);


        }

        private void CadastroLivro_FormClosed(object sender, FormClosedEventArgs e)
        {
            cadastroLivro = null;
        }

        //Botão Devolução
        private void btnDev_Click(object sender, EventArgs e)
        {
            if (devolução == null || devolução.IsDisposed)
            {
                devolução = new DevoluçãoForm();
                devolução.FormClosed += Devolução_FormClosed;
            }
            OpenChild(devolução, keepPreviousHidden: true);
        }

        private void Devolução_FormClosed(object sender, FormClosedEventArgs e)
        {
            devolução = null;
        }


        #endregion

        //Form rel
        private void btnRel_Click(object sender, EventArgs e)
        {
            if (rel == null || rel.IsDisposed)
            {
                rel = new RelForm();
                rel.FormClosed += Relatorios_FormClosed;
            }
            OpenChild(rel, keepPreviousHidden: true);

        }

        private void Relatorios_FormClosed(object sender, FormClosedEventArgs e)
        {
            rel = null;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            const string msg = "Tem certeza de que quer finalizar a sessão?";
            const string box = "Confirmação de logout";
            var confirma = MessageBox.Show(msg, box, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma == DialogResult.Yes)
            {
                // sinaliza logout para permitir fechamento real do MainForm
                Program.RequestLogout = true;

                // garante liberar notifyIcon
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }

                // fecha o MainForm (OnFormClosing permitirá o fechamento por causa da flag)
                this.Close();
            }
        }



        #endregion

        #region Interações/Funcionalidades do Form

        #region Control box
        private void picExit_Click(object sender, EventArgs e)
        {
            var confirma = MessageBox.Show(
                "Tem certeza de que quer sair da aplicação?",
                "Confirmação de saída",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirma == DialogResult.Yes)
            {
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
                Application.Exit(); // Fecha completamente o app
            }
        }



        //Funcionalidade dos botões
        private void picMax_Click(object sender, EventArgs e)
        {
            AlternarMaximizado();

            if (maximizado == false)
            {
               picMax.BackgroundImage = Resources.icons8_quadrado_arredondado_20;
            }
            else
            {
               picMax.BackgroundImage = Resources.icons8_verificar_todos_os_20;
            }
        }
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Animação de fundo
        private void picExit_MouseEnter(object sender, EventArgs e)
        {
            picExit.BackColor = Color.Gainsboro;
        }

        private void picExit_MouseLeave(object sender, EventArgs e)
        {
            picExit.BackColor = Color.Transparent;
        }

        private void picMax_MouseEnter(object sender, EventArgs e)
        {
            picMax.BackColor = Color.Gainsboro;
        }

        private void picMax_MouseLeave(object sender, EventArgs e)
        {
            picMax.BackColor = Color.Transparent;
        }

        private void picMin_MouseEnter(object sender, EventArgs e)
        {
            picMin.BackColor = Color.Gainsboro;
        }

        private void picMin_MouseLeave(object sender, EventArgs e)
        {
            picMin.BackColor = Color.Transparent;
        }
        #endregion

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!LoginForm.cancelar)
                return;
            var novidades = new NovidadesAtualizacaoStore(AppPaths.UpdateFolder);
            var registro = novidades.ObterDaVersao(AtualizacaoService.VersaoAtual());
            if (registro == null || registro.Exibida)
                return;
            using (var dialogo = new NovidadesAtualizacaoForm())
            {
                dialogo.CarregarNovidades(registro.Manifesto, instalada: true);
                dialogo.AplicarTema(corPrimaria);
                dialogo.ShowDialog(this);
            }
            try { novidades.MarcarExibida(AtualizacaoService.VersaoAtual()); }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        //Load para fechar o Login
        private void MainForm_Load(object sender, EventArgs e)
        {
            



            btnIn();
            DefinirNavegacaoAtiva(btnInicio);

            if (LoginForm.cancelar == false)
            {
                Application.Exit();
            }

            //Inicializar com tela cheia
            if (LoginForm.cancelar == true)
            {
                AlternarMaximizado();
            }
           

            ChecarMapeamentoPendenteAoInicializar();
            AgendarTutorialInicial();
        }

        private void AgendarTutorialInicial()
        {
            if (tutorialInicialAgendado)
                return;

            tutorialInicialAgendado = true;
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && TutorialStateStore.DeveExibir(Sessao.UsuarioId))
                    IniciarTutorial();
            }));
        }

        private void MainForm_TutorialSolicitado(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed)
                    IniciarTutorial();
            }));
        }

        private void IniciarTutorial()
        {
            if (tutorialOverlay != null && !tutorialOverlay.IsDisposed)
                return;

            ReabilitarBotoesDeNavegacao();
            btnIn();
            DefinirNavegacaoAtiva(btnInicio);
            inicio?.DefinirVisibilidadeDoStatus(false);

            tutorialOverlay = new GuidedTourOverlay();
            tutorialOverlay.Completed += TutorialOverlay_Completed;
            tutorialOverlay.Skipped += TutorialOverlay_Skipped;
            Controls.Add(tutorialOverlay);
            tutorialOverlay.Bounds = ClientRectangle;
            tutorialOverlay.BringToFront();
            tutorialOverlay.Start(CriarEtapasTutorial(), corPrimaria, CapturarFundoDoTutorial,
                picLogoMenu.Image ?? picLogoMenu.BackgroundImage);
        }

        private void TutorialOverlay_Completed(object sender, EventArgs e)
        {
            TutorialStateStore.MarcarComoVisto(Sessao.UsuarioId);
            EncerrarTutorial(retornarAoInicio: true);
        }

        private void TutorialOverlay_Skipped(object sender, EventArgs e)
        {
            TutorialStateStore.MarcarComoVisto(Sessao.UsuarioId);
            EncerrarTutorial(retornarAoInicio: true);
        }

        private void EncerrarTutorial(bool retornarAoInicio)
        {
            if (tutorialOverlay == null)
                return;

            FecharFormulariosCriadosPeloTutorial();
            tutorialOverlay.Completed -= TutorialOverlay_Completed;
            tutorialOverlay.Skipped -= TutorialOverlay_Skipped;
            inicio?.DefinirVisibilidadeDoStatus(true);
            GarantirMenusDoTutorial(false, false);
            if (retornarAoInicio)
            {
                ReabilitarBotoesDeNavegacao();
                btnIn();
                DefinirNavegacaoAtiva(btnInicio);
            }
            Controls.Remove(tutorialOverlay);
            tutorialOverlay.Dispose();
            tutorialOverlay = null;
        }

        private List<TutorialStep> CriarEtapasTutorial()
        {
            return new List<TutorialStep>
            {
                new TutorialStep(
                    "INÍCIO",
                    "Seu painel principal",
                    "Acompanhe a operação da biblioteca e acesse os recursos principais pelo menu lateral.",
                    () => btnInicio,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Inicio, false, false)),
                new TutorialStep(
                    "PESSOAS",
                    "Pessoas",
                    "Use esta seção para consultar, cadastrar e editar os usuários atendidos pela biblioteca.",
                    () => btnUsuario,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Pessoas, true, false)),
                new TutorialStep(
                    "PESSOAS",
                    "Consultar usuários",
                    "Encontre usuários cadastrados e consulte suas informações antes de iniciar uma operação.",
                    () => btnUser,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Pessoas, true, false)),
                new TutorialStep(
                    "PESSOAS",
                    "Cadastrar usuário",
                    "Inclua novos usuários para que eles possam utilizar os serviços da biblioteca.",
                    () => btnUserCad,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.CadastroUsuario, true, false)),
                new TutorialStep(
                    "PESSOAS",
                    "Editar usuário",
                    "Atualize informações de usuários já cadastrados sempre que for necessário.",
                    () => btnUserEdit,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.EditarUsuario, true, false)),
                new TutorialStep(
                    "ACERVO",
                    "Acervo",
                    "Aqui ficam a consulta do acervo e o cadastro dos livros didáticos da instituição.",
                    () => btnLivro,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Acervo, false, true)),
                new TutorialStep(
                    "ACERVO",
                    "Consultar acervo",
                    "Consulte a disponibilidade e os dados dos livros antes de registrar uma operação.",
                    () => btnLivros,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Acervo, false, true)),
                new TutorialStep(
                    "ACERVO",
                    "Cadastrar livro",
                    "Cadastre novos livros para deixá-los disponíveis para empréstimos.",
                    () => btnLivroCad,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.CadastroLivro, false, true)),
                new TutorialStep(
                    "EMPRÉSTIMOS",
                    "Registrar empréstimo",
                    "Registre empréstimos com todos os dados necessários para acompanhar a devolução corretamente.",
                    () => btnEmprestimo,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Emprestimos, false, true)),
                new TutorialStep(
                    "EMPRÉSTIMOS",
                    "Empréstimo rápido",
                    "Use esta opção para registrar rapidamente os empréstimos mais frequentes da rotina.",
                    () => btnEmprestimoRap,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.EmprestimoRapido, false, true)),
                new TutorialStep(
                    "EMPRÉSTIMOS",
                    "Registrar devolução",
                    "Registre a devolução assim que o livro retornar para manter o acervo atualizado.",
                    () => btnDev,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Devolucao, false, true)),
                new TutorialStep(
                    "RELATÓRIOS",
                    "Relatórios",
                    "Consulte o histórico da operação, aplique filtros e exporte as informações que precisar.",
                    () => btnRel,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Relatorios, false, false)),
                new TutorialStep(
                    "CONFIGURAÇÃO",
                    "Configuração",
                    "Altere o tema, mantenha os dados da instituição atualizados e use “Rever tutorial” quando quiser rever esta visão geral.",
                    () => btnConfiguracao,
                    () => PrepararTelaDoTutorial(TelaDoTutorial.Configuracao, false, false))
            };
        }

        private void PrepararTelaDoTutorial(TelaDoTutorial tela, bool pessoasExpandido, bool acervoExpandido)
        {
            ReabilitarBotoesDeNavegacao();

            switch (tela)
            {
                case TelaDoTutorial.Inicio:
                    btnIn();
                    DefinirNavegacaoAtiva(btnInicio);
                    break;

                case TelaDoTutorial.Pessoas:
                    if (usuario == null || usuario.IsDisposed)
                    {
                        usuario = new UsuarioForm();
                        usuario.FormClosed += Usuario_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuario);
                    }
                    OpenChild(usuario, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;

                case TelaDoTutorial.CadastroUsuario:
                    if (usuarioCad == null || usuarioCad.IsDisposed)
                    {
                        usuarioCad = new CadUsuario();
                        usuarioCad.FormClosed += UsuarioCad_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuarioCad);
                    }
                    OpenChild(usuarioCad, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;

                case TelaDoTutorial.EditarUsuario:
                    if (usuarioEdit == null || usuarioEdit.IsDisposed)
                    {
                        usuarioEdit = new EditarUsuarioForm();
                        usuarioEdit.FormClosed += UsuarioEdit_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(usuarioEdit);
                    }
                    OpenChild(usuarioEdit, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnUsuario);
                    break;

                case TelaDoTutorial.Acervo:
                    if (livros == null || livros.IsDisposed)
                    {
                        livros = new LivrosForm();
                        livros.FormClosed += Livros_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(livros);
                    }
                    OpenChild(livros, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;

                case TelaDoTutorial.CadastroLivro:
                    if (cadastroLivro == null || cadastroLivro.IsDisposed)
                    {
                        cadastroLivro = new CadastroLivroForm();
                        cadastroLivro.FormClosed += CadastroLivro_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(cadastroLivro);
                    }
                    OpenChild(cadastroLivro, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;

                case TelaDoTutorial.Emprestimos:
                    if (emprestimo == null || emprestimo.IsDisposed)
                    {
                        emprestimo = new EmprestimoForm();
                        emprestimo.FormClosed += Emprestimo_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(emprestimo);
                    }
                    OpenChild(emprestimo, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;

                case TelaDoTutorial.EmprestimoRapido:
                    if (emprestimoRap == null || emprestimoRap.IsDisposed)
                    {
                        emprestimoRap = new EmprestimoRapidoForm();
                        emprestimoRap.FormClosed += EmprestimoRap_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(emprestimoRap);
                    }
                    OpenChild(emprestimoRap, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;

                case TelaDoTutorial.Devolucao:
                    if (devolução == null || devolução.IsDisposed)
                    {
                        devolução = new DevoluçãoForm();
                        devolução.FormClosed += Devolução_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(devolução);
                    }
                    OpenChild(devolução, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnLivro);
                    break;

                case TelaDoTutorial.Relatorios:
                    if (rel == null || rel.IsDisposed)
                    {
                        rel = new RelForm();
                        rel.FormClosed += Relatorios_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(rel);
                    }
                    OpenChild(rel, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnRel);
                    break;

                case TelaDoTutorial.Configuracao:
                    if (configuracao == null || configuracao.IsDisposed)
                    {
                        configuracao = new ConfiguracaoForm();
                        configuracao.FormClosed += Configuracao_FormClosed;
                        RegistrarFormularioCriadoPeloTutorial(configuracao);
                    }
                    OpenChild(configuracao, keepPreviousHidden: true);
                    DefinirNavegacaoAtiva(btnConfiguracao);
                    break;
            }

            GarantirMenusDoTutorial(pessoasExpandido, acervoExpandido);
            PerformLayout();
            Update();
        }

        private void RegistrarFormularioCriadoPeloTutorial(Form formulario)
        {
            if (formulario != null && !formulariosCriadosPeloTutorial.Contains(formulario))
                formulariosCriadosPeloTutorial.Add(formulario);
        }

        private void FecharFormulariosCriadosPeloTutorial()
        {
            foreach (Form formulario in formulariosCriadosPeloTutorial.ToArray())
            {
                if (!formulario.IsDisposed)
                    formulario.Close();
            }

            formulariosCriadosPeloTutorial.Clear();
            activeChild = null;
        }

        private Bitmap CapturarFundoDoTutorial()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return null;

            Bitmap snapshot = new Bitmap(ClientSize.Width, ClientSize.Height);
            using (Graphics graphics = Graphics.FromImage(snapshot))
            {
                graphics.Clear(BackColor);
                DesenharControleNoSnapshot(graphics, activeChild);
                DesenharControleNoSnapshot(graphics, menu);
                DesenharControleNoSnapshot(graphics, panelControl);
            }
            return snapshot;
        }

        private void DesenharControleNoSnapshot(Graphics graphics, Control control)
        {
            if (control == null || control.IsDisposed || !control.Visible || control.Width <= 0 || control.Height <= 0)
                return;

            using (Bitmap image = new Bitmap(control.Width, control.Height))
            {
                control.DrawToBitmap(image, control.ClientRectangle);
                Point location = RectangleToClient(control.RectangleToScreen(control.ClientRectangle)).Location;
                graphics.DrawImageUnscaled(image, location);
            }
        }

        private void GarantirMenusDoTutorial(bool pessoasExpandido, bool acervoExpandido)
        {
            userTransition.Stop();
            livroTransition.Stop();
            menuAnimating = false;

            userContainer.Height = pessoasExpandido
                ? (userContainer.Tag is int alturaPessoas ? alturaPessoas : 200)
                : 56;
            livroContainer.Height = acervoExpandido
                ? (livroContainer.Tag is int alturaAcervo ? alturaAcervo : 296)
                : 56;
            userExpand = pessoasExpandido;
            livroExpand = acervoExpandido;
            menu.PerformLayout();
        }

        //Locomoção do painel
        private void panelControl_MouseDown(object sender, MouseEventArgs e)
        {
            maximizado = false;
            picMax.BackgroundImage = Resources.icons8_quadrado_arredondado_20;
            tamanho();
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        //Transição de expansão do livro e usuário
        bool userExpand = false;
        bool livroExpand = false;

        // expor leitura do estado para outros forms
        public bool IsLivroExpanded => livroExpand;
        public bool IsMenuAnimating => menuAnimating;

        private void userTransition_Tick(object sender, EventArgs e)
        {
            int expandedHeight = userContainer.Tag is int value ? value : 200;
            if (userExpand == false)
            {
                userContainer.Height += 10;
                if (userContainer.Height >= expandedHeight)
                {
                    userContainer.Height = expandedHeight;
                    userTransition.Stop();
                    userExpand = true;
                }
            }
            else
            {
                userContainer.Height -= 10;
                if (userContainer.Height <= 56)
                {
                    userContainer.Height = 56;
                    userTransition.Stop();
                    userExpand = false;
                }
            }
        }
        private void livroTransition_Tick(object sender, EventArgs e)
        {
            int expandedHeight = livroContainer.Tag is int value ? value : 296;

            if (livroExpand == false)
            {
                livroContainer.Height += 10;
                if (livroContainer.Height >= expandedHeight)
                {
                    livroContainer.Height = expandedHeight;
                    livroTransition.Stop();
                    livroExpand = true;
                }
            }
            else
            {
                livroContainer.Height -= 10;
                if (livroContainer.Height <= 56)
                {
                    livroContainer.Height = 56;
                    livroTransition.Stop();
                    livroExpand = false;
                }
            }
        }




        Form activeChild = null;
        private bool abrindoFilhoMdi;

        /// <summary>
        /// Mostra o form child como único MDI child visível (esconde o anterior),
        /// garante Dock, MdiParent, BringToFront e Activate.
        /// </summary>
        private void OpenChild(Form child, bool keepPreviousHidden = false)
        {
            if (child == null || child.IsDisposed || abrindoFilhoMdi)
                return;

            if (activeChild == child && child.Visible)
            {
                AtualizarDisponibilidadeDaNavegacao(child);
                return;
            }

            abrindoFilhoMdi = true;
            try
            {
                // Se já existe um filho ativo diferente, esconde/fecha ele
                if (activeChild != null && activeChild != child && !activeChild.IsDisposed)
                {
                    if (keepPreviousHidden) activeChild.Hide();
                    else activeChild.Close(); // fecha para liberar recursos (troque por Hide se preferir manter estado)
                }

                // se a instância atual do child foi descartada, tente recriar? (faça fora se necessário)
                child.MdiParent = this;
                child.Dock = DockStyle.Fill;
                ThemeManager.Apply(child, corPrimaria);

                var inicioForm = child as InicioForm;
                if (inicioForm != null)
                    inicioForm.AplicarTema(corPrimaria);

                // Se o form já está criado mas escondido, apenas BringToFront
                if (!child.Visible)
                    child.Show();

                ThemeManager.Apply(child, corPrimaria);
                child.BringToFront();
                child.Activate();

                activeChild = child;
                AtualizarDisponibilidadeDaNavegacao(child);
            }
            catch { /*silent para não quebrar UI*/ }
            finally
            {
                abrindoFilhoMdi = false;
            }
        }

        private void AtualizarDisponibilidadeDaNavegacao(Form child)
        {
            ReabilitarBotoesDeNavegacao();

            Button botaoAtivo = ObterBotaoDeNavegacao(child);
            if (botaoAtivo != null)
                botaoAtivo.Enabled = false;
        }

        private Button ObterBotaoDeNavegacao(Form child)
        {
            if (child is InicioForm) return btnInicio;
            if (child is UsuarioForm) return btnUser;
            if (child is CadUsuario) return btnUserCad;
            if (child is EditarUsuarioForm) return btnUserEdit;
            if (child is LivrosForm) return btnLivros;
            if (child is EmprestimoForm) return btnEmprestimo;
            if (child is EmprestimoRapidoForm) return btnEmprestimoRap;
            if (child is CadastroLivroForm) return btnLivroCad;
            if (child is DevoluçãoForm) return btnDev;
            if (child is RelForm) return btnRel;
            if (child is ConfiguracaoForm) return btnConfiguracao;
            return null;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void RestoreFromTray()
        {
            try
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
                this.Activate();
                this.BringToFront();
            }
            catch { }
        }




        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Evento é estático e o MainForm é recriado a cada login;
            // sem dessinar, o form antigo continuaria recebendo o evento.
            EventosGlobais.ConfiguracaoSalva -= MainForm_ConfiguracaoSalva;
            EventosGlobais.TutorialSolicitado -= MainForm_TutorialSolicitado;

            if (tutorialOverlay != null)
            {
                Controls.Remove(tutorialOverlay);
                tutorialOverlay.Dispose();
                tutorialOverlay = null;
            }

            if (Program.RequestLogout)
            {
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
                base.OnFormClosing(e);
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Fecha normalmente
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
                base.OnFormClosing(e);
            }
            else
            {
                try { notifyIcon.Visible = false; notifyIcon.Dispose(); } catch { }
                base.OnFormClosing(e);
            }
        }




        #endregion

        private void menu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            if (configuracao == null || configuracao.IsDisposed)
            {
                configuracao = new ConfiguracaoForm();
                configuracao.FormClosed += Configuracao_FormClosed;
            }
            OpenChild(configuracao, keepPreviousHidden: true);
        }

        private void ReabilitarBotoesDeNavegacao()
        {
            btnInicio.Enabled = true;
            btnUsuario.Enabled = true;
            btnLivro.Enabled = true;
            btnRel.Enabled = true;
            btnConfiguracao.Enabled = true;
            btnLivros.Enabled = true;
            btnEmprestimo.Enabled = true;
            btnEmprestimoRap.Enabled = true;
            btnLivroCad.Enabled = true;
            btnDev.Enabled = true;
            btnUser.Enabled = true;
            btnUserCad.Enabled = true;
            btnUserEdit.Enabled = true;
        }

        private void Configuracao_FormClosed(object sender, FormClosedEventArgs e)
        {
            configuracao = null;
        }

        private void MainForm_ConfiguracaoSalva(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(ReaplicarConfiguracaoEDashboard));
                return;
            }

            ReaplicarConfiguracaoEDashboard();
        }

        private void ReaplicarConfiguracaoEDashboard()
        {
            ReaplicarConfiguracao();

            // Após salvar, o ConfiguracaoForm já foi fechado (evento disparado
            // depois do Close): abre o dashboard e marca Início como ativo,
            // evitando o MDI em branco.
            ReabilitarBotoesDeNavegacao();
            btnIn();
            DefinirNavegacaoAtiva(btnInicio);
        }

        private void ReaplicarConfiguracao()
        {
            InstituicaoConfiguracao configuracaoInstituicao;
            try
            {
                configuracaoInstituicao = InstituicaoConfiguracaoStore.Carregar();
            }
            catch
            {
                // JSON ilegível: mantém tema/nome atuais.
                // Tratamento definitivo de corrupção pendente de aprovação.
                return;
            }

            if (configuracaoInstituicao != null && !string.IsNullOrWhiteSpace(configuracaoInstituicao.CorPrimaria))
            {
                try
                {
                    corPrimaria = ColorTranslator.FromHtml(configuracaoInstituicao.CorPrimaria);
                    if (corPrimaria.IsEmpty)
                        corPrimaria = ThemeManager.DefaultColor;
                }
                catch
                {
                    corPrimaria = ThemeManager.DefaultColor;
                }
            }
            else
            {
                corPrimaria = ThemeManager.DefaultColor;
            }

            if (configuracaoInstituicao != null && !string.IsNullOrWhiteSpace(configuracaoInstituicao.Nome))
                Text = configuracaoInstituicao.Nome;

            if (notifyIcon != null)
                notifyIcon.Text = configuracaoInstituicao?.Nome ?? "Biblioteca Escolar";

            menu.BackColor = corPrimaria;
            SubLogoManager.SetImage(picLogoMenu, SubLogoVariant.Square, corPrimaria, corPrimaria);
            ThemeManager.Apply(this, corPrimaria);
            AplicarTemaDaNavegacao();

            if (tutorialOverlay != null && !tutorialOverlay.IsDisposed)
                tutorialOverlay.ApplyTheme(corPrimaria);

            foreach (Form child in MdiChildren)
            {
                if (child.IsDisposed)
                    continue;

                ThemeManager.Apply(child, corPrimaria);

                var inicioForm = child as InicioForm;
                if (inicioForm != null)
                    inicioForm.AplicarTema(corPrimaria);
            }

            // Borda da janela: DesenharBorda registra um novo handler de Paint a
            // cada chamada e captura a cor por closure — reaplicar duplicaria o
            // handler. Permanece com a cor fixa original.
        }
    }
}

