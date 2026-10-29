using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using SUB.Forms.Configuracoes;
using SUB.Forms.Livros;
using SUB.Forms.Login;
using SUB.Forms.Relatorio;
using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Properties;
using SUB.Utils;

namespace SUB.Forms.Inicio
{
    public partial class MainForm : Form
    {
        private bool menuAnimating = false;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip notifyMenu;
        private Color corPrimaria = ThemeManager.DefaultColor;
        private Button navAtiva;
        private Color fundoClaroDoForm;
        private Color fundoClaroDoPainelControle;
        public MainForm()
        {
            InitializeComponent();
            fundoClaroDoForm = BackColor;
            fundoClaroDoPainelControle = panelControl.BackColor;
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
                ThemeManager.DefinirModo(configuracaoInstituicao);
                menu.BackColor = corPrimaria;
                Text = configuracaoInstituicao.Nome;
                SubLogoManager.SetImage(picLogoMenu, SubLogoVariant.Square, corPrimaria, corPrimaria);
            }

            AtualizarFundoMdi();
            if (InstituicaoConfiguracaoStore.CorrupcaoDetectada)
            {
                MessageBox.Show("O arquivo de configuração da instituição estava danificado e não pôde ser lido.\n" + "O tema padrão foi aplicado.\n\n" + "Uma cópia do arquivo foi guardada em:\n" + (InstituicaoConfiguracaoStore.BackupCorrompidoPath ?? "(cópia não disponível)"), "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            menu.BackColor = corPrimaria;
            ThemeManager.Apply(this, corPrimaria);
            VincularEventosNavegacao();
            AplicarTemaDaNavegacao();
            EventosGlobais.ConfiguracaoSalva += MainForm_ConfiguracaoSalva;
            EventosGlobais.TutorialSolicitado += MainForm_TutorialSolicitado;
            ThemeManager.TemaDoSistemaAlterado += ThemeManager_TemaDoSistemaAlterado;



            //aredondar cantos e desenhar borda cinza
            int meuRaio = 15;
            this.ArredondarCantos(meuRaio);
            this.DesenharBorda(Color.FromArgb(31, 72, 126), meuRaio, 1);
            // Os botões da janela são só ícones; a dica diz o que cada um faz.
            var dicas = new ToolTip(components);
            dicas.SetToolTip(picMin, "Minimizar");
            dicas.SetToolTip(picMax, "Maximizar ou restaurar");
            dicas.SetToolTip(picExit, "Fechar o SUB");
            // Inicializa NotifyIcon e menu de contexto
            notifyMenu = new ContextMenuStrip();
            var abrirItem = new ToolStripMenuItem("Abrir");
            var sairItem = new ToolStripMenuItem("Sair");
            abrirItem.Click += (s, e) => RestoreFromTray();
            sairItem.Click += (s, e) =>
            {
                // remove o ícone antes de sair para não deixar resíduo
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

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
            foreach (var botao in new[]
            {
                btnInicio,
                btnUsuario,
                btnLivro,
                btnRel,
                btnConfiguracao
            }

            )
                EstilizarBotaoMenu(botao, baseMenu, hover, texto);
            foreach (var botao in new[]
            {
                btnUser,
                btnUserCad,
                btnUserEdit,
                btnLivros,
                btnEmprestimo,
                btnEmprestimoRap,
                btnLivroCad,
                btnDev
            }

            )
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
            foreach (var botao in new[]
            {
                btnInicio,
                btnUsuario,
                btnLivro,
                btnRel,
                btnConfiguracao
            }

            )
                EstilizarBotaoMenu(botao, botao == navAtiva ? ThemeManager.Lighten(baseMenu, 0.10F) : baseMenu, hover, texto);
            foreach (var botao in new[]
            {
                btnUser,
                btnUserCad,
                btnUserEdit,
                btnLivros,
                btnEmprestimo,
                btnEmprestimoRap,
                btnLivroCad,
                btnDev
            }

            )
                EstilizarBotaoMenu(botao, itemFilho, hover, texto);
            EstilizarBotaoMenu(btnSair, ThemeManager.Darken(baseMenu, 0.14F), hover, texto);
        }

        private void tamanho()
        {
            // O tamanho base foi desenhado para 96 DPI. Ao restaurar a janela,
            // respeitamos a escala atual e nunca ultrapassamos a área útil do monitor.
            float escalaDpi = DeviceDpi > 0 ? DeviceDpi / 96f : 1f;
            Size desejado = new Size((int)Math.Round(1440 * escalaDpi), (int)Math.Round(800 * escalaDpi));
            Size areaUtil = Screen.FromHandle(Handle).WorkingArea.Size;
            Size = new Size(Math.Min(desejado.Width, areaUtil.Width), Math.Min(desejado.Height, areaUtil.Height));
        }
        private bool maximizado => WindowState == FormWindowState.Maximized;

        //Funções da API para movimentar a aba
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        // FormBorderStyle.None por padrão cria a janela sem WS_SIZEFRAME: mesmo respondendo
        // WM_NCHITTEST com HTLEFT/HTTOP/etc., o Windows ignora o pedido de resize se a classe
        // da janela não foi marcada como redimensionável nesse estilo.
        private const int WS_SIZEFRAME = 0x00040000;
        private const int WS_MAXIMIZEBOX = 0x00010000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_SIZEFRAME | WS_MAXIMIZEBOX;
                return cp;
            }
        }

