using System;
using System.Drawing;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Configuracoes
{
    public partial class PoliticaEmprestimosEditor : UserControl
    {
        private readonly Dictionary<Control, Color> coresClaras = new Dictionary<Control, Color>();
        private readonly Dictionary<RoundedTextBox, Color[]> coresCampos = new Dictionary<RoundedTextBox, Color[]>();
        private string dicaPadrao;
        private bool preenchendo;
        private bool exibirCartoes = true;
        public event Action<bool> ValidadeAlterada;
        [Browsable(false)]
        public bool ValoresValidos { get; private set; }

        [DefaultValue(true)]
        public bool ExibirCartoes
        {
            get => exibirCartoes;
            set
            {
                exibirCartoes = value;
                if (!value) { pnlFiltrosLimites.BackColor = Color.Transparent; pnlFiltrosPrazos.BackColor = Color.Transparent; }
            }
        }

        public PoliticaEmprestimosEditor()
        {
            InitializeComponent();
            GuardarCores(this);
            dicaPadrao = lblDica.Text;
            ConfigurarContador(numAluno, btnMaisAluno, btnMenosAluno, 1, 100);
            ConfigurarContador(numProfessor, btnMaisProfessor, btnMenosProfessor, 1, 100);
            ConfigurarContador(numBibliotecario, btnMaisBibliotecario, btnMenosBibliotecario, 1, 100);
            ConfigurarContador(numPrazo, btnMaisPrazo, btnMenosPrazo, 1, 365);
            ConfigurarContador(numProrrogacoes, btnMaisProrrogacoes, btnMenosProrrogacoes, 0, 1000);
            Preencher(new PoliticaEmprestimos());
        }

        private void GuardarCores(Control container)
        {
            foreach (Control control in container.Controls)
            {
                var campo = control as RoundedTextBox;
                if (campo != null)
                    coresCampos[campo] = new[] { campo.BackColor, campo.TextColor, campo.ForeColor };
                else
                {
                    if (control is Label || control is CheckBox)
                        coresClaras[control] = control.ForeColor;
                    GuardarCores(control);
                }
            }
        }

        public void AplicarTema(Color cor)
        {
            ThemeManager.Apply(this, cor);
            foreach (var item in coresClaras)
                item.Key.ForeColor = ThemeManager.Escuro ? ThemeManager.CorTextoParaEscuro(item.Value) : item.Value;
            foreach (var item in coresCampos)
            {
                item.Key.BackColor = ThemeManager.Escuro ? ThemeManager.Dark.Entrada : item.Value[0];
                item.Key.TextColor = ThemeManager.Escuro ? ThemeManager.Dark.Texto : item.Value[1];
                item.Key.ForeColor = ThemeManager.Escuro ? ThemeManager.Dark.Texto : item.Value[2];
            }
            if (!ExibirCartoes) { pnlFiltrosLimites.BackColor = Color.Transparent; pnlFiltrosPrazos.BackColor = Color.Transparent; }
            ValidarValores();
        }

        public void Preencher(PoliticaEmprestimos regras)
        {
            preenchendo = true;
            numAluno.Text = Math.Max(1, regras.LimiteAluno).ToString();
            chkSemLimiteAluno.Checked = regras.LimiteAluno < 0;
            numProfessor.Text = Math.Max(1, regras.LimiteProfessor).ToString();
            chkSemLimiteProfessor.Checked = regras.LimiteProfessor < 0;
            numBibliotecario.Text = Math.Max(1, regras.LimiteBibliotecario).ToString();
            chkSemLimiteBibliotecario.Checked = regras.LimiteBibliotecario < 0;
            numPrazo.Text = Math.Max(1, regras.PrazoDias).ToString();
            numProrrogacoes.Text = Math.Max(0, regras.MaximoProrrogacoes).ToString();
            chkSemLimiteProrrogacoes.Checked = regras.MaximoProrrogacoes < 0;
            preenchendo = false;
            ValidarValores();
        }

        private void SemLimite_CheckedChanged(object sender, EventArgs e)
        {
            numAluno.Enabled = !chkSemLimiteAluno.Checked;
            numProfessor.Enabled = !chkSemLimiteProfessor.Checked;
            numBibliotecario.Enabled = !chkSemLimiteBibliotecario.Checked;
            numProrrogacoes.Enabled = !chkSemLimiteProrrogacoes.Checked;
            ValidarValores();
        }

        private void ConfigurarContador(RoundedTextBox campo, Button mais, Button menos, int minimo, int maximo)
        {
            campo.TextChanged += (s, e) =>
            {
                int valor;
                if (minimo > 0 && int.TryParse(campo.Text, out valor) && valor == 0)
                    campo.Text = minimo.ToString();
                ValidarValores();
            };
            mais.Click += (s, e) => AjustarContador(campo, 1, minimo, maximo);
            menos.Click += (s, e) => AjustarContador(campo, -1, minimo, maximo);
            TextBox editor = campo.Controls.OfType<TextBox>().Single();
            editor.MaxLength = 4;
            editor.KeyDown += (s, e) =>
            {
                if (e.Modifiers != Keys.None || (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)) return;
                AjustarContador(campo, e.KeyCode == Keys.Up ? 1 : -1, minimo, maximo);
                e.SuppressKeyPress = true;
            };
        }

        private void AjustarContador(RoundedTextBox campo, int passo, int minimo, int maximo)
        {
            if (!campo.Enabled) return;
            int valor;
            if (int.TryParse(campo.Text, out valor))
                valor = Math.Max(minimo, Math.Min(maximo, valor));
            else
            {
                campo.Text = minimo.ToString();
                campo.Focus();
                return;
            }
            campo.Text = Math.Max(minimo, Math.Min(maximo, valor + passo)).ToString();
            campo.Focus();
            campo.SelectAll();
        }

        private bool ValidarContador(RoundedTextBox campo, Button mais, Button menos, int minimo, int maximo)
        {
            int valor;
            bool valido = int.TryParse(campo.Text, out valor) && valor >= minimo && valor <= maximo;
            mais.Enabled = campo.Enabled && (!valido || valor < maximo);
            menos.Enabled = campo.Enabled && (!valido || valor > minimo);
            return !campo.Enabled || valido;
        }

        public bool ValidarValores()
        {
            if (preenchendo) return true;
            bool valido = ValidarContador(numAluno, btnMaisAluno, btnMenosAluno, 1, 100);
            valido &= ValidarContador(numProfessor, btnMaisProfessor, btnMenosProfessor, 1, 100);
            valido &= ValidarContador(numBibliotecario, btnMaisBibliotecario, btnMenosBibliotecario, 1, 100);
            valido &= ValidarContador(numPrazo, btnMaisPrazo, btnMenosPrazo, 1, 365);
            valido &= ValidarContador(numProrrogacoes, btnMaisProrrogacoes, btnMenosProrrogacoes, 0, 1000);
            bool mudou = ValoresValidos != valido;
            ValoresValidos = valido;
            lblDica.Text = valido ? dicaPadrao : "Use limites de 1 a 100 livros e prazos de 1 a 365 dias. Prorrogações podem ser de 0 a 1000. Preencha os campos ou marque Sem limite.";
            lblDica.ForeColor = valido ? (ThemeManager.Escuro ? ThemeManager.Dark.Texto : SystemColors.ControlText)
                : (ThemeManager.Escuro ? Color.LightCoral : Color.Firebrick);
            if (mudou) ValidadeAlterada?.Invoke(valido);
            return valido;
        }

        public PoliticaEmprestimos LerRegras()
        {
            if (!ValidarValores()) throw new InvalidOperationException("Confira os valores das regras antes de continuar.");
            var regras = new PoliticaEmprestimos {
                LimiteAluno = chkSemLimiteAluno.Checked ? -1 : int.Parse(numAluno.Text), LimiteProfessor = chkSemLimiteProfessor.Checked ? -1 : int.Parse(numProfessor.Text),
                LimiteBibliotecario = chkSemLimiteBibliotecario.Checked ? -1 : int.Parse(numBibliotecario.Text), PrazoDias = int.Parse(numPrazo.Text),
                MaximoProrrogacoes = chkSemLimiteProrrogacoes.Checked ? -1 : int.Parse(numProrrogacoes.Text)
            };
            regras.Validar();
            return regras;
        }
    }
}
