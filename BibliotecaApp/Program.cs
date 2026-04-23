using BibliotecaApp.Forms.Inicio;
using BibliotecaApp.Forms.Login;
using System;
using System.Windows.Forms;


namespace BibliotecaApp
{
    internal static class Program
    {
        public static bool RequestLogout = false;

        public static bool IsOfflineMode = false;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
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
