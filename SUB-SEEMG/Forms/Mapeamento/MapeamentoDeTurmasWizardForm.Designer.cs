namespace SUB.Forms.Usuario
{
    partial class MapeamentoDeTurmasWizardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapeamentoDeTurmasWizardForm));
            this.panelPrincipal = new System.Windows.Forms.Panel();
            this.pnlTutorial = new System.Windows.Forms.Panel();
            this.pnlLinhaCabecalho = new System.Windows.Forms.Panel();
            this.lblTutorialTexto = new System.Windows.Forms.Label();
            this.lblTutorialTitulo = new System.Windows.Forms.Label();
            this.progressBarWizard = new System.Windows.Forms.ProgressBar();
            this.lblProgressoWizard = new System.Windows.Forms.Label();
            this.panelEtapa1 = new System.Windows.Forms.Panel();
            this.lblInstrucaoEtapa1 = new System.Windows.Forms.Label();
            this.pnlResultadosPadroes = new System.Windows.Forms.Panel();
            this.dgvPadroes = new System.Windows.Forms.DataGridView();
            this.panelEtapa2 = new System.Windows.Forms.Panel();
            this.lblInstrucaoEtapa2 = new System.Windows.Forms.Label();
            this.lblFiltroEtapa2 = new System.Windows.Forms.Label();
            this.cmbFiltroTurmaEtapa2 = new System.Windows.Forms.ComboBox();
            this.pnlResultadosAjustes = new System.Windows.Forms.Panel();
            this.dgvAjustesIndividuais = new System.Windows.Forms.DataGridView();
            this.panelEtapa3 = new System.Windows.Forms.Panel();
            this.lblInstrucaoEtapa3 = new System.Windows.Forms.Label();
            this.txtResumoFinal = new System.Windows.Forms.TextBox();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblFiltroNomeEtapa2 = new System.Windows.Forms.Label();
            this.txtBuscaAlunoEtapa2 = new RoundedTextBox();
            this.panelPrincipal.SuspendLayout();
            this.pnlTutorial.SuspendLayout();
            this.panelEtapa1.SuspendLayout();
            this.pnlResultadosPadroes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPadroes)).BeginInit();
            this.panelEtapa2.SuspendLayout();
            this.pnlResultadosAjustes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAjustesIndividuais)).BeginInit();
            this.panelEtapa3.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrincipal
            // 
            this.panelPrincipal.BackColor = System.Drawing.Color.White;
            this.panelPrincipal.Controls.Add(this.pnlTutorial);
            this.panelPrincipal.Controls.Add(this.progressBarWizard);
            this.panelPrincipal.Controls.Add(this.lblProgressoWizard);
            this.panelPrincipal.Controls.Add(this.panelBotoes);
            this.panelPrincipal.Controls.Add(this.panelEtapa1);
            this.panelPrincipal.Controls.Add(this.panelEtapa2);
            this.panelPrincipal.Controls.Add(this.panelEtapa3);
            this.panelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPrincipal.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelPrincipal.Name = "panelPrincipal";
            this.panelPrincipal.Size = new System.Drawing.Size(1200, 800);
            this.panelPrincipal.TabIndex = 0;
            // 
            // pnlTutorial
            // 
            this.pnlTutorial.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pnlTutorial.BackColor = System.Drawing.Color.White;
            this.pnlTutorial.Controls.Add(this.lblTutorialTexto);
            this.pnlTutorial.Controls.Add(this.lblTutorialTitulo);
            this.pnlTutorial.Controls.Add(this.pnlLinhaCabecalho);
            this.pnlTutorial.Location = new System.Drawing.Point(0, 0);
            this.pnlTutorial.Name = "pnlTutorial";
            this.pnlTutorial.Size = new System.Drawing.Size(1200, 92);
            this.pnlTutorial.TabIndex = 7;
            // 
            // lblTutorialTexto
            // 
            this.lblTutorialTexto.AutoSize = false;
            this.lblTutorialTexto.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTutorialTexto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.lblTutorialTexto.Location = new System.Drawing.Point(36, 52);
            this.lblTutorialTexto.Name = "lblTutorialTexto";
            this.lblTutorialTexto.Size = new System.Drawing.Size(1040, 20);
            this.lblTutorialTexto.TabIndex = 9;
            this.lblTutorialTexto.Text = "Revise as turmas e confirme as mudanças com segurança antes de aplicá-las ao cadastro dos alunos.";
            this.lblTutorialTexto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTutorialTitulo
            // 
            this.lblTutorialTitulo.AutoSize = true;
            this.lblTutorialTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 21F, System.Drawing.FontStyle.Bold);
            this.lblTutorialTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.lblTutorialTitulo.Location = new System.Drawing.Point(34, 14);
            this.lblTutorialTitulo.Name = "lblTutorialTitulo";
            this.lblTutorialTitulo.Size = new System.Drawing.Size(232, 38);
            this.lblTutorialTitulo.TabIndex = 8;
            this.lblTutorialTitulo.Text = "Mapeamento de turmas";
            this.lblTutorialTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLinhaCabecalho
            // 
            this.pnlLinhaCabecalho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.pnlLinhaCabecalho.Location = new System.Drawing.Point(36, 80);
            this.pnlLinhaCabecalho.Name = "pnlLinhaCabecalho";
            this.pnlLinhaCabecalho.Size = new System.Drawing.Size(48, 3);
            this.pnlLinhaCabecalho.TabIndex = 10;
            // 
            // progressBarWizard
            // 
            this.progressBarWizard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBarWizard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.progressBarWizard.Location = new System.Drawing.Point(36, 104);
            this.progressBarWizard.Name = "progressBarWizard";
            this.progressBarWizard.Size = new System.Drawing.Size(1010, 12);
            this.progressBarWizard.TabIndex = 1;
            // 
            // lblProgressoWizard
            // 
            this.lblProgressoWizard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProgressoWizard.AutoSize = true;
            this.lblProgressoWizard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProgressoWizard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblProgressoWizard.Location = new System.Drawing.Point(1064, 101);
            this.lblProgressoWizard.Name = "lblProgressoWizard";
            this.lblProgressoWizard.Size = new System.Drawing.Size(86, 19);
            this.lblProgressoWizard.TabIndex = 2;
            this.lblProgressoWizard.Text = "Etapa 1 de 3";
            // 
            // panelEtapa1
            // 
            this.panelEtapa1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelEtapa1.Controls.Add(this.lblInstrucaoEtapa1);
            this.panelEtapa1.Controls.Add(this.pnlResultadosPadroes);
            this.panelEtapa1.Location = new System.Drawing.Point(30, 130);
            this.panelEtapa1.Name = "panelEtapa1";
            this.panelEtapa1.Size = new System.Drawing.Size(1140, 570);
            this.panelEtapa1.TabIndex = 3;
            // 
            // lblInstrucaoEtapa1
            // 
            this.lblInstrucaoEtapa1.AutoSize = true;
            this.lblInstrucaoEtapa1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblInstrucaoEtapa1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblInstrucaoEtapa1.Location = new System.Drawing.Point(10, 10);
            this.lblInstrucaoEtapa1.Name = "lblInstrucaoEtapa1";
            this.lblInstrucaoEtapa1.Size = new System.Drawing.Size(790, 21);
            this.lblInstrucaoEtapa1.TabIndex = 0;
            this.lblInstrucaoEtapa1.Text = "Defina o padrão de progressão para cada turma. O sistema sugere automaticamente b" +
    "aseado em regras seguras.";
            //
            // pnlResultadosPadroes
            //
            this.pnlResultadosPadroes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlResultadosPadroes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultadosPadroes.Controls.Add(this.dgvPadroes);
            this.pnlResultadosPadroes.Location = new System.Drawing.Point(10, 45);
            this.pnlResultadosPadroes.Name = "pnlResultadosPadroes";
            this.pnlResultadosPadroes.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultadosPadroes.Size = new System.Drawing.Size(1120, 515);
            this.pnlResultadosPadroes.TabIndex = 1;
            //
            // dgvPadroes
            // 
            this.dgvPadroes.AllowUserToAddRows = false;
            this.dgvPadroes.AllowUserToDeleteRows = false;
            this.dgvPadroes.AllowUserToResizeColumns = false;
            this.dgvPadroes.AllowUserToResizeRows = false;
            this.dgvPadroes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPadroes.Location = new System.Drawing.Point(1, 1);
            this.dgvPadroes.Name = "dgvPadroes";
            this.dgvPadroes.Size = new System.Drawing.Size(1118, 513);
            this.dgvPadroes.TabIndex = 1;
            // 
            // panelEtapa2
            // 
            this.panelEtapa2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelEtapa2.Controls.Add(this.txtBuscaAlunoEtapa2);
            this.panelEtapa2.Controls.Add(this.lblFiltroNomeEtapa2);
            this.panelEtapa2.Controls.Add(this.lblInstrucaoEtapa2);
            this.panelEtapa2.Controls.Add(this.lblFiltroEtapa2);
            this.panelEtapa2.Controls.Add(this.cmbFiltroTurmaEtapa2);
            this.panelEtapa2.Controls.Add(this.pnlResultadosAjustes);
            this.panelEtapa2.Location = new System.Drawing.Point(30, 130);
            this.panelEtapa2.Name = "panelEtapa2";
            this.panelEtapa2.Size = new System.Drawing.Size(1140, 570);
            this.panelEtapa2.TabIndex = 4;
            this.panelEtapa2.Visible = false;
            // 
            // lblInstrucaoEtapa2
            // 
            this.lblInstrucaoEtapa2.AutoSize = true;
            this.lblInstrucaoEtapa2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblInstrucaoEtapa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblInstrucaoEtapa2.Location = new System.Drawing.Point(10, 10);
            this.lblInstrucaoEtapa2.Name = "lblInstrucaoEtapa2";
            this.lblInstrucaoEtapa2.Size = new System.Drawing.Size(643, 21);
            this.lblInstrucaoEtapa2.TabIndex = 0;
            this.lblInstrucaoEtapa2.Text = "Ajuste individualmente os alunos que precisam de tratamento diferente do padrão d" +
    "efinido.";
            // 
            // lblFiltroEtapa2
            // 
            this.lblFiltroEtapa2.AutoSize = true;
            this.lblFiltroEtapa2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiltroEtapa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblFiltroEtapa2.Location = new System.Drawing.Point(10, 45);
            this.lblFiltroEtapa2.Name = "lblFiltroEtapa2";
            this.lblFiltroEtapa2.Size = new System.Drawing.Size(101, 20);
            this.lblFiltroEtapa2.TabIndex = 1;
            this.lblFiltroEtapa2.Text = "Filtrar turma:";
            // 
            // cmbFiltroTurmaEtapa2
            // 
            this.cmbFiltroTurmaEtapa2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTurmaEtapa2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbFiltroTurmaEtapa2.Location = new System.Drawing.Point(120, 43);
            this.cmbFiltroTurmaEtapa2.Name = "cmbFiltroTurmaEtapa2";
            this.cmbFiltroTurmaEtapa2.Size = new System.Drawing.Size(300, 28);
            this.cmbFiltroTurmaEtapa2.TabIndex = 2;
            //
            // pnlResultadosAjustes
            //
            this.pnlResultadosAjustes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlResultadosAjustes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(229)))), ((int)(((byte)(236)))));
            this.pnlResultadosAjustes.Controls.Add(this.dgvAjustesIndividuais);
            this.pnlResultadosAjustes.Location = new System.Drawing.Point(10, 80);
            this.pnlResultadosAjustes.Name = "pnlResultadosAjustes";
            this.pnlResultadosAjustes.Padding = new System.Windows.Forms.Padding(1);
            this.pnlResultadosAjustes.Size = new System.Drawing.Size(1120, 480);
            this.pnlResultadosAjustes.TabIndex = 3;
            //
            // dgvAjustesIndividuais
            // 
            this.dgvAjustesIndividuais.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAjustesIndividuais.Location = new System.Drawing.Point(1, 1);
            this.dgvAjustesIndividuais.Name = "dgvAjustesIndividuais";
            this.dgvAjustesIndividuais.Size = new System.Drawing.Size(1118, 478);
            this.dgvAjustesIndividuais.TabIndex = 3;
            // 
            // panelEtapa3
            // 
            this.panelEtapa3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.panelEtapa3.Controls.Add(this.lblInstrucaoEtapa3);
            this.panelEtapa3.Controls.Add(this.txtResumoFinal);
            this.panelEtapa3.Location = new System.Drawing.Point(30, 130);
            this.panelEtapa3.Name = "panelEtapa3";
            this.panelEtapa3.Size = new System.Drawing.Size(1140, 570);
            this.panelEtapa3.TabIndex = 5;
            this.panelEtapa3.Visible = false;
            // 
            // lblInstrucaoEtapa3
            // 
            this.lblInstrucaoEtapa3.AutoSize = true;
            this.lblInstrucaoEtapa3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblInstrucaoEtapa3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblInstrucaoEtapa3.Location = new System.Drawing.Point(10, 10);
            this.lblInstrucaoEtapa3.Name = "lblInstrucaoEtapa3";
            this.lblInstrucaoEtapa3.Size = new System.Drawing.Size(522, 21);
            this.lblInstrucaoEtapa3.TabIndex = 0;
            this.lblInstrucaoEtapa3.Text = "Revise o resumo das alterações e confirme a aplicação ao banco de dados.";
            // 
            // txtResumoFinal
            // 
            this.txtResumoFinal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtResumoFinal.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtResumoFinal.Location = new System.Drawing.Point(10, 45);
            this.txtResumoFinal.Multiline = true;
            this.txtResumoFinal.Name = "txtResumoFinal";
            this.txtResumoFinal.ReadOnly = true;
            this.txtResumoFinal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResumoFinal.Size = new System.Drawing.Size(1120, 515);
            this.txtResumoFinal.TabIndex = 1;
            // 
            // panelBotoes
            // 
            this.panelBotoes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelBotoes.Controls.Add(this.btnAnterior);
            this.panelBotoes.Controls.Add(this.btnProximo);
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Location = new System.Drawing.Point(30, 720);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Size = new System.Drawing.Size(1140, 60);
            this.panelBotoes.TabIndex = 6;
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.btnAnterior.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(194)))), ((int)(((byte)(210)))));
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.FlatAppearance.BorderSize = 1;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnAnterior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnAnterior.Location = new System.Drawing.Point(780, 10);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(120, 40);
            this.btnAnterior.TabIndex = 0;
            this.btnAnterior.Text = "← Anterior";
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.BtnAnterior_Click);
            // 
            // btnProximo
            // 
            this.btnProximo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnProximo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProximo.FlatAppearance.BorderSize = 0;
            this.btnProximo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProximo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnProximo.ForeColor = System.Drawing.Color.White;
            this.btnProximo.Location = new System.Drawing.Point(920, 10);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(120, 40);
            this.btnProximo.TabIndex = 1;
            this.btnProximo.Text = "Avançar →";
            this.btnProximo.UseVisualStyleBackColor = false;
            this.btnProximo.Click += new System.EventHandler(this.BtnProximo_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(194)))), ((int)(((byte)(210)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 1;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnCancelar.Location = new System.Drawing.Point(10, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // lblFiltroNomeEtapa2
            // 
            this.lblFiltroNomeEtapa2.AutoSize = true;
            this.lblFiltroNomeEtapa2.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblFiltroNomeEtapa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblFiltroNomeEtapa2.Location = new System.Drawing.Point(486, 47);
            this.lblFiltroNomeEtapa2.Name = "lblFiltroNomeEtapa2";
            this.lblFiltroNomeEtapa2.Size = new System.Drawing.Size(100, 20);
            this.lblFiltroNomeEtapa2.TabIndex = 4;
            this.lblFiltroNomeEtapa2.Text = "Filtrar nome:";
            // 
            // txtBuscaAlunoEtapa2
            // 
            this.txtBuscaAlunoEtapa2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBuscaAlunoEtapa2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBuscaAlunoEtapa2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBuscaAlunoEtapa2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBuscaAlunoEtapa2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtBuscaAlunoEtapa2.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtBuscaAlunoEtapa2.BorderRadius = 10;
            this.txtBuscaAlunoEtapa2.BorderThickness = 1;
            this.txtBuscaAlunoEtapa2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBuscaAlunoEtapa2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtBuscaAlunoEtapa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtBuscaAlunoEtapa2.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtBuscaAlunoEtapa2.Location = new System.Drawing.Point(594, 41);
            this.txtBuscaAlunoEtapa2.Name = "txtBuscaAlunoEtapa2";
            this.txtBuscaAlunoEtapa2.Padding = new System.Windows.Forms.Padding(7);
            this.txtBuscaAlunoEtapa2.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtBuscaAlunoEtapa2.PlaceholderFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txtBuscaAlunoEtapa2.PlaceholderMarginLeft = 12;
            this.txtBuscaAlunoEtapa2.PlaceholderText = "Digite aqui o nome para filtrar...";
            this.txtBuscaAlunoEtapa2.SelectedText = "";
            this.txtBuscaAlunoEtapa2.SelectionLength = 0;
            this.txtBuscaAlunoEtapa2.SelectionStart = 0;
            this.txtBuscaAlunoEtapa2.Size = new System.Drawing.Size(324, 31);
            this.txtBuscaAlunoEtapa2.TabIndex = 5;
            this.txtBuscaAlunoEtapa2.TextColor = System.Drawing.Color.Black;
            this.txtBuscaAlunoEtapa2.UseSystemPasswordChar = false;
            // 
            // MapeamentoDeTurmasWizardForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.panelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapeamentoDeTurmasWizardForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SUB - Mapeamento Anual";
            this.panelPrincipal.ResumeLayout(false);
            this.panelPrincipal.PerformLayout();
            this.pnlTutorial.ResumeLayout(false);
            this.pnlTutorial.PerformLayout();
            this.panelEtapa1.ResumeLayout(false);
            this.panelEtapa1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPadroes)).EndInit();
            this.pnlResultadosPadroes.ResumeLayout(false);
            this.panelEtapa2.ResumeLayout(false);
            this.panelEtapa2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAjustesIndividuais)).EndInit();
            this.pnlResultadosAjustes.ResumeLayout(false);
            this.panelEtapa3.ResumeLayout(false);
            this.panelEtapa3.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelPrincipal;
        private System.Windows.Forms.ProgressBar progressBarWizard;
        private System.Windows.Forms.Label lblProgressoWizard;

        // Etapa 1
        private System.Windows.Forms.Panel panelEtapa1;
        private System.Windows.Forms.Label lblInstrucaoEtapa1;
        private System.Windows.Forms.Panel pnlResultadosPadroes;
        private System.Windows.Forms.DataGridView dgvPadroes;

        // Etapa 2
        private System.Windows.Forms.Panel panelEtapa2;
        private System.Windows.Forms.Label lblInstrucaoEtapa2;
        private System.Windows.Forms.Label lblFiltroEtapa2;
        private System.Windows.Forms.ComboBox cmbFiltroTurmaEtapa2;
        private System.Windows.Forms.Panel pnlResultadosAjustes;
        private System.Windows.Forms.DataGridView dgvAjustesIndividuais;

        // Etapa 3
        private System.Windows.Forms.Panel panelEtapa3;
        private System.Windows.Forms.Label lblInstrucaoEtapa3;
        private System.Windows.Forms.TextBox txtResumoFinal;

        // Botões
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnlTutorial;
        private System.Windows.Forms.Label lblTutorialTexto;
        private System.Windows.Forms.Label lblTutorialTitulo;
        private System.Windows.Forms.Label lblFiltroNomeEtapa2;
        private RoundedTextBox txtBuscaAlunoEtapa2;
        private System.Windows.Forms.Panel pnlLinhaCabecalho;
    }
}

