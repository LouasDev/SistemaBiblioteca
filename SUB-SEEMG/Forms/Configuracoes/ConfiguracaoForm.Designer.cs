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
            if (disposing)
            {
                fonteRotuloCompacto.Dispose();
                fonteStatusCompacto.Dispose();
            }
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
            this.grpInstituicao = new System.Windows.Forms.GroupBox();
            this.cbTemas = new RoundedComboBox();
            this.c = new RoundedTextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblCor = new System.Windows.Forms.Label();
            this.pnlCorPreview = new System.Windows.Forms.Panel();
            this.grpAtualizacao = new System.Windows.Forms.GroupBox();
            this.btnCancelarDownload = new CustomButton();
            this.btnAtualizar = new CustomButton();
            this.btnVerificar = new CustomButton();
            this.lblVersaoAtual = new System.Windows.Forms.Label();
            this.lblStatusAtualizacao = new System.Windows.Forms.Label();
            this.pbVerificando = new System.Windows.Forms.ProgressBar();
            this.lblNotasTitulo = new System.Windows.Forms.Label();
            this.txtNotasVersao = new System.Windows.Forms.TextBox();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnApagarDados = new System.Windows.Forms.Button();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.Titulo = new System.Windows.Forms.Label();
            this.grpInstituicao.SuspendLayout();
            this.grpAtualizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInstituicao
            // 
            this.grpInstituicao.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grpInstituicao.Controls.Add(this.cbTemas);
            this.grpInstituicao.Controls.Add(this.c);
            this.grpInstituicao.Controls.Add(this.lblNome);
            this.grpInstituicao.Controls.Add(this.lblCor);
            this.grpInstituicao.Controls.Add(this.pnlCorPreview);
            this.grpInstituicao.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInstituicao.Location = new System.Drawing.Point(106, 120);
            this.grpInstituicao.MaximumSize = new System.Drawing.Size(1100, 189);
            this.grpInstituicao.Name = "grpInstituicao";
            this.grpInstituicao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpInstituicao.Size = new System.Drawing.Size(1078, 189);
            this.grpInstituicao.TabIndex = 1;
            this.grpInstituicao.TabStop = false;
            this.grpInstituicao.Text = "Instituição";
            // 
            // cbTemas
            // 
            this.cbTemas.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbTemas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbTemas.BorderRadius = 8;
            this.cbTemas.BorderThickness = 1;
            this.cbTemas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTemas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbTemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTemas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTemas.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbTemas.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbTemas.FormattingEnabled = true;
            this.cbTemas.ItemsFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbTemas.Location = new System.Drawing.Point(14, 129);
            this.cbTemas.Name = "cbTemas";
            this.cbTemas.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14F);
            this.cbTemas.PlaceholderMargin = 10;
            this.cbTemas.PlaceholderText = "Escolha a Cor";
            this.cbTemas.Size = new System.Drawing.Size(275, 34);
            this.cbTemas.TabIndex = 6;
            this.cbTemas.SelectedIndexChanged += new System.EventHandler(this.cbTemas_SelectedIndexChanged);
            // 
            // c
            // 
            this.c.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.c.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.c.BackColor = System.Drawing.Color.White;
            this.c.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.c.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.c.BorderRadius = 8;
            this.c.BorderThickness = 1;
            this.c.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.c.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.c.Location = new System.Drawing.Point(15, 57);
            this.c.Name = "c";
            this.c.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(129)))), ((int)(((byte)(143)))));
            this.c.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.c.PlaceholderMarginLeft = 12;
            this.c.PlaceholderText = "Informe o nome da instituição";
            this.c.SelectedText = "";
            this.c.SelectionLength = 0;
            this.c.SelectionStart = 0;
            this.c.Size = new System.Drawing.Size(680, 34);
            this.c.TabIndex = 5;
            this.c.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.c.UseSystemPasswordChar = false;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblNome.Location = new System.Drawing.Point(14, 26);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(190, 25);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome da instituição:";
            // 
            // lblCor
            // 
            this.lblCor.AutoSize = true;
            this.lblCor.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblCor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblCor.Location = new System.Drawing.Point(13, 101);
            this.lblCor.Name = "lblCor";
            this.lblCor.Size = new System.Drawing.Size(302, 25);
            this.lblCor.TabIndex = 2;
            this.lblCor.Text = "Cor principal da identidade visual:";
            // 
            // pnlCorPreview
            // 
            this.pnlCorPreview.Location = new System.Drawing.Point(332, 113);
            this.pnlCorPreview.Name = "pnlCorPreview";
            this.pnlCorPreview.Size = new System.Drawing.Size(50, 50);
            this.pnlCorPreview.TabIndex = 4;
            // 
            // grpAtualizacao
            // 
            this.grpAtualizacao.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grpAtualizacao.Controls.Add(this.btnCancelarDownload);
            this.grpAtualizacao.Controls.Add(this.btnAtualizar);
            this.grpAtualizacao.Controls.Add(this.btnVerificar);
            this.grpAtualizacao.Controls.Add(this.lblVersaoAtual);
            this.grpAtualizacao.Controls.Add(this.lblStatusAtualizacao);
            this.grpAtualizacao.Controls.Add(this.pbVerificando);
            this.grpAtualizacao.Controls.Add(this.lblNotasTitulo);
            this.grpAtualizacao.Controls.Add(this.txtNotasVersao);
            this.grpAtualizacao.Controls.Add(this.pbDownload);
            this.grpAtualizacao.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.grpAtualizacao.Location = new System.Drawing.Point(106, 327);
            this.grpAtualizacao.MaximumSize = new System.Drawing.Size(1100, 433);
            this.grpAtualizacao.Name = "grpAtualizacao";
            this.grpAtualizacao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpAtualizacao.Size = new System.Drawing.Size(1078, 395);
            this.grpAtualizacao.TabIndex = 2;
            this.grpAtualizacao.TabStop = false;
            this.grpAtualizacao.Text = "Atualização";
            // 
            // btnCancelarDownload
            // 
            this.btnCancelarDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnCancelarDownload.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnCancelarDownload.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnCancelarDownload.BorderThickness = 0;
            this.btnCancelarDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDownload.FlatAppearance.BorderSize = 0;
            this.btnCancelarDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDownload.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelarDownload.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDownload.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(106)))));
            this.btnCancelarDownload.Location = new System.Drawing.Point(189, 339);
            this.btnCancelarDownload.Name = "btnCancelarDownload";
            this.btnCancelarDownload.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(82)))));
            this.btnCancelarDownload.ShowBorder = false;
            this.btnCancelarDownload.Size = new System.Drawing.Size(160, 36);
            this.btnCancelarDownload.TabIndex = 19;
            this.btnCancelarDownload.Text = "Cancelar download";
            this.btnCancelarDownload.UseVisualStyleBackColor = false;
            this.btnCancelarDownload.Visible = false;
            this.btnCancelarDownload.Click += new System.EventHandler(this.btnCancelarDownload_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAtualizar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAtualizar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAtualizar.BorderThickness = 0;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(106)))));
            this.btnAtualizar.Location = new System.Drawing.Point(23, 339);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(82)))));
            this.btnAtualizar.ShowBorder = false;
            this.btnAtualizar.Size = new System.Drawing.Size(160, 36);
            this.btnAtualizar.TabIndex = 18;
            this.btnAtualizar.Text = "Atualizar agora";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            this.btnAtualizar.Visible = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnVerificar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnVerificar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnVerificar.BorderThickness = 0;
            this.btnVerificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificar.FlatAppearance.BorderSize = 0;
            this.btnVerificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(106)))));
            this.btnVerificar.Location = new System.Drawing.Point(20, 126);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(82)))));
            this.btnVerificar.ShowBorder = false;
            this.btnVerificar.Size = new System.Drawing.Size(190, 36);
            this.btnVerificar.TabIndex = 15;
            this.btnVerificar.Text = "Verificar atualizações";
            this.btnVerificar.UseVisualStyleBackColor = false;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // lblVersaoAtual
            // 
            this.lblVersaoAtual.AutoSize = true;
            this.lblVersaoAtual.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblVersaoAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblVersaoAtual.Location = new System.Drawing.Point(18, 28);
            this.lblVersaoAtual.Name = "lblVersaoAtual";
            this.lblVersaoAtual.Size = new System.Drawing.Size(146, 25);
            this.lblVersaoAtual.TabIndex = 11;
            this.lblVersaoAtual.Text = "Versão atual: —";
            // 
            // lblStatusAtualizacao
            // 
            this.lblStatusAtualizacao.AutoSize = true;
            this.lblStatusAtualizacao.Font = new System.Drawing.Font("Segoe UI Semibold", 12.25F, System.Drawing.FontStyle.Bold);
            this.lblStatusAtualizacao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblStatusAtualizacao.Location = new System.Drawing.Point(18, 65);
            this.lblStatusAtualizacao.MaximumSize = new System.Drawing.Size(1000, 0);
            this.lblStatusAtualizacao.Name = "lblStatusAtualizacao";
            this.lblStatusAtualizacao.Size = new System.Drawing.Size(507, 23);
            this.lblStatusAtualizacao.TabIndex = 12;
            this.lblStatusAtualizacao.Text = "Clique em Verificar atualizações para consultar uma nova versão.";
            // 
            // pbVerificando
            // 
            this.pbVerificando.Location = new System.Drawing.Point(227, 130);
            this.pbVerificando.Name = "pbVerificando";
            this.pbVerificando.Size = new System.Drawing.Size(300, 30);
            this.pbVerificando.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbVerificando.TabIndex = 13;
            this.pbVerificando.Visible = false;
            // 
            // lblNotasTitulo
            // 
            this.lblNotasTitulo.AutoSize = true;
            this.lblNotasTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNotasTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblNotasTitulo.Location = new System.Drawing.Point(15, 178);
            this.lblNotasTitulo.Name = "lblNotasTitulo";
            this.lblNotasTitulo.Size = new System.Drawing.Size(155, 25);
            this.lblNotasTitulo.TabIndex = 14;
            this.lblNotasTitulo.Text = "Notas da versão:";
            // 
            // txtNotasVersao
            // 
            this.txtNotasVersao.BackColor = System.Drawing.SystemColors.Window;
            this.txtNotasVersao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotasVersao.Location = new System.Drawing.Point(18, 208);
            this.txtNotasVersao.MaxLength = 4000;
            this.txtNotasVersao.Multiline = true;
            this.txtNotasVersao.Name = "txtNotasVersao";
            this.txtNotasVersao.ReadOnly = true;
            this.txtNotasVersao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotasVersao.Size = new System.Drawing.Size(1001, 112);
            this.txtNotasVersao.TabIndex = 16;
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(362, 343);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(300, 30);
            this.pbDownload.TabIndex = 17;
            this.pbDownload.Visible = false;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.btnSalvar.Location = new System.Drawing.Point(1061, 767);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(123, 40);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // btnApagarDados
            this.btnApagarDados.Name = "btnApagarDados";
            this.btnApagarDados.Text = "Resetar aplicativo";
            this.btnApagarDados.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnApagarDados.Location = new System.Drawing.Point(106, 767);
            this.btnApagarDados.Size = new System.Drawing.Size(172, 40);
            this.btnApagarDados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApagarDados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApagarDados.TabIndex = 4;
            this.btnApagarDados.Click += new System.EventHandler(this.btnApagarDados_Click);
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 88);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 126;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(36, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(316, 17);
            this.lblSubtitulo.TabIndex = 125;
            this.lblSubtitulo.Text = "Configure e mantenha os dados do SUB atualizados.";
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.Titulo.Location = new System.Drawing.Point(34, 20);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(377, 38);
            this.Titulo.TabIndex = 124;
            this.Titulo.Text = "Configurações da instituição";
            // 
            // ConfiguracaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.pnlLinhaCabecalho);
            this.Controls.Add(this.grpInstituicao);
            this.Controls.Add(this.grpAtualizacao);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnApagarDados);
            this.Controls.Add(this.Titulo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracaoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ConfiguraçõesForm";
            this.SizeChanged += new System.EventHandler(this.ConfiguracaoForm_SizeChanged);
            this.grpInstituicao.ResumeLayout(false);
            this.grpInstituicao.PerformLayout();
            this.grpAtualizacao.ResumeLayout(false);
            this.grpAtualizacao.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox grpInstituicao;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblCor;
        private System.Windows.Forms.Panel pnlCorPreview;
        private System.Windows.Forms.GroupBox grpAtualizacao;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnApagarDados;
        private RoundedTextBox c;
        private RoundedComboBox cbTemas;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
        private System.Windows.Forms.Label lblSubtitulo;
        public System.Windows.Forms.Label Titulo;
        private CustomButton btnCancelarDownload;
        private CustomButton btnAtualizar;
        private CustomButton btnVerificar;
        private System.Windows.Forms.Label lblVersaoAtual;
        private System.Windows.Forms.Label lblStatusAtualizacao;
        private System.Windows.Forms.ProgressBar pbVerificando;
        private System.Windows.Forms.Label lblNotasTitulo;
        private System.Windows.Forms.TextBox txtNotasVersao;
        private System.Windows.Forms.ProgressBar pbDownload;
    }
}
