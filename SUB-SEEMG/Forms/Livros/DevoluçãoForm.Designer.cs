namespace SUB.Forms.Livros
{
    partial class DevoluçãoForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConfirmarDevolucao = new System.Windows.Forms.Button();
            this.btnProrrogar = new System.Windows.Forms.Button();
            this.Titulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.pnlResultadosEmprestimos = new System.Windows.Forms.Panel();
            this.dgvEmprestimos = new System.Windows.Forms.DataGridView();
            this.lblDadosLivro = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mtxCodigoBarras = new RoundedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUsuario = new RoundedTextBox();
            this.cbFiltroEmprestimo = new RoundedComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBuscarEmprestimo = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtLivro = new RoundedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstSugestoesUsuario = new System.Windows.Forms.ListBox();
            this.pnlAcoesDev = new SUB.ElementosPersonalizados.GradientPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlResultadosEmprestimos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmprestimos)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlAcoesDev.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmarDevolucao
            // 
            this.btnConfirmarDevolucao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarDevolucao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnConfirmarDevolucao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarDevolucao.FlatAppearance.BorderSize = 0;
            this.btnConfirmarDevolucao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarDevolucao.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarDevolucao.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarDevolucao.Location = new System.Drawing.Point(16, 46);
            this.btnConfirmarDevolucao.Name = "btnConfirmarDevolucao";
            this.btnConfirmarDevolucao.Size = new System.Drawing.Size(220, 44);
            this.btnConfirmarDevolucao.TabIndex = 118;
            this.btnConfirmarDevolucao.Text = "Devolver";
            this.btnConfirmarDevolucao.UseVisualStyleBackColor = false;
            this.btnConfirmarDevolucao.Click += new System.EventHandler(this.btnConfirmarDevolucao_Click);
            // 
            // btnProrrogar
            // 
            this.btnProrrogar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProrrogar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.btnProrrogar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProrrogar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(194)))), ((int)(((byte)(210)))));
            this.btnProrrogar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProrrogar.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnProrrogar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnProrrogar.Location = new System.Drawing.Point(16, 100);
            this.btnProrrogar.Name = "btnProrrogar";
            this.btnProrrogar.Size = new System.Drawing.Size(220, 44);
            this.btnProrrogar.TabIndex = 117;
            this.btnProrrogar.Text = "Prorrogar prazo";
            this.btnProrrogar.UseVisualStyleBackColor = false;
            this.btnProrrogar.Click += new System.EventHandler(this.btnProrrogar_Click);
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.Titulo.Location = new System.Drawing.Point(34, 20);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(163, 38);
            this.Titulo.TabIndex = 106;
            this.Titulo.Text = "Devoluções";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(36, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(362, 17);
            this.lblSubtitulo.TabIndex = 157;
            this.lblSubtitulo.Text = "Localize o empréstimo e confirme a devolução do exemplar.";
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 88);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 158;
            // 
            // pnlResultadosEmprestimos
            // 
            this.pnlResultadosEmprestimos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResultadosEmprestimos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultadosEmprestimos.Controls.Add(this.dgvEmprestimos);
            this.pnlResultadosEmprestimos.Location = new System.Drawing.Point(96, 323);
            this.pnlResultadosEmprestimos.Name = "pnlResultadosEmprestimos";
            this.pnlResultadosEmprestimos.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultadosEmprestimos.Size = new System.Drawing.Size(1088, 425);
            this.pnlResultadosEmprestimos.TabIndex = 122;
            // 
            // dgvEmprestimos
            // 
            this.dgvEmprestimos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmprestimos.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmprestimos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmprestimos.ColumnHeadersHeight = 42;
            this.dgvEmprestimos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvEmprestimos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmprestimos.Location = new System.Drawing.Point(1, 1);
            this.dgvEmprestimos.Name = "dgvEmprestimos";
            this.dgvEmprestimos.ReadOnly = true;
            this.dgvEmprestimos.RowHeadersWidth = 51;
            this.dgvEmprestimos.RowTemplate.Height = 40;
            this.dgvEmprestimos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmprestimos.Size = new System.Drawing.Size(1086, 423);
            this.dgvEmprestimos.TabIndex = 119;
            this.dgvEmprestimos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmprestimos_CellClick);
            this.dgvEmprestimos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmprestimos_CellContentClick);
            this.dgvEmprestimos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEmprestimos_CellFormatting);
            this.dgvEmprestimos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvEmprestimos_CellPainting);
            // 
            // lblDadosLivro
            // 
            this.lblDadosLivro.AutoSize = true;
            this.lblDadosLivro.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDadosLivro.Location = new System.Drawing.Point(22, 321);
            this.lblDadosLivro.Name = "lblDadosLivro";
            this.lblDadosLivro.Size = new System.Drawing.Size(0, 19);
            this.lblDadosLivro.TabIndex = 121;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lstSugestoesUsuario);
            this.panel1.Controls.Add(this.mtxCodigoBarras);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtUsuario);
            this.panel1.Controls.Add(this.cbFiltroEmprestimo);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnBuscarEmprestimo);
            this.panel1.Controls.Add(this.lblNome);
            this.panel1.Controls.Add(this.txtLivro);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pnlLinhaCabecalho);
            this.panel1.Controls.Add(this.lblSubtitulo);
            this.panel1.Controls.Add(this.Titulo);
            this.panel1.Controls.Add(this.pnlResultadosEmprestimos);
            this.panel1.Controls.Add(this.pnlAcoesDev);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 845);
            this.panel1.TabIndex = 0;
            // 
            // mtxCodigoBarras
            // 
            this.mtxCodigoBarras.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.mtxCodigoBarras.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.mtxCodigoBarras.BackColor = System.Drawing.Color.White;
            this.mtxCodigoBarras.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.mtxCodigoBarras.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.mtxCodigoBarras.BorderRadius = 10;
            this.mtxCodigoBarras.BorderThickness = 1;
            this.mtxCodigoBarras.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mtxCodigoBarras.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxCodigoBarras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.mtxCodigoBarras.HoverBackColor = System.Drawing.Color.LightGray;
            this.mtxCodigoBarras.Location = new System.Drawing.Point(658, 183);
            this.mtxCodigoBarras.Margin = new System.Windows.Forms.Padding(4);
            this.mtxCodigoBarras.Name = "mtxCodigoBarras";
            this.mtxCodigoBarras.OnlyNumbers = true;
            this.mtxCodigoBarras.Padding = new System.Windows.Forms.Padding(8, 10, 8, 10);
            this.mtxCodigoBarras.PlaceholderColor = System.Drawing.Color.Gray;
            this.mtxCodigoBarras.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxCodigoBarras.PlaceholderMarginLeft = 12;
            this.mtxCodigoBarras.PlaceholderText = "Escaneie para buscar...";
            this.mtxCodigoBarras.SelectedText = "";
            this.mtxCodigoBarras.SelectionLength = 0;
            this.mtxCodigoBarras.SelectionStart = 0;
            this.mtxCodigoBarras.Size = new System.Drawing.Size(243, 34);
            this.mtxCodigoBarras.TabIndex = 156;
            this.mtxCodigoBarras.TextColor = System.Drawing.Color.Black;
            this.mtxCodigoBarras.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(105, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 25);
            this.label1.TabIndex = 154;
            this.label1.Text = "Nome da pessoa:";
            // 
            // txtUsuario
            // 
            this.txtUsuario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtUsuario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtUsuario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtUsuario.BorderRadius = 10;
            this.txtUsuario.BorderThickness = 1;
            this.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtUsuario.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtUsuario.Location = new System.Drawing.Point(109, 183);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new System.Windows.Forms.Padding(7);
            this.txtUsuario.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtUsuario.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12.2F);
            this.txtUsuario.PlaceholderMarginLeft = 12;
            this.txtUsuario.PlaceholderText = "Digite o nome da pessoa...";
            this.txtUsuario.SelectedText = "";
            this.txtUsuario.SelectionLength = 0;
            this.txtUsuario.SelectionStart = 0;
            this.txtUsuario.Size = new System.Drawing.Size(258, 34);
            this.txtUsuario.TabIndex = 147;
            this.txtUsuario.TextColor = System.Drawing.Color.Black;
            this.txtUsuario.UseSystemPasswordChar = false;
            // 
            // cbFiltroEmprestimo
            // 
            this.cbFiltroEmprestimo.BackColor = System.Drawing.Color.White;
            this.cbFiltroEmprestimo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbFiltroEmprestimo.BorderRadius = 8;
            this.cbFiltroEmprestimo.BorderThickness = 1;
            this.cbFiltroEmprestimo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFiltroEmprestimo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltroEmprestimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFiltroEmprestimo.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbFiltroEmprestimo.FormattingEnabled = true;
            this.cbFiltroEmprestimo.Items.AddRange(new object[] {
            "Todos",
            "Devolvido",
            "Atrasado",
            "Ativo"});
            this.cbFiltroEmprestimo.ItemsFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbFiltroEmprestimo.Location = new System.Drawing.Point(110, 256);
            this.cbFiltroEmprestimo.Name = "cbFiltroEmprestimo";
            this.cbFiltroEmprestimo.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14F);
            this.cbFiltroEmprestimo.PlaceholderMargin = 10;
            this.cbFiltroEmprestimo.PlaceholderText = "Selecione uma situação...";
            this.cbFiltroEmprestimo.Size = new System.Drawing.Size(258, 34);
            this.cbFiltroEmprestimo.TabIndex = 153;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label6.Location = new System.Drawing.Point(106, 228);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 25);
            this.label6.TabIndex = 152;
            this.label6.Text = "Situação:";
            // 
            // btnBuscarEmprestimo
            // 
            this.btnBuscarEmprestimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnBuscarEmprestimo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarEmprestimo.FlatAppearance.BorderSize = 0;
            this.btnBuscarEmprestimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarEmprestimo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnBuscarEmprestimo.ForeColor = System.Drawing.Color.White;
            this.btnBuscarEmprestimo.Location = new System.Drawing.Point(695, 245);
            this.btnBuscarEmprestimo.Name = "btnBuscarEmprestimo";
            this.btnBuscarEmprestimo.Size = new System.Drawing.Size(168, 41);
            this.btnBuscarEmprestimo.TabIndex = 151;
            this.btnBuscarEmprestimo.Text = "Procurar";
            this.btnBuscarEmprestimo.UseVisualStyleBackColor = false;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.Transparent;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblNome.Location = new System.Drawing.Point(389, 155);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(140, 25);
            this.lblNome.TabIndex = 149;
            this.lblNome.Text = "Nome do livro:";
            // 
            // txtLivro
            // 
            this.txtLivro.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtLivro.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtLivro.BackColor = System.Drawing.Color.White;
            this.txtLivro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtLivro.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtLivro.BorderRadius = 10;
            this.txtLivro.BorderThickness = 1;
            this.txtLivro.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLivro.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLivro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtLivro.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtLivro.Location = new System.Drawing.Point(393, 183);
            this.txtLivro.Name = "txtLivro";
            this.txtLivro.Padding = new System.Windows.Forms.Padding(7);
            this.txtLivro.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtLivro.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12.2F);
            this.txtLivro.PlaceholderMarginLeft = 12;
            this.txtLivro.PlaceholderText = "Digite o nome do livro...";
            this.txtLivro.SelectedText = "";
            this.txtLivro.SelectionLength = 0;
            this.txtLivro.SelectionStart = 0;
            this.txtLivro.Size = new System.Drawing.Size(238, 34);
            this.txtLivro.TabIndex = 148;
            this.txtLivro.TextColor = System.Drawing.Color.Black;
            this.txtLivro.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label4.Location = new System.Drawing.Point(654, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 25);
            this.label4.TabIndex = 150;
            this.label4.Text = "Código de barras:";
            // 
            // lstSugestoesUsuario
            // 
            this.lstSugestoesUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSugestoesUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lstSugestoesUsuario.FormattingEnabled = true;
            this.lstSugestoesUsuario.ItemHeight = 25;
            this.lstSugestoesUsuario.Location = new System.Drawing.Point(109, 217);
            this.lstSugestoesUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.lstSugestoesUsuario.Name = "lstSugestoesUsuario";
            this.lstSugestoesUsuario.ScrollAlwaysVisible = true;
            this.lstSugestoesUsuario.Size = new System.Drawing.Size(258, 102);
            this.lstSugestoesUsuario.TabIndex = 155;
            this.lstSugestoesUsuario.Visible = false;
            // 
            // pnlAcoesDev
            // 
            this.pnlAcoesDev.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlAcoesDev.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlAcoesDev.BorderRadius = 20;
            this.pnlAcoesDev.Controls.Add(this.label2);
            this.pnlAcoesDev.Controls.Add(this.btnProrrogar);
            this.pnlAcoesDev.Controls.Add(this.btnConfirmarDevolucao);
            this.pnlAcoesDev.Gradientebottom = System.Drawing.Color.Transparent;
            this.pnlAcoesDev.Gradientetop = System.Drawing.Color.Transparent;
            this.pnlAcoesDev.Location = new System.Drawing.Point(934, 136);
            this.pnlAcoesDev.Name = "pnlAcoesDev";
            this.pnlAcoesDev.Size = new System.Drawing.Size(252, 168);
            this.pnlAcoesDev.TabIndex = 159;
            this.pnlAcoesDev.Paint += new System.Windows.Forms.PaintEventHandler(this.gradientPanel1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 25);
            this.label2.TabIndex = 151;
            this.label2.Text = "Ações:";
            // 
            // DevoluçãoForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDadosLivro);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DevoluçãoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.DevoluçãoForm_Activated);
            this.Load += new System.EventHandler(this.DevoluçãoForm_Load);
            this.pnlResultadosEmprestimos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmprestimos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlAcoesDev.ResumeLayout(false);
            this.pnlAcoesDev.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmarDevolucao;
        private System.Windows.Forms.Button btnProrrogar;
        public System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
        private System.Windows.Forms.Panel pnlResultadosEmprestimos;
        private System.Windows.Forms.DataGridView dgvEmprestimos;
        private System.Windows.Forms.Label lblDadosLivro;
        private System.Windows.Forms.Panel panel1;
        private RoundedTextBox mtxCodigoBarras;
        private System.Windows.Forms.ListBox lstSugestoesUsuario;
        public System.Windows.Forms.Label label1;
        private RoundedTextBox txtUsuario;
        private RoundedComboBox cbFiltroEmprestimo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBuscarEmprestimo;
        public System.Windows.Forms.Label lblNome;
        private RoundedTextBox txtLivro;
        private ElementosPersonalizados.GradientPanel pnlAcoesDev;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label2;
    }
}
