using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Windows.Forms;
using SUB.ElementosPersonalizados;

namespace SUB.Utils
{
    public sealed class ThemePreset
    {
        public string Name { get; private set; }
        public Color PrimaryColor { get; private set; }
        public Color SecondaryColor { get; private set; }
        public Color AccentColor { get; private set; }

        public ThemePreset(string name, Color primaryColor, Color secondaryColor, Color accentColor)
        {
            Name = name;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            AccentColor = accentColor;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class ThemeManager
    {
        private static readonly ConditionalWeakTable<Control, object> ControlsWithThemeHandler =
            new ConditionalWeakTable<Control, object>();
        private static readonly ConditionalWeakTable<DataGridView, object> GridsWithThemeHandler =
            new ConditionalWeakTable<DataGridView, object>();
        private static readonly ConditionalWeakTable<Button, CoresDoBotao> CoresAntesDeDesabilitar =
            new ConditionalWeakTable<Button, CoresDoBotao>();

        private sealed class CoresDoBotao
        {
            public Color Fundo;
            public Color Borda;
            public bool Guardadas;
        }
        // Cor do último Apply; lida pelos handlers de ControlAdded/Grid para não
        // congelar a cor capturada por closure quando o tema muda em tempo de execução.
        private static Color corTemaAtual;

        public static Color DefaultColor { get { return Presets[0].PrimaryColor; } }

        public static IList<ThemePreset> Presets { get; } = new List<ThemePreset>
        {
            new ThemePreset("Azul (padrão)", Color.FromArgb(31, 72, 126), Color.FromArgb(16, 87, 174), Color.FromArgb(238, 244, 250)),
            new ThemePreset("Verde", Color.FromArgb(35, 105, 82), Color.FromArgb(25, 125, 91), Color.FromArgb(237, 246, 241)),
            new ThemePreset("Vinho", Color.FromArgb(126, 48, 67), Color.FromArgb(153, 58, 78), Color.FromArgb(249, 239, 241)),
            new ThemePreset("Grafite", Color.FromArgb(59, 67, 78), Color.FromArgb(78, 91, 108), Color.FromArgb(240, 243, 246))
        };

        public static ThemePreset FindPreset(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                foreach (var preset in Presets)
                {
                    if (string.Equals(ColorTranslator.ToHtml(preset.PrimaryColor), value.Trim(), StringComparison.OrdinalIgnoreCase))
                        return preset;
                }
            }

            return Presets[0];
        }

        public static Color Normalize(Color color)
        {
            return color.IsEmpty ? DefaultColor : color;
        }

        public static Color Darken(Color color, float amount)
        {
            return Color.FromArgb(
                color.A,
                Math.Max(0, (int)(color.R * (1f - amount))),
                Math.Max(0, (int)(color.G * (1f - amount))),
                Math.Max(0, (int)(color.B * (1f - amount))));
        }

        public static Color Lighten(Color color, float amount)
        {
            return Color.FromArgb(
                color.A,
                Math.Min(255, (int)(color.R + (255 - color.R) * amount)),
                Math.Min(255, (int)(color.G + (255 - color.G) * amount)),
                Math.Min(255, (int)(color.B + (255 - color.B) * amount)));
        }

        public static Color GetContrastColor(Color color)
        {
            double luminance = (0.299 * color.R) + (0.587 * color.G) + (0.114 * color.B);
            return luminance >= 160 ? Color.FromArgb(20, 42, 60) : Color.White;
        }

        public static void Apply(Control root, Color primaryColor)
        {
            if (root == null)
                return;

            corTemaAtual = Normalize(primaryColor);
            ApplyRecursive(root, corTemaAtual, false);
        }

        public static void ApplyConfiguredTheme(Control root)
        {
            Color primaryColor = DefaultColor;

            try
            {
                var configuracao = InstituicaoConfiguracaoStore.Carregar();
                if (configuracao != null && !string.IsNullOrWhiteSpace(configuracao.CorPrimaria))
                    primaryColor = ColorTranslator.FromHtml(configuracao.CorPrimaria);
            }
            catch
            {
                primaryColor = DefaultColor;
            }

            Apply(root, primaryColor);
        }

        private static void ApplyRecursive(Control control, Color primaryColor, bool insideNavigation)
        {
            bool isNavigation = insideNavigation || IsNavigationContainer(control) || IsThemedHeaderPanel(control);

            if (!ControlsWithThemeHandler.TryGetValue(control, out _))
            {
                ControlsWithThemeHandler.Add(control, new object());
                control.ControlAdded += (sender, args) => ApplyRecursive(args.Control, corTemaAtual, isNavigation);
            }

            var gradientPanel = control as GradientPanel;
            if (gradientPanel != null && !PreserveDesignerGradient(control))
            {
                if (IsLoginGradient(control))
                {
                    gradientPanel.BackColor = Color.WhiteSmoke;
                    gradientPanel.Gradientetop = Color.WhiteSmoke;
                    gradientPanel.Gradientebottom = Color.WhiteSmoke;
                }
                else
                {
                    gradientPanel.Gradientetop = primaryColor;
                    gradientPanel.Gradientebottom = Darken(primaryColor, 0.12f);
                }
                gradientPanel.Invalidate();
            }

            var customButton = control as CustomButton;
            if (customButton != null)
            {
                customButton.BaseColor = isNavigation ? Darken(primaryColor, 0.08f) : primaryColor;
                customButton.BackgroundColor = customButton.BaseColor;
                customButton.HoverColor = Darken(customButton.BaseColor, 0.1f);
                customButton.PressedColor = Darken(customButton.BaseColor, 0.2f);
                customButton.BorderColor = Darken(customButton.BaseColor, 0.12f);
                customButton.ForeColor = GetContrastColor(customButton.BaseColor);
            }

            var textBox = control as RoundedTextBox;
            if (textBox != null)
            {
                textBox.BorderColor = Lighten(primaryColor, 0.25f);
                textBox.BorderFocusColor = primaryColor;
                textBox.HoverBackColor = Color.FromArgb(245, 247, 250);
            }
            var comboBox = control as RoundedComboBox;
            if (comboBox != null)
                comboBox.BorderColor = primaryColor;

            var maskedTextBox = control as RoundedMaskedTextBox;
            if (maskedTextBox != null)
            {
                maskedTextBox.BorderColor = Lighten(primaryColor, 0.25f);
                maskedTextBox.BorderFocusColor = primaryColor;
                maskedTextBox.HoverBorderColor = primaryColor;
                maskedTextBox.HoverBackColor = Color.FromArgb(245, 247, 250);
            }

            var datePicker = control as RoundedDatePicker;
            if (datePicker != null)
            {
                datePicker.BorderFocusColor = primaryColor;
                datePicker.IconHoverColor = primaryColor;
                datePicker.Invalidate();
            }

            var label = control as Label;
            if (label != null && !IsSemanticLabel(label))
                label.ForeColor = isNavigation ? GetContrastColor(primaryColor) : GetLabelColor(primaryColor);

            var grid = control as DataGridView;
            if (grid != null)
            {
                ApplyGridTheme(grid, primaryColor);
                ConfigureGridThemeHandlers(grid, primaryColor);
            }

            var panel = control as Panel;
            if (panel != null && (IsThemeLinePanel(control) || IsThemedHeaderPanel(control)))
                panel.BackColor = primaryColor;
            else if (panel != null && IsFilterSurfacePanel(control))
                panel.BackColor = Color.White;
            else if (panel != null && IsResultSurfacePanel(control))
                panel.BackColor = Lighten(primaryColor, 0.84f);
            else if (panel != null && IsNavigationContainer(control))
                panel.BackColor = primaryColor;
            else if (panel != null && isNavigation && panel.BackColor != Color.Transparent)
                panel.BackColor = Darken(primaryColor, 0.08f);

            var button = control as Button;
            if (button != null && !(control is CustomButton))
            {
                if (isNavigation)
                {
                    ApplyNavigationButton(button, primaryColor);
                }
                else
                {
                    ApplyActionButton(button, primaryColor);
                    ConfigurarEstadoDesabilitado(button);
                }
            }

            foreach (Control child in control.Controls)
                ApplyRecursive(child, primaryColor, isNavigation);
        }

        private static bool IsNavigationContainer(Control control)
        {
            string name = control.Name ?? string.Empty;
            return name.Equals("menu", StringComparison.OrdinalIgnoreCase) ||
                   name.IndexOf("navigation", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("navContainer", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool PreserveDesignerGradient(Control control)
        {
            return string.Equals(control.Name, "pnlAcoesDev", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsThemeLinePanel(Control control)
        {
            string name = control.Name ?? string.Empty;
            return name.StartsWith("sublinha", StringComparison.OrdinalIgnoreCase) ||
                   name.StartsWith("pnlLinha", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsThemedHeaderPanel(Control control)
        {
            string name = control.Name ?? string.Empty;
            return name.Equals("panelHeader", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("pnlHeader", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsFilterSurfacePanel(Control control)
        {
            return (control.Name ?? string.Empty).StartsWith("pnlFiltros", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsResultSurfacePanel(Control control)
        {
            return (control.Name ?? string.Empty).StartsWith("pnlResultados", StringComparison.OrdinalIgnoreCase);
        }

        private static void ApplyNavigationButton(Button button, Color primaryColor)
        {
            button.BackColor = Darken(primaryColor, 0.08f);
            button.ForeColor = GetContrastColor(button.BackColor);
            if (button.FlatStyle != FlatStyle.System)
            {
                button.FlatAppearance.BorderColor = Darken(button.BackColor, 0.12f);
                button.FlatAppearance.MouseOverBackColor = Darken(button.BackColor, 0.1f);
                button.FlatAppearance.MouseDownBackColor = Darken(button.BackColor, 0.2f);
            }
        }

        private static void ApplyActionButton(Button button, Color primaryColor)
        {
            if (IsCompactUtilityButton(button))
            {
                ApplyCompactUtilityButton(button, primaryColor);
                return;
            }

            bool isDestructive = IsDestructiveActionButton(button);
            bool isSecondary = !isDestructive && IsSecondaryActionButton(button);
            Color baseColor = isDestructive
                ? Color.FromArgb(178, 34, 34)
                : (isSecondary ? Lighten(primaryColor, 0.86f) : primaryColor);

            button.UseVisualStyleBackColor = false;
            button.BackColor = baseColor;
            button.ForeColor = isSecondary ? GetLabelColor(primaryColor) : GetContrastColor(baseColor);
            button.FlatAppearance.BorderColor = isSecondary ? Lighten(primaryColor, 0.62f) : Darken(baseColor, 0.12f);
            button.FlatAppearance.MouseOverBackColor = isSecondary ? Lighten(primaryColor, 0.76f) : Darken(baseColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = isSecondary ? Lighten(primaryColor, 0.68f) : Darken(baseColor, 0.2f);
        }

        // Botão plano desabilitado recebe texto escurecido pelo Windows; sobre a cor do tema
        // ficava ilegível. Usa uma superfície neutra e devolve as cores ao reabilitar,
        // preservando estilos que o formulário aplicou por conta própria.
        private static void ConfigurarEstadoDesabilitado(Button button)
        {
            CoresDoBotao cores;
            if (!CoresAntesDeDesabilitar.TryGetValue(button, out cores))
            {
                cores = new CoresDoBotao();
                CoresAntesDeDesabilitar.Add(button, cores);
                button.EnabledChanged += (sender, args) => AtualizarEstadoDesabilitado(button, cores);
            }

            cores.Guardadas = false;
            AtualizarEstadoDesabilitado(button, cores);
        }

        private static void AtualizarEstadoDesabilitado(Button button, CoresDoBotao cores)
        {
            if (button.FlatStyle != FlatStyle.Flat && button.FlatStyle != FlatStyle.Popup)
                return;

            Color fundoDesabilitado = Color.FromArgb(226, 229, 234);
            if (!button.Enabled)
            {
                if (!cores.Guardadas)
                {
                    cores.Fundo = button.BackColor;
                    cores.Borda = button.FlatAppearance.BorderColor;
                    cores.Guardadas = true;
                }
                button.BackColor = fundoDesabilitado;
                button.FlatAppearance.BorderColor = Color.FromArgb(210, 214, 220);
            }
            else if (cores.Guardadas)
            {
                // Se o formulário trocou a cor enquanto estava desabilitado, a dele prevalece.
                if (button.BackColor.ToArgb() == fundoDesabilitado.ToArgb())
                {
                    button.BackColor = cores.Fundo;
                    button.FlatAppearance.BorderColor = cores.Borda;
                }
                cores.Guardadas = false;
            }
        }

        private static void ApplyCompactUtilityButton(Button button, Color primaryColor)
        {
            button.UseVisualStyleBackColor = false;
            button.BackColor = Color.White;
            button.ForeColor = primaryColor;
            button.FlatAppearance.BorderColor = Lighten(primaryColor, 0.55f);
            button.FlatAppearance.MouseOverBackColor = Lighten(primaryColor, 0.90f);
            button.FlatAppearance.MouseDownBackColor = Lighten(primaryColor, 0.80f);
        }

        private static bool IsCompactUtilityButton(Button button)
        {
            string name = button.Name ?? string.Empty;
            return name.Equals("btnMais", StringComparison.OrdinalIgnoreCase) ||
                   name.Equals("btnMenos", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsSecondaryActionButton(Button button)
        {
            string name = button.Name ?? string.Empty;
            return name.IndexOf("cancel", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("voltar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("anterior", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("limpar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("sair", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("fechar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("export", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("prorrogar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("pdf", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsDestructiveActionButton(Button button)
        {
            string name = button.Name ?? string.Empty;
            return name.IndexOf("excluir", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("remover", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("apagar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("deletar", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsLoginGradient(Control control)
        {
            return control.Name.Equals("gradientPanel2", StringComparison.OrdinalIgnoreCase) &&
                   control.FindForm() != null &&
                   control.FindForm().GetType().Name.Equals("LoginForm", StringComparison.Ordinal);
        }

        private static bool IsSemanticColor(Color color)
        {
            float hue = color.GetHue();
            return hue <= 15 || hue >= 345 || (hue >= 25 && hue <= 165);
        }

        public static Color GetLabelColor(Color primaryColor)
        {
            return Darken(primaryColor, 0.55f);
        }

        private static bool IsSemanticLabel(Label label)
        {
            string name = label.Name ?? string.Empty;
            string text = label.Text ?? string.Empty;
            Color color = label.ForeColor;

            return IsSemanticColor(color) ||
                   name.IndexOf("erro", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("aviso", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("sucesso", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   text.IndexOf("erro", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static void ApplyGridTheme(DataGridView grid, Color primaryColor)
        {
            Color surface = Color.FromArgb(250, 251, 253);
            Color alternateSurface = Lighten(primaryColor, 0.91f);
            Color selected = Lighten(primaryColor, 0.88f);
            Color selectedText = Color.FromArgb(35, 42, 50);
            Color contrast = GetContrastColor(primaryColor);

            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = surface;
            grid.GridColor = Lighten(primaryColor, 0.58f);
            grid.DefaultCellStyle.BackColor = surface;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(35, 42, 50);
            grid.DefaultCellStyle.SelectionBackColor = selected;
            grid.DefaultCellStyle.SelectionForeColor = selectedText;
            grid.AlternatingRowsDefaultCellStyle.BackColor = alternateSurface;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(35, 42, 50);
            grid.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = contrast;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = contrast;
            grid.RowHeadersDefaultCellStyle.BackColor = Lighten(primaryColor, 0.82f);
            grid.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 42, 50);
            grid.RowHeadersDefaultCellStyle.SelectionBackColor = selected;
            grid.RowHeadersDefaultCellStyle.SelectionForeColor = selectedText;

            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.HeaderCell.Style.BackColor = primaryColor;
                column.HeaderCell.Style.ForeColor = contrast;
                column.HeaderCell.Style.SelectionBackColor = primaryColor;
                column.HeaderCell.Style.SelectionForeColor = contrast;

                if (!column.DefaultCellStyle.BackColor.IsEmpty)
                    column.DefaultCellStyle.BackColor = surface;
                if (!column.DefaultCellStyle.ForeColor.IsEmpty)
                    column.DefaultCellStyle.ForeColor = Color.FromArgb(35, 42, 50);
                if (!column.DefaultCellStyle.SelectionBackColor.IsEmpty)
                    column.DefaultCellStyle.SelectionBackColor = selected;
                if (!column.DefaultCellStyle.SelectionForeColor.IsEmpty)
                    column.DefaultCellStyle.SelectionForeColor = selectedText;
            }
        }

        private static void ConfigureGridThemeHandlers(DataGridView grid, Color primaryColor)
        {
            if (GridsWithThemeHandler.TryGetValue(grid, out _))
                return;

            GridsWithThemeHandler.Add(grid, new object());
            grid.ColumnAdded += (sender, args) => ApplyGridTheme(grid, corTemaAtual);
            grid.DataBindingComplete += (sender, args) => ApplyGridTheme(grid, corTemaAtual);
        }

    }
}

