namespace BibliotecaApp.Forms.Login
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.lbTextoInicial = new System.Windows.Forms.Label();
            this.bemVindo = new System.Windows.Forms.Label();
            this.pcLogoInicial = new System.Windows.Forms.PictureBox();
            this.pnInicial = new System.Windows.Forms.Panel();
            this.gradientPanel2 = new BibliotecaApp.ElementosPersonalizados.GradientPanel();
            this.BtnEntrar = new CustomButton();
            this.cbMostrarSenha = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblEsqueceuSenha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pcLogoInicial)).BeginInit();
            this.pnInicial.SuspendLayout();
            this.gradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(12, 574);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(71, 17);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "Version 1.0";
            this.lblVersion.Click += new System.EventHandler(this.lblVersion_Click);
            this.lblVersion.MouseEnter += new System.EventHandler(this.lblVersion_MouseEnter);
            this.lblVersion.MouseLeave += new System.EventHandler(this.lblVersion_MouseLeave);
            // 
            // lbTextoInicial
            // 
            this.lbTextoInicial.BackColor = System.Drawing.Color.Transparent;
            this.lbTextoInicial.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextoInicial.ForeColor = System.Drawing.Color.White;
            this.lbTextoInicial.Location = new System.Drawing.Point(83, 358);
            this.lbTextoInicial.Name = "lbTextoInicial";
            this.lbTextoInicial.Size = new System.Drawing.Size(284, 169);
            this.lbTextoInicial.TabIndex = 2;
            this.lbTextoInicial.Text = "Seja bem-vindo ao sistema de gerenciamento de Acervos Literarios! Aqui você pode " +
    "consultar o acervo, registrar empréstimos, devoluções e organizar sua leitura.";
            this.lbTextoInicial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bemVindo
            // 
            this.bemVindo.AutoSize = true;
            this.bemVindo.BackColor = System.Drawing.Color.Transparent;
            this.bemVindo.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bemVindo.ForeColor = System.Drawing.Color.White;
            this.bemVindo.Location = new System.Drawing.Point(152, 42);
            this.bemVindo.Name = "bemVindo";
            this.bemVindo.Size = new System.Drawing.Size(147, 37);
            this.bemVindo.TabIndex = 1;
            this.bemVindo.Text = "Bem-vindo";
            // 
            // pcLogoInicial
            // 
            this.pcLogoInicial.BackColor = System.Drawing.Color.Transparent;
            this.pcLogoInicial.Image = global::BibliotecaApp.Properties.Resources.Brasão_da_Gastão3;
            this.pcLogoInicial.Location = new System.Drawing.Point(140, 143);
            this.pcLogoInicial.Name = "pcLogoInicial";
            this.pcLogoInicial.Size = new System.Drawing.Size(170, 186);
            this.pcLogoInicial.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcLogoInicial.TabIndex = 0;
            this.pcLogoInicial.TabStop = false;
            // 
            // pnInicial
            // 
            this.pnInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.pnInicial.Controls.Add(this.gradientPanel2);
            this.pnInicial.Controls.Add(this.bemVindo);
            this.pnInicial.Controls.Add(this.pcLogoInicial);
            this.pnInicial.Controls.Add(this.lblVersion);
            this.pnInicial.Controls.Add(this.lbTextoInicial);
            this.pnInicial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInicial.Location = new System.Drawing.Point(0, 0);
            this.pnInicial.Name = "pnInicial";
            this.pnInicial.Size = new System.Drawing.Size(900, 600);
            this.pnInicial.TabIndex = 10;
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel2.BorderRadius = 10;
            this.gradientPanel2.Controls.Add(this.BtnEntrar);
            this.gradientPanel2.Controls.Add(this.cbMostrarSenha);
            this.gradientPanel2.Controls.Add(this.panel4);
            this.gradientPanel2.Controls.Add(this.panel2);
            this.gradientPanel2.Controls.Add(this.label4);
            this.gradientPanel2.Controls.Add(this.txtSenha);
            this.gradientPanel2.Controls.Add(this.txtEmail);
            this.gradientPanel2.Controls.Add(this.label3);
            this.gradientPanel2.Controls.Add(this.label2);
            this.gradientPanel2.Controls.Add(this.lblEsqueceuSenha);
            this.gradientPanel2.Gradientebottom = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel2.Gradientetop = System.Drawing.Color.WhiteSmoke;
            this.gradientPanel2.Location = new System.Drawing.Point(488, 21);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(387, 557);
            this.gradientPanel2.TabIndex = 26;
            // 
            // BtnEntrar
            // 
            this.BtnEntrar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.BtnEntrar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(72)))), ((int)(((byte)(126)))));
            this.BtnEntrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.BtnEntrar.BorderRadius = 10;
            this.BtnEntrar.BorderThickness = 0;
            this.BtnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEntrar.FlatAppearance.BorderSize = 0;
            this.BtnEntrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.BtnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.BtnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEntrar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnEntrar.ForeColor = System.Drawing.Color.White;
            this.BtnEntrar.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.BtnEntrar.Location = new System.Drawing.Point(114, 440);
            this.BtnEntrar.Name = "BtnEntrar";
            this.BtnEntrar.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            this.BtnEntrar.ShowBorder = false;
            this.BtnEntrar.Size = new System.Drawing.Size(140, 42);
            this.BtnEntrar.TabIndex = 28;
            this.BtnEntrar.Text = "Entrar";
            this.BtnEntrar.UseVisualStyleBackColor = true;
            this.BtnEntrar.Click += new System.EventHandler(this.BtnEntrar_Click);
            // 
            // cbMostrarSenha
            // 
            this.cbMostrarSenha.AutoSize = true;
            this.cbMostrarSenha.Location = new System.Drawing.Point(50, 330);
            this.cbMostrarSenha.Name = "cbMostrarSenha";
            this.cbMostrarSenha.Size = new System.Drawing.Size(102, 19);
            this.cbMostrarSenha.TabIndex = 27;
            this.cbMostrarSenha.Text = "Mostrar Senha";
            this.cbMostrarSenha.UseVisualStyleBackColor = true;
            this.cbMostrarSenha.CheckedChanged += new System.EventHandler(this.cbMostrarSenha_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.panel4.Location = new System.Drawing.Point(50, 322);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(271, 2);
            this.panel4.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.panel2.Location = new System.Drawing.Point(50, 234);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 2);
            this.panel2.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(47, 274);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenha.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenha.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(50, 301);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(271, 19);
            this.txtSenha.TabIndex = 2;
            this.txtSenha.UseSystemPasswordChar = true;
            this.txtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha_KeyDown);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(50, 217);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(271, 19);
            this.txtEmail.TabIndex = 1;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(47, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(43, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 37);
            this.label2.TabIndex = 10;
            this.label2.Text = "Faça login na sua conta.";
            // 
            // lblEsqueceuSenha
            // 
            this.lblEsqueceuSenha.AutoSize = true;
            this.lblEsqueceuSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblEsqueceuSenha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEsqueceuSenha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(245)))));
            this.lblEsqueceuSenha.Location = new System.Drawing.Point(204, 331);
            this.lblEsqueceuSenha.Name = "lblEsqueceuSenha";
            this.lblEsqueceuSenha.Size = new System.Drawing.Size(117, 15);
            this.lblEsqueceuSenha.TabIndex = 16;
            this.lblEsqueceuSenha.Text = "Esqueceu sua senha?";
            this.lblEsqueceuSenha.Click += new System.EventHandler(this.lblEsqueceuSenha_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnInicial);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BibliotecaApp";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcLogoInicial)).EndInit();
            this.pnInicial.ResumeLayout(false);
            this.pnInicial.PerformLayout();
            this.gradientPanel2.ResumeLayout(false);
            this.gradientPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcLogoInicial;
        private System.Windows.Forms.Label bemVindo;
        private System.Windows.Forms.Label lbTextoInicial;
        private System.Windows.Forms.Label lblVersion;
        private ElementosPersonalizados.GradientPanel gradientPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEsqueceuSenha;
        private System.Windows.Forms.CheckBox cbMostrarSenha;
        private System.Windows.Forms.Panel pnInicial;
        private CustomButton BtnEntrar;
    }
}