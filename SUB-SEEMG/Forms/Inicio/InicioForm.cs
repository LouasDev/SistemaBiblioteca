using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Inicio
{
    public partial class InicioForm : Form
    {
        private Color dashboardThemeColor = ThemeManager.DefaultColor;
        public void AplicarTema(Color corPrimaria)
        {
            Color cor = ThemeManager.Normalize(corPrimaria);
            dashboardThemeColor = cor;
            ThemeManager.Apply(this, cor);
            lblOla.ForeColor = ThemeManager.GetLabelColor(cor);
            AplicarTemaNoDashboard();
        }

        private (int Id, string Nome, string Turma, int Qtd) topUsuarioMaisEmprestimos = (0, "-", "", 0);
        // controls dinâmicos
        private Panel actionsPanel; // painel no canto superior direito com botão de Empréstimo Rápido
        private Button btnEmprestimoRapido; // botão principal no topo-direito, bem visível (maior)
        private Label lblStatusSmall;
        private int alturaCardsDesenho;
        private ToolTip formToolTip;
        // auto-refresh timer
        private System.Windows.Forms.Timer timerAutoRefresh;
        private Dictionary<string, bool> mainButtonsOriginalState = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<DataGridView, Label> emptyGridLabels = new Dictionary<DataGridView, Label>();
        public InicioForm()
        {
            InitializeComponent();
            ThemeManager.ApplyConfiguredTheme(this);
            this.KeyPreview = true;
            this.KeyDown += InicioForm_KeyDown;
            SUB.Utils.EventosGlobais.LivroCadastradoOuAlterado += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.BibliotecariaCadastrada += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.ProfessorCadastrado += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.LivroDidaticoCadastrado += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.LivroDevolvido += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.EmprestimoProrrogado += (s, e) => _ = CarregarEstatisticasAsync();
            SUB.Utils.EventosGlobais.EmprestimoRealizado += (s, e) => SafeInvoke(() => _ = CarregarEstatisticasAsync());
            this.Activated += (s, e) => _ = CarregarEstatisticasAsync();
        // timerAutoRefresh pode ser mantido ou removido conforme sua preferência
        }

        private void SafeInvoke(Action action)
        {
            if (this.IsHandleCreated && !this.IsDisposed && !this.Disposing)
            {
                try
                {
                    this.BeginInvoke(action);
                }
                catch
                { /* nada — evita crash se o form estiver sendo fechado */
                }
            }
        }

        private void InicioForm_Load(object sender, EventArgs e)
        {
            AppPaths.EnsureFolders();
            // relógio
            timerRelogio.Interval = 1000; // 1 segundo
            timerRelogio.Tick += timerRelogio_Tick;
            timerRelogio.Start();
            AtualizarRelogio();
            // tooltip
            formToolTip = new ToolTip
            {
                AutoPopDelay = 6000,
                InitialDelay = 300,
                ReshowDelay = 150,
                IsBalloon = false
            };
            // auto-refresh:
            timerAutoRefresh = new System.Windows.Forms.Timer();
            timerAutoRefresh.Interval = 20000; //
            timerAutoRefresh.Tick += (s, ev) =>
            {
                _ = CarregarEstatisticasAsync();
            };
            timerAutoRefresh.Start();
            // construir UI dinâmica (cards, tabs, botão de empréstimo rápido no topo direito)
            ConstruirDashboardUI();
            // primeira carga
            _ = CarregarEstatisticasAsync();
        }

        private void AtualizarRelogio()
        {
            DateTime agora = DateTime.Now;
            string saudacao = ObterSaudacao(agora);
            try
            {
                string nomeCompleto = Sessao.NomeBibliotecariaLogada ?? "";
                string primeiroNome = nomeCompleto.Split(' ').FirstOrDefault() ?? "";
                lblOla.Text = $"{saudacao}, {primeiroNome}!";
                lblOla.Font = new System.Drawing.Font("Segoe UI", 20F, FontStyle.Bold);
                lblOla.ForeColor = ThemeManager.GetLabelColor(dashboardThemeColor);
            }
            catch
            {
            }

            string diaSemana = agora.ToString("dddd");
            string data = agora.ToString("dd 'de' MMMM 'de' yyyy");
            string hora = agora.ToString("HH:mm:ss");
            lblRelogio.Text = $"{diaSemana}, {data} - {hora}";
            lblRelogio.Font = new System.Drawing.Font("Segoe UI", 16F, FontStyle.Regular);
            lblRelogio.ForeColor = ThemeManager.Escuro ? ThemeManager.Dark.TextoSuave : Color.FromArgb(60, 60, 60);
            // centralizar relógio após atualizar o texto
            CenterClock();
        }

        // centraliza lblRelogio horizontalmente dentro do panelTop (se existir)
        private void CenterClock()
        {
            try
            {
                if (lblRelogio == null || panelTop == null)
                    return;
                // força medida atualizada
                lblRelogio.AutoSize = true;
                lblRelogio.Refresh();
                int centerX = Math.Max(0, (panelTop.ClientSize.Width - lblRelogio.Width) / 2);
                // respeitar margem superior (aprox. mesma Y atual)
                int y = lblRelogio.Location.Y;
                lblRelogio.Location = new Point(centerX, y);
            }
            catch
            {
            }
        }

        private string ObterSaudacao(DateTime now)
        {
            int hora = now.Hour;
            if (hora >= 5 && hora < 12)
                return "Bom dia";
            if (hora >= 12 && hora < 18)
                return "Boa tarde";
            return "Boa noite";
        }

        private void timerRelogio_Tick(object sender, EventArgs e) => AtualizarRelogio();
        private void SetStatus(string texto)
        {
            if (lblStatusSmall == null)
                return;
            try
            {
                BeginInvoke(new Action(() => lblStatusSmall.Text = texto));
            }
            catch
            {
            }
        }

        public void DefinirVisibilidadeDoStatus(bool visivel)
        {
            if (lblStatusSmall == null || IsDisposed)
                return;
            lblStatusSmall.Visible = visivel;
        }

        private void BtnEmprestimoRapido_Click(object sender, EventArgs e)
        {
            AbrirEmprestimoRapido();
        }

        private void AbrirEmprestimoRapido()
        {
            try
            {
                MainForm mainForm = this.MdiParent as MainForm;
                if (mainForm == null)
                {
                    MessageBox.Show("Não foi possível identificar a janela principal (MainForm).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔥 chama a animação e clique — MAS só expande o menu de livros se ele ainda NÃO estiver expandido
                // (evita fechar o submenu caso já esteja aberto)
                if (!mainForm.IsLivroExpanded && !mainForm.IsMenuAnimating)
                {
                    mainForm.btnLivro_Click(null, EventArgs.Empty);
                }

                // abrir empréstimo rápido sempre (depois de garantir que submenu de livros esteja aberto)
                mainForm.btnEmprestimoRap_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir Empréstimo Rápido: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EmprestimoRap_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Form main = this.FindForm();
                if (main == null)
                    main = this.MdiParent;
                if (main == null)
                    return;
                foreach (var kv in mainButtonsOriginalState)
                    SetButtonEnabledOnForm(main, kv.Key, kv.Value);
                _ = CarregarEstatisticasAsync();
            }
            catch
            { /*silent*/
            }
        }

        private Control FindControlOnForm(Form form, string controlName)
        {
            if (form == null || string.IsNullOrWhiteSpace(controlName))
                return null;
            var found = form.Controls.Find(controlName, true);
            if (found != null && found.Length > 0)
                return found[0];
            var t = form.GetType();
            var field = t.GetField(controlName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (field != null)
            {
                var val = field.GetValue(form);
                if (val is Control c)
                    return c;
            }

            var prop = t.GetProperty(controlName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (prop != null)
            {
                var val = prop.GetValue(form);
                if (val is Control c2)
                    return c2;
            }

            return null;
        }

        private void SetButtonEnabledOnForm(Form form, string controlName, bool enabled)
        {
            var ctrl = FindControlOnForm(form, controlName);
            if (ctrl != null)
                try
                {
                    ctrl.Enabled = enabled;
                }
                catch
                {
                }
        }

        private void InicioForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                e.SuppressKeyPress = true;
                AbrirEmprestimoRapido();
            }
        }

        private void lblResultado_Click(object sender, EventArgs e)
        {
        }
    }
}
