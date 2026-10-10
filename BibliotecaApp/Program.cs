using SUB.Forms.Inicio;
using SUB.Forms.Login;
using SUB.Forms.Setup;
using SUB.Models;
using SUB.Utils;
using System;
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
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!InstituicaoConfiguracaoStore.Existe())
            {
                using (var primeiroUso = new PrimeiroUsoForm())
                {
                    if (primeiroUso.ShowDialog() != DialogResult.OK)
                        return;
                }

                DatabaseInitializer.EnsureCreated();
            }

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

