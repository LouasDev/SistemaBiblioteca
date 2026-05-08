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
            // 1. Criamos a pergunta de confirmação
            DialogResult confirmacao = MessageBox.Show(
                "Deseja salvar as alterações de configuração?",
                "Confirmar Alterações",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 2. Só prosseguimos se o usuário clicar em "Sim"
            if (confirmacao == DialogResult.Yes)
            {
                var config = ConfigService.Carregar();
                config.NomeEscola = txtEscola.Text;
                config.NomeBiblioteca = txtBiblioteca.Text;

                if (!string.IsNullOrEmpty(caminhoTemporarioFoto))
                {
                    try
                    {
                        string extensao = Path.GetExtension(caminhoTemporarioFoto);
                        string nomeArquivo = "logo_instituicao" + extensao;
                        string destinoFinal = Path.Combine(ConfigService.ObterCaminhoMidia(), nomeArquivo);

                        File.Copy(caminhoTemporarioFoto, destinoFinal, true);
                        config.CaminhoLogo = destinoFinal;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao processar imagem: " + ex.Message);
                        return; // Interrompe o salvamento se a imagem der erro crítico
                    }
                }

                // 3. Salva e atualiza a interface
                ConfigService.Salvar(config);
                AtualizarTodosOsForms();

                MessageBox.Show("Configurações salvas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Opcional: Avisar que nada foi alterado
                // MessageBox.Show("Operação cancelada.");
            }
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

        private void AtualizarTodosOsForms()
        {
            // Percorre todos os formulários que o Windows abriu para o seu App
            foreach (Form f in Application.OpenForms)
            {
                // Se encontrar o Menu Principal, avisa para atualizar a foto
                if (f is MainForm menu)
                {
                    menu.AtualizarFotoMenu();
                }

                // Se encontrar o Login, avisa para recarregar tudo
                if (f is LoginForm login)
                {
                    login.RecarregarConfiguracoes();
                }
            }
        }

    }
}
