using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

[ToolboxItem(true)]
public class CustomButton : Button
{
    private int _borderRadius = 8;
    private Color _baseColor = Color.FromArgb(1, 105, 111);
    private bool _isHovered = false;
    private bool _isPressed = false;

    // ──────────────────────────────────────────
    // ÚNICA PROPRIEDADE VISÍVEL NO DESIGNER
    // ──────────────────────────────────────────

    [Category("Aparência Custom")]
    [Description("Raio do arredondamento dos cantos. 0 = quadrado.")]
    [DefaultValue(8)]
    public int BorderRadius
    {
        get => _borderRadius;
        set { _borderRadius = Math.Max(0, value); ApplyRegion(); Invalidate(); }
    }

    [Category("Aparência Custom")]
    [Description("Cor principal do botão.")]
    public Color BaseColor
    {
        get => _baseColor;
        set { _baseColor = value; Invalidate(); }
    }

    // ──────────────────────────────────────────
    // ESCONDE TUDO QUE NÃO DEVE APARECER
    // ──────────────────────────────────────────

    [Browsable(false)] public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }
    [Browsable(false)] public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }
    [Browsable(false)] public new FlatButtonAppearance FlatAppearance => base.FlatAppearance;
    [Browsable(false)] public new FlatStyle FlatStyle { get => base.FlatStyle; set => base.FlatStyle = value; }

    // Esconde propriedades que aparecem na imagem
    [Browsable(false)] public Color BackgroundColor { get; set; }
    [Browsable(false)] public int BorderThickness { get; set; }
    [Browsable(false)] public Color BorderColor { get; set; }
    [Browsable(false)] public Color HoverColor { get; set; }
    [Browsable(false)] public Color PressedColor { get; set; }
    [Browsable(false)] public bool ShowBorder { get; set; }

    // ──────────────────────────────────────────
    // CONSTRUTOR
    // ──────────────────────────────────────────

    public CustomButton()
    {
        base.FlatStyle = FlatStyle.Flat;
        base.FlatAppearance.BorderSize = 0;
        base.FlatAppearance.MouseOverBackColor = Color.Transparent;
        base.FlatAppearance.MouseDownBackColor = Color.Transparent;
        base.ForeColor = Color.White;
        Font = new Font("Segoe UI", 10f, FontStyle.Regular);
        Size = new Size(140, 42);
        Cursor = Cursors.Hand;

        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true
        );
    }

    // ──────────────────────────────────────────
    // REGION
    // ──────────────────────────────────────────

    private void ApplyRegion()
    {
        this.Region?.Dispose();
        this.Region = null;

        RectangleF rf = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);
        using (GraphicsPath path = CreateRoundedPath(rf, _borderRadius))
            this.Region = new Region(path);
    }

    protected override void OnResize(EventArgs e) { base.OnResize(e); ApplyRegion(); }

    // ──────────────────────────────────────────
    // ESTADOS
    // ──────────────────────────────────────────

    protected override void OnMouseEnter(EventArgs e) { _isHovered = true; Invalidate(); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { _isHovered = false; _isPressed = false; Invalidate(); base.OnMouseLeave(e); }
    protected override void OnMouseDown(MouseEventArgs e) { _isPressed = true; Invalidate(); base.OnMouseDown(e); }
    protected override void OnMouseUp(MouseEventArgs e) { _isPressed = false; Invalidate(); base.OnMouseUp(e); }

    // ──────────────────────────────────────────
    // DESENHO
    // ──────────────────────────────────────────

    protected override void OnPaint(PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;

        RectangleF rect = new RectangleF(0.5f, 0.5f, Width - 1f, Height - 1f);

        Color bgColor = _isPressed ? Darken(_baseColor, 0.20f)
                      : _isHovered ? Darken(_baseColor, 0.10f)
                      : _baseColor;

        using (GraphicsPath path = CreateRoundedPath(rect, _borderRadius))
        using (SolidBrush brush = new SolidBrush(bgColor))
            g.FillPath(brush, path);

        TextRenderer.DrawText(
            g, Text, Font,
            new Rectangle(0, 0, Width, Height),
            base.ForeColor,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        );
    }

    // ──────────────────────────────────────────
    // HELPERS
    // ──────────────────────────────────────────

    private Color Darken(Color color, float amount)
    {
        return Color.FromArgb(
            color.A,
            (int)Math.Max(0, color.R - 255 * amount),
            (int)Math.Max(0, color.G - 255 * amount),
            (int)Math.Max(0, color.B - 255 * amount)
        );
    }

    private GraphicsPath CreateRoundedPath(RectangleF rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();

        if (radius <= 0)
        {
            path.AddRectangle(rect);
            path.CloseFigure();
            return path;
        }

        float r = Math.Min(radius, Math.Min(rect.Width, rect.Height) / 2f);
        float d = r * 2f;

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

        path.CloseFigure();
        return path;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { this.Region?.Dispose(); this.Region = null; }
        base.Dispose(disposing);
    }
}