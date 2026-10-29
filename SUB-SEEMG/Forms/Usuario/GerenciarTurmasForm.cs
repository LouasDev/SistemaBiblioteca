using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SUB.Utils;

namespace SUB.Forms.Usuario
{
    public partial class GerenciarTurmasForm : Form
    {
        private readonly List<string> _turmas = new List<string>();
        private readonly Dictionary<string, string> _renomeacoes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private string _turmaEmEdicao;
        private string _turmaOriginalEmEdicao;
        public GerenciarTurmasForm()
        {
            InitializeComponent();
            ThemeManager.ApplyConfiguredTheme(this);
            txtNovaTurma.KeyPress += TxtNovaTurma_KeyPress;
        }

        private void GerenciarTurmasForm_Load(object sender, EventArgs e)
        {
            _turmas.Clear();
            _turmas.AddRange(TurmasUtil.Carregar());
            AtualizarLista();
            txtNovaTurma.Focus();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            string turma = (txtNovaTurma.Text ?? string.Empty).Trim().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(turma))
            {
                ExibirStatus("Digite o nome da turma para adicioná-la.", true);
                txtNovaTurma.Focus();
                return;
            }

            if (_turmas.Contains(turma, StringComparer.OrdinalIgnoreCase) && !string.Equals(turma, _turmaEmEdicao, StringComparison.OrdinalIgnoreCase))
            {
                ExibirStatus("Esta turma já está na lista.", true);
                txtNovaTurma.SelectAll();
                txtNovaTurma.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(_turmaEmEdicao))
            {
                int indice = _turmas.FindIndex(item => string.Equals(item, _turmaEmEdicao, StringComparison.OrdinalIgnoreCase));
                if (indice >= 0)
                    _turmas[indice] = turma;
                if (string.Equals(_turmaOriginalEmEdicao, turma, StringComparison.OrdinalIgnoreCase))
                    _renomeacoes.Remove(_turmaOriginalEmEdicao);
                else
                    _renomeacoes[_turmaOriginalEmEdicao] = turma;
                _turmaEmEdicao = null;
                _turmaOriginalEmEdicao = null;
                btnAdicionar.Text = "Adicionar turma";
                ExibirStatus("Edição preparada. Clique em Salvar alterações para confirmar.", false);
            }
            else
            {
                _turmas.Add(turma);
                ExibirStatus("Turma adicionada. Clique em Salvar alterações para confirmar.", false);
            }

            txtNovaTurma.Text = string.Empty;
            AtualizarLista();
            txtNovaTurma.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string turma = lstTurmas.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(turma))
                return;
            _turmaEmEdicao = turma;
            _turmaOriginalEmEdicao = _renomeacoes.Where(item => string.Equals(item.Value, turma, StringComparison.OrdinalIgnoreCase)).Select(item => item.Key).FirstOrDefault() ?? turma;
            txtNovaTurma.Text = turma;
            txtNovaTurma.SelectAll();
            btnAdicionar.Text = "Salvar edição";
            ExibirStatus("Edite o nome e confirme a alteração.", false);
            txtNovaTurma.Focus();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            string turma = lstTurmas.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(turma))
                return;
            if (TurmaPossuiUsuarios(turma))
            {
                ExibirStatus("Esta turma possui alunos cadastrados e não pode ser removida.", true);
                return;
            }

            _turmas.RemoveAll(item => string.Equals(item, turma, StringComparison.OrdinalIgnoreCase));
            AtualizarLista();
            ExibirStatus("Turma removida. Clique em Salvar alterações para confirmar.", false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                TurmasUtil.Salvar(_turmas, _renomeacoes);
                _renomeacoes.Clear();
                ExibirStatus(_turmas.Count == 0 ? "Nenhuma turma cadastrada. Você poderá adicionar quando precisar." : "Turmas atualizadas com sucesso.", false);
            }
            catch (Exception ex)
            {
                ExibirStatus("Não foi possível salvar as turmas: " + ex.Message, true);
            }
        }

        private void lstTurmas_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemover.Enabled = lstTurmas.SelectedIndex >= 0;
            btnEditar.Enabled = lstTurmas.SelectedIndex >= 0;
        }

        private void txtNovaTurma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAdicionar.PerformClick();
            }
        }

        private void TxtNovaTurma_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) || txtNovaTurma.SelectionStart != 0 || txtNovaTurma.Text.Length != 0)
                return;
            txtNovaTurma.Text = e.KeyChar + "° ";
            txtNovaTurma.SelectionStart = txtNovaTurma.Text.Length;
            e.Handled = true;
        }

        private void AtualizarLista()
        {
            string selecionada = lstTurmas.SelectedItem as string;
            lstTurmas.BeginUpdate();
            lstTurmas.Items.Clear();
            foreach (string turma in _turmas.OrderBy(item => item, StringComparer.CurrentCultureIgnoreCase))
                lstTurmas.Items.Add(turma);
            lstTurmas.EndUpdate();
            if (!string.IsNullOrWhiteSpace(selecionada))
                lstTurmas.SelectedItem = selecionada;
            lblContagem.Text = _turmas.Count == 1 ? "1 turma cadastrada" : _turmas.Count + " turmas cadastradas";
            btnRemover.Enabled = lstTurmas.SelectedIndex >= 0;
            btnEditar.Enabled = lstTurmas.SelectedIndex >= 0;
        }

        private bool TurmaPossuiUsuarios(string turma)
        {
            if (TurmasUtil.EmUso(turma))
                return true;
            string turmaOriginal = _renomeacoes.Where(item => string.Equals(item.Value, turma, StringComparison.OrdinalIgnoreCase)).Select(item => item.Key).FirstOrDefault();
            return !string.IsNullOrWhiteSpace(turmaOriginal) && TurmasUtil.EmUso(turmaOriginal);
        }

        private void ExibirStatus(string mensagem, bool erro)
        {
            lblStatus.Text = mensagem;
            lblStatus.ForeColor = erro ? Color.FromArgb(184, 50, 50) : ThemeManager.GetLabelColor(ThemeManager.Normalize(btnSalvar.BackColor));
        }
    }
}
