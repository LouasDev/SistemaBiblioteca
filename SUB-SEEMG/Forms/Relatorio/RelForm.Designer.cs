namespace SUB.Forms.Relatorio
{
    partial class RelForm
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
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblLivro = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlResultadosRelatorio = new System.Windows.Forms.Panel();
            this.pnlFiltrosRelatorio = new System.Windows.Forms.Panel();
            this.lstSugestoesUsuario = new System.Windows.Forms.ListBox();
            this.lstLivros = new System.Windows.Forms.ListBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFim = new RoundedDatePicker();
            this.dtpInicio = new RoundedDatePicker();
            this.cbBibliotecaria = new RoundedComboBox();
            this.txtLivro = new RoundedTextBox();
            this.cmbAcao = new RoundedComboBox();
            this.txtUsuario = new RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlResultadosRelatorio.SuspendLayout();
            this.pnlFiltrosRelatorio.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHistorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorico.Location = new System.Drawing.Point(1, 1);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHistorico.Size = new System.Drawing.Size(1086, 483);
            this.dgvHistorico.TabIndex = 23;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblUsuario.Location = new System.Drawing.Point(7, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(159, 25);
            this.lblUsuario.TabIndex = 116;
            this.lblUsuario.Text = "Nome da pessoa:";
            // 
            // lblLivro
            // 
            this.lblLivro.AutoSize = true;
            this.lblLivro.BackColor = System.Drawing.Color.Transparent;
            this.lblLivro.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLivro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblLivro.Location = new System.Drawing.Point(435, 16);
            this.lblLivro.Name = "lblLivro";
            this.lblLivro.Size = new System.Drawing.Size(140, 25);
            this.lblLivro.TabIndex = 117;
            this.lblLivro.Text = "Nome do livro:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(864, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 25);
            this.label2.TabIndex = 119;
            this.label2.Text = "Tipo de Ação:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label3.Location = new System.Drawing.Point(7, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 25);
            this.label3.TabIndex = 120;
            this.label3.Text = "Início do período:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label5.Location = new System.Drawing.Point(208, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 25);
            this.label5.TabIndex = 122;
            this.label5.Text = "Fim do período:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlResultadosRelatorio);
            this.panel1.Controls.Add(this.pnlFiltrosRelatorio);
            this.panel1.Controls.Add(this.pnlLinhaCabecalho);
            this.panel1.Controls.Add(this.lblSubtitulo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 845);
            this.panel1.TabIndex = 123;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // pnlResultadosRelatorio
            // 
            this.pnlResultadosRelatorio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResultadosRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultadosRelatorio.Controls.Add(this.dgvHistorico);
            this.pnlResultadosRelatorio.Location = new System.Drawing.Point(96, 332);
            this.pnlResultadosRelatorio.Name = "pnlResultadosRelatorio";
            this.pnlResultadosRelatorio.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultadosRelatorio.Size = new System.Drawing.Size(1088, 485);
            this.pnlResultadosRelatorio.TabIndex = 135;
            // 
            // pnlFiltrosRelatorio
            // 
            this.pnlFiltrosRelatorio.BackColor = System.Drawing.Color.White;
            this.pnlFiltrosRelatorio.Controls.Add(this.lstSugestoesUsuario);
            this.pnlFiltrosRelatorio.Controls.Add(this.dtpFim);
            this.pnlFiltrosRelatorio.Controls.Add(this.dtpInicio);
            this.pnlFiltrosRelatorio.Controls.Add(this.lstLivros);
            this.pnlFiltrosRelatorio.Controls.Add(this.btnExportar);
            this.pnlFiltrosRelatorio.Controls.Add(this.cbBibliotecaria);
            this.pnlFiltrosRelatorio.Controls.Add(this.txtLivro);
            this.pnlFiltrosRelatorio.Controls.Add(this.label6);
            this.pnlFiltrosRelatorio.Controls.Add(this.btnFiltrar);
            this.pnlFiltrosRelatorio.Controls.Add(this.label2);
            this.pnlFiltrosRelatorio.Controls.Add(this.label5);
            this.pnlFiltrosRelatorio.Controls.Add(this.cmbAcao);
            this.pnlFiltrosRelatorio.Controls.Add(this.label3);
            this.pnlFiltrosRelatorio.Controls.Add(this.lblUsuario);
            this.pnlFiltrosRelatorio.Controls.Add(this.lblLivro);
            this.pnlFiltrosRelatorio.Controls.Add(this.txtUsuario);
            this.pnlFiltrosRelatorio.Location = new System.Drawing.Point(96, 112);
            this.pnlFiltrosRelatorio.Name = "pnlFiltrosRelatorio";
            this.pnlFiltrosRelatorio.Size = new System.Drawing.Size(1164, 200);
            this.pnlFiltrosRelatorio.TabIndex = 134;
            // 
            // lstSugestoesUsuario
            // 
            this.lstSugestoesUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSugestoesUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lstSugestoesUsuario.FormattingEnabled = true;
            this.lstSugestoesUsuario.ItemHeight = 25;
            this.lstSugestoesUsuario.Location = new System.Drawing.Point(7, 82);
            this.lstSugestoesUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.lstSugestoesUsuario.Name = "lstSugestoesUsuario";
            this.lstSugestoesUsuario.ScrollAlwaysVisible = true;
            this.lstSugestoesUsuario.Size = new System.Drawing.Size(387, 77);
            this.lstSugestoesUsuario.TabIndex = 126;
            this.lstSugestoesUsuario.Visible = false;
            // 
            // lstLivros
            // 
            this.lstLivros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstLivros.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lstLivros.FormattingEnabled = true;
            this.lstLivros.ItemHeight = 25;
            this.lstLivros.Location = new System.Drawing.Point(435, 82);
            this.lstLivros.Margin = new System.Windows.Forms.Padding(4);
            this.lstLivros.Name = "lstLivros";
            this.lstLivros.ScrollAlwaysVisible = true;
            this.lstLivros.Size = new System.Drawing.Size(387, 77);
            this.lstLivros.TabIndex = 127;
            this.lstLivros.Visible = false;
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(194)))), ((int)(((byte)(210)))));
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnExportar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnExportar.Location = new System.Drawing.Point(896, 146);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(160, 40);
            this.btnExportar.TabIndex = 129;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label6.Location = new System.Drawing.Point(435, 100);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 25);
            this.label6.TabIndex = 124;
            this.label6.Text = "Bibliotecária responsável:";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(896, 100);
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(160, 40);
            this.btnFiltrar.TabIndex = 111;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 88);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 133;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(36, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(320, 17);
            this.lblSubtitulo.TabIndex = 132;
            this.lblSubtitulo.Text = "Filtre o histórico e exporte informações da operação.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.label1.Location = new System.Drawing.Point(34, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 38);
            this.label1.TabIndex = 128;
            this.label1.Text = "Relatórios";
            // 
            // dtpFim
            // 
            this.dtpFim.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFim.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.dtpFim.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.dtpFim.BorderRadius = 10;
            this.dtpFim.BorderThickness = 1;
            this.dtpFim.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.dtpFim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpFim.HoverBackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpFim.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpFim.IconHoverAreaColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dtpFim.IconHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpFim.Location = new System.Drawing.Point(208, 120);
            this.dtpFim.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFim.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFim.Name = "dtpFim";
            this.dtpFim.PlaceholderColor = System.Drawing.Color.Gray;
            this.dtpFim.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12.2F);
            this.dtpFim.PlaceholderText = "";
            this.dtpFim.SelectedDate = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            this.dtpFim.Size = new System.Drawing.Size(162, 40);
            this.dtpFim.TabIndex = 131;
            this.dtpFim.TabStop = false;
            this.dtpFim.Value = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            // 
            // dtpInicio
            // 
            this.dtpInicio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpInicio.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.dtpInicio.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.dtpInicio.BorderRadius = 10;
            this.dtpInicio.BorderThickness = 1;
            this.dtpInicio.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.dtpInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpInicio.HoverBackColor = System.Drawing.Color.WhiteSmoke;
            this.dtpInicio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpInicio.IconHoverAreaColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.dtpInicio.IconHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.dtpInicio.Location = new System.Drawing.Point(7, 120);
            this.dtpInicio.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpInicio.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.PlaceholderColor = System.Drawing.Color.Gray;
            this.dtpInicio.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12.2F);
            this.dtpInicio.PlaceholderText = "";
            this.dtpInicio.SelectedDate = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            this.dtpInicio.Size = new System.Drawing.Size(162, 40);
            this.dtpInicio.TabIndex = 130;
            this.dtpInicio.TabStop = false;
            this.dtpInicio.Value = new System.DateTime(2025, 11, 1, 0, 0, 0, 0);
            // 
            // cbBibliotecaria
            // 
            this.cbBibliotecaria.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbBibliotecaria.BorderRadius = 8;
            this.cbBibliotecaria.BorderThickness = 1;
            this.cbBibliotecaria.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbBibliotecaria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBibliotecaria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbBibliotecaria.FormattingEnabled = true;
            this.cbBibliotecaria.ItemsFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbBibliotecaria.Location = new System.Drawing.Point(435, 126);
            this.cbBibliotecaria.Name = "cbBibliotecaria";
            this.cbBibliotecaria.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14F);
            this.cbBibliotecaria.PlaceholderMargin = 10;
            this.cbBibliotecaria.PlaceholderText = "Selecione a bibliotecária...";
            this.cbBibliotecaria.Size = new System.Drawing.Size(387, 34);
            this.cbBibliotecaria.TabIndex = 125;
            // 
            // txtLivro
            // 
            this.txtLivro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtLivro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtLivro.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLivro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtLivro.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtLivro.BorderRadius = 10;
            this.txtLivro.BorderThickness = 1;
            this.txtLivro.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLivro.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLivro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtLivro.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtLivro.Location = new System.Drawing.Point(435, 42);
            this.txtLivro.Name = "txtLivro";
            this.txtLivro.Padding = new System.Windows.Forms.Padding(7);
            this.txtLivro.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtLivro.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLivro.PlaceholderMarginLeft = 12;
            this.txtLivro.PlaceholderText = "Digite aqui o nome do livro...";
            this.txtLivro.SelectedText = "";
            this.txtLivro.SelectionLength = 0;
            this.txtLivro.SelectionStart = 0;
            this.txtLivro.Size = new System.Drawing.Size(387, 40);
            this.txtLivro.TabIndex = 91;
            this.txtLivro.TextColor = System.Drawing.Color.Black;
            this.txtLivro.UseSystemPasswordChar = false;
            // 
            // cmbAcao
            // 
            this.cmbAcao.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbAcao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbAcao.BorderRadius = 8;
            this.cmbAcao.BorderThickness = 1;
            this.cmbAcao.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAcao.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAcao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.cmbAcao.FormattingEnabled = true;
            this.cmbAcao.Items.AddRange(new object[] {
            "Todas",
            "Empréstimos",
            "Todas",
            "Empréstimos"});
            this.cmbAcao.ItemsFont = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAcao.Location = new System.Drawing.Point(864, 45);
            this.cmbAcao.Name = "cmbAcao";
            this.cmbAcao.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAcao.PlaceholderMargin = 10;
            this.cmbAcao.PlaceholderText = "Filtre o tipo da ação...";
            this.cmbAcao.Size = new System.Drawing.Size(246, 34);
            this.cmbAcao.TabIndex = 90;
            // 
            // txtUsuario
            // 
            this.txtUsuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUsuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUsuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtUsuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtUsuario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtUsuario.BorderRadius = 10;
            this.txtUsuario.BorderThickness = 1;
            this.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtUsuario.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtUsuario.Location = new System.Drawing.Point(7, 42);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new System.Windows.Forms.Padding(7);
            this.txtUsuario.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtUsuario.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.PlaceholderMarginLeft = 12;
            this.txtUsuario.PlaceholderText = "Digite aqui o nome do usuário...";
            this.txtUsuario.SelectedText = "";
            this.txtUsuario.SelectionLength = 0;
            this.txtUsuario.SelectionStart = 0;
            this.txtUsuario.Size = new System.Drawing.Size(387, 40);
            this.txtUsuario.TabIndex = 1;
            this.txtUsuario.TextColor = System.Drawing.Color.Black;
            this.txtUsuario.UseSystemPasswordChar = false;
            this.txtUsuario.Load += new System.EventHandler(this.txtUsuario_Load);
            // 
            // RelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RelForm";
            this.Text = "InicioForm";
            this.Load += new System.EventHandler(this.RelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlResultadosRelatorio.ResumeLayout(false);
            this.pnlFiltrosRelatorio.ResumeLayout(false);
            this.pnlFiltrosRelatorio.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvHistorico;
        private RoundedComboBox cmbAcao;
        public RoundedTextBox txtUsuario;
        public RoundedTextBox txtLivro;
        private System.Windows.Forms.Button btnFiltrar;
        public System.Windows.Forms.Label lblUsuario;
        public System.Windows.Forms.Label lblLivro;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private RoundedComboBox cbBibliotecaria;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstSugestoesUsuario;
        private System.Windows.Forms.ListBox lstLivros;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExportar;
        private RoundedDatePicker dtpFim;
        private RoundedDatePicker dtpInicio;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
        private System.Windows.Forms.Panel pnlFiltrosRelatorio;
        private System.Windows.Forms.Panel pnlResultadosRelatorio;
    }
}
