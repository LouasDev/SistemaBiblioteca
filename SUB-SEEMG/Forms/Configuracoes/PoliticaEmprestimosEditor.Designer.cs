namespace SUB.Forms.Configuracoes
{
    partial class PoliticaEmprestimosEditor
    {
        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblLimites = new System.Windows.Forms.Label();
            this.lblLimitesDica = new System.Windows.Forms.Label();
            this.lblPrazos = new System.Windows.Forms.Label();
            this.lblDica = new System.Windows.Forms.Label();
            this.pnlFiltrosLimites = new SUB.ElementosPersonalizados.RoundedSurfacePanel();
            this.lblAluno = new System.Windows.Forms.Label();
            this.numAluno = new RoundedTextBox();
            this.chkSemLimiteAluno = new System.Windows.Forms.CheckBox();
            this.lblProfessor = new System.Windows.Forms.Label();
            this.numProfessor = new RoundedTextBox();
            this.chkSemLimiteProfessor = new System.Windows.Forms.CheckBox();
            this.lblBibliotecario = new System.Windows.Forms.Label();
            this.numBibliotecario = new RoundedTextBox();
            this.chkSemLimiteBibliotecario = new System.Windows.Forms.CheckBox();
            this.btnMaisAluno = new System.Windows.Forms.Button();
            this.btnMenosAluno = new System.Windows.Forms.Button();
            this.btnMaisProfessor = new System.Windows.Forms.Button();
            this.btnMenosProfessor = new System.Windows.Forms.Button();
            this.btnMaisBibliotecario = new System.Windows.Forms.Button();
            this.btnMenosBibliotecario = new System.Windows.Forms.Button();
            this.pnlFiltrosPrazos = new SUB.ElementosPersonalizados.RoundedSurfacePanel();
            this.lblPrazo = new System.Windows.Forms.Label();
            this.numPrazo = new RoundedTextBox();
            this.lblProrrogacoes = new System.Windows.Forms.Label();
            this.numProrrogacoes = new RoundedTextBox();
            this.chkSemLimiteProrrogacoes = new System.Windows.Forms.CheckBox();
            this.btnMaisPrazo = new System.Windows.Forms.Button();
            this.btnMenosPrazo = new System.Windows.Forms.Button();
            this.btnMaisProrrogacoes = new System.Windows.Forms.Button();
            this.btnMenosProrrogacoes = new System.Windows.Forms.Button();
            this.pnlFiltrosLimites.SuspendLayout();
            this.pnlFiltrosPrazos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            // 
            // lblSubtitulo
            // 
            // 
            // lblLimites
            // 
            this.lblLimites.BackColor = System.Drawing.Color.Transparent;
            this.lblLimites.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLimites.Location = new System.Drawing.Point(18, 14);
            this.lblLimites.Name = "lblLimites";
            this.lblLimites.Size = new System.Drawing.Size(444, 26);
            this.lblLimites.TabIndex = 0;
            this.lblLimites.Text = "Livros simultâneos por pessoa";
            // 
            // lblLimitesDica
            // 
            this.lblLimitesDica.BackColor = System.Drawing.Color.Transparent;
            this.lblLimitesDica.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLimitesDica.Location = new System.Drawing.Point(18, 42);
            this.lblLimitesDica.Name = "lblLimitesDica";
            this.lblLimitesDica.Size = new System.Drawing.Size(444, 18);
            this.lblLimitesDica.TabIndex = 1;
            this.lblLimitesDica.Text = "Os livros em atraso também entram nessa contagem.";
            // 
            // lblPrazos
            // 
            this.lblPrazos.BackColor = System.Drawing.Color.Transparent;
            this.lblPrazos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPrazos.Location = new System.Drawing.Point(18, 14);
            this.lblPrazos.Name = "lblPrazos";
            this.lblPrazos.Size = new System.Drawing.Size(444, 26);
            this.lblPrazos.TabIndex = 0;
            this.lblPrazos.Text = "Prazos e prorrogações";
            // 
            // lblDica
            // 
            this.lblDica.BackColor = System.Drawing.Color.Transparent;
            this.lblDica.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDica.Location = new System.Drawing.Point(2, 356);
            this.lblDica.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblDica.Name = "lblDica";
            this.lblDica.Size = new System.Drawing.Size(476, 54);
            this.lblDica.TabIndex = 2;
            this.lblDica.Text = "Use zero para impedir novos empréstimos ou prorrogações. Empréstimos rápidos segu" +
    "em regras separadas. Os prazos já registrados são preservados; cada prorrogação " +
    "permite até 14 dias.";
            // 
            // pnlFiltrosLimites
            // 
            this.pnlFiltrosLimites.BackColor = System.Drawing.Color.White;
            this.pnlFiltrosLimites.Controls.Add(this.lblLimites);
            this.pnlFiltrosLimites.Controls.Add(this.lblLimitesDica);
            this.pnlFiltrosLimites.Controls.Add(this.lblAluno);
            this.pnlFiltrosLimites.Controls.Add(this.numAluno);
            this.pnlFiltrosLimites.Controls.Add(this.chkSemLimiteAluno);
            this.pnlFiltrosLimites.Controls.Add(this.lblProfessor);
            this.pnlFiltrosLimites.Controls.Add(this.numProfessor);
            this.pnlFiltrosLimites.Controls.Add(this.chkSemLimiteProfessor);
            this.pnlFiltrosLimites.Controls.Add(this.lblBibliotecario);
            this.pnlFiltrosLimites.Controls.Add(this.numBibliotecario);
            this.pnlFiltrosLimites.Controls.Add(this.chkSemLimiteBibliotecario);
            this.pnlFiltrosLimites.Controls.Add(this.btnMaisAluno);
            this.pnlFiltrosLimites.Controls.Add(this.btnMenosAluno);
            this.pnlFiltrosLimites.Controls.Add(this.btnMaisProfessor);
            this.pnlFiltrosLimites.Controls.Add(this.btnMenosProfessor);
            this.pnlFiltrosLimites.Controls.Add(this.btnMaisBibliotecario);
            this.pnlFiltrosLimites.Controls.Add(this.btnMenosBibliotecario);
            this.pnlFiltrosLimites.Location = new System.Drawing.Point(0, 0);
            this.pnlFiltrosLimites.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlFiltrosLimites.Name = "pnlFiltrosLimites";
            this.pnlFiltrosLimites.Size = new System.Drawing.Size(480, 190);
            this.pnlFiltrosLimites.TabIndex = 0;
            // 
            // lblAluno
            // 
            this.lblAluno.BackColor = System.Drawing.Color.Transparent;
            this.lblAluno.Location = new System.Drawing.Point(18, 65);
            this.lblAluno.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblAluno.Name = "lblAluno";
            this.lblAluno.Size = new System.Drawing.Size(170, 28);
            this.lblAluno.TabIndex = 2;
            this.lblAluno.Text = "Alunos";
            // 
            // numAluno
            // 
            this.numAluno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numAluno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numAluno.BackColor = System.Drawing.Color.White;
            this.numAluno.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.numAluno.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.numAluno.BorderRadius = 10;
            this.numAluno.BorderThickness = 1;
            this.numAluno.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numAluno.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numAluno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.numAluno.HoverBackColor = System.Drawing.Color.LightGray;
            this.numAluno.Location = new System.Drawing.Point(190, 61);
            this.numAluno.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.numAluno.Name = "numAluno";
            this.numAluno.OnlyNumbers = true;
            this.numAluno.Padding = new System.Windows.Forms.Padding(7);
            this.numAluno.PlaceholderColor = System.Drawing.Color.Gray;
            this.numAluno.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F);
            this.numAluno.PlaceholderMarginLeft = 12;
            this.numAluno.PlaceholderText = "";
            this.numAluno.SelectedText = "";
            this.numAluno.SelectionLength = 0;
            this.numAluno.SelectionStart = 0;
            this.numAluno.Size = new System.Drawing.Size(86, 35);
            this.numAluno.TabIndex = 0;
            this.numAluno.TextColor = System.Drawing.Color.Black;
            this.numAluno.UseSystemPasswordChar = false;
            // 
            // chkSemLimiteAluno
            // 
            this.chkSemLimiteAluno.Location = new System.Drawing.Point(328, 65);
            this.chkSemLimiteAluno.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.chkSemLimiteAluno.Name = "chkSemLimiteAluno";
            this.chkSemLimiteAluno.Size = new System.Drawing.Size(132, 28);
            this.chkSemLimiteAluno.TabIndex = 1;
            this.chkSemLimiteAluno.Text = "Sem limite";
            this.chkSemLimiteAluno.CheckedChanged += new System.EventHandler(this.SemLimite_CheckedChanged);
            // 
            // lblProfessor
            // 
            this.lblProfessor.BackColor = System.Drawing.Color.Transparent;
            this.lblProfessor.Location = new System.Drawing.Point(18, 107);
            this.lblProfessor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblProfessor.Name = "lblProfessor";
            this.lblProfessor.Size = new System.Drawing.Size(170, 28);
            this.lblProfessor.TabIndex = 3;
            this.lblProfessor.Text = "Professores";
            // 
            // numProfessor
            // 
            this.numProfessor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numProfessor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numProfessor.BackColor = System.Drawing.Color.White;
            this.numProfessor.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.numProfessor.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.numProfessor.BorderRadius = 10;
            this.numProfessor.BorderThickness = 1;
            this.numProfessor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numProfessor.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numProfessor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.numProfessor.HoverBackColor = System.Drawing.Color.LightGray;
            this.numProfessor.Location = new System.Drawing.Point(190, 103);
            this.numProfessor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.numProfessor.Name = "numProfessor";
            this.numProfessor.OnlyNumbers = true;
            this.numProfessor.Padding = new System.Windows.Forms.Padding(7);
            this.numProfessor.PlaceholderColor = System.Drawing.Color.Gray;
            this.numProfessor.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F);
            this.numProfessor.PlaceholderMarginLeft = 12;
            this.numProfessor.PlaceholderText = "";
            this.numProfessor.SelectedText = "";
            this.numProfessor.SelectionLength = 0;
            this.numProfessor.SelectionStart = 0;
            this.numProfessor.Size = new System.Drawing.Size(86, 35);
            this.numProfessor.TabIndex = 2;
            this.numProfessor.TextColor = System.Drawing.Color.Black;
            this.numProfessor.UseSystemPasswordChar = false;
            // 
            // chkSemLimiteProfessor
            // 
            this.chkSemLimiteProfessor.Location = new System.Drawing.Point(328, 107);
            this.chkSemLimiteProfessor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.chkSemLimiteProfessor.Name = "chkSemLimiteProfessor";
            this.chkSemLimiteProfessor.Size = new System.Drawing.Size(132, 28);
            this.chkSemLimiteProfessor.TabIndex = 3;
            this.chkSemLimiteProfessor.Text = "Sem limite";
            this.chkSemLimiteProfessor.CheckedChanged += new System.EventHandler(this.SemLimite_CheckedChanged);
            // 
            // lblBibliotecario
            // 
            this.lblBibliotecario.BackColor = System.Drawing.Color.Transparent;
            this.lblBibliotecario.Location = new System.Drawing.Point(18, 149);
            this.lblBibliotecario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblBibliotecario.Name = "lblBibliotecario";
            this.lblBibliotecario.Size = new System.Drawing.Size(170, 28);
            this.lblBibliotecario.TabIndex = 4;
            this.lblBibliotecario.Text = "Bibliotecários";
            // 
            // numBibliotecario
            // 
            this.numBibliotecario.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numBibliotecario.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numBibliotecario.BackColor = System.Drawing.Color.White;
            this.numBibliotecario.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.numBibliotecario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.numBibliotecario.BorderRadius = 10;
            this.numBibliotecario.BorderThickness = 1;
            this.numBibliotecario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numBibliotecario.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numBibliotecario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.numBibliotecario.HoverBackColor = System.Drawing.Color.LightGray;
            this.numBibliotecario.Location = new System.Drawing.Point(190, 145);
            this.numBibliotecario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.numBibliotecario.Name = "numBibliotecario";
            this.numBibliotecario.OnlyNumbers = true;
            this.numBibliotecario.Padding = new System.Windows.Forms.Padding(7);
            this.numBibliotecario.PlaceholderColor = System.Drawing.Color.Gray;
            this.numBibliotecario.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F);
            this.numBibliotecario.PlaceholderMarginLeft = 12;
            this.numBibliotecario.PlaceholderText = "";
            this.numBibliotecario.SelectedText = "";
            this.numBibliotecario.SelectionLength = 0;
            this.numBibliotecario.SelectionStart = 0;
            this.numBibliotecario.Size = new System.Drawing.Size(86, 35);
            this.numBibliotecario.TabIndex = 4;
            this.numBibliotecario.TextColor = System.Drawing.Color.Black;
            this.numBibliotecario.UseSystemPasswordChar = false;
            // 
            // chkSemLimiteBibliotecario
            // 
            this.chkSemLimiteBibliotecario.Location = new System.Drawing.Point(328, 149);
            this.chkSemLimiteBibliotecario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.chkSemLimiteBibliotecario.Name = "chkSemLimiteBibliotecario";
            this.chkSemLimiteBibliotecario.Size = new System.Drawing.Size(132, 28);
            this.chkSemLimiteBibliotecario.TabIndex = 5;
            this.chkSemLimiteBibliotecario.Text = "Sem limite";
            this.chkSemLimiteBibliotecario.CheckedChanged += new System.EventHandler(this.SemLimite_CheckedChanged);
            // 
            // btnMaisAluno
            // 
            this.btnMaisAluno.AccessibleName = "Aumentar Aluno";
            this.btnMaisAluno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisAluno.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMaisAluno.Location = new System.Drawing.Point(280, 60);
            this.btnMaisAluno.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaisAluno.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaisAluno.Tag = "PreservarCores";
            this.btnMaisAluno.Name = "btnMaisAluno";
            this.btnMaisAluno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMaisAluno.Size = new System.Drawing.Size(28, 18);
            this.btnMaisAluno.TabIndex = 6;
            this.btnMaisAluno.TabStop = false;
            this.btnMaisAluno.Text = "▲";
            this.btnMaisAluno.UseVisualStyleBackColor = false;
            this.btnMaisAluno.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaisAluno.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMenosAluno
            // 
            this.btnMenosAluno.AccessibleName = "Diminuir Aluno";
            this.btnMenosAluno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosAluno.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMenosAluno.Location = new System.Drawing.Point(280, 80);
            this.btnMenosAluno.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenosAluno.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMenosAluno.Tag = "PreservarCores";
            this.btnMenosAluno.Name = "btnMenosAluno";
            this.btnMenosAluno.Size = new System.Drawing.Size(28, 18);
            this.btnMenosAluno.TabIndex = 7;
            this.btnMenosAluno.TabStop = false;
            this.btnMenosAluno.Text = "▼";
            this.btnMenosAluno.UseVisualStyleBackColor = false;
            this.btnMenosAluno.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenosAluno.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMaisProfessor
            // 
            this.btnMaisProfessor.AccessibleName = "Aumentar Professor";
            this.btnMaisProfessor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisProfessor.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMaisProfessor.Location = new System.Drawing.Point(280, 102);
            this.btnMaisProfessor.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaisProfessor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaisProfessor.Tag = "PreservarCores";
            this.btnMaisProfessor.Name = "btnMaisProfessor";
            this.btnMaisProfessor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMaisProfessor.Size = new System.Drawing.Size(28, 18);
            this.btnMaisProfessor.TabIndex = 8;
            this.btnMaisProfessor.TabStop = false;
            this.btnMaisProfessor.Text = "▲";
            this.btnMaisProfessor.UseVisualStyleBackColor = false;
            this.btnMaisProfessor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaisProfessor.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMenosProfessor
            // 
            this.btnMenosProfessor.AccessibleName = "Diminuir Professor";
            this.btnMenosProfessor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosProfessor.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMenosProfessor.Location = new System.Drawing.Point(280, 122);
            this.btnMenosProfessor.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenosProfessor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMenosProfessor.Tag = "PreservarCores";
            this.btnMenosProfessor.Name = "btnMenosProfessor";
            this.btnMenosProfessor.Size = new System.Drawing.Size(28, 18);
            this.btnMenosProfessor.TabIndex = 9;
            this.btnMenosProfessor.TabStop = false;
            this.btnMenosProfessor.Text = "▼";
            this.btnMenosProfessor.UseVisualStyleBackColor = false;
            this.btnMenosProfessor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenosProfessor.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMaisBibliotecario
            // 
            this.btnMaisBibliotecario.AccessibleName = "Aumentar Bibliotecario";
            this.btnMaisBibliotecario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisBibliotecario.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMaisBibliotecario.Location = new System.Drawing.Point(280, 144);
            this.btnMaisBibliotecario.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaisBibliotecario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaisBibliotecario.Tag = "PreservarCores";
            this.btnMaisBibliotecario.Name = "btnMaisBibliotecario";
            this.btnMaisBibliotecario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMaisBibliotecario.Size = new System.Drawing.Size(28, 18);
            this.btnMaisBibliotecario.TabIndex = 10;
            this.btnMaisBibliotecario.TabStop = false;
            this.btnMaisBibliotecario.Text = "▲";
            this.btnMaisBibliotecario.UseVisualStyleBackColor = false;
            this.btnMaisBibliotecario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaisBibliotecario.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMenosBibliotecario
            // 
            this.btnMenosBibliotecario.AccessibleName = "Diminuir Bibliotecario";
            this.btnMenosBibliotecario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosBibliotecario.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMenosBibliotecario.Location = new System.Drawing.Point(280, 164);
            this.btnMenosBibliotecario.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenosBibliotecario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMenosBibliotecario.Tag = "PreservarCores";
            this.btnMenosBibliotecario.Name = "btnMenosBibliotecario";
            this.btnMenosBibliotecario.Size = new System.Drawing.Size(28, 18);
            this.btnMenosBibliotecario.TabIndex = 11;
            this.btnMenosBibliotecario.TabStop = false;
            this.btnMenosBibliotecario.Text = "▼";
            this.btnMenosBibliotecario.UseVisualStyleBackColor = false;
            this.btnMenosBibliotecario.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenosBibliotecario.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // pnlFiltrosPrazos
            // 
            this.pnlFiltrosPrazos.BackColor = System.Drawing.Color.White;
            this.pnlFiltrosPrazos.Controls.Add(this.lblPrazos);
            this.pnlFiltrosPrazos.Controls.Add(this.lblPrazo);
            this.pnlFiltrosPrazos.Controls.Add(this.numPrazo);
            this.pnlFiltrosPrazos.Controls.Add(this.lblProrrogacoes);
            this.pnlFiltrosPrazos.Controls.Add(this.numProrrogacoes);
            this.pnlFiltrosPrazos.Controls.Add(this.chkSemLimiteProrrogacoes);
            this.pnlFiltrosPrazos.Controls.Add(this.btnMaisPrazo);
            this.pnlFiltrosPrazos.Controls.Add(this.btnMenosPrazo);
            this.pnlFiltrosPrazos.Controls.Add(this.btnMaisProrrogacoes);
            this.pnlFiltrosPrazos.Controls.Add(this.btnMenosProrrogacoes);
            this.pnlFiltrosPrazos.Location = new System.Drawing.Point(0, 204);
            this.pnlFiltrosPrazos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlFiltrosPrazos.Name = "pnlFiltrosPrazos";
            this.pnlFiltrosPrazos.Size = new System.Drawing.Size(480, 140);
            this.pnlFiltrosPrazos.TabIndex = 1;
            // 
            // lblPrazo
            // 
            this.lblPrazo.BackColor = System.Drawing.Color.Transparent;
            this.lblPrazo.Location = new System.Drawing.Point(18, 49);
            this.lblPrazo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.Size = new System.Drawing.Size(170, 28);
            this.lblPrazo.TabIndex = 1;
            this.lblPrazo.Text = "Prazo (dias)";
            // 
            // numPrazo
            // 
            this.numPrazo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numPrazo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numPrazo.BackColor = System.Drawing.Color.White;
            this.numPrazo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.numPrazo.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.numPrazo.BorderRadius = 10;
            this.numPrazo.BorderThickness = 1;
            this.numPrazo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numPrazo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numPrazo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.numPrazo.HoverBackColor = System.Drawing.Color.LightGray;
            this.numPrazo.Location = new System.Drawing.Point(190, 45);
            this.numPrazo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.numPrazo.Name = "numPrazo";
            this.numPrazo.OnlyNumbers = true;
            this.numPrazo.Padding = new System.Windows.Forms.Padding(7);
            this.numPrazo.PlaceholderColor = System.Drawing.Color.Gray;
            this.numPrazo.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F);
            this.numPrazo.PlaceholderMarginLeft = 12;
            this.numPrazo.PlaceholderText = "";
            this.numPrazo.SelectedText = "";
            this.numPrazo.SelectionLength = 0;
            this.numPrazo.SelectionStart = 0;
            this.numPrazo.Size = new System.Drawing.Size(86, 35);
            this.numPrazo.TabIndex = 0;
            this.numPrazo.TextColor = System.Drawing.Color.Black;
            this.numPrazo.UseSystemPasswordChar = false;
            // 
            // lblProrrogacoes
            // 
            this.lblProrrogacoes.BackColor = System.Drawing.Color.Transparent;
            this.lblProrrogacoes.Location = new System.Drawing.Point(18, 93);
            this.lblProrrogacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblProrrogacoes.Name = "lblProrrogacoes";
            this.lblProrrogacoes.Size = new System.Drawing.Size(170, 28);
            this.lblProrrogacoes.TabIndex = 2;
            this.lblProrrogacoes.Text = "Prorrogações permitidas";
            // 
            // numProrrogacoes
            // 
            this.numProrrogacoes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.numProrrogacoes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.numProrrogacoes.BackColor = System.Drawing.Color.White;
            this.numProrrogacoes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.numProrrogacoes.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.numProrrogacoes.BorderRadius = 10;
            this.numProrrogacoes.BorderThickness = 1;
            this.numProrrogacoes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numProrrogacoes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numProrrogacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.numProrrogacoes.HoverBackColor = System.Drawing.Color.LightGray;
            this.numProrrogacoes.Location = new System.Drawing.Point(190, 89);
            this.numProrrogacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.numProrrogacoes.Name = "numProrrogacoes";
            this.numProrrogacoes.OnlyNumbers = true;
            this.numProrrogacoes.Padding = new System.Windows.Forms.Padding(7);
            this.numProrrogacoes.PlaceholderColor = System.Drawing.Color.Gray;
            this.numProrrogacoes.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F);
            this.numProrrogacoes.PlaceholderMarginLeft = 12;
            this.numProrrogacoes.PlaceholderText = "";
            this.numProrrogacoes.SelectedText = "";
            this.numProrrogacoes.SelectionLength = 0;
            this.numProrrogacoes.SelectionStart = 0;
            this.numProrrogacoes.Size = new System.Drawing.Size(86, 35);
            this.numProrrogacoes.TabIndex = 1;
            this.numProrrogacoes.TextColor = System.Drawing.Color.Black;
            this.numProrrogacoes.UseSystemPasswordChar = false;
            // 
            // chkSemLimiteProrrogacoes
            // 
            this.chkSemLimiteProrrogacoes.Location = new System.Drawing.Point(328, 93);
            this.chkSemLimiteProrrogacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.chkSemLimiteProrrogacoes.Name = "chkSemLimiteProrrogacoes";
            this.chkSemLimiteProrrogacoes.Size = new System.Drawing.Size(132, 28);
            this.chkSemLimiteProrrogacoes.TabIndex = 2;
            this.chkSemLimiteProrrogacoes.Text = "Sem limite";
            this.chkSemLimiteProrrogacoes.CheckedChanged += new System.EventHandler(this.SemLimite_CheckedChanged);
            // 
            // btnMaisPrazo
            // 
            this.btnMaisPrazo.AccessibleName = "Aumentar Prazo";
            this.btnMaisPrazo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisPrazo.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMaisPrazo.Location = new System.Drawing.Point(280, 44);
            this.btnMaisPrazo.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaisPrazo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaisPrazo.Tag = "PreservarCores";
            this.btnMaisPrazo.Name = "btnMaisPrazo";
            this.btnMaisPrazo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMaisPrazo.Size = new System.Drawing.Size(28, 18);
            this.btnMaisPrazo.TabIndex = 3;
            this.btnMaisPrazo.TabStop = false;
            this.btnMaisPrazo.Text = "▲";
            this.btnMaisPrazo.UseVisualStyleBackColor = false;
            this.btnMaisPrazo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaisPrazo.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMenosPrazo
            // 
            this.btnMenosPrazo.AccessibleName = "Diminuir Prazo";
            this.btnMenosPrazo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosPrazo.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMenosPrazo.Location = new System.Drawing.Point(280, 64);
            this.btnMenosPrazo.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenosPrazo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMenosPrazo.Tag = "PreservarCores";
            this.btnMenosPrazo.Name = "btnMenosPrazo";
            this.btnMenosPrazo.Size = new System.Drawing.Size(28, 18);
            this.btnMenosPrazo.TabIndex = 4;
            this.btnMenosPrazo.TabStop = false;
            this.btnMenosPrazo.Text = "▼";
            this.btnMenosPrazo.UseVisualStyleBackColor = false;
            this.btnMenosPrazo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenosPrazo.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMaisProrrogacoes
            // 
            this.btnMaisProrrogacoes.AccessibleName = "Aumentar Prorrogacoes";
            this.btnMaisProrrogacoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaisProrrogacoes.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMaisProrrogacoes.Location = new System.Drawing.Point(280, 88);
            this.btnMaisProrrogacoes.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaisProrrogacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMaisProrrogacoes.Tag = "PreservarCores";
            this.btnMaisProrrogacoes.Name = "btnMaisProrrogacoes";
            this.btnMaisProrrogacoes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMaisProrrogacoes.Size = new System.Drawing.Size(28, 18);
            this.btnMaisProrrogacoes.TabIndex = 5;
            this.btnMaisProrrogacoes.TabStop = false;
            this.btnMaisProrrogacoes.Text = "▲";
            this.btnMaisProrrogacoes.UseVisualStyleBackColor = false;
            this.btnMaisProrrogacoes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMaisProrrogacoes.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnMenosProrrogacoes
            // 
            this.btnMenosProrrogacoes.AccessibleName = "Diminuir Prorrogacoes";
            this.btnMenosProrrogacoes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenosProrrogacoes.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.btnMenosProrrogacoes.Location = new System.Drawing.Point(280, 108);
            this.btnMenosProrrogacoes.Margin = new System.Windows.Forms.Padding(0);
            this.btnMenosProrrogacoes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnMenosProrrogacoes.Tag = "PreservarCores";
            this.btnMenosProrrogacoes.Name = "btnMenosProrrogacoes";
            this.btnMenosProrrogacoes.Size = new System.Drawing.Size(28, 18);
            this.btnMenosProrrogacoes.TabIndex = 6;
            this.btnMenosProrrogacoes.TabStop = false;
            this.btnMenosProrrogacoes.Text = "▼";
            this.btnMenosProrrogacoes.UseVisualStyleBackColor = false;
            this.btnMenosProrrogacoes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnMenosProrrogacoes.ForeColor = System.Drawing.Color.FromArgb(30, 61, 88);
            // 
            // btnAplicar
            // 
            // 
            // btnCancelar
            // 
            // 
            // PoliticaEmprestimosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Size = new System.Drawing.Size(480, 410);
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblDica);
            this.Controls.Add(this.pnlFiltrosLimites);
            this.Controls.Add(this.pnlFiltrosPrazos);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "PoliticaEmprestimosEditor";
            this.pnlFiltrosLimites.ResumeLayout(false);
            this.pnlFiltrosPrazos.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Label lblLimites;
        private System.Windows.Forms.Label lblLimitesDica;
        private System.Windows.Forms.Label lblPrazos;
        private System.Windows.Forms.Label lblDica;
        private SUB.ElementosPersonalizados.RoundedSurfacePanel pnlFiltrosLimites;
        private SUB.ElementosPersonalizados.RoundedSurfacePanel pnlFiltrosPrazos;
        private System.Windows.Forms.Label lblAluno;
        private RoundedTextBox numAluno;
        private System.Windows.Forms.Button btnMenosAluno;
        private System.Windows.Forms.Button btnMaisAluno;
        private System.Windows.Forms.CheckBox chkSemLimiteAluno;
        private System.Windows.Forms.Label lblProfessor;
        private RoundedTextBox numProfessor;
        private System.Windows.Forms.Button btnMenosProfessor;
        private System.Windows.Forms.Button btnMaisProfessor;
        private System.Windows.Forms.CheckBox chkSemLimiteProfessor;
        private System.Windows.Forms.Label lblBibliotecario;
        private RoundedTextBox numBibliotecario;
        private System.Windows.Forms.Button btnMenosBibliotecario;
        private System.Windows.Forms.Button btnMaisBibliotecario;
        private System.Windows.Forms.CheckBox chkSemLimiteBibliotecario;
        private System.Windows.Forms.Label lblPrazo;
        private RoundedTextBox numPrazo;
        private System.Windows.Forms.Button btnMenosPrazo;
        private System.Windows.Forms.Button btnMaisPrazo;
        private System.Windows.Forms.Label lblProrrogacoes;
        private RoundedTextBox numProrrogacoes;
        private System.Windows.Forms.Button btnMenosProrrogacoes;
        private System.Windows.Forms.Button btnMaisProrrogacoes;
        private System.Windows.Forms.CheckBox chkSemLimiteProrrogacoes;
    }
}
