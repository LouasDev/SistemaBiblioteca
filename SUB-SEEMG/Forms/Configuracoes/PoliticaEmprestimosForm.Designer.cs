namespace SUB.Forms.Configuracoes
{
    partial class PoliticaEmprestimosForm
    {
        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.editorRegras = new PoliticaEmprestimosEditor();
            this.btnAplicar = new CustomButton();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(24, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(632, 34);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Regras de empréstimo";
            this.lblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.Location = new System.Drawing.Point(26, 54);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(630, 24);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Defina os limites e prazos dos empréstimos da biblioteca.";
            this.btnAplicar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnAplicar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAplicar.BorderColor = System.Drawing.Color.Empty;
            this.btnAplicar.BorderRadius = 10;
            this.btnAplicar.BorderThickness = 0;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.HoverColor = System.Drawing.Color.Empty;
            this.btnAplicar.Location = new System.Drawing.Point(516, 494);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.PressedColor = System.Drawing.Color.Empty;
            this.btnAplicar.ShowBorder = false;
            this.btnAplicar.Size = new System.Drawing.Size(140, 42);
            this.btnAplicar.TabIndex = 3;
            this.btnAplicar.Text = "Aplicar regras";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(368, 494);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(132, 42);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnCancelar);
            this.editorRegras.Name = "editorRegras";
            this.editorRegras.Location = new System.Drawing.Point(24, 84);
            this.editorRegras.Size = new System.Drawing.Size(632, 410);
            this.editorRegras.TabIndex = 0;
            this.Controls.Add(this.editorRegras);
            this.AcceptButton = this.btnAplicar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(680, 566);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PoliticaEmprestimosForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Regras de empréstimo";
            this.btnCancelar.Location = new System.Drawing.Point(368, 510);
            this.btnAplicar.Location = new System.Drawing.Point(516, 510);
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private PoliticaEmprestimosEditor editorRegras;
        private CustomButton btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
