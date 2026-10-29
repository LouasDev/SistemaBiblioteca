using System;
using System.Windows.Forms;

namespace SUB.Utils.Etiquetas
{
    public enum LabelActionChoice
    {
        None = 0,
        Pdf = 1,
        Print = 2
    }

    internal sealed partial class LabelActionDialog : Form
    {
        public int MaxQuantity { get; set; } = 1;
        public int Quantity { get; private set; } = 1;
        public bool AllowQuantitySelection { get; set; } = true;
        public LabelActionChoice Choice { get; private set; } = LabelActionChoice.None;

        public LabelActionDialog(string tituloLivro = null)
        {
            InitializeComponent();
            ThemeManager.ApplyConfiguredTheme(this);
            string tituloSafe = string.IsNullOrWhiteSpace(tituloLivro) ? "o livro" : $"\"{tituloLivro}\"";
            lblMensagem.Text = $"Como deseja gerar as etiquetas para {tituloSafe}?";
            SetQuantity(1);
            ApplyQuantityVisibility();
        }

        public LabelActionDialog(string tituloLivro, int maxQuantity) : this(tituloLivro)
        {
            MaxQuantity = Math.Max(1, maxQuantity);
            SetQuantity(1);
        }

        public void ConfigureQuantity(bool allowSelection, int fixedQuantity)
        {
            AllowQuantitySelection = allowSelection;
            MaxQuantity = Math.Max(1, fixedQuantity);
            SetQuantity(Math.Max(1, fixedQuantity));
            ApplyQuantityVisibility();
        }

        private void ApplyQuantityVisibility()
        {
            bool visible = AllowQuantitySelection;
            lblQuantidade.Visible = visible;
            numQuantidade.Visible = visible;
            btnMais.Visible = visible;
            btnMenos.Visible = visible;
        }

        private void SetQuantity(int value)
        {
            if (value < 1)
                value = 1;
            if (value > MaxQuantity)
                value = MaxQuantity;
            Quantity = value;
            numQuantidade.Text = Quantity.ToString();
        }

        private int ParseQuantity()
        {
            int value;
            return int.TryParse(numQuantidade.Text, out value) ? value : 1;
        }

        private void NumQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void NumQuantidade_Leave(object sender, EventArgs e)
        {
            SetQuantity(ParseQuantity());
        }

        private void BtnMais_Click(object sender, EventArgs e)
        {
            SetQuantity(ParseQuantity() + 1);
        }

        private void BtnMenos_Click(object sender, EventArgs e)
        {
            SetQuantity(ParseQuantity() - 1);
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            SetQuantity(ParseQuantity());
            Choice = LabelActionChoice.Pdf;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SetQuantity(ParseQuantity());
            Choice = LabelActionChoice.Print;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Choice = LabelActionChoice.None;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LabelActionDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape)
                return;
            Choice = LabelActionChoice.None;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
