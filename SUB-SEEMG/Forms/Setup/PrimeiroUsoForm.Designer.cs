namespace SUB.Forms.Setup
{
    partial class PrimeiroUsoForm
    {
        private System.ComponentModel.IContainer components = null;

        private SUB.ElementosPersonalizados.GradientPanel pnlInstitucional;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblNomeSistema;
        private System.Windows.Forms.Label lblConfiguracaoInicial;
        private System.Windows.Forms.Label lblTextoInstitucional;
        private System.Windows.Forms.Label lblRoteiro;
        private System.Windows.Forms.Label lblPasso1Numero;
        private System.Windows.Forms.Label lblPasso1Texto;
        private System.Windows.Forms.Panel linhaRoteiro;
        private System.Windows.Forms.Label lblPasso2Numero;
        private System.Windows.Forms.Label lblPasso2Texto;
        private System.Windows.Forms.Label lblPasso3Numero;
        private System.Windows.Forms.Label lblPasso3Texto;
        private System.Windows.Forms.Label lblIniciativa;
        private System.Windows.Forms.Panel linhaInstitucional;
        private System.Windows.Forms.Panel pnlConteudo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblEtapaAtual;
        private System.Windows.Forms.Panel indicadorEtapa1;
        private System.Windows.Forms.Panel indicadorEtapa2;
        private System.Windows.Forms.Label lblErroGeral;
        private System.Windows.Forms.Panel pnlEtapa1;
        private System.Windows.Forms.Label lblTituloEtapa1;
        private System.Windows.Forms.Label lblNomeInstituicao;
        private RoundedTextBox _nomeInstituicao;
        private System.Windows.Forms.Label lblErroNomeInstituicao;
        private System.Windows.Forms.Label lblTema;
        private RoundedComboBox _tema;
        private RoundedComboBox _aparencia;
        private System.Windows.Forms.Label lblAparencia;
        private System.Windows.Forms.Label lblDicaAparencia;
        private System.Windows.Forms.Panel _visualizadorCor;
        private System.Windows.Forms.Label lblErroTema;
        private CustomButton btnAvancar;
        private System.Windows.Forms.Panel pnlEtapa2;
        private System.Windows.Forms.Label lblTituloEtapa2;
        private System.Windows.Forms.Label lblNomeBibliotecaria;
        private RoundedTextBox _nomeBibliotecaria;
        private System.Windows.Forms.Label lblErroNomeBibliotecaria;
        private System.Windows.Forms.Label lblEmailBibliotecaria;
        private RoundedTextBox _emailBibliotecaria;
        private System.Windows.Forms.Label lblAuxiliarEmail;
        private System.Windows.Forms.Label lblErroEmailBibliotecaria;
        private System.Windows.Forms.Label lblTelefoneBibliotecaria;
        private RoundedMaskedTextBox _telefoneBibliotecaria;
        private System.Windows.Forms.Label lblErroTelefoneBibliotecaria;
        private System.Windows.Forms.Label lblSenhaBibliotecaria;
        private RoundedTextBox _senhaBibliotecaria;
        private System.Windows.Forms.Label lblErroSenhaBibliotecaria;
        private System.Windows.Forms.Label lblConfirmarSenhaBibliotecaria;
        private RoundedTextBox _confirmarSenhaBibliotecaria;
        private System.Windows.Forms.Label lblErroConfirmarSenhaBibliotecaria;
        private System.Windows.Forms.CheckBox chkMostrarSenhas;
        private System.Windows.Forms.Panel pnlCodigoRecuperacao;
        private System.Windows.Forms.Label lblCodigoTitulo;
        private System.Windows.Forms.Label lblCodigoAuxiliar;
        private System.Windows.Forms.TextBox _codigoMestre;
        private System.Windows.Forms.Button exportarCodigo;
        private System.Windows.Forms.Label statusCodigo;
        private System.Windows.Forms.Button btnVoltar;
        private CustomButton btnAvancarEtapa2;
        private CustomButton salvar;
        private System.Windows.Forms.Panel pnlEtapa3;
        private System.Windows.Forms.Label lblTituloEtapa3;
        private System.Windows.Forms.Label lblTurmasIniciais;
        private RoundedTextBox txtNovaTurma;
        private System.Windows.Forms.Button btnAdicionarTurma;
        private System.Windows.Forms.ListBox lstTurmasIniciais;
        private System.Windows.Forms.Button btnRemoverTurma;
        private System.Windows.Forms.Label lblErroTurmas;
        private System.Windows.Forms.Button btnVoltarEtapa3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrimeiroUsoForm));
            this.pnlInstitucional = new SUB.ElementosPersonalizados.GradientPanel();
            this.linhaRoteiro2 = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblNomeSistema = new System.Windows.Forms.Label();
            this.lblConfiguracaoInicial = new System.Windows.Forms.Label();
            this.lblTextoInstitucional = new System.Windows.Forms.Label();
            this.lblRoteiro = new System.Windows.Forms.Label();
            this.lblPasso1Numero = new System.Windows.Forms.Label();
            this.lblPasso1Texto = new System.Windows.Forms.Label();
            this.linhaRoteiro = new System.Windows.Forms.Panel();
            this.lblPasso2Numero = new System.Windows.Forms.Label();
            this.lblPasso2Texto = new System.Windows.Forms.Label();
            this.lblPasso3Numero = new System.Windows.Forms.Label();
            this.lblPasso3Texto = new System.Windows.Forms.Label();
            this.linhaInstitucional = new System.Windows.Forms.Panel();
            this.lblIniciativa = new System.Windows.Forms.Label();
            this.gradientPanel1 = new SUB.ElementosPersonalizados.GradientPanel();
            this.gradientPanel2 = new SUB.ElementosPersonalizados.GradientPanel();
            this.pnlConteudo = new System.Windows.Forms.Panel();
            this.indicadorEtapa3 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblEtapaAtual = new System.Windows.Forms.Label();
            this.indicadorEtapa1 = new System.Windows.Forms.Panel();
            this.indicadorEtapa2 = new System.Windows.Forms.Panel();
            this.lblErroGeral = new System.Windows.Forms.Label();
            this.pnlEtapa1 = new System.Windows.Forms.Panel();
            this.lblTituloEtapa1 = new System.Windows.Forms.Label();
            this.lblNomeInstituicao = new System.Windows.Forms.Label();
            this._nomeInstituicao = new RoundedTextBox();
            this.lblErroNomeInstituicao = new System.Windows.Forms.Label();
            this.lblTema = new System.Windows.Forms.Label();
            this._tema = new RoundedComboBox();
            this._aparencia = new RoundedComboBox();
            this.lblAparencia = new System.Windows.Forms.Label();
            this.lblDicaAparencia = new System.Windows.Forms.Label();
            this._visualizadorCor = new System.Windows.Forms.Panel();
            this.lblErroTema = new System.Windows.Forms.Label();
            this.btnAvancar = new CustomButton();
            this.pnlEtapa2 = new System.Windows.Forms.Panel();
            this.lblTituloEtapa2 = new System.Windows.Forms.Label();
            this.lblNomeBibliotecaria = new System.Windows.Forms.Label();
            this._nomeBibliotecaria = new RoundedTextBox();
            this.lblErroNomeBibliotecaria = new System.Windows.Forms.Label();
            this.lblEmailBibliotecaria = new System.Windows.Forms.Label();
            this._emailBibliotecaria = new RoundedTextBox();
            this.lblAuxiliarEmail = new System.Windows.Forms.Label();
            this.lblErroEmailBibliotecaria = new System.Windows.Forms.Label();
            this.lblTelefoneBibliotecaria = new System.Windows.Forms.Label();
            this._telefoneBibliotecaria = new RoundedMaskedTextBox();
            this.lblErroTelefoneBibliotecaria = new System.Windows.Forms.Label();
            this.lblSenhaBibliotecaria = new System.Windows.Forms.Label();
            this._senhaBibliotecaria = new RoundedTextBox();
            this.lblErroSenhaBibliotecaria = new System.Windows.Forms.Label();
            this.lblConfirmarSenhaBibliotecaria = new System.Windows.Forms.Label();
            this._confirmarSenhaBibliotecaria = new RoundedTextBox();
            this.lblErroConfirmarSenhaBibliotecaria = new System.Windows.Forms.Label();
            this.chkMostrarSenhas = new System.Windows.Forms.CheckBox();
            this.pnlCodigoRecuperacao = new System.Windows.Forms.Panel();
            this.lblCodigoTitulo = new System.Windows.Forms.Label();
            this.lblCodigoAuxiliar = new System.Windows.Forms.Label();
            this._codigoMestre = new System.Windows.Forms.TextBox();
            this.exportarCodigo = new System.Windows.Forms.Button();
            this.statusCodigo = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnAvancarEtapa2 = new CustomButton();
            this.pnlEtapa3 = new System.Windows.Forms.Panel();
            this.lblTituloEtapa3 = new System.Windows.Forms.Label();
            this.lblTurmasIniciais = new System.Windows.Forms.Label();
            this.txtNovaTurma = new RoundedTextBox();
            this.btnAdicionarTurma = new System.Windows.Forms.Button();
            this.lstTurmasIniciais = new System.Windows.Forms.ListBox();
            this.btnRemoverTurma = new System.Windows.Forms.Button();
            this.lblErroTurmas = new System.Windows.Forms.Label();
            this.btnVoltarEtapa3 = new System.Windows.Forms.Button();
            this.salvar = new CustomButton();
            this.btnFechar = new System.Windows.Forms.PictureBox();
            this.pnlInstitucional.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlConteudo.SuspendLayout();
            this.pnlEtapa1.SuspendLayout();
            this.pnlEtapa2.SuspendLayout();
            this.pnlCodigoRecuperacao.SuspendLayout();
            this.pnlEtapa3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInstitucional
            // 
            this.pnlInstitucional.BackColor = System.Drawing.Color.Transparent;
            this.pnlInstitucional.BorderRadius = 0;
            this.pnlInstitucional.Controls.Add(this.linhaRoteiro2);
            this.pnlInstitucional.Controls.Add(this.pnlLogo);
            this.pnlInstitucional.Controls.Add(this.lblSub);
            this.pnlInstitucional.Controls.Add(this.lblNomeSistema);
            this.pnlInstitucional.Controls.Add(this.lblConfiguracaoInicial);
            this.pnlInstitucional.Controls.Add(this.lblTextoInstitucional);
            this.pnlInstitucional.Controls.Add(this.lblRoteiro);
            this.pnlInstitucional.Controls.Add(this.lblPasso1Numero);
            this.pnlInstitucional.Controls.Add(this.lblPasso1Texto);
            this.pnlInstitucional.Controls.Add(this.linhaRoteiro);
            this.pnlInstitucional.Controls.Add(this.lblPasso2Numero);
            this.pnlInstitucional.Controls.Add(this.lblPasso2Texto);
            this.pnlInstitucional.Controls.Add(this.lblPasso3Numero);
            this.pnlInstitucional.Controls.Add(this.lblPasso3Texto);
            this.pnlInstitucional.Controls.Add(this.linhaInstitucional);
            this.pnlInstitucional.Controls.Add(this.lblIniciativa);
            this.pnlInstitucional.Controls.Add(this.gradientPanel1);
            this.pnlInstitucional.Controls.Add(this.gradientPanel2);
            this.pnlInstitucional.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlInstitucional.Gradientebottom = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.pnlInstitucional.Gradientetop = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.pnlInstitucional.Location = new System.Drawing.Point(0, 0);
            this.pnlInstitucional.Name = "pnlInstitucional";
            this.pnlInstitucional.Size = new System.Drawing.Size(365, 750);
            this.pnlInstitucional.TabIndex = 2;
            this.pnlInstitucional.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlInstitucional_Paint);
            // 
            // linhaRoteiro2
            // 
            this.linhaRoteiro2.BackColor = System.Drawing.Color.White;
            this.linhaRoteiro2.Location = new System.Drawing.Point(53, 512);
            this.linhaRoteiro2.Name = "linhaRoteiro2";
            this.linhaRoteiro2.Size = new System.Drawing.Size(2, 48);
            this.linhaRoteiro2.TabIndex = 9;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.White;
            this.pnlLogo.Controls.Add(this.picLogo);
            this.pnlLogo.Location = new System.Drawing.Point(40, 40);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(84, 84);
            this.pnlLogo.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Image = global::SUB.Properties.Resources.sub_200x200px;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(84, 84);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblSub
            // 
            this.lblSub.BackColor = System.Drawing.Color.Transparent;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblSub.ForeColor = System.Drawing.Color.White;
            this.lblSub.Location = new System.Drawing.Point(142, 40);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(180, 40);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "SUB";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNomeSistema
            // 
            this.lblNomeSistema.BackColor = System.Drawing.Color.Transparent;
            this.lblNomeSistema.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblNomeSistema.ForeColor = System.Drawing.Color.White;
            this.lblNomeSistema.Location = new System.Drawing.Point(145, 80);
            this.lblNomeSistema.Name = "lblNomeSistema";
            this.lblNomeSistema.Size = new System.Drawing.Size(180, 44);
            this.lblNomeSistema.TabIndex = 2;
            this.lblNomeSistema.Text = "Sistema Único Bibliotecário";
            // 
            // lblConfiguracaoInicial
            // 
            this.lblConfiguracaoInicial.BackColor = System.Drawing.Color.Transparent;
            this.lblConfiguracaoInicial.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblConfiguracaoInicial.ForeColor = System.Drawing.Color.White;
            this.lblConfiguracaoInicial.Location = new System.Drawing.Point(40, 175);
            this.lblConfiguracaoInicial.Name = "lblConfiguracaoInicial";
            this.lblConfiguracaoInicial.Size = new System.Drawing.Size(285, 77);
            this.lblConfiguracaoInicial.TabIndex = 3;
            this.lblConfiguracaoInicial.Text = "Vamos preparar sua biblioteca";
            this.lblConfiguracaoInicial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTextoInstitucional
            // 
            this.lblTextoInstitucional.BackColor = System.Drawing.Color.Transparent;
            this.lblTextoInstitucional.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblTextoInstitucional.ForeColor = System.Drawing.Color.White;
            this.lblTextoInstitucional.Location = new System.Drawing.Point(40, 274);
            this.lblTextoInstitucional.Name = "lblTextoInstitucional";
            this.lblTextoInstitucional.Size = new System.Drawing.Size(285, 70);
            this.lblTextoInstitucional.TabIndex = 4;
            this.lblTextoInstitucional.Text = "Prepare o ambiente da sua instituição para organizar acervo, usuários e empréstim" +
    "os em um único sistema.";
            // 
            // lblRoteiro
            // 
            this.lblRoteiro.AutoSize = true;
            this.lblRoteiro.BackColor = System.Drawing.Color.Transparent;
            this.lblRoteiro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoteiro.ForeColor = System.Drawing.Color.White;
            this.lblRoteiro.Location = new System.Drawing.Point(40, 372);
            this.lblRoteiro.Name = "lblRoteiro";
            this.lblRoteiro.Size = new System.Drawing.Size(166, 15);
            this.lblRoteiro.TabIndex = 5;
            this.lblRoteiro.Text = "ROTEIRO DE CONFIGURAÇÃO";
            // 
            // lblPasso1Numero
            // 
            this.lblPasso1Numero.BackColor = System.Drawing.Color.White;
            this.lblPasso1Numero.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPasso1Numero.Location = new System.Drawing.Point(40, 408);
            this.lblPasso1Numero.Name = "lblPasso1Numero";
            this.lblPasso1Numero.Size = new System.Drawing.Size(28, 28);
            this.lblPasso1Numero.TabIndex = 6;
            this.lblPasso1Numero.Text = "1";
            this.lblPasso1Numero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPasso1Texto
            // 
            this.lblPasso1Texto.BackColor = System.Drawing.Color.Transparent;
            this.lblPasso1Texto.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPasso1Texto.ForeColor = System.Drawing.Color.White;
            this.lblPasso1Texto.Location = new System.Drawing.Point(82, 405);
            this.lblPasso1Texto.Name = "lblPasso1Texto";
            this.lblPasso1Texto.Size = new System.Drawing.Size(220, 34);
            this.lblPasso1Texto.TabIndex = 7;
            this.lblPasso1Texto.Text = "Instituição e identidade visual";
            this.lblPasso1Texto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linhaRoteiro
            // 
            this.linhaRoteiro.BackColor = System.Drawing.Color.White;
            this.linhaRoteiro.Location = new System.Drawing.Point(53, 436);
            this.linhaRoteiro.Name = "linhaRoteiro";
            this.linhaRoteiro.Size = new System.Drawing.Size(2, 48);
            this.linhaRoteiro.TabIndex = 8;
            // 
            // lblPasso2Numero
            // 
            this.lblPasso2Numero.BackColor = System.Drawing.Color.White;
            this.lblPasso2Numero.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPasso2Numero.Location = new System.Drawing.Point(40, 484);
            this.lblPasso2Numero.Name = "lblPasso2Numero";
            this.lblPasso2Numero.Size = new System.Drawing.Size(28, 28);
            this.lblPasso2Numero.TabIndex = 9;
            this.lblPasso2Numero.Text = "2";
            this.lblPasso2Numero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPasso2Texto
            // 
            this.lblPasso2Texto.BackColor = System.Drawing.Color.Transparent;
            this.lblPasso2Texto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPasso2Texto.ForeColor = System.Drawing.Color.White;
            this.lblPasso2Texto.Location = new System.Drawing.Point(82, 481);
            this.lblPasso2Texto.Name = "lblPasso2Texto";
            this.lblPasso2Texto.Size = new System.Drawing.Size(263, 34);
            this.lblPasso2Texto.TabIndex = 10;
            this.lblPasso2Texto.Text = "Acesso do primeiro bibliotecário";
            this.lblPasso2Texto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPasso3Numero
            // 
            this.lblPasso3Numero.BackColor = System.Drawing.Color.White;
            this.lblPasso3Numero.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPasso3Numero.Location = new System.Drawing.Point(40, 560);
            this.lblPasso3Numero.Name = "lblPasso3Numero";
            this.lblPasso3Numero.Size = new System.Drawing.Size(28, 28);
            this.lblPasso3Numero.TabIndex = 11;
            this.lblPasso3Numero.Text = "3";
            this.lblPasso3Numero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPasso3Texto
            // 
            this.lblPasso3Texto.BackColor = System.Drawing.Color.Transparent;
            this.lblPasso3Texto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPasso3Texto.ForeColor = System.Drawing.Color.White;
            this.lblPasso3Texto.Location = new System.Drawing.Point(82, 557);
            this.lblPasso3Texto.Name = "lblPasso3Texto";
            this.lblPasso3Texto.Size = new System.Drawing.Size(263, 34);
            this.lblPasso3Texto.TabIndex = 12;
            this.lblPasso3Texto.Text = "Turmas da instituição";
            this.lblPasso3Texto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linhaInstitucional
            // 
            this.linhaInstitucional.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.linhaInstitucional.Location = new System.Drawing.Point(54, 651);
            this.linhaInstitucional.Name = "linhaInstitucional";
            this.linhaInstitucional.Size = new System.Drawing.Size(256, 1);
            this.linhaInstitucional.TabIndex = 5;
            // 
            // lblIniciativa
            // 
            this.lblIniciativa.BackColor = System.Drawing.Color.Transparent;
            this.lblIniciativa.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblIniciativa.ForeColor = System.Drawing.Color.White;
            this.lblIniciativa.Location = new System.Drawing.Point(42, 666);
            this.lblIniciativa.Name = "lblIniciativa";
            this.lblIniciativa.Size = new System.Drawing.Size(280, 30);
            this.lblIniciativa.TabIndex = 6;
            this.lblIniciativa.Text = "Uma iniciativa da SEE/MG";
            this.lblIniciativa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.gradientPanel1.BorderRadius = 0;
            this.gradientPanel1.Gradientebottom = System.Drawing.Color.White;
            this.gradientPanel1.Gradientetop = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(1, 1);
            this.gradientPanel1.TabIndex = 7;
            this.gradientPanel1.Visible = false;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.White;
            this.gradientPanel2.BorderRadius = 0;
            this.gradientPanel2.Gradientebottom = System.Drawing.Color.Transparent;
            this.gradientPanel2.Gradientetop = System.Drawing.Color.Transparent;
            this.gradientPanel2.Location = new System.Drawing.Point(37, 37);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(90, 90);
            this.gradientPanel2.TabIndex = 11;
            this.gradientPanel2.Visible = false;
            // 
            // pnlConteudo
            // 
            this.pnlConteudo.BackColor = System.Drawing.Color.White;
            this.pnlConteudo.Controls.Add(this.indicadorEtapa3);
            this.pnlConteudo.Controls.Add(this.lblTitulo);
            this.pnlConteudo.Controls.Add(this.lblSubtitulo);
            this.pnlConteudo.Controls.Add(this.lblEtapaAtual);
            this.pnlConteudo.Controls.Add(this.indicadorEtapa1);
            this.pnlConteudo.Controls.Add(this.indicadorEtapa2);
            this.pnlConteudo.Controls.Add(this.lblErroGeral);
            this.pnlConteudo.Controls.Add(this.pnlEtapa1);
            this.pnlConteudo.Controls.Add(this.pnlEtapa2);
            this.pnlConteudo.Controls.Add(this.pnlEtapa3);
            this.pnlConteudo.Location = new System.Drawing.Point(397, 27);
            this.pnlConteudo.Name = "pnlConteudo";
            this.pnlConteudo.Size = new System.Drawing.Size(574, 696);
            this.pnlConteudo.TabIndex = 1;
            // 
            // indicadorEtapa3
            // 
            this.indicadorEtapa3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(234)))));
            this.indicadorEtapa3.Location = new System.Drawing.Point(371, 108);
            this.indicadorEtapa3.Name = "indicadorEtapa3";
            this.indicadorEtapa3.Size = new System.Drawing.Size(160, 5);
            this.indicadorEtapa3.TabIndex = 5;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.lblTitulo.Location = new System.Drawing.Point(38, 24);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(269, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configure o SUB";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(101)))), ((int)(((byte)(115)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(42, 72);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(380, 24);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Informe os dados iniciais da sua instituição.";
            // 
            // lblEtapaAtual
            // 
            this.lblEtapaAtual.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEtapaAtual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.lblEtapaAtual.Location = new System.Drawing.Point(421, 39);
            this.lblEtapaAtual.Name = "lblEtapaAtual";
            this.lblEtapaAtual.Size = new System.Drawing.Size(112, 22);
            this.lblEtapaAtual.TabIndex = 2;
            this.lblEtapaAtual.Text = "Etapa 1 de 3";
            this.lblEtapaAtual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // indicadorEtapa1
            // 
            this.indicadorEtapa1.Location = new System.Drawing.Point(42, 108);
            this.indicadorEtapa1.Name = "indicadorEtapa1";
            this.indicadorEtapa1.Size = new System.Drawing.Size(160, 5);
            this.indicadorEtapa1.TabIndex = 3;
            // 
            // indicadorEtapa2
            // 
            this.indicadorEtapa2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(227)))), ((int)(((byte)(234)))));
            this.indicadorEtapa2.Location = new System.Drawing.Point(207, 108);
            this.indicadorEtapa2.Name = "indicadorEtapa2";
            this.indicadorEtapa2.Size = new System.Drawing.Size(160, 5);
            this.indicadorEtapa2.TabIndex = 4;
            // 
            // lblErroGeral
            // 
            this.lblErroGeral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.lblErroGeral.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblErroGeral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.lblErroGeral.Location = new System.Drawing.Point(42, 119);
            this.lblErroGeral.Name = "lblErroGeral";
            this.lblErroGeral.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.lblErroGeral.Size = new System.Drawing.Size(491, 34);
            this.lblErroGeral.TabIndex = 5;
            this.lblErroGeral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblErroGeral.Visible = false;
            // 
            // pnlEtapa1
            // 
            this.pnlEtapa1.Controls.Add(this.lblTituloEtapa1);
            this.pnlEtapa1.Controls.Add(this.lblNomeInstituicao);
            this.pnlEtapa1.Controls.Add(this._nomeInstituicao);
            this.pnlEtapa1.Controls.Add(this.lblErroNomeInstituicao);
            this.pnlEtapa1.Controls.Add(this.lblTema);
            this.pnlEtapa1.Controls.Add(this._tema);
            this.pnlEtapa1.Controls.Add(this._aparencia);
            this.pnlEtapa1.Controls.Add(this.lblAparencia);
            this.pnlEtapa1.Controls.Add(this.lblDicaAparencia);
            this.pnlEtapa1.Controls.Add(this._visualizadorCor);
            this.pnlEtapa1.Controls.Add(this.lblErroTema);
            this.pnlEtapa1.Controls.Add(this.btnAvancar);
            this.pnlEtapa1.Location = new System.Drawing.Point(42, 153);
            this.pnlEtapa1.Name = "pnlEtapa1";
            this.pnlEtapa1.Size = new System.Drawing.Size(491, 517);
            this.pnlEtapa1.TabIndex = 6;
            // 
            // lblTituloEtapa1
            // 
            this.lblTituloEtapa1.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloEtapa1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.lblTituloEtapa1.Location = new System.Drawing.Point(0, 12);
            this.lblTituloEtapa1.Name = "lblTituloEtapa1";
            this.lblTituloEtapa1.Size = new System.Drawing.Size(420, 32);
            this.lblTituloEtapa1.TabIndex = 0;
            this.lblTituloEtapa1.Text = "Instituição e tema";
            // 
            // lblNomeInstituicao
            // 
            this.lblNomeInstituicao.AutoSize = true;
            this.lblNomeInstituicao.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNomeInstituicao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblNomeInstituicao.Location = new System.Drawing.Point(0, 73);
            this.lblNomeInstituicao.Name = "lblNomeInstituicao";
            this.lblNomeInstituicao.Size = new System.Drawing.Size(140, 17);
            this.lblNomeInstituicao.TabIndex = 1;
            this.lblNomeInstituicao.Text = "Nome da instituição *";
            // 
            // _nomeInstituicao
            // 
            this._nomeInstituicao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this._nomeInstituicao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this._nomeInstituicao.BackColor = System.Drawing.Color.White;
            this._nomeInstituicao.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._nomeInstituicao.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._nomeInstituicao.BorderRadius = 8;
            this._nomeInstituicao.BorderThickness = 1;
            this._nomeInstituicao.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._nomeInstituicao.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._nomeInstituicao.Location = new System.Drawing.Point(3, 96);
            this._nomeInstituicao.Name = "_nomeInstituicao";
            this._nomeInstituicao.PlaceholderColor = System.Drawing.Color.Gray;
            this._nomeInstituicao.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10.5F);
            this._nomeInstituicao.PlaceholderMarginLeft = 12;
            this._nomeInstituicao.PlaceholderText = "Ex.: Escola Estadual...";
            this._nomeInstituicao.SelectedText = "";
            this._nomeInstituicao.SelectionLength = 0;
            this._nomeInstituicao.SelectionStart = 0;
            this._nomeInstituicao.Size = new System.Drawing.Size(485, 34);
            this._nomeInstituicao.TabIndex = 0;
            this._nomeInstituicao.TextColor = System.Drawing.Color.Black;
            this._nomeInstituicao.UseSystemPasswordChar = false;
            // 
            // lblErroNomeInstituicao
            // 
            this.lblErroNomeInstituicao.AutoEllipsis = true;
            this.lblErroNomeInstituicao.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroNomeInstituicao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroNomeInstituicao.Location = new System.Drawing.Point(2, 132);
            this.lblErroNomeInstituicao.Name = "lblErroNomeInstituicao";
            this.lblErroNomeInstituicao.Size = new System.Drawing.Size(485, 17);
            this.lblErroNomeInstituicao.TabIndex = 2;
            this.lblErroNomeInstituicao.Visible = false;
            // 
            // lblTema
            // 
            this.lblTema.AutoSize = true;
            this.lblTema.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblTema.Location = new System.Drawing.Point(0, 155);
            this.lblTema.Name = "lblTema";
            this.lblTema.Size = new System.Drawing.Size(120, 17);
            this.lblTema.TabIndex = 3;
            this.lblTema.Text = "Tema do sistema *";
            // 
            // _tema
            // 
            this._tema.BackColor = System.Drawing.Color.White;
            this._tema.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._tema.BorderRadius = 8;
            this._tema.BorderThickness = 1;
            this._tema.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._tema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._tema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._tema.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._tema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this._tema.ItemsFont = new System.Drawing.Font("Segoe UI", 10F);
            this._tema.Location = new System.Drawing.Point(3, 178);
            this._tema.Name = "_tema";
            this._tema.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._tema.PlaceholderMargin = 10;
            this._tema.PlaceholderText = "Selecione...";
            this._tema.Size = new System.Drawing.Size(421, 27);
            this._tema.TabIndex = 1;
            //
            // _aparencia
            //
            this._aparencia.BackColor = System.Drawing.Color.White;
            this._aparencia.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._aparencia.BorderRadius = 8;
            this._aparencia.BorderThickness = 1;
            this._aparencia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._aparencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._aparencia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._aparencia.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._aparencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this._aparencia.Items.AddRange(new object[] { "Claro (Padrão)", "Escuro", "Sistema" });
            this._aparencia.ItemsFont = new System.Drawing.Font("Segoe UI", 10F);
            this._aparencia.Location = new System.Drawing.Point(3, 273);
            this._aparencia.Name = "_aparencia";
            this._aparencia.PlaceholderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
            this._aparencia.PlaceholderMargin = 10;
            this._aparencia.PlaceholderText = "Selecione a aparência";
            this._aparencia.Size = new System.Drawing.Size(485, 34);
            this._aparencia.TabIndex = 2;
            // lblAparencia
            this.lblAparencia.AutoSize = true;
            this.lblAparencia.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAparencia.ForeColor = System.Drawing.Color.FromArgb(44, 53, 65);
            this.lblAparencia.Location = new System.Drawing.Point(0, 250);
            this.lblAparencia.Name = "lblAparencia";
            this.lblAparencia.Size = new System.Drawing.Size(78, 17);
            this.lblAparencia.Text = "Aparência";
            // lblDicaAparencia
            this.lblDicaAparencia.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDicaAparencia.ForeColor = System.Drawing.Color.FromArgb(105, 114, 126);
            this.lblDicaAparencia.Location = new System.Drawing.Point(2, 315);
            this.lblDicaAparencia.Name = "lblDicaAparencia";
            this.lblDicaAparencia.Size = new System.Drawing.Size(485, 38);
            this.lblDicaAparencia.Text = "A aparência muda ao selecionar. Você poderá alterá-la nas Configurações.";
            // 
            // _visualizadorCor
            // 
            this._visualizadorCor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._visualizadorCor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._visualizadorCor.Location = new System.Drawing.Point(438, 170);
            this._visualizadorCor.Name = "_visualizadorCor";
            this._visualizadorCor.Size = new System.Drawing.Size(51, 42);
            this._visualizadorCor.TabIndex = 4;
            // 
            // lblErroTema
            // 
            this.lblErroTema.AutoEllipsis = true;
            this.lblErroTema.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroTema.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroTema.Location = new System.Drawing.Point(1, 205);
            this.lblErroTema.Name = "lblErroTema";
            this.lblErroTema.Size = new System.Drawing.Size(423, 17);
            this.lblErroTema.TabIndex = 5;
            this.lblErroTema.Visible = false;
            // 
            // btnAvancar
            // 
            this.btnAvancar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnAvancar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAvancar.BorderColor = System.Drawing.Color.Empty;
            this.btnAvancar.BorderThickness = 0;
            this.btnAvancar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAvancar.FlatAppearance.BorderSize = 0;
            this.btnAvancar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAvancar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAvancar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAvancar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnAvancar.ForeColor = System.Drawing.Color.White;
            this.btnAvancar.HoverColor = System.Drawing.Color.Empty;
            this.btnAvancar.Location = new System.Drawing.Point(313, 458);
            this.btnAvancar.Name = "btnAvancar";
            this.btnAvancar.PressedColor = System.Drawing.Color.Empty;
            this.btnAvancar.ShowBorder = false;
            this.btnAvancar.Size = new System.Drawing.Size(175, 44);
            this.btnAvancar.TabIndex = 3;
            this.btnAvancar.Text = "Avançar";
            this.btnAvancar.Click += new System.EventHandler(this.btnAvancar_Click);
            // 
            // pnlEtapa2
            // 
            this.pnlEtapa2.Controls.Add(this.lblTituloEtapa2);
            this.pnlEtapa2.Controls.Add(this.lblNomeBibliotecaria);
            this.pnlEtapa2.Controls.Add(this._nomeBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblErroNomeBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblEmailBibliotecaria);
            this.pnlEtapa2.Controls.Add(this._emailBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblAuxiliarEmail);
            this.pnlEtapa2.Controls.Add(this.lblErroEmailBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblTelefoneBibliotecaria);
            this.pnlEtapa2.Controls.Add(this._telefoneBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblErroTelefoneBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblSenhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this._senhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblErroSenhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblConfirmarSenhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this._confirmarSenhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.lblErroConfirmarSenhaBibliotecaria);
            this.pnlEtapa2.Controls.Add(this.chkMostrarSenhas);
            this.pnlEtapa2.Controls.Add(this.pnlCodigoRecuperacao);
            this.pnlEtapa2.Controls.Add(this.btnVoltar);
            this.pnlEtapa2.Controls.Add(this.btnAvancarEtapa2);
            this.pnlEtapa2.Location = new System.Drawing.Point(42, 153);
            this.pnlEtapa2.Name = "pnlEtapa2";
            this.pnlEtapa2.Size = new System.Drawing.Size(491, 517);
            this.pnlEtapa2.TabIndex = 7;
            this.pnlEtapa2.Visible = false;
            // 
            // lblTituloEtapa2
            // 
            this.lblTituloEtapa2.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloEtapa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.lblTituloEtapa2.Location = new System.Drawing.Point(0, 0);
            this.lblTituloEtapa2.Name = "lblTituloEtapa2";
            this.lblTituloEtapa2.Size = new System.Drawing.Size(460, 30);
            this.lblTituloEtapa2.TabIndex = 0;
            this.lblTituloEtapa2.Text = "Acesso do primeiro bibliotecário";
            this.lblTituloEtapa2.Click += new System.EventHandler(this.lblTituloEtapa2_Click);
            // 
            // lblNomeBibliotecaria
            // 
            this.lblNomeBibliotecaria.AutoSize = true;
            this.lblNomeBibliotecaria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblNomeBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblNomeBibliotecaria.Location = new System.Drawing.Point(0, 35);
            this.lblNomeBibliotecaria.Name = "lblNomeBibliotecaria";
            this.lblNomeBibliotecaria.Size = new System.Drawing.Size(102, 15);
            this.lblNomeBibliotecaria.TabIndex = 1;
            this.lblNomeBibliotecaria.Text = "Nome completo *";
            // 
            // _nomeBibliotecaria
            // 
            this._nomeBibliotecaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this._nomeBibliotecaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this._nomeBibliotecaria.BackColor = System.Drawing.Color.White;
            this._nomeBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._nomeBibliotecaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._nomeBibliotecaria.BorderRadius = 8;
            this._nomeBibliotecaria.BorderThickness = 1;
            this._nomeBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._nomeBibliotecaria.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._nomeBibliotecaria.Location = new System.Drawing.Point(3, 54);
            this._nomeBibliotecaria.Name = "_nomeBibliotecaria";
            this._nomeBibliotecaria.PlaceholderColor = System.Drawing.Color.Gray;
            this._nomeBibliotecaria.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10.5F);
            this._nomeBibliotecaria.PlaceholderMarginLeft = 12;
            this._nomeBibliotecaria.PlaceholderText = "Nome completo";
            this._nomeBibliotecaria.SelectedText = "";
            this._nomeBibliotecaria.SelectionLength = 0;
            this._nomeBibliotecaria.SelectionStart = 0;
            this._nomeBibliotecaria.Size = new System.Drawing.Size(485, 34);
            this._nomeBibliotecaria.TabIndex = 0;
            this._nomeBibliotecaria.TextColor = System.Drawing.Color.Black;
            this._nomeBibliotecaria.UseSystemPasswordChar = false;
            // 
            // lblErroNomeBibliotecaria
            // 
            this.lblErroNomeBibliotecaria.AutoEllipsis = true;
            this.lblErroNomeBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroNomeBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroNomeBibliotecaria.Location = new System.Drawing.Point(3, 90);
            this.lblErroNomeBibliotecaria.Name = "lblErroNomeBibliotecaria";
            this.lblErroNomeBibliotecaria.Size = new System.Drawing.Size(485, 17);
            this.lblErroNomeBibliotecaria.TabIndex = 2;
            this.lblErroNomeBibliotecaria.Visible = false;
            // 
            // lblEmailBibliotecaria
            // 
            this.lblEmailBibliotecaria.AutoSize = true;
            this.lblEmailBibliotecaria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmailBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblEmailBibliotecaria.Location = new System.Drawing.Point(0, 111);
            this.lblEmailBibliotecaria.Name = "lblEmailBibliotecaria";
            this.lblEmailBibliotecaria.Size = new System.Drawing.Size(49, 15);
            this.lblEmailBibliotecaria.TabIndex = 3;
            this.lblEmailBibliotecaria.Text = "E-mail *";
            // 
            // _emailBibliotecaria
            // 
            this._emailBibliotecaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this._emailBibliotecaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this._emailBibliotecaria.BackColor = System.Drawing.Color.White;
            this._emailBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._emailBibliotecaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._emailBibliotecaria.BorderRadius = 8;
            this._emailBibliotecaria.BorderThickness = 1;
            this._emailBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._emailBibliotecaria.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._emailBibliotecaria.Location = new System.Drawing.Point(3, 130);
            this._emailBibliotecaria.Name = "_emailBibliotecaria";
            this._emailBibliotecaria.PlaceholderColor = System.Drawing.Color.Gray;
            this._emailBibliotecaria.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10.5F);
            this._emailBibliotecaria.PlaceholderMarginLeft = 12;
            this._emailBibliotecaria.PlaceholderText = "nome@instituicao.gov.br";
            this._emailBibliotecaria.SelectedText = "";
            this._emailBibliotecaria.SelectionLength = 0;
            this._emailBibliotecaria.SelectionStart = 0;
            this._emailBibliotecaria.Size = new System.Drawing.Size(485, 34);
            this._emailBibliotecaria.TabIndex = 1;
            this._emailBibliotecaria.TextColor = System.Drawing.Color.Black;
            this._emailBibliotecaria.UseSystemPasswordChar = false;
            // 
            // lblAuxiliarEmail
            // 
            this.lblAuxiliarEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAuxiliarEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(114)))), ((int)(((byte)(126)))));
            this.lblAuxiliarEmail.Location = new System.Drawing.Point(3, 166);
            this.lblAuxiliarEmail.Name = "lblAuxiliarEmail";
            this.lblAuxiliarEmail.Size = new System.Drawing.Size(250, 16);
            this.lblAuxiliarEmail.TabIndex = 4;
            this.lblAuxiliarEmail.Text = "Será usado para entrar no SUB.";
            // 
            // lblErroEmailBibliotecaria
            // 
            this.lblErroEmailBibliotecaria.AutoEllipsis = true;
            this.lblErroEmailBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroEmailBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroEmailBibliotecaria.Location = new System.Drawing.Point(255, 166);
            this.lblErroEmailBibliotecaria.Name = "lblErroEmailBibliotecaria";
            this.lblErroEmailBibliotecaria.Size = new System.Drawing.Size(233, 17);
            this.lblErroEmailBibliotecaria.TabIndex = 5;
            this.lblErroEmailBibliotecaria.Visible = false;
            // 
            // lblTelefoneBibliotecaria
            // 
            this.lblTelefoneBibliotecaria.AutoSize = true;
            this.lblTelefoneBibliotecaria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTelefoneBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblTelefoneBibliotecaria.Location = new System.Drawing.Point(0, 191);
            this.lblTelefoneBibliotecaria.Name = "lblTelefoneBibliotecaria";
            this.lblTelefoneBibliotecaria.Size = new System.Drawing.Size(60, 15);
            this.lblTelefoneBibliotecaria.TabIndex = 6;
            this.lblTelefoneBibliotecaria.Text = "Telefone *";
            // 
            // _telefoneBibliotecaria
            // 
            this._telefoneBibliotecaria.BackColor = System.Drawing.Color.White;
            this._telefoneBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._telefoneBibliotecaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._telefoneBibliotecaria.BorderRadius = 8;
            this._telefoneBibliotecaria.Cursor = System.Windows.Forms.Cursors.IBeam;
            this._telefoneBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._telefoneBibliotecaria.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._telefoneBibliotecaria.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._telefoneBibliotecaria.LeftMargin = 7;
            this._telefoneBibliotecaria.Location = new System.Drawing.Point(3, 210);
            this._telefoneBibliotecaria.Mask = "(00) 00000-0009";
            this._telefoneBibliotecaria.MaskTextColor = System.Drawing.Color.Black;
            this._telefoneBibliotecaria.Name = "_telefoneBibliotecaria";
            this._telefoneBibliotecaria.Padding = new System.Windows.Forms.Padding(9, 5, 5, 5);
            this._telefoneBibliotecaria.Size = new System.Drawing.Size(485, 34);
            this._telefoneBibliotecaria.TabIndex = 2;
            // 
            // lblErroTelefoneBibliotecaria
            // 
            this.lblErroTelefoneBibliotecaria.AutoEllipsis = true;
            this.lblErroTelefoneBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroTelefoneBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroTelefoneBibliotecaria.Location = new System.Drawing.Point(3, 247);
            this.lblErroTelefoneBibliotecaria.Name = "lblErroTelefoneBibliotecaria";
            this.lblErroTelefoneBibliotecaria.Size = new System.Drawing.Size(485, 17);
            this.lblErroTelefoneBibliotecaria.TabIndex = 7;
            this.lblErroTelefoneBibliotecaria.Visible = false;
            // 
            // lblSenhaBibliotecaria
            // 
            this.lblSenhaBibliotecaria.AutoSize = true;
            this.lblSenhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSenhaBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblSenhaBibliotecaria.Location = new System.Drawing.Point(0, 267);
            this.lblSenhaBibliotecaria.Name = "lblSenhaBibliotecaria";
            this.lblSenhaBibliotecaria.Size = new System.Drawing.Size(48, 15);
            this.lblSenhaBibliotecaria.TabIndex = 8;
            this.lblSenhaBibliotecaria.Text = "Senha *";
            // 
            // _senhaBibliotecaria
            // 
            this._senhaBibliotecaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this._senhaBibliotecaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this._senhaBibliotecaria.BackColor = System.Drawing.Color.White;
            this._senhaBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._senhaBibliotecaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._senhaBibliotecaria.BorderRadius = 8;
            this._senhaBibliotecaria.BorderThickness = 1;
            this._senhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._senhaBibliotecaria.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._senhaBibliotecaria.Location = new System.Drawing.Point(3, 286);
            this._senhaBibliotecaria.Name = "_senhaBibliotecaria";
            this._senhaBibliotecaria.PlaceholderColor = System.Drawing.Color.Gray;
            this._senhaBibliotecaria.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10.5F);
            this._senhaBibliotecaria.PlaceholderMarginLeft = 12;
            this._senhaBibliotecaria.PlaceholderText = "6 a 25 caracteres";
            this._senhaBibliotecaria.SelectedText = "";
            this._senhaBibliotecaria.SelectionLength = 0;
            this._senhaBibliotecaria.SelectionStart = 0;
            this._senhaBibliotecaria.Size = new System.Drawing.Size(235, 34);
            this._senhaBibliotecaria.TabIndex = 3;
            this._senhaBibliotecaria.TextColor = System.Drawing.Color.Black;
            this._senhaBibliotecaria.UseSystemPasswordChar = true;
            // 
            // lblErroSenhaBibliotecaria
            // 
            this.lblErroSenhaBibliotecaria.AutoEllipsis = true;
            this.lblErroSenhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroSenhaBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroSenhaBibliotecaria.Location = new System.Drawing.Point(3, 322);
            this.lblErroSenhaBibliotecaria.Name = "lblErroSenhaBibliotecaria";
            this.lblErroSenhaBibliotecaria.Size = new System.Drawing.Size(235, 17);
            this.lblErroSenhaBibliotecaria.TabIndex = 9;
            this.lblErroSenhaBibliotecaria.Visible = false;
            // 
            // lblConfirmarSenhaBibliotecaria
            // 
            this.lblConfirmarSenhaBibliotecaria.AutoSize = true;
            this.lblConfirmarSenhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblConfirmarSenhaBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lblConfirmarSenhaBibliotecaria.Location = new System.Drawing.Point(250, 267);
            this.lblConfirmarSenhaBibliotecaria.Name = "lblConfirmarSenhaBibliotecaria";
            this.lblConfirmarSenhaBibliotecaria.Size = new System.Drawing.Size(102, 15);
            this.lblConfirmarSenhaBibliotecaria.TabIndex = 10;
            this.lblConfirmarSenhaBibliotecaria.Text = "Confirmar senha *";
            // 
            // _confirmarSenhaBibliotecaria
            // 
            this._confirmarSenhaBibliotecaria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this._confirmarSenhaBibliotecaria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this._confirmarSenhaBibliotecaria.BackColor = System.Drawing.Color.White;
            this._confirmarSenhaBibliotecaria.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this._confirmarSenhaBibliotecaria.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this._confirmarSenhaBibliotecaria.BorderRadius = 8;
            this._confirmarSenhaBibliotecaria.BorderThickness = 1;
            this._confirmarSenhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._confirmarSenhaBibliotecaria.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._confirmarSenhaBibliotecaria.Location = new System.Drawing.Point(253, 286);
            this._confirmarSenhaBibliotecaria.Name = "_confirmarSenhaBibliotecaria";
            this._confirmarSenhaBibliotecaria.PlaceholderColor = System.Drawing.Color.Gray;
            this._confirmarSenhaBibliotecaria.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10.5F);
            this._confirmarSenhaBibliotecaria.PlaceholderMarginLeft = 12;
            this._confirmarSenhaBibliotecaria.PlaceholderText = "Repita a senha";
            this._confirmarSenhaBibliotecaria.SelectedText = "";
            this._confirmarSenhaBibliotecaria.SelectionLength = 0;
            this._confirmarSenhaBibliotecaria.SelectionStart = 0;
            this._confirmarSenhaBibliotecaria.Size = new System.Drawing.Size(235, 34);
            this._confirmarSenhaBibliotecaria.TabIndex = 4;
            this._confirmarSenhaBibliotecaria.TextColor = System.Drawing.Color.Black;
            this._confirmarSenhaBibliotecaria.UseSystemPasswordChar = true;
            // 
            // lblErroConfirmarSenhaBibliotecaria
            // 
            this.lblErroConfirmarSenhaBibliotecaria.AutoEllipsis = true;
            this.lblErroConfirmarSenhaBibliotecaria.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblErroConfirmarSenhaBibliotecaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroConfirmarSenhaBibliotecaria.Location = new System.Drawing.Point(253, 322);
            this.lblErroConfirmarSenhaBibliotecaria.Name = "lblErroConfirmarSenhaBibliotecaria";
            this.lblErroConfirmarSenhaBibliotecaria.Size = new System.Drawing.Size(235, 17);
            this.lblErroConfirmarSenhaBibliotecaria.TabIndex = 11;
            this.lblErroConfirmarSenhaBibliotecaria.Visible = false;
            // 
            // chkMostrarSenhas
            // 
            this.chkMostrarSenhas.AutoSize = true;
            this.chkMostrarSenhas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkMostrarSenhas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(94)))));
            this.chkMostrarSenhas.Location = new System.Drawing.Point(3, 344);
            this.chkMostrarSenhas.Name = "chkMostrarSenhas";
            this.chkMostrarSenhas.Size = new System.Drawing.Size(106, 19);
            this.chkMostrarSenhas.TabIndex = 5;
            this.chkMostrarSenhas.Text = "Mostrar senhas";
            this.chkMostrarSenhas.CheckedChanged += new System.EventHandler(this.chkMostrarSenhas_CheckedChanged);
            // 
            // pnlCodigoRecuperacao
            // 
            this.pnlCodigoRecuperacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlCodigoRecuperacao.Controls.Add(this.lblCodigoTitulo);
            this.pnlCodigoRecuperacao.Controls.Add(this.lblCodigoAuxiliar);
            this.pnlCodigoRecuperacao.Controls.Add(this._codigoMestre);
            this.pnlCodigoRecuperacao.Controls.Add(this.exportarCodigo);
            this.pnlCodigoRecuperacao.Controls.Add(this.statusCodigo);
            this.pnlCodigoRecuperacao.Location = new System.Drawing.Point(3, 367);
            this.pnlCodigoRecuperacao.Name = "pnlCodigoRecuperacao";
            this.pnlCodigoRecuperacao.Size = new System.Drawing.Size(485, 104);
            this.pnlCodigoRecuperacao.TabIndex = 12;
            // 
            // lblCodigoTitulo
            // 
            this.lblCodigoTitulo.AutoSize = true;
            this.lblCodigoTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCodigoTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(48)))), ((int)(((byte)(61)))));
            this.lblCodigoTitulo.Location = new System.Drawing.Point(12, 8);
            this.lblCodigoTitulo.Name = "lblCodigoTitulo";
            this.lblCodigoTitulo.Size = new System.Drawing.Size(148, 17);
            this.lblCodigoTitulo.TabIndex = 0;
            this.lblCodigoTitulo.Text = "Código de recuperação";
            // 
            // lblCodigoAuxiliar
            // 
            this.lblCodigoAuxiliar.Font = new System.Drawing.Font("Segoe UI", 8.3F);
            this.lblCodigoAuxiliar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(101)))), ((int)(((byte)(115)))));
            this.lblCodigoAuxiliar.Location = new System.Drawing.Point(12, 28);
            this.lblCodigoAuxiliar.Name = "lblCodigoAuxiliar";
            this.lblCodigoAuxiliar.Size = new System.Drawing.Size(458, 17);
            this.lblCodigoAuxiliar.TabIndex = 1;
            this.lblCodigoAuxiliar.Text = "Guarde este código para recuperar o acesso administrativo.";
            // 
            // _codigoMestre
            // 
            this._codigoMestre.BackColor = System.Drawing.Color.White;
            this._codigoMestre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._codigoMestre.Font = new System.Drawing.Font("Consolas", 8.5F);
            this._codigoMestre.Location = new System.Drawing.Point(14, 50);
            this._codigoMestre.Name = "_codigoMestre";
            this._codigoMestre.ReadOnly = true;
            this._codigoMestre.Size = new System.Drawing.Size(322, 21);
            this._codigoMestre.TabIndex = 2;
            // 
            // exportarCodigo
            // 
            this.exportarCodigo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exportarCodigo.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.exportarCodigo.Location = new System.Drawing.Point(344, 48);
            this.exportarCodigo.Name = "exportarCodigo";
            this.exportarCodigo.Size = new System.Drawing.Size(127, 28);
            this.exportarCodigo.TabIndex = 6;
            this.exportarCodigo.Text = "Salvar código...";
            this.exportarCodigo.Click += new System.EventHandler(this.exportarCodigo_Click);
            // 
            // statusCodigo
            // 
            this.statusCodigo.AutoEllipsis = true;
            this.statusCodigo.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.statusCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(114)))), ((int)(((byte)(126)))));
            this.statusCodigo.Location = new System.Drawing.Point(12, 80);
            this.statusCodigo.Name = "statusCodigo";
            this.statusCodigo.Size = new System.Drawing.Size(459, 17);
            this.statusCodigo.TabIndex = 7;
            this.statusCodigo.Text = "Pendente de salvamento. Salve o código para concluir.";
            // 
            // btnVoltar
            // 
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVoltar.Location = new System.Drawing.Point(3, 480);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(92, 34);
            this.btnVoltar.TabIndex = 7;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnAvancarEtapa2
            // 
            this.btnAvancarEtapa2.BackgroundColor = System.Drawing.Color.Empty;
            this.btnAvancarEtapa2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.btnAvancarEtapa2.BorderColor = System.Drawing.Color.Empty;
            this.btnAvancarEtapa2.BorderThickness = 0;
            this.btnAvancarEtapa2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAvancarEtapa2.FlatAppearance.BorderSize = 0;
            this.btnAvancarEtapa2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAvancarEtapa2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAvancarEtapa2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAvancarEtapa2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAvancarEtapa2.ForeColor = System.Drawing.Color.White;
            this.btnAvancarEtapa2.HoverColor = System.Drawing.Color.Empty;
            this.btnAvancarEtapa2.Location = new System.Drawing.Point(288, 477);
            this.btnAvancarEtapa2.Name = "btnAvancarEtapa2";
            this.btnAvancarEtapa2.PressedColor = System.Drawing.Color.Empty;
            this.btnAvancarEtapa2.ShowBorder = false;
            this.btnAvancarEtapa2.Size = new System.Drawing.Size(200, 40);
            this.btnAvancarEtapa2.TabIndex = 8;
            this.btnAvancarEtapa2.Text = "Avançar";
            this.btnAvancarEtapa2.Click += new System.EventHandler(this.btnAvancarEtapa2_Click);
            // 
            // pnlEtapa3
            // 
            this.pnlEtapa3.Controls.Add(this.lblTituloEtapa3);
            this.pnlEtapa3.Controls.Add(this.lblTurmasIniciais);
            this.pnlEtapa3.Controls.Add(this.txtNovaTurma);
            this.pnlEtapa3.Controls.Add(this.btnAdicionarTurma);
            this.pnlEtapa3.Controls.Add(this.lstTurmasIniciais);
            this.pnlEtapa3.Controls.Add(this.btnRemoverTurma);
            this.pnlEtapa3.Controls.Add(this.lblErroTurmas);
            this.pnlEtapa3.Controls.Add(this.btnVoltarEtapa3);
            this.pnlEtapa3.Controls.Add(this.salvar);
            this.pnlEtapa3.Location = new System.Drawing.Point(42, 153);
            this.pnlEtapa3.Name = "pnlEtapa3";
            this.pnlEtapa3.Size = new System.Drawing.Size(491, 517);
            this.pnlEtapa3.TabIndex = 8;
            this.pnlEtapa3.Visible = false;
            // 
            // lblTituloEtapa3
            // 
            this.lblTituloEtapa3.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblTituloEtapa3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(39)))), ((int)(((byte)(53)))));
            this.lblTituloEtapa3.Location = new System.Drawing.Point(0, 0);
            this.lblTituloEtapa3.Name = "lblTituloEtapa3";
            this.lblTituloEtapa3.Size = new System.Drawing.Size(485, 30);
            this.lblTituloEtapa3.TabIndex = 0;
            this.lblTituloEtapa3.Text = "Turmas da instituição";
            // 
            // lblTurmasIniciais
            // 
            this.lblTurmasIniciais.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTurmasIniciais.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(82)))), ((int)(((byte)(94)))));
            this.lblTurmasIniciais.Location = new System.Drawing.Point(0, 40);
            this.lblTurmasIniciais.Name = "lblTurmasIniciais";
            this.lblTurmasIniciais.Size = new System.Drawing.Size(485, 38);
            this.lblTurmasIniciais.TabIndex = 1;
            this.lblTurmasIniciais.Text = "Adicione as turmas usadas pela instituição. Elas poderão ser gerenciadas depois e" +
    "m Pessoas.";
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
            this.txtNovaTurma.Location = new System.Drawing.Point(3, 97);
            this.txtNovaTurma.Name = "txtNovaTurma";
            this.txtNovaTurma.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtNovaTurma.PlaceholderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.txtNovaTurma.PlaceholderMarginLeft = 12;
            this.txtNovaTurma.PlaceholderText = "Ex.: 6º ANO A";
            this.txtNovaTurma.SelectedText = "";
            this.txtNovaTurma.SelectionLength = 0;
            this.txtNovaTurma.SelectionStart = 0;
            this.txtNovaTurma.Size = new System.Drawing.Size(300, 34);
            this.txtNovaTurma.TabIndex = 0;
            this.txtNovaTurma.TextColor = System.Drawing.Color.Black;
            this.txtNovaTurma.UseSystemPasswordChar = false;
            this.txtNovaTurma.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNovaTurma_KeyDown);
            // 
            // btnAdicionarTurma
            // 
            this.btnAdicionarTurma.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdicionarTurma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarTurma.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdicionarTurma.Location = new System.Drawing.Point(313, 96);
            this.btnAdicionarTurma.Name = "btnAdicionarTurma";
            this.btnAdicionarTurma.Size = new System.Drawing.Size(175, 36);
            this.btnAdicionarTurma.TabIndex = 1;
            this.btnAdicionarTurma.Text = "Adicionar turma";
            this.btnAdicionarTurma.UseVisualStyleBackColor = false;
            this.btnAdicionarTurma.Click += new System.EventHandler(this.btnAdicionarTurma_Click);
            // 
            // lstTurmasIniciais
            // 
            this.lstTurmasIniciais.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstTurmasIniciais.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lstTurmasIniciais.FormattingEnabled = true;
            this.lstTurmasIniciais.ItemHeight = 19;
            this.lstTurmasIniciais.Location = new System.Drawing.Point(3, 151);
            this.lstTurmasIniciais.Name = "lstTurmasIniciais";
            this.lstTurmasIniciais.Size = new System.Drawing.Size(485, 211);
            this.lstTurmasIniciais.TabIndex = 2;
            this.lstTurmasIniciais.SelectedIndexChanged += new System.EventHandler(this.lstTurmasIniciais_SelectedIndexChanged);
            // 
            // btnRemoverTurma
            // 
            this.btnRemoverTurma.Enabled = false;
            this.btnRemoverTurma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverTurma.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRemoverTurma.Location = new System.Drawing.Point(345, 370);
            this.btnRemoverTurma.Name = "btnRemoverTurma";
            this.btnRemoverTurma.Size = new System.Drawing.Size(143, 32);
            this.btnRemoverTurma.TabIndex = 3;
            this.btnRemoverTurma.Text = "Remover selecionada";
            this.btnRemoverTurma.UseVisualStyleBackColor = false;
            this.btnRemoverTurma.Click += new System.EventHandler(this.btnRemoverTurma_Click);
            // 
            // lblErroTurmas
            // 
            this.lblErroTurmas.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblErroTurmas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroTurmas.Location = new System.Drawing.Point(3, 373);
            this.lblErroTurmas.Name = "lblErroTurmas";
            this.lblErroTurmas.Size = new System.Drawing.Size(330, 26);
            this.lblErroTurmas.TabIndex = 4;
            this.lblErroTurmas.Visible = false;
            // 
            // btnVoltarEtapa3
            // 
            this.btnVoltarEtapa3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltarEtapa3.FlatAppearance.BorderSize = 0;
            this.btnVoltarEtapa3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltarEtapa3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnVoltarEtapa3.Location = new System.Drawing.Point(3, 480);
            this.btnVoltarEtapa3.Name = "btnVoltarEtapa3";
            this.btnVoltarEtapa3.Size = new System.Drawing.Size(92, 34);
            this.btnVoltarEtapa3.TabIndex = 4;
            this.btnVoltarEtapa3.Text = "Voltar";
            this.btnVoltarEtapa3.Click += new System.EventHandler(this.btnVoltarEtapa3_Click);
            // 
            // salvar
            // 
            this.salvar.BackgroundColor = System.Drawing.Color.Empty;
            this.salvar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(139)))), ((int)(((byte)(150)))));
            this.salvar.BorderColor = System.Drawing.Color.Empty;
            this.salvar.BorderThickness = 0;
            this.salvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.salvar.Enabled = false;
            this.salvar.FlatAppearance.BorderSize = 0;
            this.salvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.salvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.salvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.salvar.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.salvar.ForeColor = System.Drawing.Color.White;
            this.salvar.HoverColor = System.Drawing.Color.Empty;
            this.salvar.Location = new System.Drawing.Point(288, 477);
            this.salvar.Name = "salvar";
            this.salvar.PressedColor = System.Drawing.Color.Empty;
            this.salvar.ShowBorder = false;
            this.salvar.Size = new System.Drawing.Size(200, 40);
            this.salvar.TabIndex = 8;
            this.salvar.Text = "Concluir configuração";
            this.salvar.Click += new System.EventHandler(this.salvar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFechar.Image = global::SUB.Properties.Resources.icons8_x_20;
            this.btnFechar.Location = new System.Drawing.Point(974, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(23, 24);
            this.btnFechar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnFechar.TabIndex = 3;
            this.btnFechar.TabStop = false;
            this.btnFechar.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // PrimeiroUsoForm
            // 
            this.AcceptButton = this.btnAvancar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.pnlConteudo);
            this.Controls.Add(this.pnlInstitucional);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrimeiroUsoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração inicial do SUB";
            this.pnlInstitucional.ResumeLayout(false);
            this.pnlInstitucional.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlConteudo.ResumeLayout(false);
            this.pnlConteudo.PerformLayout();
            this.pnlEtapa1.ResumeLayout(false);
            this.pnlEtapa1.PerformLayout();
            this.pnlEtapa2.ResumeLayout(false);
            this.pnlEtapa2.PerformLayout();
            this.pnlCodigoRecuperacao.ResumeLayout(false);
            this.pnlCodigoRecuperacao.PerformLayout();
            this.pnlEtapa3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnFechar)).EndInit();
            this.ResumeLayout(false);

        }

        private ElementosPersonalizados.GradientPanel gradientPanel1;
        private ElementosPersonalizados.GradientPanel gradientPanel2;
        private System.Windows.Forms.Panel linhaRoteiro2;
        private System.Windows.Forms.Panel indicadorEtapa3;
        private System.Windows.Forms.PictureBox btnFechar;
    }
}
