namespace SUB.Forms.Configuracoes
{
    partial class ConfiguracaoForm
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
            this.pnlRaiz = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.grpInstituicao = new System.Windows.Forms.GroupBox();
            this.tblInstituicao = new System.Windows.Forms.TableLayoutPanel();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblCor = new System.Windows.Forms.Label();
            this.pnlLinhaCor = new System.Windows.Forms.FlowLayoutPanel();
            this.cbTemas = new System.Windows.Forms.ComboBox();
            this.pnlCorPreview = new System.Windows.Forms.Panel();
            this.lblCorHex = new System.Windows.Forms.Label();
            this.grpAtualizacao = new System.Windows.Forms.GroupBox();
            this.tblAtualizacao = new System.Windows.Forms.TableLayoutPanel();
            this.lblVersaoAtual = new System.Windows.Forms.Label();
            this.lblStatusAtualizacao = new System.Windows.Forms.Label();
            this.pnlVerificar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnVerificar = new CustomButton();
            this.pbVerificando = new System.Windows.Forms.ProgressBar();
            this.lblNotasTitulo = new System.Windows.Forms.Label();
            this.txtNotasVersao = new System.Windows.Forms.TextBox();
            this.pnlDownload = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAtualizar = new CustomButton();
            this.btnCancelarDownload = new CustomButton();
            this.pbDownload = new System.Windows.Forms.ProgressBar();
            this.pnlBotoes = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.pnlRaiz.SuspendLayout();
            this.grpInstituicao.SuspendLayout();
            this.tblInstituicao.SuspendLayout();
            this.pnlLinhaCor.SuspendLayout();
            this.grpAtualizacao.SuspendLayout();
            this.tblAtualizacao.SuspendLayout();
            this.pnlVerificar.SuspendLayout();
            this.pnlDownload.SuspendLayout();
            this.pnlBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRaiz
            // 
            this.pnlRaiz.ColumnCount = 1;
            this.pnlRaiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlRaiz.Controls.Add(this.lblTitulo, 0, 0);
            this.pnlRaiz.Controls.Add(this.grpInstituicao, 0, 1);
            this.pnlRaiz.Controls.Add(this.grpAtualizacao, 0, 2);
            this.pnlRaiz.Controls.Add(this.pnlBotoes, 0, 3);
            this.pnlRaiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRaiz.Location = new System.Drawing.Point(0, 0);
            this.pnlRaiz.Name = "pnlRaiz";
            this.pnlRaiz.Padding = new System.Windows.Forms.Padding(24, 20, 24, 16);
            this.pnlRaiz.RowCount = 4;
            this.pnlRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.pnlRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.pnlRaiz.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlRaiz.Size = new System.Drawing.Size(1040, 660);
            this.pnlRaiz.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(27, 20);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(986, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Configurações da instituição";
            // 
            // grpInstituicao
            // 
            this.grpInstituicao.Controls.Add(this.tblInstituicao);
            this.grpInstituicao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInstituicao.Location = new System.Drawing.Point(27, 52);
            this.grpInstituicao.Margin = new System.Windows.Forms.Padding(3, 3, 3, 12);
            this.grpInstituicao.Name = "grpInstituicao";
            this.grpInstituicao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpInstituicao.Size = new System.Drawing.Size(986, 35);
            this.grpInstituicao.TabIndex = 1;
            this.grpInstituicao.TabStop = false;
            this.grpInstituicao.Text = "Instituição";
            // 
            // tblInstituicao
            // 
            this.tblInstituicao.ColumnCount = 1;
            this.tblInstituicao.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInstituicao.Controls.Add(this.lblNome, 0, 0);
            this.tblInstituicao.Controls.Add(this.txtNome, 0, 1);
            this.tblInstituicao.Controls.Add(this.lblCor, 0, 2);
            this.tblInstituicao.Controls.Add(this.pnlLinhaCor, 0, 3);
            this.tblInstituicao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInstituicao.Location = new System.Drawing.Point(12, 23);
            this.tblInstituicao.Name = "tblInstituicao";
            this.tblInstituicao.RowCount = 4;
            this.tblInstituicao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblInstituicao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblInstituicao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblInstituicao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblInstituicao.Size = new System.Drawing.Size(962, 0);
            this.tblInstituicao.TabIndex = 0;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(3, 4);
            this.lblNome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(113, 13);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome da instituição:";
            // 
            // txtNome
            // 
            this.txtNome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNome.Location = new System.Drawing.Point(3, 19);
            this.txtNome.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.txtNome.MaxLength = 150;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(956, 22);
            this.txtNome.TabIndex = 1;
            // 
            // lblCor
            // 
            this.lblCor.AutoSize = true;
            this.lblCor.Location = new System.Drawing.Point(3, 51);
            this.lblCor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.lblCor.Name = "lblCor";
            this.lblCor.Size = new System.Drawing.Size(183, 13);
            this.lblCor.TabIndex = 2;
            this.lblCor.Text = "Cor principal da identidade visual:";
            // 
            // pnlLinhaCor
            // 
            this.pnlLinhaCor.Controls.Add(this.cbTemas);
            this.pnlLinhaCor.Controls.Add(this.pnlCorPreview);
            this.pnlLinhaCor.Controls.Add(this.lblCorHex);
            this.pnlLinhaCor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLinhaCor.Location = new System.Drawing.Point(3, 66);
            this.pnlLinhaCor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.pnlLinhaCor.Name = "pnlLinhaCor";
            this.pnlLinhaCor.Size = new System.Drawing.Size(956, 44);
            this.pnlLinhaCor.TabIndex = 3;
            this.pnlLinhaCor.WrapContents = false;
            // 
            // cbTemas
            // 
            this.cbTemas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTemas.Location = new System.Drawing.Point(3, 4);
            this.cbTemas.Margin = new System.Windows.Forms.Padding(3, 4, 8, 3);
            this.cbTemas.Name = "cbTemas";
            this.cbTemas.Size = new System.Drawing.Size(240, 21);
            this.cbTemas.TabIndex = 0;
            this.cbTemas.SelectedIndexChanged += new System.EventHandler(this.cbTemas_SelectedIndexChanged);
            // 
            // pnlCorPreview
            // 
            this.pnlCorPreview.Location = new System.Drawing.Point(254, 3);
            this.pnlCorPreview.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.pnlCorPreview.Name = "pnlCorPreview";
            this.pnlCorPreview.Size = new System.Drawing.Size(180, 36);
            this.pnlCorPreview.TabIndex = 1;
            this.pnlCorPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCorPreview_Paint);
            // 
            // lblCorHex
            // 
            this.lblCorHex.AutoSize = true;
            this.lblCorHex.Location = new System.Drawing.Point(445, 13);
            this.lblCorHex.Margin = new System.Windows.Forms.Padding(3, 13, 3, 0);
            this.lblCorHex.Name = "lblCorHex";
            this.lblCorHex.Size = new System.Drawing.Size(0, 13);
            this.lblCorHex.TabIndex = 2;
            // 
            // grpAtualizacao
            // 
            this.grpAtualizacao.Controls.Add(this.tblAtualizacao);
            this.grpAtualizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAtualizacao.Location = new System.Drawing.Point(27, 102);
            this.grpAtualizacao.Name = "grpAtualizacao";
            this.grpAtualizacao.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpAtualizacao.Size = new System.Drawing.Size(986, 494);
            this.grpAtualizacao.TabIndex = 2;
            this.grpAtualizacao.TabStop = false;
            this.grpAtualizacao.Text = "Atualização";
            // 
            // tblAtualizacao
            // 
            this.tblAtualizacao.ColumnCount = 1;
            this.tblAtualizacao.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAtualizacao.Controls.Add(this.lblVersaoAtual, 0, 0);
            this.tblAtualizacao.Controls.Add(this.lblStatusAtualizacao, 0, 1);
            this.tblAtualizacao.Controls.Add(this.pnlVerificar, 0, 2);
            this.tblAtualizacao.Controls.Add(this.lblNotasTitulo, 0, 3);
            this.tblAtualizacao.Controls.Add(this.txtNotasVersao, 0, 4);
            this.tblAtualizacao.Controls.Add(this.pnlDownload, 0, 5);
            this.tblAtualizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAtualizacao.Location = new System.Drawing.Point(12, 23);
            this.tblAtualizacao.Name = "tblAtualizacao";
            this.tblAtualizacao.RowCount = 6;
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAtualizacao.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblAtualizacao.Size = new System.Drawing.Size(962, 459);
            this.tblAtualizacao.TabIndex = 0;
            // 
            // lblVersaoAtual
            // 
            this.lblVersaoAtual.AutoSize = true;
            this.lblVersaoAtual.Location = new System.Drawing.Point(3, 2);
            this.lblVersaoAtual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 4);
            this.lblVersaoAtual.Name = "lblVersaoAtual";
            this.lblVersaoAtual.Size = new System.Drawing.Size(87, 13);
            this.lblVersaoAtual.TabIndex = 0;
            this.lblVersaoAtual.Text = "Versão atual: —";
            // 
            // lblStatusAtualizacao
            // 
            this.lblStatusAtualizacao.AutoSize = true;
            this.lblStatusAtualizacao.Location = new System.Drawing.Point(3, 21);
            this.lblStatusAtualizacao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 6);
            this.lblStatusAtualizacao.MaximumSize = new System.Drawing.Size(620, 0);
            this.lblStatusAtualizacao.Name = "lblStatusAtualizacao";
            this.lblStatusAtualizacao.Size = new System.Drawing.Size(337, 13);
            this.lblStatusAtualizacao.TabIndex = 1;
            this.lblStatusAtualizacao.Text = "Clique em Verificar atualizações para consultar uma nova versão.";
            // 
            // pnlVerificar
            // 
            this.pnlVerificar.Controls.Add(this.btnVerificar);
            this.pnlVerificar.Controls.Add(this.pbVerificando);
            this.pnlVerificar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVerificar.Location = new System.Drawing.Point(3, 43);
            this.pnlVerificar.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.pnlVerificar.Name = "pnlVerificar";
            this.pnlVerificar.Size = new System.Drawing.Size(956, 42);
            this.pnlVerificar.TabIndex = 2;
            this.pnlVerificar.WrapContents = false;
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnVerificar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnVerificar.BorderColor = System.Drawing.Color.Empty;
            this.btnVerificar.BorderThickness = 0;
            this.btnVerificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerificar.FlatAppearance.BorderSize = 0;
            this.btnVerificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.HoverColor = System.Drawing.Color.Empty;
            this.btnVerificar.Location = new System.Drawing.Point(3, 3);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.PressedColor = System.Drawing.Color.Empty;
            this.btnVerificar.ShowBorder = false;
            this.btnVerificar.Size = new System.Drawing.Size(190, 36);
            this.btnVerificar.TabIndex = 0;
            this.btnVerificar.TabStop = false;
            this.btnVerificar.Text = "Verificar atualizações";
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // pbVerificando
            // 
            this.pbVerificando.Location = new System.Drawing.Point(202, 9);
            this.pbVerificando.Margin = new System.Windows.Forms.Padding(6, 9, 3, 3);
            this.pbVerificando.Name = "pbVerificando";
            this.pbVerificando.Size = new System.Drawing.Size(180, 18);
            this.pbVerificando.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbVerificando.TabIndex = 1;
            this.pbVerificando.Visible = false;
            // 
            // lblNotasTitulo
            // 
            this.lblNotasTitulo.AutoSize = true;
            this.lblNotasTitulo.Location = new System.Drawing.Point(3, 95);
            this.lblNotasTitulo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.lblNotasTitulo.Name = "lblNotasTitulo";
            this.lblNotasTitulo.Size = new System.Drawing.Size(92, 13);
            this.lblNotasTitulo.TabIndex = 3;
            this.lblNotasTitulo.Text = "Notas da versão:";
            // 
            // txtNotasVersao
            // 
            this.txtNotasVersao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotasVersao.Location = new System.Drawing.Point(3, 110);
            this.txtNotasVersao.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.txtNotasVersao.MaxLength = 4000;
            this.txtNotasVersao.Multiline = true;
            this.txtNotasVersao.Name = "txtNotasVersao";
            this.txtNotasVersao.ReadOnly = true;
            this.txtNotasVersao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotasVersao.Size = new System.Drawing.Size(956, 295);
            this.txtNotasVersao.TabIndex = 4;
            // 
            // pnlDownload
            // 
            this.pnlDownload.Controls.Add(this.btnAtualizar);
            this.pnlDownload.Controls.Add(this.btnCancelarDownload);
            this.pnlDownload.Controls.Add(this.pbDownload);
            this.pnlDownload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDownload.Location = new System.Drawing.Point(3, 414);
            this.pnlDownload.Name = "pnlDownload";
            this.pnlDownload.Size = new System.Drawing.Size(956, 42);
            this.pnlDownload.TabIndex = 5;
            this.pnlDownload.WrapContents = false;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnAtualizar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnAtualizar.BorderColor = System.Drawing.Color.Empty;
            this.btnAtualizar.BorderThickness = 0;
            this.btnAtualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.HoverColor = System.Drawing.Color.Empty;
            this.btnAtualizar.Location = new System.Drawing.Point(3, 3);
            this.btnAtualizar.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.PressedColor = System.Drawing.Color.Empty;
            this.btnAtualizar.ShowBorder = false;
            this.btnAtualizar.Size = new System.Drawing.Size(160, 36);
            this.btnAtualizar.TabIndex = 0;
            this.btnAtualizar.TabStop = false;
            this.btnAtualizar.Text = "Atualizar agora";
            this.btnAtualizar.Visible = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnCancelarDownload
            // 
            this.btnCancelarDownload.BackgroundColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(105)))), ((int)(((byte)(111)))));
            this.btnCancelarDownload.BorderColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.BorderThickness = 0;
            this.btnCancelarDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDownload.FlatAppearance.BorderSize = 0;
            this.btnCancelarDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelarDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDownload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelarDownload.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDownload.HoverColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.Location = new System.Drawing.Point(171, 3);
            this.btnCancelarDownload.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnCancelarDownload.Name = "btnCancelarDownload";
            this.btnCancelarDownload.PressedColor = System.Drawing.Color.Empty;
            this.btnCancelarDownload.ShowBorder = false;
            this.btnCancelarDownload.Size = new System.Drawing.Size(175, 36);
            this.btnCancelarDownload.TabIndex = 1;
            this.btnCancelarDownload.TabStop = false;
            this.btnCancelarDownload.Text = "Cancelar download";
            this.btnCancelarDownload.Visible = false;
            this.btnCancelarDownload.Click += new System.EventHandler(this.btnCancelarDownload_Click);
            // 
            // pbDownload
            // 
            this.pbDownload.Location = new System.Drawing.Point(355, 9);
            this.pbDownload.Margin = new System.Windows.Forms.Padding(6, 9, 3, 3);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(260, 18);
            this.pbDownload.TabIndex = 2;
            this.pbDownload.Visible = false;
            // 
            // pnlBotoes
            // 
            this.pnlBotoes.AutoSize = true;
            this.pnlBotoes.Controls.Add(this.btnCancelar);
            this.pnlBotoes.Controls.Add(this.btnSalvar);
            this.pnlBotoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBotoes.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlBotoes.Location = new System.Drawing.Point(27, 602);
            this.pnlBotoes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlBotoes.Name = "pnlBotoes";
            this.pnlBotoes.Size = new System.Drawing.Size(986, 42);
            this.pnlBotoes.TabIndex = 3;
            this.pnlBotoes.WrapContents = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Location = new System.Drawing.Point(883, 3);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 36);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Location = new System.Drawing.Point(775, 3);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(0, 3, 8, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 36);
            this.btnSalvar.TabIndex = 1;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // ConfiguracaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(1040, 660);
            this.Controls.Add(this.pnlRaiz);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfiguracaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.pnlRaiz.ResumeLayout(false);
            this.pnlRaiz.PerformLayout();
            this.grpInstituicao.ResumeLayout(false);
            this.tblInstituicao.ResumeLayout(false);
            this.tblInstituicao.PerformLayout();
            this.pnlLinhaCor.ResumeLayout(false);
            this.pnlLinhaCor.PerformLayout();
            this.grpAtualizacao.ResumeLayout(false);
            this.tblAtualizacao.ResumeLayout(false);
            this.tblAtualizacao.PerformLayout();
            this.pnlVerificar.ResumeLayout(false);
            this.pnlDownload.ResumeLayout(false);
            this.pnlBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlRaiz;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpInstituicao;
        private System.Windows.Forms.TableLayoutPanel tblInstituicao;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCor;
        private System.Windows.Forms.FlowLayoutPanel pnlLinhaCor;
        private System.Windows.Forms.ComboBox cbTemas;
        private System.Windows.Forms.Panel pnlCorPreview;
        private System.Windows.Forms.Label lblCorHex;
        private System.Windows.Forms.GroupBox grpAtualizacao;
        private System.Windows.Forms.TableLayoutPanel tblAtualizacao;
        private System.Windows.Forms.Label lblVersaoAtual;
        private System.Windows.Forms.Label lblStatusAtualizacao;
        private System.Windows.Forms.FlowLayoutPanel pnlVerificar;
        private CustomButton btnVerificar;
        private System.Windows.Forms.ProgressBar pbVerificando;
        private System.Windows.Forms.Label lblNotasTitulo;
        private System.Windows.Forms.TextBox txtNotasVersao;
        private System.Windows.Forms.FlowLayoutPanel pnlDownload;
        private CustomButton btnAtualizar;
        private CustomButton btnCancelarDownload;
        private System.Windows.Forms.ProgressBar pbDownload;
        private System.Windows.Forms.FlowLayoutPanel pnlBotoes;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
