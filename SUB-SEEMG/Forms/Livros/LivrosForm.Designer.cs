namespace SUB.Forms.Livros
{
    partial class LivrosForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlResultados = new System.Windows.Forms.Panel();
            this.dgvLivros = new System.Windows.Forms.DataGridView();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.lblUnidades = new System.Windows.Forms.Label();
            this.lblTitulos = new System.Windows.Forms.Label();
            this.mtxCodigoBarras = new RoundedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNome = new RoundedTextBox();
            this.cbDisponibilidade = new RoundedComboBox();
            this.btnProcurar = new System.Windows.Forms.Button();
            this.cbFiltro = new RoundedComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.Titulo = new System.Windows.Forms.Label();
            this.lblTeste = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnlResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).BeginInit();
            this.pnlFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pnlResultados);
            this.panel1.Controls.Add(this.pnlFiltros);
            this.panel1.Controls.Add(this.pnlLinhaCabecalho);
            this.panel1.Controls.Add(this.lblSubtitulo);
            this.panel1.Controls.Add(this.Titulo);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 845);
            this.panel1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(532, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 138;
            this.label4.Text = "teste";
            // 
            // pnlResultados
            // 
            this.pnlResultados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlResultados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultados.Controls.Add(this.dgvLivros);
            this.pnlResultados.Location = new System.Drawing.Point(96, 304);
            this.pnlResultados.Name = "pnlResultados";
            this.pnlResultados.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultados.Size = new System.Drawing.Size(1088, 486);
            this.pnlResultados.TabIndex = 137;
            // 
            // dgvLivros
            // 
            this.dgvLivros.AllowUserToAddRows = false;
            this.dgvLivros.AllowUserToDeleteRows = false;
            this.dgvLivros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLivros.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLivros.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLivros.ColumnHeadersHeight = 42;
            this.dgvLivros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLivros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLivros.Location = new System.Drawing.Point(1, 1);
            this.dgvLivros.Name = "dgvLivros";
            this.dgvLivros.ReadOnly = true;
            this.dgvLivros.RowHeadersWidth = 51;
            this.dgvLivros.RowTemplate.Height = 40;
            this.dgvLivros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLivros.Size = new System.Drawing.Size(1086, 484);
            this.dgvLivros.TabIndex = 3;
            this.dgvLivros.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLivros_CellContentClick);
            this.dgvLivros.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Lista_CellFormatting);
            this.dgvLivros.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvLivros_CellPainting);
            // 
            // pnlFiltros
            // 
            this.pnlFiltros.BackColor = System.Drawing.Color.White;
            this.pnlFiltros.Controls.Add(this.lblUnidades);
            this.pnlFiltros.Controls.Add(this.lblTitulos);
            this.pnlFiltros.Controls.Add(this.mtxCodigoBarras);
            this.pnlFiltros.Controls.Add(this.label3);
            this.pnlFiltros.Controls.Add(this.label2);
            this.pnlFiltros.Controls.Add(this.label6);
            this.pnlFiltros.Controls.Add(this.txtNome);
            this.pnlFiltros.Controls.Add(this.cbDisponibilidade);
            this.pnlFiltros.Controls.Add(this.btnProcurar);
            this.pnlFiltros.Controls.Add(this.cbFiltro);
            this.pnlFiltros.Controls.Add(this.label1);
            this.pnlFiltros.Location = new System.Drawing.Point(96, 112);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(1088, 172);
            this.pnlFiltros.TabIndex = 136;
            // 
            // lblUnidades
            // 
            this.lblUnidades.AutoSize = true;
            this.lblUnidades.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidades.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblUnidades.Location = new System.Drawing.Point(875, 34);
            this.lblUnidades.Name = "lblUnidades";
            this.lblUnidades.Size = new System.Drawing.Size(74, 20);
            this.lblUnidades.TabIndex = 135;
            this.lblUnidades.Text = "Unidades";
            // 
            // lblTitulos
            // 
            this.lblTitulos.AutoSize = true;
            this.lblTitulos.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblTitulos.Location = new System.Drawing.Point(875, 59);
            this.lblTitulos.Name = "lblTitulos";
            this.lblTitulos.Size = new System.Drawing.Size(57, 20);
            this.lblTitulos.TabIndex = 11;
            this.lblTitulos.Text = "Titulos";
            // 
            // mtxCodigoBarras
            // 
            this.mtxCodigoBarras.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.mtxCodigoBarras.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.mtxCodigoBarras.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mtxCodigoBarras.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.mtxCodigoBarras.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.mtxCodigoBarras.BorderRadius = 10;
            this.mtxCodigoBarras.BorderThickness = 1;
            this.mtxCodigoBarras.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mtxCodigoBarras.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxCodigoBarras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.mtxCodigoBarras.HoverBackColor = System.Drawing.Color.LightGray;
            this.mtxCodigoBarras.Location = new System.Drawing.Point(20, 113);
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
            this.mtxCodigoBarras.Size = new System.Drawing.Size(452, 40);
            this.mtxCodigoBarras.TabIndex = 2;
            this.mtxCodigoBarras.TextColor = System.Drawing.Color.Black;
            this.mtxCodigoBarras.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label3.Location = new System.Drawing.Point(20, 87);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 25);
            this.label3.TabIndex = 134;
            this.label3.Text = "Código de barras:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(20, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 132;
            this.label2.Text = "Pesquisa:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label6.Location = new System.Drawing.Point(521, 87);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 25);
            this.label6.TabIndex = 126;
            this.label6.Text = "Disponibilidade:";
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
            this.txtNome.Location = new System.Drawing.Point(20, 39);
            this.txtNome.Name = "txtNome";
            this.txtNome.Padding = new System.Windows.Forms.Padding(7);
            this.txtNome.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtNome.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtNome.PlaceholderMarginLeft = 12;
            this.txtNome.PlaceholderText = "Digite para filtrar...";
            this.txtNome.SelectedText = "";
            this.txtNome.SelectionLength = 0;
            this.txtNome.SelectionStart = 0;
            this.txtNome.Size = new System.Drawing.Size(452, 40);
            this.txtNome.TabIndex = 1;
            this.txtNome.TextColor = System.Drawing.Color.Black;
            this.txtNome.UseSystemPasswordChar = false;
            // 
            // cbDisponibilidade
            // 
            this.cbDisponibilidade.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbDisponibilidade.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbDisponibilidade.BorderRadius = 8;
            this.cbDisponibilidade.BorderThickness = 1;
            this.cbDisponibilidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbDisponibilidade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbDisponibilidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDisponibilidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDisponibilidade.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbDisponibilidade.FormattingEnabled = true;
            this.cbDisponibilidade.Items.AddRange(new object[] {
            "Todos",
            "Disponíveis",
            "Indisponíveis"});
            this.cbDisponibilidade.ItemsFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbDisponibilidade.Location = new System.Drawing.Point(521, 116);
            this.cbDisponibilidade.Name = "cbDisponibilidade";
            this.cbDisponibilidade.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14F);
            this.cbDisponibilidade.PlaceholderMargin = 10;
            this.cbDisponibilidade.PlaceholderText = "Filtre por disponibilidade...";
            this.cbDisponibilidade.Size = new System.Drawing.Size(272, 34);
            this.cbDisponibilidade.TabIndex = 4;
            // 
            // btnProcurar
            // 
            this.btnProcurar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnProcurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProcurar.FlatAppearance.BorderSize = 0;
            this.btnProcurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcurar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnProcurar.ForeColor = System.Drawing.Color.White;
            this.btnProcurar.Location = new System.Drawing.Point(875, 110);
            this.btnProcurar.Name = "btnProcurar";
            this.btnProcurar.Size = new System.Drawing.Size(160, 40);
            this.btnProcurar.TabIndex = 5;
            this.btnProcurar.Text = "Procurar";
            this.btnProcurar.UseVisualStyleBackColor = false;
            this.btnProcurar.Click += new System.EventHandler(this.btnProcurar_Click);
            // 
            // cbFiltro
            // 
            this.cbFiltro.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cbFiltro.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbFiltro.BorderRadius = 8;
            this.cbFiltro.BorderThickness = 1;
            this.cbFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbFiltro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFiltro.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbFiltro.FormattingEnabled = true;
            this.cbFiltro.Items.AddRange(new object[] {
            "Nome",
            "Autor",
            "Gênero"});
            this.cbFiltro.ItemsFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cbFiltro.Location = new System.Drawing.Point(521, 42);
            this.cbFiltro.Name = "cbFiltro";
            this.cbFiltro.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14F);
            this.cbFiltro.PlaceholderMargin = 10;
            this.cbFiltro.PlaceholderText = "Escolha um tipo de busca...";
            this.cbFiltro.Size = new System.Drawing.Size(275, 34);
            this.cbFiltro.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(521, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 128;
            this.label1.Text = "Busque por:";
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 88);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 123;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(36, 62);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(359, 17);
            this.lblSubtitulo.TabIndex = 122;
            this.lblSubtitulo.Text = "Pesquise e acompanhe a disponibilidade do acervo escolar.";
            // 
            // Titulo
            // 
            this.Titulo.AutoSize = true;
            this.Titulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.Titulo.Location = new System.Drawing.Point(34, 20);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(106, 38);
            this.Titulo.TabIndex = 107;
            this.Titulo.Text = "Acervo";
            // 
            // lblTeste
            // 
            this.lblTeste.AutoSize = true;
            this.lblTeste.Location = new System.Drawing.Point(129, 9);
            this.lblTeste.Name = "lblTeste";
            this.lblTeste.Size = new System.Drawing.Size(0, 15);
            this.lblTeste.TabIndex = 17;
            // 
            // LivrosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 845);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTeste);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LivrosForm";
            this.Load += new System.EventHandler(this.LivrosForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlResultados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLivros)).EndInit();
            this.pnlFiltros.ResumeLayout(false);
            this.pnlFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTeste;
        private System.Windows.Forms.DataGridView dgvLivros;
        public System.Windows.Forms.Label Titulo;
        private RoundedComboBox cbDisponibilidade;
        private System.Windows.Forms.Label label6;
        private RoundedComboBox cbFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnProcurar;
        private RoundedTextBox txtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private RoundedTextBox mtxCodigoBarras;
        private System.Windows.Forms.Label lblUnidades;
        private System.Windows.Forms.Label lblTitulos;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
        private System.Windows.Forms.Panel pnlFiltros;
        private System.Windows.Forms.Panel pnlResultados;
        private System.Windows.Forms.Label label4;
    }
}
