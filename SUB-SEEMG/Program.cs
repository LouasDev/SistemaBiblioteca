using SUB.Forms.Inicio;
using SUB.Forms.Login;
using SUB.Forms.Setup;
using SUB.Models;
using SUB.Utils;
using System;
using System.Threading;
using System.Windows.Forms;


namespace SUB
{
    internal static class Program
    {
        public static bool RequestLogout = false;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!ResetService.AguardarInstanciaAnterior(args))
                return;
            bool instanciaUnica;
            using (var mutexUnica = new Mutex(true, "Local\\SUB-InstanciaUnica", out instanciaUnica))
            {
                if (!instanciaUnica)
                {
                    MessageBox.Show("O SUB já está em execução.", "SUB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                try
                {
                    // A limpeza ocorre numa instância nova, depois de encerrar
                    // os Forms, tarefas e conexões da instalação anterior.
                    new ResetLocalStore(AppPaths.AppDataFolder).ConcluirSePendente();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível concluir o reset. O assistente de primeiro uso não será aberto.\n\n" +
                        ex.Message + "\n\nFeche arquivos de dados abertos e reinicie o SUB para retomar a operação autorizada.",
                        "Reset não concluído", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!InstituicaoConfiguracaoStore.Existe())
                {
                    using (var primeiroUso = new PrimeiroUsoForm())
                    {
                        if (primeiroUso.ShowDialog() != DialogResult.OK)
                            return;
                    }

                }

                try
                {
                    DatabaseInitializer.EnsureCreated();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Falha ao preparar o banco de dados.\n\n" + ex.Message +
                        "\n\nSe uma atualização recente for a causa, os arquivos da versão anterior e o backup" +
                        "\ndo banco ficaram em:\n" + AppPaths.BackupsFolder +
                        "\n\nConsulte o texto RESTAURACAO.txt na pasta de rollback correspondente.",
                        "SUB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AtualizacaoAplicador.ConcluirAtualizacaoPendente();

                while (true)
                {
                    Sessao.Limpar();
                    LoginForm.UsuarioBibliotecaria = false;
                    using (LoginForm login = new LoginForm())
                    {
                        if (login.ShowDialog() == DialogResult.OK)
                        {
                            Application.Run(new MainForm());
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}

