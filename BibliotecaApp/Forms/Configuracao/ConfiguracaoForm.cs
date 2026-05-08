using BibliotecaApp.Forms.Inicio;
using BibliotecaApp.Forms.Login;
using BibliotecaApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Forms.Configuracao
{
    public partial class ConfiguracaoForm : Form
    {
        public ConfiguracaoForm()
        {
            InitializeComponent();
        }

        private string caminhoTemporarioFoto = "";

        private void btnImportar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Imagens|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    caminhoTemporarioFoto = ofd.FileName;
                    picPreview.Image = Image.FromFile(ofd.FileName); // Preview na tela de config
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var config = new ConfiguracaoApp
            {
                NomeEscola = txtEscola.Text,
                NomeBiblioteca = txtBiblioteca.Text,
                CaminhoLogo = caminhoTemporarioFoto, // Salva o caminho
                UltimaAlteracao = DateTime.Now
            };

            ConfigService.Salvar(config);

            // ATUALIZAÇÃO EM TEMPO REAL (Sem fechar o programa)
            foreach (Form f in Application.OpenForms)
            {
                // Se o Form aberto tiver um método chamado "RecarregarConfiguracoes", ele executa
                //if (f is MainForm main) main.RecarregarConfiguracoes();
                if (f is LoginForm login) login.RecarregarConfiguracoes();
            }

            MessageBox.Show("Configurações aplicadas com sucesso!", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfiguracaoForm_Load(object sender, EventArgs e)
        {
            // Carrega o que já existe no arquivo
            var config = ConfigService.Carregar();

            // Preenche os campos com os dados salvos
            txtEscola.Text = config.NomeEscola;
            txtBiblioteca.Text = config.NomeBiblioteca;

            // Mostra a foto que já estava salva, se existir
            if (!string.IsNullOrEmpty(config.CaminhoLogo) && File.Exists(config.CaminhoLogo))
            {
                using (var stream = new FileStream(config.CaminhoLogo, FileMode.Open, FileAccess.Read))
                {
                    picPreview.Image = Image.FromStream(stream);
                }
            }
        }
    }
}
