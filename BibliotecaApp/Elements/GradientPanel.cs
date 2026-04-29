using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BibliotecaApp.ElementosPersonalizados
{
    public class GradientPanel : Panel
    {
        // Propriedades para definir as cores do gradiente
        public Color Gradientetop { get; set; } = Color.RoyalBlue;
        public Color Gradientebottom { get; set; } = Color.DarkBlue;

        // Propriedade para definir o raio da borda arredondada
        private int borderRadius = 20;
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate(); // Redesenha o painel ao alterar o valor
            }
        }

        // Construtor do painel
        public GradientPanel()
        {
            this.Resize += GradientPanel_Resize;
            this.DoubleBuffered = true; // Evita flickering ao redimensionar
        }

        private void GradientPanel_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        // Método auxiliar para calcular o caminho arredondado
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            // Canto superior esquerdo
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            // Canto superior direito
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            // Canto inferior direito
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            // Canto inferior esquerdo
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Melhora a qualidade do desenho (suaviza as bordas)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            LinearGradientBrush linear = new LinearGradientBrush(
                this.ClientRectangle,
                this.Gradientetop,
                this.Gradientebottom,
                90F
            );

            // Se o raio for maior que 2, aplicamos o arredondamento
            if (borderRadius > 2)
            {
                Rectangle rectSurface = this.ClientRectangle;

                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                {
                    // Altera a região do controle para o formato arredondado
                    this.Region = new Region(pathSurface);

                    // Preenche o controle com o gradiente seguindo o GraphicsPath
                    e.Graphics.FillPath(linear, pathSurface);
                }
            }
            else
            {
                // Comportamento padrão (quadrado) se o raio for muito pequeno
                this.Region = new Region(this.ClientRectangle);
                e.Graphics.FillRectangle(linear, this.ClientRectangle);
            }

            // Liberar o pincel da memória
            linear.Dispose();
        }
    }
}