namespace SUB.Forms.Usuario
{
    partial class GerenciarTurmasForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlConteudo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel pnlLinha;
        private System.Windows.Forms.Label lblNovaTurma;
        private RoundedTextBox txtNovaTurma;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.ListBox lstTurmas;
        private System.Windows.Forms.Label lblContagem;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlConteudo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.pnlLinha = new System.Windows.Forms.Panel();
            this.lblNovaTurma = new System.Windows.Forms.Label();
            this.txtNovaTurma = new RoundedTextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.lstTurmas = new System.Windows.Forms.ListBox();
            this.lblContagem = new System.Windows.Forms.Label();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlConteudo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.BackColor = System.Drawing.Color.White;
            this.pnlConteudo.Controls.Add(this.lblTitulo);
            this.pnlConteudo.Controls.Add(this.lblSubtitulo);
            this.pnlConteudo.Controls.Add(this.pnlLinha);
            this.pnlConteudo.Controls.Add(this.lblNovaTurma);
            this.pnlConteudo.Controls.Add(this.txtNovaTurma);
            this.pnlConteudo.Controls.Add(this.btnAdicionar);
            this.pnlConteudo.Controls.Add(this.btnEditar);
            this.pnlConteudo.Controls.Add(this.lstTurmas);
            this.pnlConteudo.Controls.Add(this.lblContagem);
            this.pnlConteudo.Controls.Add(this.btnRemover);
            this.pnlConteudo.Controls.Add(this.btnSalvar);
            this.pnlConteudo.Controls.Add(this.lblStatus);
            this.pnlConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlConteudo.Location = new System.Drawing.Point(0, 0);
            this.pnlConteudo.Name = "pnlConteudo";
            this.pnlConteudo.Size = new System.Drawing.Size(650, 500);
            this.pnlConteudo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(40, 23);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(108, 38);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Turmas";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 66);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(385, 19);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Defina as turmas usadas no cadastro de alunos da instituição.";
            // 
            // pnlLinha
            // 
            this.pnlLinha.Location = new System.Drawing.Point(42, 92);
            this.pnlLinha.Name = "pnlLinha";
            this.pnlLinha.Size = new System.Drawing.Size(48, 3);
            this.pnlLinha.TabIndex = 2;
            // 
            // lblNovaTurma
            // 
            this.lblNovaTurma.AutoSize = true;
            this.lblNovaTurma.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblNovaTurma.Location = new System.Drawing.Point(42, 140);
            this.lblNovaTurma.Name = "lblNovaTurma";
            this.lblNovaTurma.Size = new System.Drawing.Size(83, 19);
            this.lblNovaTurma.TabIndex = 3;
            this.lblNovaTurma.Text = "Nova turma";
            // 
            // txtNovaTurma
            // 
            this.txtNovaTurma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtNovaTurma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtNovaTurma.BackColor = System.Drawing.Color.White;
            this.txtNovaTurma.BorderColor = System.Drawing.Color.Black;
            this.txtNovaTurma.BorderFocusColor = System.Drawing.Color.Blue;
            this.txtNovaTurma.BorderRadius = 8;
            this.txtNovaTurma.BorderThickness = 2;
            this.txtNovaTurma.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNovaTurma.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtNovaTurma.Location = new System.Drawing.Point(42, 167);
            this.txtNovaTurma.Name = "txtNovaTurma";
            this.txtNovaTurma.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtNovaTurma.PlaceholderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtNovaTurma.PlaceholderMarginLeft = 12;
            this.txtNovaTurma.PlaceholderText = "Ex.: 6º ANO A";
            this.txtNovaTurma.SelectedText = "";
            this.txtNovaTurma.SelectionLength = 0;
            this.txtNovaTurma.SelectionStart = 0;
            this.txtNovaTurma.Size = new System.Drawing.Size(375, 34);
            this.txtNovaTurma.TabIndex = 0;
            this.txtNovaTurma.TextColor = System.Drawing.Color.Black;
            this.txtNovaTurma.UseSystemPasswordChar = false;
            this.txtNovaTurma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNovaTurma_KeyDown);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdicionar.Location = new System.Drawing.Point(432, 165);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(170, 38);
            this.btnAdicionar.TabIndex = 1;
            this.btnAdicionar.Text = "Adicionar turma";
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnEditar.Location = new System.Drawing.Point(284, 398);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(140, 38);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar selecionada";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // lstTurmas
            // 
            this.lstTurmas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTurmas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lstTurmas.FormattingEnabled = true;
            this.lstTurmas.ItemHeight = 20;
            this.lstTurmas.Location = new System.Drawing.Point(42, 216);
            this.lstTurmas.Name = "lstTurmas";
            this.lstTurmas.Size = new System.Drawing.Size(560, 162);
            this.lstTurmas.TabIndex = 2;
            this.lstTurmas.SelectedIndexChanged += new System.EventHandler(this.lstTurmas_SelectedIndexChanged);
            // 
            // lblContagem
            // 
            this.lblContagem.AutoSize = true;
            this.lblContagem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblContagem.Location = new System.Drawing.Point(42, 404);
            this.lblContagem.Name = "lblContagem";
            this.lblContagem.Size = new System.Drawing.Size(0, 17);
            this.lblContagem.TabIndex = 4;
            // 
            // btnRemover
            // 
            this.btnRemover.Enabled = false;
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRemover.Location = new System.Drawing.Point(432, 398);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(170, 38);
            this.btnRemover.TabIndex = 3;
            this.btnRemover.Text = "Remover selecionada";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.Location = new System.Drawing.Point(432, 440);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(170, 38);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar alterações";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatus.Location = new System.Drawing.Point(42, 438);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(395, 40);
            this.lblStatus.TabIndex = 5;
            // 
            // GerenciarTurmasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 500);
            this.Controls.Add(this.pnlConteudo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GerenciarTurmasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gerenciar turmas";
            this.Load += new System.EventHandler(this.GerenciarTurmasForm_Load);
            this.pnlConteudo.ResumeLayout(false);
            this.pnlConteudo.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
