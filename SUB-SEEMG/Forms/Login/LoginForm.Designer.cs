namespace SUB.Forms.Login
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pnInicial = new System.Windows.Forms.Panel();
            this.gradientPanel2 = new SUB.ElementosPersonalizados.GradientPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAcessoInstitucional = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSubtituloLogin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new RoundedTextBox();
            this.lblErroEmail = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new RoundedTextBox();
            this.lblErroSenha = new System.Windows.Forms.Label();
            this.cbMostrarSenha = new System.Windows.Forms.CheckBox();
            this.lblEsqueceuSenha = new System.Windows.Forms.LinkLabel();
            this.lblStatusLogin = new System.Windows.Forms.Label();
            this.BtnEntrar = new CustomButton();
            this.lblAcessoRestrito = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSub = new System.Windows.Forms.Label();
            this.lblBiblioteca = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.picLogoLogin = new System.Windows.Forms.PictureBox();
            this.lblContextoLogin = new System.Windows.Forms.Label();
            this.lblNomeEscola = new System.Windows.Forms.Label();
            this.lblMarcaTexto = new System.Windows.Forms.Label();
            this.linhaMarca = new System.Windows.Forms.Panel();
            this.lblIniciativa = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.LinkLabel();
            this.gradientPanel1 = new SUB.ElementosPersonalizados.GradientPanel();
            this.pnInicial.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // pnInicial
            // 
            this.pnInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.pnInicial.Controls.Add(this.gradientPanel2);
            this.pnInicial.Controls.Add(this.panel1);
            this.pnInicial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInicial.Location = new System.Drawing.Point(0, 0);
            this.pnInicial.Name = "pnInicial";
            this.pnInicial.Size = new System.Drawing.Size(960, 640);
            this.pnInicial.TabIndex = 0;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gradientPanel2.BorderRadius = 0;
            this.gradientPanel2.Controls.Add(this.pictureBox1);
            this.gradientPanel2.Controls.Add(this.lblAcessoInstitucional);
            this.gradientPanel2.Controls.Add(this.label2);
            this.gradientPanel2.Controls.Add(this.lblSubtituloLogin);
            this.gradientPanel2.Controls.Add(this.label3);
            this.gradientPanel2.Controls.Add(this.txtEmail);
            this.gradientPanel2.Controls.Add(this.lblErroEmail);
            this.gradientPanel2.Controls.Add(this.label4);
            this.gradientPanel2.Controls.Add(this.txtSenha);
            this.gradientPanel2.Controls.Add(this.lblErroSenha);
            this.gradientPanel2.Controls.Add(this.cbMostrarSenha);
            this.gradientPanel2.Controls.Add(this.lblEsqueceuSenha);
            this.gradientPanel2.Controls.Add(this.lblStatusLogin);
            this.gradientPanel2.Controls.Add(this.BtnEntrar);
            this.gradientPanel2.Controls.Add(this.lblAcessoRestrito);
            this.gradientPanel2.Gradientebottom = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gradientPanel2.Gradientetop = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gradientPanel2.Location = new System.Drawing.Point(360, 0);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(600, 640);
            this.gradientPanel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::SUB.Properties.Resources.icons8_x_20;
            this.pictureBox1.Location = new System.Drawing.Point(564, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblAcessoInstitucional
            // 
            this.lblAcessoInstitucional.AutoSize = true;
            this.lblAcessoInstitucional.BackColor = System.Drawing.Color.Transparent;
            this.lblAcessoInstitucional.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAcessoInstitucional.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.lblAcessoInstitucional.Location = new System.Drawing.Point(88, 72);
            this.lblAcessoInstitucional.Name = "lblAcessoInstitucional";
            this.lblAcessoInstitucional.Size = new System.Drawing.Size(143, 15);
            this.lblAcessoInstitucional.TabIndex = 1;
            this.lblAcessoInstitucional.Text = "ACESSO INSTITUCIONAL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label2.Location = new System.Drawing.Point(84, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 45);
            this.label2.TabIndex = 2;
            this.label2.Text = "Acesse o SUB";
            // 
            // lblSubtituloLogin
            // 
            this.lblSubtituloLogin.AutoSize = true;
            this.lblSubtituloLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtituloLogin.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtituloLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(104)))), ((int)(((byte)(120)))));
            this.lblSubtituloLogin.Location = new System.Drawing.Point(88, 148);
            this.lblSubtituloLogin.Name = "lblSubtituloLogin";
            this.lblSubtituloLogin.Size = new System.Drawing.Size(233, 19);
            this.lblSubtituloLogin.TabIndex = 3;
            this.lblSubtituloLogin.Text = "Entre com sua conta de bibliotecária.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(55)))), ((int)(((byte)(69)))));
            this.label3.Location = new System.Drawing.Point(88, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "E-mail:";
            // 
            // txtEmail
            // 
            this.txtEmail.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtEmail.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.txtEmail.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.BorderThickness = 1;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtEmail.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtEmail.Location = new System.Drawing.Point(90, 229);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(129)))), ((int)(((byte)(143)))));
            this.txtEmail.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.PlaceholderMarginLeft = 12;
            this.txtEmail.PlaceholderText = "nome@instituicao.gov.br";
            this.txtEmail.SelectedText = "";
            this.txtEmail.SelectionLength = 0;
            this.txtEmail.SelectionStart = 0;
            this.txtEmail.Size = new System.Drawing.Size(420, 42);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.txtEmail.UseSystemPasswordChar = false;
            // 
            // lblErroEmail
            // 
            this.lblErroEmail.AutoEllipsis = true;
            this.lblErroEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblErroEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblErroEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroEmail.Location = new System.Drawing.Point(90, 271);
            this.lblErroEmail.Name = "lblErroEmail";
            this.lblErroEmail.Size = new System.Drawing.Size(420, 18);
            this.lblErroEmail.TabIndex = 6;
            this.lblErroEmail.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(55)))), ((int)(((byte)(69)))));
            this.label4.Location = new System.Drawing.Point(88, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Senha:";
            // 
            // txtSenha
            // 
            this.txtSenha.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtSenha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtSenha.BackColor = System.Drawing.Color.White;
            this.txtSenha.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.txtSenha.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.txtSenha.BorderRadius = 8;
            this.txtSenha.BorderThickness = 1;
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSenha.HoverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSenha.Location = new System.Drawing.Point(90, 320);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(129)))), ((int)(((byte)(143)))));
            this.txtSenha.PlaceholderFont = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSenha.PlaceholderMarginLeft = 12;
            this.txtSenha.PlaceholderText = "Digite sua senha";
            this.txtSenha.SelectedText = "";
            this.txtSenha.SelectionLength = 0;
            this.txtSenha.SelectionStart = 0;
            this.txtSenha.Size = new System.Drawing.Size(420, 42);
            this.txtSenha.TabIndex = 1;
            this.txtSenha.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lblErroSenha
            // 
            this.lblErroSenha.AutoEllipsis = true;
            this.lblErroSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblErroSenha.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblErroSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblErroSenha.Location = new System.Drawing.Point(90, 364);
            this.lblErroSenha.Name = "lblErroSenha";
            this.lblErroSenha.Size = new System.Drawing.Size(420, 18);
            this.lblErroSenha.TabIndex = 9;
            this.lblErroSenha.Visible = false;
            // 
            // cbMostrarSenha
            // 
            this.cbMostrarSenha.AutoSize = true;
            this.cbMostrarSenha.BackColor = System.Drawing.Color.Transparent;
            this.cbMostrarSenha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbMostrarSenha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbMostrarSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(86)))), ((int)(((byte)(102)))));
            this.cbMostrarSenha.Location = new System.Drawing.Point(90, 387);
            this.cbMostrarSenha.Name = "cbMostrarSenha";
            this.cbMostrarSenha.Size = new System.Drawing.Size(101, 19);
            this.cbMostrarSenha.TabIndex = 2;
            this.cbMostrarSenha.Text = "Mostrar senha";
            this.cbMostrarSenha.UseVisualStyleBackColor = false;
            this.cbMostrarSenha.CheckedChanged += new System.EventHandler(this.cbMostrarSenha_CheckedChanged);
            // 
            // lblEsqueceuSenha
            // 
            this.lblEsqueceuSenha.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(54)))), ((int)(((byte)(96)))));
            this.lblEsqueceuSenha.AutoSize = true;
            this.lblEsqueceuSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblEsqueceuSenha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEsqueceuSenha.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEsqueceuSenha.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblEsqueceuSenha.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.lblEsqueceuSenha.Location = new System.Drawing.Point(377, 387);
            this.lblEsqueceuSenha.Name = "lblEsqueceuSenha";
            this.lblEsqueceuSenha.Size = new System.Drawing.Size(125, 15);
            this.lblEsqueceuSenha.TabIndex = 3;
            this.lblEsqueceuSenha.TabStop = true;
            this.lblEsqueceuSenha.Text = "Recuperar meu acesso";
            this.lblEsqueceuSenha.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.lblEsqueceuSenha.Click += new System.EventHandler(this.lblEsqueceuSenha_Click);
            // 
            // lblStatusLogin
            // 
            this.lblStatusLogin.AutoEllipsis = true;
            this.lblStatusLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblStatusLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblStatusLogin.Location = new System.Drawing.Point(90, 427);
            this.lblStatusLogin.Name = "lblStatusLogin";
            this.lblStatusLogin.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblStatusLogin.Size = new System.Drawing.Size(420, 36);
            this.lblStatusLogin.TabIndex = 12;
            this.lblStatusLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatusLogin.Visible = false;
            // 
            // BtnEntrar
            // 
            this.BtnEntrar.BackColor = System.Drawing.Color.Transparent;
            this.BtnEntrar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.BtnEntrar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.BtnEntrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.BtnEntrar.BorderThickness = 0;
            this.BtnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEntrar.FlatAppearance.BorderSize = 0;
            this.BtnEntrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEntrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.BtnEntrar.ForeColor = System.Drawing.Color.White;
            this.BtnEntrar.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(61)))), ((int)(((byte)(106)))));
            this.BtnEntrar.Location = new System.Drawing.Point(90, 480);
            this.BtnEntrar.Name = "BtnEntrar";
            this.BtnEntrar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(47)))), ((int)(((byte)(82)))));
            this.BtnEntrar.ShowBorder = false;
            this.BtnEntrar.Size = new System.Drawing.Size(420, 46);
            this.BtnEntrar.TabIndex = 4;
            this.BtnEntrar.Text = "Entrar";
            this.BtnEntrar.UseVisualStyleBackColor = false;
            this.BtnEntrar.Click += new System.EventHandler(this.BtnEntrar_Click);
            // 
            // lblAcessoRestrito
            // 
            this.lblAcessoRestrito.BackColor = System.Drawing.Color.Transparent;
            this.lblAcessoRestrito.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAcessoRestrito.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(116)))), ((int)(((byte)(130)))));
            this.lblAcessoRestrito.Location = new System.Drawing.Point(90, 542);
            this.lblAcessoRestrito.Name = "lblAcessoRestrito";
            this.lblAcessoRestrito.Size = new System.Drawing.Size(420, 18);
            this.lblAcessoRestrito.TabIndex = 14;
            this.lblAcessoRestrito.Text = "Acesso restrito a pessoas autorizadas pela instituição.";
            this.lblAcessoRestrito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.panel1.Controls.Add(this.lblSub);
            this.panel1.Controls.Add(this.lblBiblioteca);
            this.panel1.Controls.Add(this.pnlLogo);
            this.panel1.Controls.Add(this.lblContextoLogin);
            this.panel1.Controls.Add(this.lblNomeEscola);
            this.panel1.Controls.Add(this.lblMarcaTexto);
            this.panel1.Controls.Add(this.linhaMarca);
            this.panel1.Controls.Add(this.lblIniciativa);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.gradientPanel1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 640);
            this.panel1.TabIndex = 0;
            // 
            // lblSub
            // 
            this.lblSub.BackColor = System.Drawing.Color.Transparent;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblSub.ForeColor = System.Drawing.Color.White;
            this.lblSub.Location = new System.Drawing.Point(146, 43);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(83, 42);
            this.lblSub.TabIndex = 0;
            this.lblSub.Text = "SUB";
            this.lblSub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBiblioteca
            // 
            this.lblBiblioteca.BackColor = System.Drawing.Color.Transparent;
            this.lblBiblioteca.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblBiblioteca.ForeColor = System.Drawing.Color.White;
            this.lblBiblioteca.Location = new System.Drawing.Point(149, 82);
            this.lblBiblioteca.Name = "lblBiblioteca";
            this.lblBiblioteca.Size = new System.Drawing.Size(100, 42);
            this.lblBiblioteca.TabIndex = 1;
            this.lblBiblioteca.Text = "Sistema Único Bibliotecário";
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.pnlLogo.Controls.Add(this.picLogoLogin);
            this.pnlLogo.Location = new System.Drawing.Point(44, 43);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(84, 84);
            this.pnlLogo.TabIndex = 2;
            // 
            // picLogoLogin
            // 
            this.picLogoLogin.BackColor = System.Drawing.Color.Transparent;
            this.picLogoLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogoLogin.Location = new System.Drawing.Point(0, 0);
            this.picLogoLogin.Name = "picLogoLogin";
            this.picLogoLogin.Padding = new System.Windows.Forms.Padding(4);
            this.picLogoLogin.Size = new System.Drawing.Size(84, 84);
            this.picLogoLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogoLogin.TabIndex = 0;
            this.picLogoLogin.TabStop = false;
            // 
            // lblContextoLogin
            // 
            this.lblContextoLogin.AutoSize = true;
            this.lblContextoLogin.BackColor = System.Drawing.Color.Transparent;
            this.lblContextoLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblContextoLogin.ForeColor = System.Drawing.Color.White;
            this.lblContextoLogin.Location = new System.Drawing.Point(40, 188);
            this.lblContextoLogin.Name = "lblContextoLogin";
            this.lblContextoLogin.Size = new System.Drawing.Size(155, 15);
            this.lblContextoLogin.TabIndex = 3;
            this.lblContextoLogin.Text = "BIBLIOTECA EM OPERAÇÃO";
            // 
            // lblNomeEscola
            // 
            this.lblNomeEscola.AutoEllipsis = true;
            this.lblNomeEscola.BackColor = System.Drawing.Color.Transparent;
            this.lblNomeEscola.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblNomeEscola.ForeColor = System.Drawing.Color.White;
            this.lblNomeEscola.Location = new System.Drawing.Point(40, 216);
            this.lblNomeEscola.Name = "lblNomeEscola";
            this.lblNomeEscola.Size = new System.Drawing.Size(280, 114);
            this.lblNomeEscola.TabIndex = 3;
            this.lblNomeEscola.Text = "Biblioteca da instituição";
            this.lblNomeEscola.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMarcaTexto
            // 
            this.lblMarcaTexto.BackColor = System.Drawing.Color.Transparent;
            this.lblMarcaTexto.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblMarcaTexto.ForeColor = System.Drawing.Color.White;
            this.lblMarcaTexto.Location = new System.Drawing.Point(40, 352);
            this.lblMarcaTexto.Name = "lblMarcaTexto";
            this.lblMarcaTexto.Size = new System.Drawing.Size(274, 64);
            this.lblMarcaTexto.TabIndex = 4;
            this.lblMarcaTexto.Text = "Continue de onde parou: acompanhe o acervo, os leitores e a circulação da sua ins" +
    "tituição.";
            // 
            // linhaMarca
            // 
            this.linhaMarca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(167)))), ((int)(((byte)(207)))));
            this.linhaMarca.Location = new System.Drawing.Point(40, 470);
            this.linhaMarca.Name = "linhaMarca";
            this.linhaMarca.Size = new System.Drawing.Size(280, 1);
            this.linhaMarca.TabIndex = 5;
            // 
            // lblIniciativa
            // 
            this.lblIniciativa.BackColor = System.Drawing.Color.Transparent;
            this.lblIniciativa.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblIniciativa.ForeColor = System.Drawing.Color.White;
            this.lblIniciativa.Location = new System.Drawing.Point(40, 486);
            this.lblIniciativa.Name = "lblIniciativa";
            this.lblIniciativa.Size = new System.Drawing.Size(280, 23);
            this.lblIniciativa.TabIndex = 6;
            this.lblIniciativa.Text = "Uma iniciativa da SEE/MG";
            this.lblIniciativa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.ActiveLinkColor = System.Drawing.Color.White;
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblVersion.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblVersion.LinkColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(40, 594);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(142, 15);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.TabStop = true;
            this.lblVersion.Text = "Sobre o SUB e sua equipe";
            this.lblVersion.VisitedLinkColor = System.Drawing.Color.White;
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            this.lblVersion.MouseEnter += new System.EventHandler(this.lblVersion_MouseEnter);
            this.lblVersion.MouseLeave += new System.EventHandler(this.lblVersion_MouseLeave);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.Color.White;
            this.gradientPanel1.BorderRadius = 0;
            this.gradientPanel1.Gradientebottom = System.Drawing.Color.Transparent;
            this.gradientPanel1.Gradientetop = System.Drawing.Color.Transparent;
            this.gradientPanel1.Location = new System.Drawing.Point(41, 40);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(90, 90);
            this.gradientPanel1.TabIndex = 8;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.BtnEntrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.pnInicial);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SUB — Acesso institucional";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnInicial.ResumeLayout(false);
            this.gradientPanel2.ResumeLayout(false);
            this.gradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogoLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnInicial;
        private SUB.ElementosPersonalizados.GradientPanel gradientPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAcessoInstitucional;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSubtituloLogin;
        private System.Windows.Forms.Label label3;
        private RoundedTextBox txtEmail;
        private System.Windows.Forms.Label lblErroEmail;
        private System.Windows.Forms.Label label4;
        private RoundedTextBox txtSenha;
        private System.Windows.Forms.Label lblErroSenha;
        private System.Windows.Forms.CheckBox cbMostrarSenha;
        private System.Windows.Forms.LinkLabel lblEsqueceuSenha;
        private System.Windows.Forms.Label lblStatusLogin;
        private CustomButton BtnEntrar;
        private System.Windows.Forms.Label lblAcessoRestrito;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblBiblioteca;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox picLogoLogin;
        private System.Windows.Forms.Label lblContextoLogin;
        private System.Windows.Forms.Label lblNomeEscola;
        private System.Windows.Forms.Label lblMarcaTexto;
        private System.Windows.Forms.Panel linhaMarca;
        private System.Windows.Forms.Label lblIniciativa;
        private System.Windows.Forms.LinkLabel lblVersion;
        private ElementosPersonalizados.GradientPanel gradientPanel1;
    }
}
