namespace SUB.Forms.Configuracoes
{
    partial class ResetTermoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CodigoMestre = null;
                if (dlgCodigoMestre != null)
                    dlgCodigoMestre.Dispose();
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblIntroducao = new System.Windows.Forms.Label();
            this.grpDados = new System.Windows.Forms.GroupBox();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.lblUsuariosValor = new System.Windows.Forms.Label();
            this.lblTitulos = new System.Windows.Forms.Label();
            this.lblTitulosValor = new System.Windows.Forms.Label();
            this.lblEmprestimos = new System.Windows.Forms.Label();
            this.lblEmprestimosValor = new System.Windows.Forms.Label();
            this.lblRapidos = new System.Windows.Forms.Label();
            this.lblRapidosValor = new System.Windows.Forms.Label();
            this.lblTurmas = new System.Windows.Forms.Label();
            this.lblTurmasValor = new System.Windows.Forms.Label();
            this.lblOutrosDados = new System.Windows.Forms.Label();
            this.pnlAviso = new System.Windows.Forms.Panel();
            this.lblAviso = new System.Windows.Forms.Label();
            this.chkConcordo = new System.Windows.Forms.CheckBox();
            this.grpArquivo = new System.Windows.Forms.GroupBox();
            this.btnSelecionarTxt = new System.Windows.Forms.Button();
            this.lblArquivo = new System.Windows.Forms.Label();
            this.lblStatusArquivo = new System.Windows.Forms.Label();
            this.pnlRodape = new System.Windows.Forms.Panel();
            this.lblEtapa = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnContinuar = new System.Windows.Forms.Button();
            this.dlgCodigoMestre = new System.Windows.Forms.OpenFileDialog();
            this.pnlHeader.SuspendLayout();
            this.grpDados.SuspendLayout();
            this.pnlAviso.SuspendLayout();
            this.grpArquivo.SuspendLayout();
            this.pnlRodape.SuspendLayout();
            this.SuspendLayout();
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(31, 72, 126);
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.lblSubtitulo);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(720, 80);
            this.pnlHeader.TabIndex = 0;
            // lblTitulo
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(24, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(672, 38);
            this.lblTitulo.Text = "Termo de confirmação do reset";
            // lblSubtitulo
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.White;
            this.lblSubtitulo.Location = new System.Drawing.Point(26, 51);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(670, 22);
            this.lblSubtitulo.Text = "Revise as informações antes de autorizar a exclusão dos dados.";
            // lblIntroducao
            this.lblIntroducao.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblIntroducao.Location = new System.Drawing.Point(24, 96);
            this.lblIntroducao.Name = "lblIntroducao";
            this.lblIntroducao.Size = new System.Drawing.Size(672, 36);
            this.lblIntroducao.Text = "O aplicativo voltará ao primeiro uso. Veja abaixo os registros desta instalação que serão apagados:";
            // grpDados
            this.grpDados.Controls.Add(this.lblUsuarios);
            this.grpDados.Controls.Add(this.lblUsuariosValor);
            this.grpDados.Controls.Add(this.lblTitulos);
            this.grpDados.Controls.Add(this.lblTitulosValor);
            this.grpDados.Controls.Add(this.lblEmprestimos);
            this.grpDados.Controls.Add(this.lblEmprestimosValor);
            this.grpDados.Controls.Add(this.lblRapidos);
            this.grpDados.Controls.Add(this.lblRapidosValor);
            this.grpDados.Controls.Add(this.lblTurmas);
            this.grpDados.Controls.Add(this.lblTurmasValor);
            this.grpDados.Controls.Add(this.lblOutrosDados);
            this.grpDados.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.grpDados.Location = new System.Drawing.Point(24, 142);
            this.grpDados.Name = "grpDados";
            this.grpDados.Size = new System.Drawing.Size(672, 156);
            this.grpDados.TabIndex = 1;
            this.grpDados.TabStop = false;
            this.grpDados.Text = "Dados que serão excluídos";
            // lblUsuarios
            this.lblUsuarios.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblUsuarios.Location = new System.Drawing.Point(16, 27);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(232, 24);
            this.lblUsuarios.Text = "Usuários cadastrados";
            // lblUsuariosValor
            this.lblUsuariosValor.Location = new System.Drawing.Point(252, 27);
            this.lblUsuariosValor.Name = "lblUsuariosValor";
            this.lblUsuariosValor.Size = new System.Drawing.Size(64, 24);
            this.lblUsuariosValor.Text = "—";
            this.lblUsuariosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lblTitulos
            this.lblTitulos.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblTitulos.Location = new System.Drawing.Point(350, 27);
            this.lblTitulos.Name = "lblTitulos";
            this.lblTitulos.Size = new System.Drawing.Size(230, 24);
            this.lblTitulos.Text = "Títulos no acervo";
            // lblTitulosValor
            this.lblTitulosValor.Location = new System.Drawing.Point(588, 27);
            this.lblTitulosValor.Name = "lblTitulosValor";
            this.lblTitulosValor.Size = new System.Drawing.Size(64, 24);
            this.lblTitulosValor.Text = "—";
            this.lblTitulosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lblEmprestimos
            this.lblEmprestimos.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblEmprestimos.Location = new System.Drawing.Point(16, 56);
            this.lblEmprestimos.Name = "lblEmprestimos";
            this.lblEmprestimos.Size = new System.Drawing.Size(232, 24);
            this.lblEmprestimos.Text = "Empréstimos e histórico";
            // lblEmprestimosValor
            this.lblEmprestimosValor.Location = new System.Drawing.Point(252, 56);
            this.lblEmprestimosValor.Name = "lblEmprestimosValor";
            this.lblEmprestimosValor.Size = new System.Drawing.Size(64, 24);
            this.lblEmprestimosValor.Text = "—";
            this.lblEmprestimosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lblRapidos
            this.lblRapidos.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblRapidos.Location = new System.Drawing.Point(350, 56);
            this.lblRapidos.Name = "lblRapidos";
            this.lblRapidos.Size = new System.Drawing.Size(230, 24);
            this.lblRapidos.Text = "Empréstimos rápidos";
            // lblRapidosValor
            this.lblRapidosValor.Location = new System.Drawing.Point(588, 56);
            this.lblRapidosValor.Name = "lblRapidosValor";
            this.lblRapidosValor.Size = new System.Drawing.Size(64, 24);
            this.lblRapidosValor.Text = "—";
            this.lblRapidosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lblTurmas
            this.lblTurmas.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblTurmas.Location = new System.Drawing.Point(16, 85);
            this.lblTurmas.Name = "lblTurmas";
            this.lblTurmas.Size = new System.Drawing.Size(232, 24);
            this.lblTurmas.Text = "Turmas cadastradas";
            // lblTurmasValor
            this.lblTurmasValor.Location = new System.Drawing.Point(252, 85);
            this.lblTurmasValor.Name = "lblTurmasValor";
            this.lblTurmasValor.Size = new System.Drawing.Size(64, 24);
            this.lblTurmasValor.Text = "—";
            this.lblTurmasValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // lblOutrosDados
            this.lblOutrosDados.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOutrosDados.Location = new System.Drawing.Point(16, 116);
            this.lblOutrosDados.Name = "lblOutrosDados";
            this.lblOutrosDados.Size = new System.Drawing.Size(636, 32);
            this.lblOutrosDados.Text = "Também serão apagados: identidade da instituição, configurações, mapeamentos, relatórios internos e backups locais.";
            // pnlAviso
            this.pnlAviso.BackColor = System.Drawing.Color.FromArgb(253, 239, 239);
            this.pnlAviso.Controls.Add(this.lblAviso);
            this.pnlAviso.Location = new System.Drawing.Point(24, 310);
            this.pnlAviso.Name = "pnlAviso";
            this.pnlAviso.Size = new System.Drawing.Size(672, 62);
            this.pnlAviso.TabIndex = 2;
            // lblAviso
            this.lblAviso.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAviso.ForeColor = System.Drawing.Color.FromArgb(150, 40, 40);
            this.lblAviso.Location = new System.Drawing.Point(12, 8);
            this.lblAviso.Name = "lblAviso";
            this.lblAviso.Size = new System.Drawing.Size(648, 48);
            this.lblAviso.Text = "A exclusão é definitiva e inclui os backups locais. Arquivos exportados para outras pastas permanecem. Um novo código mestre será gerado no primeiro uso.";
            // chkConcordo
            this.chkConcordo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.chkConcordo.Location = new System.Drawing.Point(24, 384);
            this.chkConcordo.Name = "chkConcordo";
            this.chkConcordo.Size = new System.Drawing.Size(672, 38);
            this.chkConcordo.TabIndex = 3;
            this.chkConcordo.Text = "Li as informações e compreendo que todos os dados locais serão apagados.";
            this.chkConcordo.UseVisualStyleBackColor = true;
            this.chkConcordo.CheckedChanged += new System.EventHandler(this.chkConcordo_CheckedChanged);
            // grpArquivo
            this.grpArquivo.Controls.Add(this.btnSelecionarTxt);
            this.grpArquivo.Controls.Add(this.lblArquivo);
            this.grpArquivo.Controls.Add(this.lblStatusArquivo);
            this.grpArquivo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.grpArquivo.Location = new System.Drawing.Point(24, 434);
            this.grpArquivo.Name = "grpArquivo";
            this.grpArquivo.Size = new System.Drawing.Size(672, 104);
            this.grpArquivo.TabIndex = 4;
            this.grpArquivo.TabStop = false;
            this.grpArquivo.Text = "TXT de recuperação salvo no primeiro uso";
            // btnSelecionarTxt
            this.btnSelecionarTxt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarTxt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSelecionarTxt.Location = new System.Drawing.Point(16, 28);
            this.btnSelecionarTxt.Name = "btnSelecionarTxt";
            this.btnSelecionarTxt.Size = new System.Drawing.Size(182, 34);
            this.btnSelecionarTxt.TabIndex = 0;
            this.btnSelecionarTxt.Text = "Selecionar arquivo TXT";
            this.btnSelecionarTxt.Click += new System.EventHandler(this.btnSelecionarTxt_Click);
            // lblArquivo
            this.lblArquivo.AutoEllipsis = true;
            this.lblArquivo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblArquivo.Location = new System.Drawing.Point(212, 28);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(440, 34);
            this.lblArquivo.Text = "Nenhum arquivo selecionado";
            this.lblArquivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // lblStatusArquivo
            this.lblStatusArquivo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusArquivo.ForeColor = System.Drawing.Color.FromArgb(91, 104, 120);
            this.lblStatusArquivo.Location = new System.Drawing.Point(16, 70);
            this.lblStatusArquivo.Name = "lblStatusArquivo";
            this.lblStatusArquivo.Size = new System.Drawing.Size(636, 30);
            this.lblStatusArquivo.Text = "O arquivo deve corresponder ao código mestre desta instalação.";
            // pnlRodape
            this.pnlRodape.Controls.Add(this.lblEtapa);
            this.pnlRodape.Controls.Add(this.btnCancelar);
            this.pnlRodape.Controls.Add(this.btnContinuar);
            this.pnlRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlRodape.Location = new System.Drawing.Point(0, 554);
            this.pnlRodape.Name = "pnlRodape";
            this.pnlRodape.Size = new System.Drawing.Size(720, 78);
            this.pnlRodape.TabIndex = 5;
            // lblEtapa
            this.lblEtapa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblEtapa.Location = new System.Drawing.Point(24, 22);
            this.lblEtapa.Name = "lblEtapa";
            this.lblEtapa.Size = new System.Drawing.Size(340, 35);
            this.lblEtapa.Text = "Etapa 1 de 2\r\nArquivo de recuperação";
            // btnCancelar
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Location = new System.Drawing.Point(404, 20);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(110, 38);
            this.btnCancelar.TabIndex = 0;
            this.btnCancelar.Text = "Cancelar";
            // btnContinuar
            this.btnContinuar.Enabled = false;
            this.btnContinuar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinuar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnContinuar.Location = new System.Drawing.Point(526, 20);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(170, 38);
            this.btnContinuar.TabIndex = 1;
            this.btnContinuar.Text = "Validar e continuar";
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // dlgCodigoMestre
            this.dlgCodigoMestre.CheckFileExists = true;
            this.dlgCodigoMestre.CheckPathExists = true;
            this.dlgCodigoMestre.DefaultExt = "txt";
            this.dlgCodigoMestre.Filter = "Arquivo de recuperação (*.txt)|*.txt";
            this.dlgCodigoMestre.Multiselect = false;
            this.dlgCodigoMestre.RestoreDirectory = true;
            this.dlgCodigoMestre.Title = "Selecione o TXT salvo no primeiro uso do SUB";
            // ResetTermoForm
            this.AcceptButton = this.btnContinuar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(720, 632);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblIntroducao);
            this.Controls.Add(this.grpDados);
            this.Controls.Add(this.pnlAviso);
            this.Controls.Add(this.chkConcordo);
            this.Controls.Add(this.grpArquivo);
            this.Controls.Add(this.pnlRodape);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResetTermoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reset do aplicativo";
            this.Load += new System.EventHandler(this.ResetTermoForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.grpDados.ResumeLayout(false);
            this.pnlAviso.ResumeLayout(false);
            this.grpArquivo.ResumeLayout(false);
            this.pnlRodape.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblIntroducao;
        private System.Windows.Forms.GroupBox grpDados;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblUsuariosValor;
        private System.Windows.Forms.Label lblTitulos;
        private System.Windows.Forms.Label lblTitulosValor;
        private System.Windows.Forms.Label lblEmprestimos;
        private System.Windows.Forms.Label lblEmprestimosValor;
        private System.Windows.Forms.Label lblRapidos;
        private System.Windows.Forms.Label lblRapidosValor;
        private System.Windows.Forms.Label lblTurmas;
        private System.Windows.Forms.Label lblTurmasValor;
        private System.Windows.Forms.Label lblOutrosDados;
        private System.Windows.Forms.Panel pnlAviso;
        private System.Windows.Forms.Label lblAviso;
        private System.Windows.Forms.CheckBox chkConcordo;
        private System.Windows.Forms.GroupBox grpArquivo;
        private System.Windows.Forms.Button btnSelecionarTxt;
        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.Label lblStatusArquivo;
        private System.Windows.Forms.Panel pnlRodape;
        private System.Windows.Forms.Label lblEtapa;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnContinuar;
        private System.Windows.Forms.OpenFileDialog dlgCodigoMestre;
    }
}