        // Sem borda nativa (FormBorderStyle.None), o WinForms não sabe até onde a janela
        // pode crescer ao maximizar — sem isto ela cobriria a barra de tarefas.
        private void AtualizarLimitesDeMaximizacao()
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
        }

        // Redimensionar arrastando a borda. FormBorderStyle.None tira esse comportamento de
        // graça; respondendo WM_NCHITTEST com o código de borda certo, o Windows volta a
        // tratar essas faixas como se fossem a moldura nativa (cursor de resize incluído).
        private const int WM_NCHITTEST = 0x84;
        private const int ESPESSURA_BORDA_RESIZE = 6;

        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST && WindowState == FormWindowState.Normal)
            {
                // LPARAM empacota as coordenadas de tela como dois shorts com sinal
                // (LOWORD/HIWORD); um cast direto para int quebra em monitores à
                // esquerda/acima do principal, onde a coordenada é negativa.
                int lparam = m.LParam.ToInt32();
                short telaX = unchecked((short)(lparam & 0xFFFF));
                short telaY = unchecked((short)((lparam >> 16) & 0xFFFF));
                Point cursor = PointToClient(new Point(telaX, telaY));

                bool dentro = cursor.X >= 0 && cursor.Y >= 0 && cursor.X < ClientSize.Width && cursor.Y < ClientSize.Height;
                if (dentro)
                {
                    bool perto_esquerda = cursor.X <= ESPESSURA_BORDA_RESIZE;
                    bool perto_direita = cursor.X >= ClientSize.Width - ESPESSURA_BORDA_RESIZE;
                    bool perto_topo = cursor.Y <= ESPESSURA_BORDA_RESIZE;
                    bool perto_baixo = cursor.Y >= ClientSize.Height - ESPESSURA_BORDA_RESIZE;

                    if (perto_topo && perto_esquerda) { m.Result = (IntPtr)HTTOPLEFT; return; }
                    if (perto_topo && perto_direita) { m.Result = (IntPtr)HTTOPRIGHT; return; }
                    if (perto_baixo && perto_esquerda) { m.Result = (IntPtr)HTBOTTOMLEFT; return; }
                    if (perto_baixo && perto_direita) { m.Result = (IntPtr)HTBOTTOMRIGHT; return; }
                    if (perto_esquerda) { m.Result = (IntPtr)HTLEFT; return; }
                    if (perto_direita) { m.Result = (IntPtr)HTRIGHT; return; }
                    if (perto_topo) { m.Result = (IntPtr)HTTOP; return; }
                    if (perto_baixo) { m.Result = (IntPtr)HTBOTTOM; return; }
                }
            }

            base.WndProc(ref m);
        }

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
            AtualizarFundoMdi();
        }

        private void AtualizarFundoMdi()
        {
            var cliente = Controls.OfType<MdiClient>().FirstOrDefault();
            if (cliente != null)
                cliente.BackColor = ThemeManager.Escuro ? ThemeManager.Dark.Fundo : Color.FromArgb(232, 234, 237);
        }

        private Color CorDeRealceDosControles()
        {
            return ThemeManager.Escuro ? ThemeManager.Dark.EntradaHover : Color.Gainsboro;
        }

        //Variável criada para inicialização do Form de inicio no MDI
        private void btnInicio_Click(object sender, EventArgs e)
        {
            btnIn();
        }

        //Função de maximizar/restaurar o Form
        private void AlternarMaximizado()
        {
            AtualizarLimitesDeMaximizacao();
            WindowState = maximizado ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void PanelControl_MouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ChecarMapeamentoPendenteAoInicializar()
        {
            try
            {
                var estado = new MapeamentoAnualStore().ObterEstadoDoAno(DateTime.Now.Year);
                if (!estado.DeveLembrar)
                    return;
                string mensagem = estado.Status == MapeamentoAnualStore.RequerRevisao ? $"O registro local do mapeamento de {estado.Ano} precisou de revisão. Nenhuma conclusão foi presumida.\nDeseja revisar o mapeamento agora ou ser lembrado no próximo acesso?" : $"Existe um mapeamento pendente para o ano {estado.Ano}.\nDeseja executá-lo agora ou ser lembrado no próximo acesso?";
                var resposta = MessageBox.Show(mensagem, "Mapeamento anual", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                if (resposta == DialogResult.Yes)
                    AbrirMapeamentoModal();
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
                MessageBox.Show("Mapeamento de turmas concluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultado == DialogResult.Cancel)
            {
                MessageBox.Show("Mapeamento cancelado. Será solicitado novamente na próxima inicialização.", "Mapeamento Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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
            if (activeChild == null)
                activeChild = null;
        }

        //Form usuário
        private async void btnUsuario_Click(object sender, EventArgs e)
        {
            if (menuAnimating)
                return; // já estamos animando -> ignora
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
        private void Usuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            usuario = null;
        }

        //Botão Expansão do Livro
        public async void btnLivro_Click(object sender, EventArgs e)
        {
            if (menuAnimating)
                return;
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
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

                // fecha o MainForm (OnFormClosing permitirá o fechamento por causa da flag)
                this.Close();
            }
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            var confirma = MessageBox.Show("Tem certeza de que quer sair da aplicação?", "Confirmação de saída", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirma == DialogResult.Yes)
            {
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

                Application.Exit(); // Fecha completamente o app
            }
        }

        //Funcionalidade dos botões
        private void picMax_Click(object sender, EventArgs e)
        {
            AlternarMaximizado();
        }

        // Mantém o ícone do botão coerente também quando o estado muda sem passar pelo
        // clique (arrastar a aba até o topo maximiza, arrastar de volta restaura).
        private void MainForm_Resize(object sender, EventArgs e)
        {
            picMax.BackgroundImage = maximizado
                ? Resources.icons8_verificar_todos_os_20
                : Resources.icons8_quadrado_arredondado_20;
        }

        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Animação de fundo
        private void picExit_MouseEnter(object sender, EventArgs e)
        {
            picExit.BackColor = CorDeRealceDosControles();
        }

        private void picExit_MouseLeave(object sender, EventArgs e)
        {
            picExit.BackColor = Color.Transparent;
        }

        private void picMax_MouseEnter(object sender, EventArgs e)
        {
            picMax.BackColor = CorDeRealceDosControles();
        }

        private void picMax_MouseLeave(object sender, EventArgs e)
        {
            picMax.BackColor = Color.Transparent;
        }

        private void picMin_MouseEnter(object sender, EventArgs e)
        {
            picMin.BackColor = CorDeRealceDosControles();
        }

        private void picMin_MouseLeave(object sender, EventArgs e)
        {
            picMin.BackColor = Color.Transparent;
        }

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

            try
            {
                novidades.MarcarExibida(AtualizacaoService.VersaoAtual());
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
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

        //Locomoção do painel
        private void panelControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // Com WS_SIZEFRAME ativo (ver CreateParams) o próprio Windows já trata HTCAPTION
            // como a titlebar nativa: arrastar até o topo maximiza, até a lateral faz snap de
            // metade de tela, e arrastar de volta restaura sob o cursor. Forçar WindowState
            // aqui por conta própria conflita com esse snap nativo e deixa a janela com
            // tamanho inconsistente (sobra conteúdo de outra janela visível atrás).
            AtualizarLimitesDeMaximizacao();
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
                    if (keepPreviousHidden)
                        activeChild.Hide();
                    else
                        activeChild.Close(); // fecha para liberar recursos (troque por Hide se preferir manter estado)
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
            catch
            { /*silent para não quebrar UI*/
            }
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
            if (child is InicioForm)
                return btnInicio;
            if (child is UsuarioForm)
                return btnUser;
            if (child is CadUsuario)
                return btnUserCad;
            if (child is EditarUsuarioForm)
                return btnUserEdit;
            if (child is LivrosForm)
                return btnLivros;
            if (child is EmprestimoForm)
                return btnEmprestimo;
            if (child is EmprestimoRapidoForm)
                return btnEmprestimoRap;
            if (child is CadastroLivroForm)
                return btnLivroCad;
            if (child is DevoluçãoForm)
                return btnDev;
            if (child is RelForm)
                return btnRel;
            if (child is ConfiguracaoForm)
                return btnConfiguracao;
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
            catch
            {
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Evento é estático e o MainForm é recriado a cada login;
            // sem dessinar, o form antigo continuaria recebendo o evento.
            EventosGlobais.ConfiguracaoSalva -= MainForm_ConfiguracaoSalva;
            EventosGlobais.TutorialSolicitado -= MainForm_TutorialSolicitado;
            ThemeManager.TemaDoSistemaAlterado -= ThemeManager_TemaDoSistemaAlterado;

            if (tutorialOverlay != null)
            {
                Controls.Remove(tutorialOverlay);
                tutorialOverlay.Dispose();
                tutorialOverlay = null;
            }

            if (Program.RequestLogout)
            {
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

                base.OnFormClosing(e);
                return;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Fecha normalmente
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

                base.OnFormClosing(e);
            }
            else
            {
                try
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                }
                catch
                {
                }

                base.OnFormClosing(e);
            }
        }

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
                BeginInvoke(new Action(() => ReaplicarConfiguracaoEDashboard()));
                return;
            }

            ReaplicarConfiguracaoEDashboard();
        }

        private void ReaplicarConfiguracaoEDashboard(bool forcarRecriacaoDasTelas = false)
        {
            ReaplicarConfiguracao(forcarRecriacaoDasTelas);

            // Após salvar, o ConfiguracaoForm já foi fechado (evento disparado
            // depois do Close): abre o dashboard e marca Início como ativo,
            // evitando o MDI em branco.
            ReabilitarBotoesDeNavegacao();
            btnIn();
            DefinirNavegacaoAtiva(btnInicio);
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

            ReaplicarConfiguracaoEDashboard(true);
        }

        private void ReaplicarConfiguracao(bool forcarRecriacaoDasTelas = false)
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

            // As cores claras originais das telas não são guardadas; ao trocar de modo as telas
            // abertas são fechadas e recriadas sob demanda já no modo novo.
            bool novoModoEscuro = ThemeManager.ResolverModoEscuro(configuracaoInstituicao);
            if (forcarRecriacaoDasTelas || novoModoEscuro != ThemeManager.Escuro)
            {
                if (!forcarRecriacaoDasTelas)
                    ThemeManager.DefinirModo(configuracaoInstituicao);
                foreach (Form filho in MdiChildren.ToArray())
                    filho.Close();
                activeChild = null;
                BackColor = fundoClaroDoForm;
                panelControl.BackColor = fundoClaroDoPainelControle;
                AtualizarFundoMdi();
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
