using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

// IMPORTANTE: Mude 'SeuProjeto' para o nome exato do seu projeto (o mesmo que aparece no Form1.cs)
namespace SeuProjeto
{
    public static class FormHelper
    {
        // As importações de DLL devem ficar no topo da classe, nunca dentro de métodos
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Método de Extensão para Arredondar
        public static void ArredondarCantos(this Form form, int raio)
        {
            form.FormBorderStyle = FormBorderStyle.None;

            // Criamos um tratador de evento manual para o Resize
            form.Resize += (sender, e) => {
                form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, raio, raio));
            };

            // Aplica o arredondamento inicial
            form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, raio, raio));
        }

        // Método de Extensão para Mover
        public static void HabilitarMovimentacao(this Form form)
        {
            form.MouseDown += (sender, e) => {
                ReleaseCapture();
                SendMessage(form.Handle, 0x112, 0xf012, 0);
            };
        }
        public static void DesenharBorda(this Form form, Color cor, int raio, int espessura)
        {
            form.Paint += (s, e) => {
                // Ativa a suavização (Anti-aliasing)
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    // O segredo está aqui: tiramos 1 pixel da largura/altura (-1) 
                    // para a borda não ser cortada pela "Region" do Form.
                    float recuo = espessura / 2f;
                    float width = form.Width - 1;
                    float height = form.Height - 1;

                    path.AddArc(0, 0, raio, raio, 180, 90); // Topo Esquerda
                    path.AddArc(width - raio, 0, raio, raio, 270, 90); // Topo Direita
                    path.AddArc(width - raio, height - raio, raio, raio, 0, 90); // Baixo Direita
                    path.AddArc(0, height - raio, raio, raio, 90, 90); // Baixo Esquerda
                    path.CloseAllFigures();

                    using (Pen pen = new Pen(cor, espessura))
                    {
                        // Desenha o caminho completo (curvas + retas)
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
        }
    }
}