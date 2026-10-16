namespace SUB.Forms.Inicio
{
    partial class InicioForm
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InicioForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.topPanelInside = new System.Windows.Forms.Panel();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRelogio = new System.Windows.Forms.Label();
            this.lblOla = new System.Windows.Forms.Label();
            this.timerRelogio = new System.Windows.Forms.Timer(this.components);
            this.panelTop = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.topPanelInside.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.topPanelInside);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(18);
            this.panel1.Size = new System.Drawing.Size(1040, 574);
            this.panel1.TabIndex = 3;
            // 
            // topPanelInside
            // 
            this.topPanelInside.BackColor = System.Drawing.Color.White;
            this.topPanelInside.Controls.Add(this.flowCards);
            this.topPanelInside.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanelInside.Location = new System.Drawing.Point(18, 18);
            this.topPanelInside.Name = "topPanelInside";
            this.topPanelInside.Size = new System.Drawing.Size(1004, 160);
            this.topPanelInside.TabIndex = 0;
            // 
            // flowCards
            // 
            this.flowCards.AutoScroll = true;
            this.flowCards.BackColor = System.Drawing.Color.White;
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCards.Location = new System.Drawing.Point(0, 0);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(12);
            this.flowCards.Size = new System.Drawing.Size(1004, 160);
            this.flowCards.TabIndex = 0;
            this.flowCards.WrapContents = false;
            // 
            // lblRelogio
            // 
            this.lblRelogio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRelogio.AutoSize = true;
            this.lblRelogio.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelogio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.lblRelogio.Location = new System.Drawing.Point(333, 28);
            this.lblRelogio.Name = "lblRelogio";
            this.lblRelogio.Size = new System.Drawing.Size(481, 32);
            this.lblRelogio.TabIndex = 2;
            this.lblRelogio.Text = "Quarta, 01 de Janeiro de 2025 - 00:00:00";
            // 
            // lblOla
            // 
            this.lblOla.AutoSize = true;
            this.lblOla.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblOla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(61)))), ((int)(((byte)(88)))));
            this.lblOla.Location = new System.Drawing.Point(19, 26);
            this.lblOla.Name = "lblOla";
            this.lblOla.Size = new System.Drawing.Size(249, 37);
            this.lblOla.TabIndex = 1;
            this.lblOla.Text = "Olá, NomeUsuário!";
            // 
            // timerRelogio
            // 
            this.timerRelogio.Interval = 1000;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.lblOla);
            this.panelTop.Controls.Add(this.lblRelogio);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelTop.Size = new System.Drawing.Size(1040, 86);
            this.panelTop.TabIndex = 4;
            // 
            // InicioForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1040, 660);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InicioForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Início";
            this.Load += new System.EventHandler(this.InicioForm_Load);
            this.panel1.ResumeLayout(false);
            this.topPanelInside.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblRelogio;
        private System.Windows.Forms.Label lblOla;
        private System.Windows.Forms.Timer timerRelogio;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel topPanelInside;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
    }
}
