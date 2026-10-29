using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedComboBox : ComboBox
{
    private string _placeholder = "Selecione...";
    private Font _placeholderFont = new Font("Segoe UI", 12F, FontStyle.Italic);
    private int _borderRadius = 8;
    private int _borderThickness = 2;
    private Color _borderColor = Color.Black;
    private int _placeholderMargin = 10;
    private Font _itemsFont = new Font("Segoe UI", 10F);
    [Category("Aparência")]
    public string PlaceholderText
    {
        get => _placeholder;
        set
        {
            _placeholder = value;
            Invalidate();
        }
    }

    [Category("Aparência")]
    public Font PlaceholderFont
    {
        get => _placeholderFont;
        set
        {
            _placeholderFont = value;
            Invalidate();
        }
    }

    [Category("Aparência")]
    public int BorderRadius
    {
        get => _borderRadius;
        set
        {
            _borderRadius = value;
            Invalidate();
        }
    }

    [Category("Aparência")]
    public int BorderThickness
    {
        get => _borderThickness;
        set
        {
            _borderThickness = value;
            Invalidate();
        }
    }

    [Category("Aparência")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Invalidate();
        }
    }

    private Color _arrowColor = Color.Empty;
    // Vazio usa o botão nativo do Windows; com cor definida, desenha uma seta própria
    // sobre o fundo do controle (o botão nativo é sempre claro).
    [Category("Aparência")]
    public Color ArrowColor
    {
        get => _arrowColor;
        set
        {
            _arrowColor = value;
            Invalidate();
        }
    }

    private Color _placeholderColor = Color.Gray;
    [Category("Aparência")]
    public Color PlaceholderColor
    {
        get => _placeholderColor;
        set
        {
            _placeholderColor = value;
            Invalidate();
        }
    }

    // Vazio usa o realce do Windows (azul do sistema) no item escolhido da lista aberta.
    [Category("Aparência")]
    public Color HighlightColor { get; set; } = Color.Empty;

    [Category("Aparência")]
    public Color HighlightTextColor { get; set; } = Color.Empty;

    [Category("Aparência")]
    public int PlaceholderMargin
    {
        get => _placeholderMargin;
        set
        {
            _placeholderMargin = value;
            Invalidate();
        }
    }

    [Category("Aparência")]
    public Font ItemsFont
    {
        get => _itemsFont;
        set
        {
            _itemsFont = value;
            Invalidate();
        }
    }

    public RoundedComboBox()
    {
        this.DrawMode = DrawMode.OwnerDrawFixed;
        this.DropDownStyle = ComboBoxStyle.DropDownList;
        this.FlatStyle = FlatStyle.Flat;
        this.Font = new Font("Segoe UI", 10F);
        this.SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        // Os cantos mostram o container; BackColor preenche somente a curva.
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
        finally
        {
            e.Graphics.Restore(state);
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (Width < 2 || Height < 2)
            return;
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
        using (Pen pen = new Pen(BorderColor, BorderThickness))
        {
            using (var fill = new SolidBrush(BackColor))
                e.Graphics.FillPath(fill, path);
            e.Graphics.DrawPath(pen, path);
        }

        // Texto do item selecionado ou placeholder
        string textToDraw = this.SelectedIndex >= 0 ? this.GetItemText(this.SelectedItem) : PlaceholderText;
        Font textFont = this.SelectedIndex >= 0 ? this.ItemsFont : this.PlaceholderFont;
        Color textColor = this.SelectedIndex >= 0 ? this.ForeColor : PlaceholderColor;
        using (Brush textBrush = new SolidBrush(textColor))
        {
            Rectangle textRect = new Rectangle(PlaceholderMargin, 0, this.Width - PlaceholderMargin - 20, this.Height);
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(textToDraw, textFont, textBrush, textRect, format);
        }

        {
            float centroX = this.Width - 14f;
            float centroY = this.Height / 2f;
            using (Pen seta = new Pen(ArrowColor.IsEmpty ? ForeColor : ArrowColor, 1.6f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            }

            )
            {
                e.Graphics.DrawLine(seta, centroX - 4f, centroY - 2f, centroX, centroY + 2f);
                e.Graphics.DrawLine(seta, centroX, centroY + 2f, centroX + 4f, centroY - 2f);
            }
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = Math.Min(Math.Max(0, radius * 2), Math.Min(bounds.Width, bounds.Height));
        if (diameter == 0) { path.AddRectangle(bounds); return path; }
        path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }

    protected override void OnSelectedIndexChanged(EventArgs e)
    {
        base.OnSelectedIndexChanged(e);
        Invalidate();
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        bool realce = (e.State & DrawItemState.Selected) != 0 && !HighlightColor.IsEmpty;
        if (realce)
        {
            using (var fundo = new SolidBrush(HighlightColor))
                e.Graphics.FillRectangle(fundo, e.Bounds);
        }
        else
        {
            e.DrawBackground();
        }

        if (e.Index >= 0 && e.Index < Items.Count)
        {
            string itemText = this.GetItemText(Items[e.Index]);
            Color corTexto = realce && !HighlightTextColor.IsEmpty ? HighlightTextColor : ForeColor;
            using (Brush textBrush = new SolidBrush(corTexto))
            {
                e.Graphics.DrawString(itemText, ItemsFont, textBrush, e.Bounds);
            }
        }

        e.DrawFocusRectangle();
    }
}
