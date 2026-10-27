using System.Security.Cryptography.X509Certificates;

namespace SUB.Forms.Inicio
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelControl = new System.Windows.Forms.Panel();
            this.ControlPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.picExit = new System.Windows.Forms.PictureBox();
            this.picMax = new System.Windows.Forms.PictureBox();
            this.picMin = new System.Windows.Forms.PictureBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.relContainer = new System.Windows.Forms.Panel();
            this.btnRel = new System.Windows.Forms.Button();
            this.userContainer = new System.Windows.Forms.Panel();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnUserCad = new System.Windows.Forms.Button();
            this.btnUsuario = new System.Windows.Forms.Button();
            this.btnUserEdit = new System.Windows.Forms.Button();
            this.incioContainer = new System.Windows.Forms.Panel();
            this.btnInicio = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblContextoMenu = new System.Windows.Forms.Label();
            this.lblMarcaMenu = new System.Windows.Forms.Label();
            this.picLogoMenu = new System.Windows.Forms.PictureBox();
            this.livroContainer = new System.Windows.Forms.Panel();
            this.btnDev = new System.Windows.Forms.Button();
            this.btnLivros = new System.Windows.Forms.Button();
            this.btnLivroCad = new System.Windows.Forms.Button();
            this.btnEmprestimoRap = new System.Windows.Forms.Button();
            this.btnLivro = new System.Windows.Forms.Button();
            this.btnEmprestimo = new System.Windows.Forms.Button();
            this.livroTransition = new System.Windows.Forms.Timer(this.components);
            this.userTransition = new System.Windows.Forms.Timer(this.components);
            this.btnConfiguracao = new System.Windows.Forms.Button();
            this.panelControl.SuspendLayout();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).BeginInit();
            this.relContainer.SuspendLayout();
            this.userContainer.SuspendLayout();
            this.incioContainer.SuspendLayout();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoMenu)).BeginInit();
            this.livroContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.AllowDrop = true;
            this.panelControl.BackColor = System.Drawing.Color.White;
            this.panelControl.Controls.Add(this.ControlPanel);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.ForeColor = System.Drawing.SystemColors.Control;
            this.panelControl.Location = new System.Drawing.Point(264, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1176, 30);
            this.panelControl.TabIndex = 0;
            this.panelControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelControl_MouseDown);
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlPanel.Controls.Add(this.picExit);
            this.ControlPanel.Controls.Add(this.picMax);
            this.ControlPanel.Controls.Add(this.picMin);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.ControlPanel.Location = new System.Drawing.Point(1068, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(108, 30);
            this.ControlPanel.TabIndex = 4;
            // 
            // picExit
            // 
            this.picExit.BackColor = System.Drawing.Color.Transparent;
            this.picExit.BackgroundImage = global::SUB.Properties.Resources.icons8_x_20;
            this.picExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picExit.Location = new System.Drawing.Point(80, 3);
            this.picExit.Name = "picExit";
            this.picExit.Size = new System.Drawing.Size(25, 25);
            this.picExit.TabIndex = 3;
            this.picExit.TabStop = false;
            this.picExit.Click += new System.EventHandler(this.picExit_Click);
            this.picExit.MouseEnter += new System.EventHandler(this.picExit_MouseEnter);
            this.picExit.MouseLeave += new System.EventHandler(this.picExit_MouseLeave);
            // 
            // picMax
            // 
            this.picMax.BackColor = System.Drawing.Color.Transparent;
            this.picMax.BackgroundImage = global::SUB.Properties.Resources.icons8_verificar_todos_os_20;
            this.picMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMax.Location = new System.Drawing.Point(49, 3);
            this.picMax.Name = "picMax";
            this.picMax.Size = new System.Drawing.Size(25, 25);
            this.picMax.TabIndex = 6;
            this.picMax.TabStop = false;
            this.picMax.Click += new System.EventHandler(this.picMax_Click);
            this.picMax.MouseEnter += new System.EventHandler(this.picMax_MouseEnter);
            this.picMax.MouseLeave += new System.EventHandler(this.picMax_MouseLeave);
            // 
            // picMin
            // 
            this.picMin.BackColor = System.Drawing.Color.Transparent;
            this.picMin.BackgroundImage = global::SUB.Properties.Resources.icons8_menos_20;
            this.picMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMin.Location = new System.Drawing.Point(18, 3);
            this.picMin.Name = "picMin";
            this.picMin.Size = new System.Drawing.Size(25, 25);
            this.picMin.TabIndex = 5;
            this.picMin.TabStop = false;
            this.picMin.Click += new System.EventHandler(this.picMin_Click);
            this.picMin.MouseEnter += new System.EventHandler(this.picMin_MouseEnter);
            this.picMin.MouseLeave += new System.EventHandler(this.picMin_MouseLeave);
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Image = global::SUB.Properties.Resources.icons8_sair_25;
            this.btnSair.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.Location = new System.Drawing.Point(3, 427);
            this.btnSair.Name = "btnSair";
            this.btnSair.Padding = new System.Windows.Forms.Padding(18, 0, 10, 0);
            this.btnSair.Size = new System.Drawing.Size(258, 54);
            this.btnSair.TabIndex = 3;
            this.btnSair.TabStop = false;
            this.btnSair.Text = "  Encerrar sessão";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // relContainer
            // 
            this.relContainer.Controls.Add(this.btnRel);
            this.relContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.relContainer.Location = new System.Drawing.Point(3, 303);
            this.relContainer.Name = "relContainer";
            this.relContainer.Size = new System.Drawing.Size(258, 56);
            this.relContainer.TabIndex = 6;
            this.relContainer.Tag = 56;
            // 
            // btnRel
            // 
            this.btnRel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnRel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnRel.FlatAppearance.BorderSize = 0;
            this.btnRel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnRel.ForeColor = System.Drawing.Color.White;
            this.btnRel.Image = global::SUB.Properties.Resources.icons8_relatório_25;
            this.btnRel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRel.Location = new System.Drawing.Point(0, 0);
            this.btnRel.Name = "btnRel";
            this.btnRel.Padding = new System.Windows.Forms.Padding(16, 0, 10, 0);
            this.btnRel.Size = new System.Drawing.Size(258, 56);
            this.btnRel.TabIndex = 3;
            this.btnRel.TabStop = false;
            this.btnRel.Text = "  Relatórios";
            this.btnRel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRel.UseVisualStyleBackColor = false;
            this.btnRel.Click += new System.EventHandler(this.btnRel_Click);
            // 
            // userContainer
            // 
            this.userContainer.Controls.Add(this.btnUser);
            this.userContainer.Controls.Add(this.btnUserCad);
            this.userContainer.Controls.Add(this.btnUsuario);
            this.userContainer.Controls.Add(this.btnUserEdit);
            this.userContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userContainer.Location = new System.Drawing.Point(3, 179);
            this.userContainer.Name = "userContainer";
            this.userContainer.Size = new System.Drawing.Size(258, 56);
            this.userContainer.TabIndex = 4;
            this.userContainer.Tag = 200;
            // 
            // btnUser
            // 
            this.btnUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUser.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnUser.FlatAppearance.BorderSize = 0;
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUser.ForeColor = System.Drawing.Color.White;
            this.btnUser.Image = global::SUB.Properties.Resources.icons8_usuário_masculino_25;
            this.btnUser.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.Location = new System.Drawing.Point(0, 56);
            this.btnUser.Name = "btnUser";
            this.btnUser.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnUser.Size = new System.Drawing.Size(258, 48);
            this.btnUser.TabIndex = 15;
            this.btnUser.TabStop = false;
            this.btnUser.Text = "  Consultar usuários";
            this.btnUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUser.UseVisualStyleBackColor = false;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnUserCad
            // 
            this.btnUserCad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnUserCad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserCad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnUserCad.FlatAppearance.BorderSize = 0;
            this.btnUserCad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserCad.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserCad.ForeColor = System.Drawing.Color.White;
            this.btnUserCad.Image = global::SUB.Properties.Resources.icons8_adicionar_usuário_masculino_25;
            this.btnUserCad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserCad.Location = new System.Drawing.Point(0, 104);
            this.btnUserCad.Name = "btnUserCad";
            this.btnUserCad.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnUserCad.Size = new System.Drawing.Size(258, 48);
            this.btnUserCad.TabIndex = 13;
            this.btnUserCad.TabStop = false;
            this.btnUserCad.Text = "  Cadastrar usuário";
            this.btnUserCad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserCad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserCad.UseVisualStyleBackColor = false;
            this.btnUserCad.Click += new System.EventHandler(this.btnUserCad_Click);
            // 
            // btnUsuario
            // 
            this.btnUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnUsuario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUsuario.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnUsuario.FlatAppearance.BorderSize = 0;
            this.btnUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuario.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnUsuario.ForeColor = System.Drawing.Color.White;
            this.btnUsuario.Image = global::SUB.Properties.Resources.icons8_chamada_em_conferência_25;
            this.btnUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.Location = new System.Drawing.Point(0, 0);
            this.btnUsuario.Name = "btnUsuario";
            this.btnUsuario.Padding = new System.Windows.Forms.Padding(16, 0, 10, 0);
            this.btnUsuario.Size = new System.Drawing.Size(258, 56);
            this.btnUsuario.TabIndex = 3;
            this.btnUsuario.TabStop = false;
            this.btnUsuario.Text = "  Pessoas";
            this.btnUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUsuario.UseVisualStyleBackColor = false;
            this.btnUsuario.Click += new System.EventHandler(this.btnUsuario_Click);
            // 
            // btnUserEdit
            // 
            this.btnUserEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnUserEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUserEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnUserEdit.FlatAppearance.BorderSize = 0;
            this.btnUserEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnUserEdit.ForeColor = System.Drawing.Color.White;
            this.btnUserEdit.Image = global::SUB.Properties.Resources.icons8_registration_25;
            this.btnUserEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserEdit.Location = new System.Drawing.Point(0, 152);
            this.btnUserEdit.Name = "btnUserEdit";
            this.btnUserEdit.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnUserEdit.Size = new System.Drawing.Size(258, 48);
            this.btnUserEdit.TabIndex = 14;
            this.btnUserEdit.TabStop = false;
            this.btnUserEdit.Text = "  Editar usuário";
            this.btnUserEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserEdit.UseVisualStyleBackColor = false;
            this.btnUserEdit.Click += new System.EventHandler(this.btnUserEdit_Click);
            // 
            // incioContainer
            // 
            this.incioContainer.Controls.Add(this.btnInicio);
            this.incioContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.incioContainer.Location = new System.Drawing.Point(3, 117);
            this.incioContainer.Name = "incioContainer";
            this.incioContainer.Size = new System.Drawing.Size(258, 56);
            this.incioContainer.TabIndex = 2;
            this.incioContainer.Tag = 56;
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnInicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInicio.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnInicio.FlatAppearance.BorderSize = 0;
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnInicio.ForeColor = System.Drawing.Color.White;
            this.btnInicio.Image = global::SUB.Properties.Resources.icons8_página_inicial_25;
            this.btnInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInicio.Location = new System.Drawing.Point(0, 0);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Padding = new System.Windows.Forms.Padding(16, 0, 10, 0);
            this.btnInicio.Size = new System.Drawing.Size(258, 56);
            this.btnInicio.TabIndex = 3;
            this.btnInicio.TabStop = false;
            this.btnInicio.Text = "  Início";
            this.btnInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.menu.Controls.Add(this.panel1);
            this.menu.Controls.Add(this.incioContainer);
            this.menu.Controls.Add(this.userContainer);
            this.menu.Controls.Add(this.livroContainer);
            this.menu.Controls.Add(this.relContainer);
            this.menu.Controls.Add(this.btnConfiguracao);
            this.menu.Controls.Add(this.btnSair);
            this.menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(0, 12, 0, 16);
            this.menu.Size = new System.Drawing.Size(264, 800);
            this.menu.TabIndex = 1;
            this.menu.WrapContents = false;
            this.menu.Paint += new System.Windows.Forms.PaintEventHandler(this.menu_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblContextoMenu);
            this.panel1.Controls.Add(this.lblMarcaMenu);
            this.panel1.Controls.Add(this.picLogoMenu);
            this.panel1.Location = new System.Drawing.Point(3, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 96);
            this.panel1.TabIndex = 10;
            // 
            // lblContextoMenu
            // 
            this.lblContextoMenu.BackColor = System.Drawing.Color.Transparent;
            this.lblContextoMenu.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContextoMenu.ForeColor = System.Drawing.Color.White;
            this.lblContextoMenu.Location = new System.Drawing.Point(91, 48);
            this.lblContextoMenu.Name = "lblContextoMenu";
            this.lblContextoMenu.Size = new System.Drawing.Size(150, 32);
            this.lblContextoMenu.TabIndex = 12;
            this.lblContextoMenu.Text = "Sistema Único\r\nBibliotecário";
            // 
            // lblMarcaMenu
            // 
            this.lblMarcaMenu.AutoSize = true;
            this.lblMarcaMenu.BackColor = System.Drawing.Color.Transparent;
            this.lblMarcaMenu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarcaMenu.ForeColor = System.Drawing.Color.White;
            this.lblMarcaMenu.Location = new System.Drawing.Point(90, 20);
            this.lblMarcaMenu.Name = "lblMarcaMenu";
            this.lblMarcaMenu.Size = new System.Drawing.Size(55, 30);
            this.lblMarcaMenu.TabIndex = 11;
            this.lblMarcaMenu.Text = "SUB";
            // 
            // picLogoMenu
            // 
            this.picLogoMenu.BackColor = System.Drawing.Color.Transparent;
            this.picLogoMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLogoMenu.Location = new System.Drawing.Point(18, 20);
            this.picLogoMenu.Name = "picLogoMenu";
            this.picLogoMenu.Size = new System.Drawing.Size(56, 56);
            this.picLogoMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogoMenu.TabIndex = 9;
            this.picLogoMenu.TabStop = false;
            // 
            // livroContainer
            // 
            this.livroContainer.Controls.Add(this.btnDev);
            this.livroContainer.Controls.Add(this.btnLivros);
            this.livroContainer.Controls.Add(this.btnLivroCad);
            this.livroContainer.Controls.Add(this.btnEmprestimoRap);
            this.livroContainer.Controls.Add(this.btnLivro);
            this.livroContainer.Controls.Add(this.btnEmprestimo);
            this.livroContainer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.livroContainer.Location = new System.Drawing.Point(3, 241);
            this.livroContainer.Name = "livroContainer";
            this.livroContainer.Size = new System.Drawing.Size(258, 56);
            this.livroContainer.TabIndex = 8;
            this.livroContainer.Tag = 296;
            // 
            // btnDev
            // 
            this.btnDev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnDev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnDev.FlatAppearance.BorderSize = 0;
            this.btnDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDev.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDev.ForeColor = System.Drawing.Color.White;
            this.btnDev.Image = ((System.Drawing.Image)(resources.GetObject("btnDev.Image")));
            this.btnDev.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDev.Location = new System.Drawing.Point(0, 248);
            this.btnDev.Name = "btnDev";
            this.btnDev.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnDev.Size = new System.Drawing.Size(258, 48);
            this.btnDev.TabIndex = 7;
            this.btnDev.TabStop = false;
            this.btnDev.Text = "  Registrar devolução";
            this.btnDev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDev.UseVisualStyleBackColor = false;
            this.btnDev.Click += new System.EventHandler(this.btnDev_Click);
            // 
            // btnLivros
            // 
            this.btnLivros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnLivros.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLivros.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnLivros.FlatAppearance.BorderSize = 0;
            this.btnLivros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivros.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLivros.ForeColor = System.Drawing.Color.White;
            this.btnLivros.Image = global::SUB.Properties.Resources.icons8_livro_25;
            this.btnLivros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivros.Location = new System.Drawing.Point(0, 56);
            this.btnLivros.Name = "btnLivros";
            this.btnLivros.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnLivros.Size = new System.Drawing.Size(258, 48);
            this.btnLivros.TabIndex = 9;
            this.btnLivros.TabStop = false;
            this.btnLivros.Text = "  Consultar acervo";
            this.btnLivros.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivros.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLivros.UseVisualStyleBackColor = false;
            this.btnLivros.Click += new System.EventHandler(this.btnLivros_Click);
            // 
            // btnLivroCad
            // 
            this.btnLivroCad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnLivroCad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLivroCad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnLivroCad.FlatAppearance.BorderSize = 0;
            this.btnLivroCad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivroCad.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLivroCad.ForeColor = System.Drawing.Color.White;
            this.btnLivroCad.Image = ((System.Drawing.Image)(resources.GetObject("btnLivroCad.Image")));
            this.btnLivroCad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivroCad.Location = new System.Drawing.Point(0, 200);
            this.btnLivroCad.Name = "btnLivroCad";
            this.btnLivroCad.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnLivroCad.Size = new System.Drawing.Size(258, 48);
            this.btnLivroCad.TabIndex = 6;
            this.btnLivroCad.TabStop = false;
            this.btnLivroCad.Text = "Cadastrar livro";
            this.btnLivroCad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivroCad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLivroCad.UseVisualStyleBackColor = false;
            this.btnLivroCad.Click += new System.EventHandler(this.btnLivroCad_Click);
            // 
            // btnEmprestimoRap
            // 
            this.btnEmprestimoRap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnEmprestimoRap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmprestimoRap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnEmprestimoRap.FlatAppearance.BorderSize = 0;
            this.btnEmprestimoRap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmprestimoRap.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnEmprestimoRap.ForeColor = System.Drawing.Color.White;
            this.btnEmprestimoRap.Image = ((System.Drawing.Image)(resources.GetObject("btnEmprestimoRap.Image")));
            this.btnEmprestimoRap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmprestimoRap.Location = new System.Drawing.Point(0, 152);
            this.btnEmprestimoRap.Name = "btnEmprestimoRap";
            this.btnEmprestimoRap.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnEmprestimoRap.Size = new System.Drawing.Size(258, 48);
            this.btnEmprestimoRap.TabIndex = 5;
            this.btnEmprestimoRap.TabStop = false;
            this.btnEmprestimoRap.Text = "  Empréstimo rápido";
            this.btnEmprestimoRap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmprestimoRap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEmprestimoRap.UseVisualStyleBackColor = false;
            this.btnEmprestimoRap.Click += new System.EventHandler(this.btnEmprestimoRap_Click);
            // 
            // btnLivro
            // 
            this.btnLivro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnLivro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLivro.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnLivro.FlatAppearance.BorderSize = 0;
            this.btnLivro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLivro.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLivro.ForeColor = System.Drawing.Color.White;
            this.btnLivro.Image = ((System.Drawing.Image)(resources.GetObject("btnLivro.Image")));
            this.btnLivro.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivro.Location = new System.Drawing.Point(0, 0);
            this.btnLivro.Name = "btnLivro";
            this.btnLivro.Padding = new System.Windows.Forms.Padding(16, 0, 10, 0);
            this.btnLivro.Size = new System.Drawing.Size(258, 56);
            this.btnLivro.TabIndex = 3;
            this.btnLivro.TabStop = false;
            this.btnLivro.Text = "  Acervo";
            this.btnLivro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLivro.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLivro.UseVisualStyleBackColor = false;
            this.btnLivro.Click += new System.EventHandler(this.btnLivro_Click);
            // 
            // btnEmprestimo
            // 
            this.btnEmprestimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(98)))), ((int)(((byte)(144)))));
            this.btnEmprestimo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmprestimo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(88)))), ((int)(((byte)(130)))));
            this.btnEmprestimo.FlatAppearance.BorderSize = 0;
            this.btnEmprestimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmprestimo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnEmprestimo.ForeColor = System.Drawing.Color.White;
            this.btnEmprestimo.Image = ((System.Drawing.Image)(resources.GetObject("btnEmprestimo.Image")));
            this.btnEmprestimo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmprestimo.Location = new System.Drawing.Point(0, 104);
            this.btnEmprestimo.Name = "btnEmprestimo";
            this.btnEmprestimo.Padding = new System.Windows.Forms.Padding(20, 0, 10, 0);
            this.btnEmprestimo.Size = new System.Drawing.Size(258, 48);
            this.btnEmprestimo.TabIndex = 4;
            this.btnEmprestimo.TabStop = false;
            this.btnEmprestimo.Text = "  Registrar empréstimo";
            this.btnEmprestimo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmprestimo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEmprestimo.UseVisualStyleBackColor = false;
            this.btnEmprestimo.Click += new System.EventHandler(this.btnEmprestimo_Click);
            // 
            // livroTransition
            // 
            this.livroTransition.Interval = 5;
            this.livroTransition.Tick += new System.EventHandler(this.livroTransition_Tick);
            // 
            // userTransition
            // 
            this.userTransition.Interval = 5;
            this.userTransition.Tick += new System.EventHandler(this.userTransition_Tick);
            // 
            // btnConfiguracao
            // 
            this.btnConfiguracao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(87)))), ((int)(((byte)(174)))));
            this.btnConfiguracao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguracao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(78)))), ((int)(((byte)(157)))));
            this.btnConfiguracao.FlatAppearance.BorderSize = 0;
            this.btnConfiguracao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracao.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnConfiguracao.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracao.Image = global::SUB.Properties.Resources.configuracao_25;
            this.btnConfiguracao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguracao.Location = new System.Drawing.Point(3, 365);
            this.btnConfiguracao.Name = "btnConfiguracao";
            this.btnConfiguracao.Padding = new System.Windows.Forms.Padding(16, 0, 10, 0);
            this.btnConfiguracao.Size = new System.Drawing.Size(258, 56);
            this.btnConfiguracao.TabIndex = 4;
            this.btnConfiguracao.TabStop = false;
            this.btnConfiguracao.Text = "  Configuração";
            this.btnConfiguracao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfiguracao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfiguracao.UseVisualStyleBackColor = false;
            this.btnConfiguracao.Click += new System.EventHandler(this.btnConfiguracao_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1440, 800);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "SUB";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelControl.ResumeLayout(false);
            this.ControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMin)).EndInit();
            this.relContainer.ResumeLayout(false);
            this.userContainer.ResumeLayout(false);
            this.incioContainer.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoMenu)).EndInit();
            this.livroContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.PictureBox picExit;
        private System.Windows.Forms.FlowLayoutPanel ControlPanel;
        private System.Windows.Forms.PictureBox picMin;
        private System.Windows.Forms.PictureBox picMax;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Panel relContainer;
        private System.Windows.Forms.Button btnRel;
        private System.Windows.Forms.Button btnUsuario;
        private System.Windows.Forms.Panel incioContainer;
        private System.Windows.Forms.FlowLayoutPanel menu;
        private System.Windows.Forms.Panel livroContainer;
        private System.Windows.Forms.Button btnDev;
        private System.Windows.Forms.Button btnLivroCad;
        private System.Windows.Forms.Button btnEmprestimoRap;
        private System.Windows.Forms.Button btnEmprestimo;
        private System.Windows.Forms.Button btnLivro;
        private System.Windows.Forms.Timer livroTransition;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.PictureBox picLogoMenu;
        private System.Windows.Forms.Button btnLivros;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblContextoMenu;
        private System.Windows.Forms.Label lblMarcaMenu;
        private System.Windows.Forms.Button btnUserCad;
        private System.Windows.Forms.Timer userTransition;
        public System.Windows.Forms.Panel userContainer;
        public System.Windows.Forms.Button btnUser;
        public System.Windows.Forms.Button btnUserEdit;
        private System.Windows.Forms.Button btnConfiguracao;
    }
}


