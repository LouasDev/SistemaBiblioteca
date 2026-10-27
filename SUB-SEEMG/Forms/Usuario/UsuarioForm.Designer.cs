namespace SUB.Forms.Usuario
{
    partial class UsuarioForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlResultadosUsuarios = new System.Windows.Forms.Panel();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.pnlFiltrosUsuarios = new System.Windows.Forms.Panel();
            this.lblEmprestimo = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.Titulo = new System.Windows.Forms.Label();
            this.btnGerenciarTurmas = new System.Windows.Forms.Button();
            this.cmbEmprestimo = new RoundedComboBox();
            this.cmbTipoUsuario = new RoundedComboBox();
            this.txtNome = new RoundedTextBox();
            this.panel1.SuspendLayout();
            this.pnlResultadosUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.pnlFiltrosUsuarios.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlResultadosUsuarios);
            this.panel1.Controls.Add(this.pnlFiltrosUsuarios);
            this.panel1.Controls.Add(this.pnlLinhaCabecalho);
            this.panel1.Controls.Add(this.lblSubtitulo);
            this.panel1.Controls.Add(this.Titulo);
            this.panel1.Controls.Add(this.btnGerenciarTurmas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 845);
            this.panel1.TabIndex = 6;
            // 
            // pnlResultadosUsuarios
            // 
            this.pnlResultadosUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResultadosUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultadosUsuarios.Controls.Add(this.dgvUsuarios);
            this.pnlResultadosUsuarios.Location = new System.Drawing.Point(96, 244);
            this.pnlResultadosUsuarios.Name = "pnlResultadosUsuarios";
            this.pnlResultadosUsuarios.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultadosUsuarios.Size = new System.Drawing.Size(1088, 573);
            this.pnlResultadosUsuarios.TabIndex = 117;
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsuarios.Location = new System.Drawing.Point(1, 1);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUsuarios.Size = new System.Drawing.Size(1086, 571);
            this.dgvUsuarios.TabIndex = 5;
            this.dgvUsuarios.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuarios_CellContentClick);
            // 
            // pnlFiltrosUsuarios
            // 
            this.pnlFiltrosUsuarios.BackColor = System.Drawing.Color.White;
            this.pnlFiltrosUsuarios.Controls.Add(this.lblEmprestimo);
            this.pnlFiltrosUsuarios.Controls.Add(this.cmbEmprestimo);
            this.pnlFiltrosUsuarios.Controls.Add(this.btnFiltrar);
            this.pnlFiltrosUsuarios.Controls.Add(this.label3);
            this.pnlFiltrosUsuarios.Controls.Add(this.cmbTipoUsuario);
            this.pnlFiltrosUsuarios.Controls.Add(this.label1);
            this.pnlFiltrosUsuarios.Controls.Add(this.txtNome);
            this.pnlFiltrosUsuarios.Location = new System.Drawing.Point(93, 116);
            this.pnlFiltrosUsuarios.Name = "pnlFiltrosUsuarios";
            this.pnlFiltrosUsuarios.Size = new System.Drawing.Size(1175, 112);
            this.pnlFiltrosUsuarios.TabIndex = 116;
            // 
            // lblEmprestimo
            // 
            this.lblEmprestimo.AutoSize = true;
            this.lblEmprestimo.BackColor = System.Drawing.Color.Transparent;
            this.lblEmprestimo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmprestimo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblEmprestimo.Location = new System.Drawing.Point(699, 16);
            this.lblEmprestimo.Name = "lblEmprestimo";
            this.lblEmprestimo.Size = new System.Drawing.Size(225, 25);
            this.lblEmprestimo.TabIndex = 112;
            this.lblEmprestimo.Text = "Situação do empréstimo:";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnFiltrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(997, 42);
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(160, 40);
            this.btnFiltrar.TabIndex = 4;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label3.Location = new System.Drawing.Point(400, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 25);
            this.label3.TabIndex = 89;
            this.label3.Text = "Tipo de usuário:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 87;
            this.label1.Text = "Nome:";
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 88);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 115;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(36, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(328, 17);
            this.lblSubtitulo.TabIndex = 114;
            this.lblSubtitulo.Text = "Consulte e filtre os usuários cadastrados na biblioteca.";
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.Titulo.Location = new System.Drawing.Point(34, 20);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(115, 38);
            this.Titulo.TabIndex = 86;
            this.Titulo.Text = "Pessoas";
            // 
            // btnGerenciarTurmas
            // 
            this.btnGerenciarTurmas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerenciarTurmas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerenciarTurmas.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGerenciarTurmas.Location = new System.Drawing.Point(1010, 35);
            this.btnGerenciarTurmas.Name = "btnGerenciarTurmas";
            this.btnGerenciarTurmas.Size = new System.Drawing.Size(174, 38);
            this.btnGerenciarTurmas.TabIndex = 118;
            this.btnGerenciarTurmas.Text = "Gerenciar turmas";
            this.btnGerenciarTurmas.UseVisualStyleBackColor = false;
            this.btnGerenciarTurmas.Click += new System.EventHandler(this.btnGerenciarTurmas_Click);
            // 
            // cmbEmprestimo
            // 
            this.cmbEmprestimo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbEmprestimo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbEmprestimo.BorderRadius = 8;
            this.cmbEmprestimo.BorderThickness = 1;
            this.cmbEmprestimo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEmprestimo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmprestimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEmprestimo.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmprestimo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.cmbEmprestimo.FormattingEnabled = true;
            this.cmbEmprestimo.Items.AddRange(new object[] {
            "Todos",
            "Sem empréstimo",
            "Ativo",
            "Atrasado"});
            this.cmbEmprestimo.ItemsFont = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmprestimo.Location = new System.Drawing.Point(699, 45);
            this.cmbEmprestimo.Name = "cmbEmprestimo";
            this.cmbEmprestimo.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmprestimo.PlaceholderMargin = 10;
            this.cmbEmprestimo.PlaceholderText = "Filtre por situação...";
            this.cmbEmprestimo.Size = new System.Drawing.Size(250, 34);
            this.cmbEmprestimo.TabIndex = 2;
            // 
            // cmbTipoUsuario
            // 
            this.cmbTipoUsuario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbTipoUsuario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbTipoUsuario.BorderRadius = 8;
            this.cmbTipoUsuario.BorderThickness = 1;
            this.cmbTipoUsuario.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTipoUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoUsuario.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.cmbTipoUsuario.FormattingEnabled = true;
            this.cmbTipoUsuario.Items.AddRange(new object[] {
            "Todos",
            "Aluno(a)",
            "Bibliotecário(a)",
            "Professor(a)",
            "Outros"});
            this.cmbTipoUsuario.ItemsFont = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoUsuario.Location = new System.Drawing.Point(400, 45);
            this.cmbTipoUsuario.Name = "cmbTipoUsuario";
            this.cmbTipoUsuario.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoUsuario.PlaceholderMargin = 10;
            this.cmbTipoUsuario.PlaceholderText = "Filtre por tipo de usuário...";
            this.cmbTipoUsuario.Size = new System.Drawing.Size(270, 34);
            this.cmbTipoUsuario.TabIndex = 3;
            // 
            // txtNome
            // 
            this.txtNome.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtNome.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtNome.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtNome.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtNome.BorderRadius = 10;
            this.txtNome.BorderThickness = 1;
            this.txtNome.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNome.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtNome.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtNome.Location = new System.Drawing.Point(20, 42);
            this.txtNome.Name = "txtNome";
            this.txtNome.Padding = new System.Windows.Forms.Padding(7);
            this.txtNome.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtNome.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.PlaceholderMarginLeft = 12;
            this.txtNome.PlaceholderText = "Digite aqui o nome...";
            this.txtNome.SelectedText = "";
            this.txtNome.SelectionLength = 0;
            this.txtNome.SelectionStart = 0;
            this.txtNome.Size = new System.Drawing.Size(350, 40);
            this.txtNome.TabIndex = 1;
            this.txtNome.TextColor = System.Drawing.Color.Black;
            this.txtNome.UseSystemPasswordChar = false;
            // 
            // UsuarioForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsuarioForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InicioForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlResultadosUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.pnlFiltrosUsuarios.ResumeLayout(false);
            this.pnlFiltrosUsuarios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label Titulo;
        public System.Windows.Forms.Label label3;
        private RoundedComboBox cmbTipoUsuario;
        private System.Windows.Forms.Button btnFiltrar;
        public System.Windows.Forms.Label lblEmprestimo;
        private RoundedComboBox cmbEmprestimo;
        public RoundedTextBox txtNome;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
        private System.Windows.Forms.Panel pnlFiltrosUsuarios;
        private System.Windows.Forms.Panel pnlResultadosUsuarios;
        private System.Windows.Forms.Button btnGerenciarTurmas;
    }
}
