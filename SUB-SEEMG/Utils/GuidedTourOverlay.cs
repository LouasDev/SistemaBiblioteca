using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SUB.Utils
{
    public sealed class TutorialStep
    {
        public TutorialStep(string section, string title, string message, Func<Control> target, Action prepare)
        {
            Section = section;
            Title = title;
            Message = message;
            Target = target;
            Prepare = prepare;
        }

        public string Section { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public Func<Control> Target { get; private set; }
        public Action Prepare { get; private set; }
    }

    public sealed class GuidedTourOverlay : Panel
    {
        private enum TourScreen
        {
            Welcome,
            Step,
            Completion
        }

        private readonly Panel card;
        private readonly PictureBox picIcon;
        private readonly Label lblProgress;
        private readonly Label lblSection;
        private readonly Label lblTitle;
        private readonly Label lblMessage;
        private readonly Button btnBack;
        private readonly Button btnNext;
        private readonly Button btnSkip;
        private readonly Timer cardTransitionTimer;
        private IList<TutorialStep> steps;
        private Func<Bitmap> captureBackground;
        private Image brandImage;
        private int stepIndex;
        private TourScreen screen;
        private Color primaryColor = ThemeManager.DefaultColor;
        private Form owner;
        private bool ownerKeyPreview;
        private Bitmap backgroundSnapshot;
        private int cardTargetTop;
        public event EventHandler Completed;
        public event EventHandler Skipped;
        public GuidedTourOverlay()
        {
            Dock = DockStyle.None;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TabStop = true;
            DoubleBuffered = true;
            BackColor = Color.FromArgb(28, 39, 53);
            card = new Panel
            {
                Size = new Size(474, 246),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            picIcon = new PictureBox
            {
                Location = new Point(412, 23),
                Size = new Size(38, 38),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            lblSection = CreateLabel(new Point(24, 20), new Size(360, 17), 8.5F, FontStyle.Bold);
            lblTitle = CreateLabel(new Point(24, 39), new Size(370, 31), 15F, FontStyle.Bold);
            lblMessage = CreateLabel(new Point(24, 85), new Size(426, 57), 10F, FontStyle.Regular);
            lblMessage.TextAlign = ContentAlignment.TopLeft;
            lblProgress = CreateLabel(new Point(24, 150), new Size(250, 18), 8.5F, FontStyle.Regular);
            lblProgress.TextAlign = ContentAlignment.MiddleLeft;
            btnBack = CreateButton("Voltar", new Point(24, 188), new Size(92, 34));
            btnSkip = CreateButton("Pular tutorial", new Point(128, 188), new Size(132, 34));
            btnNext = CreateButton("Próximo", new Point(344, 188), new Size(106, 34));
            btnBack.Click += (sender, args) => ShowStep(stepIndex - 1);
            btnNext.Click += (sender, args) => Advance();
            btnSkip.Click += (sender, args) => Skipped?.Invoke(this, EventArgs.Empty);
            cardTransitionTimer = new Timer
            {
                Interval = 15
            };
            cardTransitionTimer.Tick += CardTransitionTimer_Tick;
            card.Controls.Add(picIcon);
            card.Controls.Add(lblSection);
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblMessage);
            card.Controls.Add(lblProgress);
            card.Controls.Add(btnBack);
            card.Controls.Add(btnSkip);
            card.Controls.Add(btnNext);
            card.Paint += Card_Paint;
            Controls.Add(card);
            Resize += (sender, args) =>
            {
                CaptureBackground();
                PositionCard();
            };
        }

        public void Start(IList<TutorialStep> tutorialSteps, Color themeColor, Func<Bitmap> backgroundProvider, Image logo)
        {
            if (tutorialSteps == null || tutorialSteps.Count == 0)
                throw new ArgumentException("O tutorial precisa ter pelo menos uma etapa.", nameof(tutorialSteps));
            steps = tutorialSteps;
            captureBackground = backgroundProvider;
            brandImage = logo;
            owner = FindForm();
            if (owner != null)
            {
                ownerKeyPreview = owner.KeyPreview;
                owner.KeyPreview = true;
                owner.KeyDown += Owner_KeyDown;
            }

            ApplyTheme(themeColor);
            ShowWelcome();
            BeginInvoke(new Action(() => btnNext.Focus()));
        }

        public void ApplyTheme(Color themeColor)
        {
            primaryColor = ThemeManager.Normalize(themeColor);
            // No modo escuro o cartão usa as superfícies do tema: os textos pensados para o
            // cartão branco ficariam sem contraste (o título já vem claro de GetLabelColor).
            bool escuro = ThemeManager.Escuro;
            card.BackColor = escuro ? ThemeManager.Dark.Superficie : Color.White;
            lblSection.ForeColor = escuro ? ThemeManager.Lighten(primaryColor, 0.45F) : primaryColor;
            lblProgress.ForeColor = escuro ? ThemeManager.Dark.TextoSuave : Color.FromArgb(92, 102, 114);
            lblTitle.ForeColor = ThemeManager.GetLabelColor(primaryColor);
            lblMessage.ForeColor = escuro ? ThemeManager.Dark.Texto : Color.FromArgb(48, 58, 70);
            StyleSecondaryButton(btnBack, primaryColor);
            StyleSecondaryButton(btnSkip, primaryColor);
            btnNext.BackColor = primaryColor;
            btnNext.ForeColor = ThemeManager.GetContrastColor(primaryColor);
            btnNext.FlatAppearance.BorderColor = ThemeManager.Darken(primaryColor, 0.12F);
            btnNext.FlatAppearance.MouseOverBackColor = ThemeManager.Darken(primaryColor, 0.08F);
            btnNext.FlatAppearance.MouseDownBackColor = ThemeManager.Darken(primaryColor, 0.16F);
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && owner != null)
            {
                owner.KeyDown -= Owner_KeyDown;
                owner.KeyPreview = ownerKeyPreview;
            }

            if (disposing && backgroundSnapshot != null)
                backgroundSnapshot.Dispose();
            if (disposing)
            {
                cardTransitionTimer.Stop();
                cardTransitionTimer.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (backgroundSnapshot != null)
                e.Graphics.DrawImage(backgroundSnapshot, ClientRectangle);
            else
                e.Graphics.Clear(BackColor);
            using (SolidBrush shade = new SolidBrush(Color.FromArgb(118, 10, 22, 36)))
                e.Graphics.FillRectangle(shade, ClientRectangle);
            DrawTarget(e.Graphics);
            DrawCardShadow(e.Graphics);
        }

        private void Advance()
        {
            if (screen == TourScreen.Welcome)
            {
                ShowStep(0);
                return;
            }

            if (screen == TourScreen.Completion)
            {
                Completed?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (stepIndex == steps.Count - 1)
                ShowCompletion();
            else
                ShowStep(stepIndex + 1);
        }

        private void ShowWelcome()
        {
            screen = TourScreen.Welcome;
            CaptureBackground();
            ConfigureCard("CONHEÇA O SUB", "Bem-vinda ao SUB", "Este é um passeio rápido pelos recursos essenciais da biblioteca. Nada será preenchido ou alterado durante o tour.", brandImage, "", false, true, "Começar tour", "Pular por agora");
        }

        private void ShowCompletion()
        {
            screen = TourScreen.Completion;
            CaptureBackground();
            ConfigureCard("TOUR CONCLUÍDO", "Obrigado por usar o SUB!", "Você já conhece os caminhos principais para organizar a operação da biblioteca. Quando quiser, o tutorial estará em Configuração.", brandImage, "", false, false, "Começar a usar o sistema", null);
        }

        private void ShowStep(int index)
        {
            if (index < 0 || steps == null || index >= steps.Count)
                return;
            stepIndex = index;
            screen = TourScreen.Step;
            TutorialStep step = steps[stepIndex];
            step.Prepare?.Invoke();
            CaptureBackground();
            Control target = GetTarget();
            string progress = string.Format("ETAPA {0} DE {1}", stepIndex + 1, steps.Count);
            ConfigureCard(step.Section, step.Title, step.Message, GetTargetImage(target) ?? brandImage, progress, stepIndex > 0, true, stepIndex == steps.Count - 1 ? "Concluir" : "Próximo", "Pular tutorial");
        }

        private void ConfigureCard(string section, string title, string message, Image image, string progress, bool allowBack, bool allowSkip, string nextText, string skipText)
        {
            lblSection.Text = section;
            lblTitle.Text = title;
            lblMessage.Text = message;
            lblProgress.Text = progress;
            picIcon.Image = image;
            btnBack.Visible = allowBack;
            btnBack.Enabled = allowBack;
            btnSkip.Visible = allowSkip;
            btnSkip.Text = skipText ?? string.Empty;
            btnNext.Text = nextText;
            btnNext.Width = screen == TourScreen.Completion ? 202 : 106;
            btnNext.Left = screen == TourScreen.Completion ? 248 : 344;
            PositionCard();
            card.Top += 8;
            card.Visible = true;
            card.BringToFront();
            cardTransitionTimer.Start();
            card.Invalidate();
            Invalidate();
        }

        private void CaptureBackground()
        {
            Bitmap next = null;
            try
            {
                next = captureBackground?.Invoke();
            }
            catch
            {
            }

            if (next == null && ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                next = new Bitmap(ClientSize.Width, ClientSize.Height);
                using (Graphics graphics = Graphics.FromImage(next))
                    graphics.Clear(BackColor);
            }

            Bitmap previous = backgroundSnapshot;
            backgroundSnapshot = next;
            if (previous != null)
                previous.Dispose();
        }

        private void PositionCard()
        {
            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;
            Rectangle target = GetTargetBounds(GetTarget());
            int x = target.IsEmpty ? (ClientSize.Width - card.Width) / 2 : target.Right + 26;
            int y = target.IsEmpty ? (ClientSize.Height - card.Height) / 2 : target.Top - 12;
            if (x + card.Width > ClientSize.Width - 18)
                x = Math.Max(18, target.IsEmpty ? (ClientSize.Width - card.Width) / 2 : target.Left - card.Width - 26);
            if (x < 18)
                x = Math.Max(18, (ClientSize.Width - card.Width) / 2);
            cardTargetTop = Math.Max(18, Math.Min(y, ClientSize.Height - card.Height - 18));
            card.Location = new Point(x, cardTargetTop);
        }

        private void DrawTarget(Graphics graphics)
        {
            if (screen != TourScreen.Step)
                return;
            Rectangle target = GetTargetBounds(GetTarget());
            if (target.IsEmpty)
                return;
            target.Inflate(5, 5);
            using (GraphicsPath path = RoundedRectangle(target, 8))
            using (Pen glow = new Pen(Color.FromArgb(140, Color.White), 7F))
            using (Pen border = new Pen(Color.White, 2.5F))
            {
                graphics.DrawPath(glow, path);
                graphics.DrawPath(border, path);
            }
        }

        private void DrawCardShadow(Graphics graphics)
        {
            Rectangle shadow = card.Bounds;
            shadow.Offset(0, 8);
            using (GraphicsPath shadowPath = RoundedRectangle(shadow, 15))
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(64, 0, 0, 0)))
                graphics.FillPath(shadowBrush, shadowPath);
        }

        private void CardTransitionTimer_Tick(object sender, EventArgs e)
        {
            if (card.Top <= cardTargetTop)
            {
                card.Top = cardTargetTop;
                cardTransitionTimer.Stop();
                return;
            }

            card.Top = Math.Max(cardTargetTop, card.Top - 2);
            Invalidate();
        }

        private void Card_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using (GraphicsPath path = RoundedRectangle(bounds, 15))
            using (Pen pen = new Pen(ThemeManager.Escuro ? ThemeManager.Dark.Borda : ThemeManager.Lighten(primaryColor, 0.58F), 1F))
                e.Graphics.DrawPath(pen, path);
            using (SolidBrush accent = new SolidBrush(primaryColor))
                e.Graphics.FillRectangle(accent, new Rectangle(0, 0, 6, card.Height));
            if (screen == TourScreen.Step && steps != null)
            {
                int startX = card.Width - 200;
                for (int i = 0; i < steps.Count; i++)
                {
                    Color pendente = ThemeManager.Escuro ? ThemeManager.Dark.Borda : ThemeManager.Lighten(primaryColor, 0.78F);
                    using (SolidBrush dot = new SolidBrush(i <= stepIndex ? primaryColor : pendente))
                        e.Graphics.FillEllipse(dot, startX + (i * 13), 154, 7, 7);
                }
            }
        }

        private void Owner_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Visible || steps == null)
                return;
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                if (screen == TourScreen.Completion)
                    Completed?.Invoke(this, EventArgs.Empty);
                else
                    Skipped?.Invoke(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;
                Advance();
            }
            else if (e.KeyCode == Keys.Left && screen == TourScreen.Step && stepIndex > 0)
            {
                e.SuppressKeyPress = true;
                ShowStep(stepIndex - 1);
            }
        }

        private Control GetTarget()
        {
            if (screen != TourScreen.Step || steps == null || stepIndex < 0 || stepIndex >= steps.Count)
                return null;
            try
            {
                return steps[stepIndex].Target?.Invoke();
            }
            catch
            {
                return null;
            }
        }

        private Rectangle GetTargetBounds(Control target)
        {
            if (target == null || target.IsDisposed || !target.Visible)
                return Rectangle.Empty;
            return RectangleToClient(target.RectangleToScreen(target.ClientRectangle));
        }

        private static Image GetTargetImage(Control target)
        {
            Button button = target as Button;
            return button?.Image;
        }

        private static Label CreateLabel(Point location, Size size, float fontSize, FontStyle style)
        {
            return new Label
            {
                AutoSize = false,
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", fontSize, style)
            };
        }

        private static Button CreateButton(string text, Point location, Size size)
        {
            return new Button
            {
                Text = text,
                Location = location,
                Size = size,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance =
                {
                    BorderSize = 1
                },
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                UseVisualStyleBackColor = false
            };
        }

        private static void StyleSecondaryButton(Button button, Color primaryColor)
        {
            if (ThemeManager.Escuro)
            {
                button.BackColor = ThemeManager.Dark.SuperficieAlt;
                button.ForeColor = ThemeManager.Dark.Texto;
                button.FlatAppearance.BorderColor = ThemeManager.Dark.Borda;
                button.FlatAppearance.MouseOverBackColor = ThemeManager.Dark.EntradaHover;
                button.FlatAppearance.MouseDownBackColor = ThemeManager.Dark.Selecao;
                return;
            }

            button.BackColor = ThemeManager.Lighten(primaryColor, 0.92F);
            button.ForeColor = ThemeManager.GetLabelColor(primaryColor);
            button.FlatAppearance.BorderColor = ThemeManager.Lighten(primaryColor, 0.58F);
            button.FlatAppearance.MouseOverBackColor = ThemeManager.Lighten(primaryColor, 0.84F);
            button.FlatAppearance.MouseDownBackColor = ThemeManager.Lighten(primaryColor, 0.76F);
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
