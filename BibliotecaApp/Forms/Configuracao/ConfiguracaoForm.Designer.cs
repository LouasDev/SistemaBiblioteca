namespace BibliotecaApp.Forms.Configuracao
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
            this.lblNome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Titulo = new System.Windows.Forms.Label();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancelar = new CustomButton();
            this.btnSalvar = new CustomButton();
            this.btnImportar = new CustomButton();
            this.txtBiblioteca = new RoundedTextBox();
            this.txtEscola = new RoundedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNome
            // 
            this.lblNome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNome.AutoSize = true;
            this.lblNome.BackColor = System.Drawing.Color.Transparent;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.lblNome.Location = new System.Drawing.Point(332, 116);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(152, 25);
            this.lblNome.TabIndex = 96;
            this.lblNome.Text = "Nome Da Escola";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label1.Location = new System.Drawing.Point(332, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 98;
            this.label1.Text = "Nome Da Biblioteca";
            // 
            // Titulo
            // 
            this.Titulo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Titulo.Font = new System.Drawing.Font("Segoe UI", 25.25F, System.Drawing.FontStyle.Bold);
            this.Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.Titulo.Location = new System.Drawing.Point(490, 28);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(255, 46);
            this.Titulo.TabIndex = 100;
            this.Titulo.Text = "Configurações";
            // 
            // picPreview
            // 
            this.picPreview.Location = new System.Drawing.Point(354, 322);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(100, 100);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 101;
            this.picPreview.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.label2.Location = new System.Drawing.Point(338, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 25);
            this.label2.TabIndex = 104;
            this.label2.Text = "Importar Foto";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnCancelar.BaseColor = System.Drawing.Color.Brown;
            this.btnCancelar.BorderColor = System.Drawing.Color.Empty;
            this.btnCancelar.BorderThickness = 0;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.HoverColor = System.Drawing.Color.Empty;
            this.btnCancelar.Location = new System.Drawing.Point(498, 808);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.PressedColor = System.Drawing.Color.Empty;
            this.btnCancelar.ShowBorder = false;
            this.btnCancelar.Size = new System.Drawing.Size(140, 42);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnSalvar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnSalvar.BorderColor = System.Drawing.Color.Empty;
            this.btnSalvar.BorderThickness = 0;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.HoverColor = System.Drawing.Color.Empty;
            this.btnSalvar.Location = new System.Drawing.Point(644, 808);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.PressedColor = System.Drawing.Color.Empty;
            this.btnSalvar.ShowBorder = false;
            this.btnSalvar.Size = new System.Drawing.Size(140, 42);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.BackgroundColor = System.Drawing.Color.Empty;
            this.btnImportar.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.btnImportar.BorderColor = System.Drawing.Color.Empty;
            this.btnImportar.BorderThickness = 0;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnImportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnImportar.ForeColor = System.Drawing.Color.White;
            this.btnImportar.HoverColor = System.Drawing.Color.Empty;
            this.btnImportar.Location = new System.Drawing.Point(334, 435);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.PressedColor = System.Drawing.Color.Empty;
            this.btnImportar.ShowBorder = false;
            this.btnImportar.Size = new System.Drawing.Size(140, 42);
            this.btnImportar.TabIndex = 3;
            this.btnImportar.Text = "Importar Foto";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // txtBiblioteca
            // 
            this.txtBiblioteca.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBiblioteca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBiblioteca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBiblioteca.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBiblioteca.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtBiblioteca.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtBiblioteca.BorderRadius = 10;
            this.txtBiblioteca.BorderThickness = 1;
            this.txtBiblioteca.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBiblioteca.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBiblioteca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtBiblioteca.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtBiblioteca.Location = new System.Drawing.Point(332, 228);
            this.txtBiblioteca.Name = "txtBiblioteca";
            this.txtBiblioteca.Padding = new System.Windows.Forms.Padding(7);
            this.txtBiblioteca.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtBiblioteca.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtBiblioteca.PlaceholderMarginLeft = 12;
            this.txtBiblioteca.PlaceholderText = "Digite aqui o nome...";
            this.txtBiblioteca.SelectedText = "";
            this.txtBiblioteca.SelectionLength = 0;
            this.txtBiblioteca.SelectionStart = 0;
            this.txtBiblioteca.Size = new System.Drawing.Size(617, 40);
            this.txtBiblioteca.TabIndex = 2;
            this.txtBiblioteca.TextColor = System.Drawing.Color.Black;
            this.txtBiblioteca.UseSystemPasswordChar = false;
            // 
            // txtEscola
            // 
            this.txtEscola.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEscola.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtEscola.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtEscola.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtEscola.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.txtEscola.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.txtEscola.BorderRadius = 10;
            this.txtEscola.BorderThickness = 1;
            this.txtEscola.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEscola.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEscola.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(42)))), ((int)(((byte)(60)))));
            this.txtEscola.HoverBackColor = System.Drawing.Color.LightGray;
            this.txtEscola.Location = new System.Drawing.Point(332, 144);
            this.txtEscola.Name = "txtEscola";
            this.txtEscola.Padding = new System.Windows.Forms.Padding(7);
            this.txtEscola.PlaceholderColor = System.Drawing.Color.Gray;
            this.txtEscola.PlaceholderFont = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtEscola.PlaceholderMarginLeft = 12;
            this.txtEscola.PlaceholderText = "Digite aqui o nome...";
            this.txtEscola.SelectedText = "";
            this.txtEscola.SelectionLength = 0;
            this.txtEscola.SelectionStart = 0;
            this.txtEscola.Size = new System.Drawing.Size(617, 40);
            this.txtEscola.TabIndex = 1;
            this.txtEscola.TextColor = System.Drawing.Color.Black;
            this.txtEscola.UseSystemPasswordChar = false;
            // 
            // ConfiguracaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 905);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.Titulo);
            this.Controls.Add(this.txtBiblioteca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEscola);
            this.Controls.Add(this.lblNome);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfiguracaoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracao";
            this.Load += new System.EventHandler(this.ConfiguracaoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RoundedTextBox txtEscola;
        public System.Windows.Forms.Label lblNome;
        private RoundedTextBox txtBiblioteca;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.PictureBox picPreview;
        private CustomButton btnImportar;
        public System.Windows.Forms.Label label2;
        private CustomButton btnSalvar;
        private CustomButton btnCancelar;
    }
}