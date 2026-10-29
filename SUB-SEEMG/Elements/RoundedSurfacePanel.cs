using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SUB.ElementosPersonalizados
{
    public class RoundedSurfacePanel : Panel
    {
        private int borderRadius = 20;

        public RoundedSurfacePanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        [Category("Aparência")]
        [DefaultValue(20)]
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = Math.Max(0, value); Invalidate(true); }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaintBackground(e);
                return;
            }
            GraphicsState state = e.Graphics.Save();
            try
            {
                e.Graphics.TranslateTransform(-Left, -Top);
                using (var parentPaint = new PaintEventArgs(e.Graphics, Bounds))
                {
                    InvokePaintBackground(Parent, parentPaint);
                    InvokePaint(Parent, parentPaint);
                }
            }
            finally { e.Graphics.Restore(state); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width > 0 && Height > 0)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                float radius = Math.Min(borderRadius, Math.Min(Width, Height) / 2f);
                using (var path = new GraphicsPath())
                using (var brush = new SolidBrush(BackColor))
                {
                    float diameter = radius * 2;
                    if (diameter <= 0)
                        path.AddRectangle(ClientRectangle);
                    else
                    {
                        path.AddArc(0, 0, diameter, diameter, 180, 90);
                        path.AddArc(Width - diameter, 0, diameter, diameter, 270, 90);
                        path.AddArc(Width - diameter, Height - diameter, diameter, diameter, 0, 90);
                        path.AddArc(0, Height - diameter, diameter, diameter, 90, 90);
                        path.CloseFigure();
                    }
                    e.Graphics.FillPath(brush, path);
                }
            }
            base.OnPaint(e);
        }
    }
}
