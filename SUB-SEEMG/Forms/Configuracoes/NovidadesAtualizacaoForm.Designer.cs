namespace SUB.Forms.Configuracoes
{
    partial class NovidadesAtualizacaoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.rtbNovidades = new System.Windows.Forms.RichTextBox();
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlRodape.SuspendLayout();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(31, 72, 126);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.lblSubtitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 88);
            this.pnlHeader.TabIndex = 0;
            // lblTitulo
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(672, 38);
            this.lblTitulo.Text = "Novidades da atualização";
            // lblSubtitulo
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtitulo.Location = new System.Drawing.Point(26, 54);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(670, 24);
            this.lblSubtitulo.Text = "Confira as melhorias e correções desta versão.";
            // lblVersao
            this.lblVersao.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblVersao.Location = new System.Drawing.Point(24, 106);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(320, 28);
            this.lblVersao.Text = "SUB —";
            // lblData
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblData.ForeColor = System.Drawing.Color.FromArgb(93, 105, 120);
            this.lblData.Location = new System.Drawing.Point(346, 110);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(350, 24);
            this.lblData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // rtbNovidades
            this.rtbNovidades.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.rtbNovidades.BackColor = System.Drawing.Color.White;
            this.rtbNovidades.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNovidades.DetectUrls = false;
            this.rtbNovidades.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rtbNovidades.Location = new System.Drawing.Point(28, 152);
            this.rtbNovidades.Name = "rtbNovidades";
            this.rtbNovidades.ReadOnly = true;
            this.rtbNovidades.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbNovidades.Size = new System.Drawing.Size(664, 288);
            this.rtbNovidades.TabIndex = 1;
            this.rtbNovidades.Text = "";
            // pnlRodape
            this.pnlRodape.BackColor = System.Drawing.Color.FromArgb(238, 244, 250);
            this.pnlRodape.Controls.Add(this.btnFechar);
            this.pnlRodape.Controls.Add(this.btnContinuar);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Location = new System.Drawing.Point(0, 460);
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Size = new System.Drawing.Size(720, 72);
            this.pnlRodape.TabIndex = 2;
            // btnFechar
            this.btnFechar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnFechar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnFechar.Location = new System.Drawing.Point(24, 18);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(124, 36);
            this.btnFechar.TabIndex = 0;
            this.btnFechar.Text = "Entendi";
            this.btnFechar.UseVisualStyleBackColor = true;
            // btnContinuar
            this.btnContinuar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnContinuar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnContinuar.Location = new System.Drawing.Point(478, 18);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(218, 36);
            this.btnContinuar.TabIndex = 1;
            this.btnContinuar.Text = "Continuar atualização";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Visible = false;
            // NovidadesAtualizacaoForm
            this.AcceptButton = this.btnFechar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnFechar;
            this.ClientSize = new System.Drawing.Size(720, 532);
            this.Controls.Add(this.rtbNovidades);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.pnlRodape);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NovidadesAtualizacaoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Novidades do SUB";
            this.pnlHeader.ResumeLayout(false);
            this.pnlRodape.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.RichTextBox rtbNovidades;
        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnContinuar;
    }
}
