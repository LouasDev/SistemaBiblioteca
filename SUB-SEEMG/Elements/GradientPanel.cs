using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SUB.ElementosPersonalizados
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Com cantos arredondados, o que fica fora da curva mostra o container,
            // não o BackColor do painel.
            if (borderRadius > 2 && Parent != null)
                ButtonRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            else
                base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ClientRectangle.Width <= 0 || ClientRectangle.Height <= 0)
                return;

            using (LinearGradientBrush linear = new LinearGradientBrush(
                this.ClientRectangle,
                this.Gradientetop,
                this.Gradientebottom,
                90F))
            {
                if (borderRadius > 2)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    using (GraphicsPath pathSurface = GetFigurePath(this.ClientRectangle, borderRadius))
                    {
                        if (BackColor.A > 0)
                        {
                            using (var fundo = new SolidBrush(BackColor))
                                e.Graphics.FillPath(fundo, pathSurface);
                        }
                        e.Graphics.FillPath(linear, pathSurface);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(linear, this.ClientRectangle);
                }
            }
        }
    }
}
