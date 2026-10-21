namespace SUB.Forms.Configuracoes
{
    partial class ConfiguracaoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpInstituicao = new System.Windows.Forms.GroupBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblCor = new System.Windows.Forms.Label();
            this.cbTemas = new System.Windows.Forms.ComboBox();
            this.pnlCorPreview = new System.Windows.Forms.Panel();
            this.lblCorHex = new System.Windows.Forms.Label();
            this.grpAtualizacao = new System.Windows.Forms.GroupBox();
            this.lblVersaoAtual = new System.Windows.Forms.Label();
            this.lblStatusAtualizacao = new System.Windows.Forms.Label();
            this.btnVerificar = new CustomButton();
            this.pbVerificando = new System.Windows.Forms.ProgressBar();
            this.lblNotasTitulo = new System.Windows.Forms.Label();
            this.txtNotasVersao = new System.Windows.Forms.TextBox();
            this.btnAtualizar = new CustomButton();
            this.btnCancelarDownload = new CustomButton();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpInstituicao.SuspendLayout();
            this.grpAtualizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(24, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(228, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configurações da instituição";
            // 
            // grpInstituicao
            // 
            this.grpInstituicao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInstituicao.Controls.Add(this.lblNome);
            this.grpInstituicao.Controls.Add(this.txtNome);
            this.grpInstituicao.Controls.Add(this.lblCor);
            this.grpInstituicao.Controls.Add(this.cbTemas);
            this.grpInstituicao.Controls.Add(this.pnlCorPreview);
            this.grpInstituicao.Controls.Add(this.lblCorHex);
            this.grpInstituicao.Location = new System.Drawing.Point(24, 52);
            this.grpInstituicao.Name = "grpInstituicao";
            this.grpInstituicao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpInstituicao.Size = new System.Drawing.Size(992, 200);
            this.grpInstituicao.TabIndex = 1;
            this.grpInstituicao.TabStop = false;
            this.grpInstituicao.Text = "Instituição";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(16, 28);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(113, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome da instituição:";
            // 
            // txtNome
            // 
            this.txtNome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNome.Location = new System.Drawing.Point(16, 48);
            this.txtNome.MaxLength = 150;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(640, 22);
            this.txtNome.TabIndex = 1;
            // 
            // lblCor
            // 
            this.lblCor.AutoSize = true;
            this.lblCor.Location = new System.Drawing.Point(16, 90);
            this.lblCor.Name = "lblCor";
            this.lblCor.Size = new System.Drawing.Size(183, 13);
            this.lblCor.TabIndex = 2;
            this.lblCor.Text = "Cor principal da identidade visual:";
            // 
            // cbTemas
            // 
            this.cbTemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTemas.Location = new System.Drawing.Point(16, 112);
            this.cbTemas.Name = "cbTemas";
            this.cbTemas.Size = new System.Drawing.Size(240, 21);
            this.cbTemas.TabIndex = 3;
            this.cbTemas.SelectedIndexChanged += new System.EventHandler(this.cbTemas_SelectedIndexChanged);
            // 
            // pnlCorPreview
            // 
            this.pnlCorPreview.Location = new System.Drawing.Point(272, 108);
            this.pnlCorPreview.Name = "pnlCorPreview";
            this.pnlCorPreview.Size = new System.Drawing.Size(60, 60);
            this.pnlCorPreview.TabIndex = 4;
            this.pnlCorPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCorPreview_Paint);
            // 
            // lblCorHex
            // 
            this.lblCorHex.AutoSize = true;
            this.lblCorHex.Location = new System.Drawing.Point(464, 116);
            this.lblCorHex.Name = "lblCorHex";
            this.lblCorHex.Size = new System.Drawing.Size(0, 13);
            this.lblCorHex.TabIndex = 5;
            // 
            // grpAtualizacao
            // 
            this.grpAtualizacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAtualizacao.Controls.Add(this.lblVersaoAtual);
            this.grpAtualizacao.Controls.Add(this.lblStatusAtualizacao);
            this.grpAtualizacao.Controls.Add(this.btnVerificar);
            this.grpAtualizacao.Controls.Add(this.pbVerificando);
            this.grpAtualizacao.Controls.Add(this.lblNotasTitulo);
            this.grpAtualizacao.Controls.Add(this.txtNotasVersao);
            this.grpAtualizacao.Controls.Add(this.btnAtualizar);
            this.grpAtualizacao.Controls.Add(this.btnCancelarDownload);
            this.grpAtualizacao.Controls.Add(this.pbDownload);
            this.grpAtualizacao.Location = new System.Drawing.Point(24, 264);
            this.grpAtualizacao.Name = "grpAtualizacao";
            this.grpAtualizacao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpAtualizacao.Size = new System.Drawing.Size(992, 340);
            this.grpAtualizacao.TabIndex = 2;
            this.grpAtualizacao.TabStop = false;
            this.grpAtualizacao.Text = "Atualização";
            // 
            // lblVersaoAtual
            // 
            this.lblVersaoAtual.AutoSize = true;
            this.lblVersaoAtual.Location = new System.Drawing.Point(16, 24);
            this.lblVersaoAtual.Name = "lblVersaoAtual";
            this.lblVersaoAtual.Size = new System.Drawing.Size(87, 13);
            this.lblVersaoAtual.TabIndex = 0;
            this.lblVersaoAtual.Text = "Versão atual: —";
            // 
            // lblStatusAtualizacao
            // 
            this.lblStatusAtualizacao.AutoSize = true;
            this.lblStatusAtualizacao.Location = new System.Drawing.Point(16, 46);
            this.lblStatusAtualizacao.MaximumSize = new System.Drawing.Size(620, 0);
            this.lblStatusAtualizacao.Name = "lblStatusAtualizacao";
            this.lblStatusAtualizacao.Size = new System.Drawing.Size(337, 13);
            this.lblStatusAtualizacao.TabIndex = 1;
            this.lblStatusAtualizacao.Text = "Clique em Verificar atualizações para consultar uma nova versão.";
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnVerificar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnVerificar.BorderColor = System.Drawing.Color.Empty;
            this.btnVerificar.BorderThickness = 0;
            this.btnVerificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificar.FlatAppearance.BorderSize = 0;
            this.btnVerificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.HoverColor = System.Drawing.Color.Empty;
            this.btnVerificar.Location = new System.Drawing.Point(16, 72);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.PressedColor = System.Drawing.Color.Empty;
            this.btnVerificar.ShowBorder = false;
            this.btnVerificar.Size = new System.Drawing.Size(190, 36);
            this.btnVerificar.TabIndex = 2;
            this.btnVerificar.TabStop = false;
            this.btnVerificar.Text = "Verificar atualizações";
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // pbVerificando
            // 
            this.pbVerificando.Location = new System.Drawing.Point(214, 81);
            this.pbVerificando.Name = "pbVerificando";
            this.pbVerificando.Size = new System.Drawing.Size(180, 18);
            this.pbVerificando.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbVerificando.TabIndex = 3;
            this.pbVerificando.Visible = false;
            // 
            // lblNotasTitulo
            // 
            this.lblNotasTitulo.AutoSize = true;
            this.lblNotasTitulo.Location = new System.Drawing.Point(16, 120);
            this.lblNotasTitulo.Name = "lblNotasTitulo";
            this.lblNotasTitulo.Size = new System.Drawing.Size(92, 13);
            this.lblNotasTitulo.TabIndex = 4;
            this.lblNotasTitulo.Text = "Notas da versão:";
            // 
            // txtNotasVersao
            // 
            this.txtNotasVersao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotasVersao.Location = new System.Drawing.Point(16, 140);
            this.txtNotasVersao.MaxLength = 4000;
            this.txtNotasVersao.Multiline = true;
            this.txtNotasVersao.Name = "txtNotasVersao";
            this.txtNotasVersao.ReadOnly = true;
            this.txtNotasVersao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotasVersao.Size = new System.Drawing.Size(960, 120);
            this.txtNotasVersao.TabIndex = 5;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAtualizar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnAtualizar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnAtualizar.BorderColor = System.Drawing.Color.Empty;
            this.btnAtualizar.BorderThickness = 0;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.HoverColor = System.Drawing.Color.Empty;
            this.btnAtualizar.Location = new System.Drawing.Point(16, 272);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.PressedColor = System.Drawing.Color.Empty;
            this.btnAtualizar.ShowBorder = false;
            this.btnAtualizar.Size = new System.Drawing.Size(160, 36);
            this.btnAtualizar.TabIndex = 6;
            this.btnAtualizar.TabStop = false;
            this.btnAtualizar.Text = "Atualizar agora";
            this.btnAtualizar.Visible = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnCancelarDownload
            // 
            this.btnCancelarDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelarDownload.BackgroundColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnCancelarDownload.BorderColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.BorderThickness = 0;
            this.btnCancelarDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDownload.FlatAppearance.BorderSize = 0;
            this.btnCancelarDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDownload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelarDownload.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDownload.HoverColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.Location = new System.Drawing.Point(184, 272);
            this.btnCancelarDownload.Name = "btnCancelarDownload";
            this.btnCancelarDownload.PressedColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.ShowBorder = false;
            this.btnCancelarDownload.Size = new System.Drawing.Size(175, 36);
            this.btnCancelarDownload.TabIndex = 7;
            this.btnCancelarDownload.TabStop = false;
            this.btnCancelarDownload.Text = "Cancelar download";
            this.btnCancelarDownload.Visible = false;
            this.btnCancelarDownload.Click += new System.EventHandler(this.btnCancelarDownload_Click);
            // 
            // pbDownload
            // 
            this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pbDownload.Location = new System.Drawing.Point(372, 281);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(260, 18);
            this.pbDownload.TabIndex = 8;
            this.pbDownload.Visible = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Location = new System.Drawing.Point(808, 608);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 36);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(916, 608);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 36);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // ConfiguracaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(1040, 660);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.grpInstituicao);
            this.Controls.Add(this.grpAtualizacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(760, 560);
            this.Name = "ConfiguracaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.grpInstituicao.ResumeLayout(false);
            this.grpInstituicao.PerformLayout();
            this.grpAtualizacao.ResumeLayout(false);
            this.grpAtualizacao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpInstituicao;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCor;
        private System.Windows.Forms.ComboBox cbTemas;
        private System.Windows.Forms.Panel pnlCorPreview;
        private System.Windows.Forms.Label lblCorHex;
        private System.Windows.Forms.GroupBox grpAtualizacao;
        private System.Windows.Forms.Label lblVersaoAtual;
        private System.Windows.Forms.Label lblStatusAtualizacao;
        private CustomButton btnVerificar;
        private System.Windows.Forms.ProgressBar pbVerificando;
        private System.Windows.Forms.Label lblNotasTitulo;
        private System.Windows.Forms.TextBox txtNotasVersao;
        private CustomButton btnAtualizar;
        private CustomButton btnCancelarDownload;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}