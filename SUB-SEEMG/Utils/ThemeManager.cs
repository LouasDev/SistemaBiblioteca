using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        private static readonly ConditionalWeakTable<Control, object> ControlsComBarraDeTitulo =
            new ConditionalWeakTable<Control, object>();
        private static readonly ConditionalWeakTable<Control, object> ControlsComRolagemEscura =
            new ConditionalWeakTable<Control, object>();
        private static readonly ConditionalWeakTable<Control, object> ControlsComProgressoEscuro =
            new ConditionalWeakTable<Control, object>();
        private static readonly ConditionalWeakTable<Control, object> ControlsComTextoAcompanhado =
            new ConditionalWeakTable<Control, object>();
        private static bool adaptandoCorDoTexto;

        // Cor do último Apply; lida pelos handlers de ControlAdded/Grid para não
        // congelar a cor capturada por closure quando o tema muda em tempo de execução.
        private static Color corTemaAtual;

        // Modo de aparência da instalação. Definido a partir de instituicao.json;
        // quem muda o modo recria as telas abertas, pois as cores claras originais
        // não são guardadas para voltar atrás.
        public static bool Escuro { get; private set; }

        public static void DefinirModo(bool escuro)
        {
            Escuro = escuro;
        }

        public static class Dark
        {
            public static readonly Color Fundo = Color.FromArgb(21, 24, 30);
            public static readonly Color Superficie = Color.FromArgb(29, 33, 41);
            public static readonly Color SuperficieAlt = Color.FromArgb(36, 41, 51);
            public static readonly Color Entrada = Color.FromArgb(40, 45, 56);
            public static readonly Color EntradaHover = Color.FromArgb(47, 53, 66);
            public static readonly Color Borda = Color.FromArgb(62, 69, 84);
            public static readonly Color Selecao = Color.FromArgb(54, 62, 78);
            public static readonly Color Texto = Color.FromArgb(232, 235, 241);
            public static readonly Color TextoSuave = Color.FromArgb(157, 166, 180);
            public static readonly Color TextoDesabilitado = Color.FromArgb(98, 106, 120);
            public static readonly Color Desabilitado = Color.FromArgb(46, 51, 62);
            public static readonly Color BordaDesabilitado = Color.FromArgb(60, 66, 78);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamanho);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string subApp, string subIdList);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWA_CAPTION_COLOR = 35;
        private const int DWMWA_TEXT_COLOR = 36;

        public static double Luminancia(Color cor)
        {
            return (0.299 * cor.R) + (0.587 * cor.G) + (0.114 * cor.B);
        }

        // Distância entre o maior e o menor canal: perto de zero é cinza (ou azul-acinzentado),
        // alto é cor de verdade. Diferente da saturação HSL, não explode em tons muito escuros.
        public static int Croma(Color cor)
        {
            return Math.Max(cor.R, Math.Max(cor.G, cor.B)) - Math.Min(cor.R, Math.Min(cor.G, cor.B));
        }

        // Texto escuro de um formulário claro vira texto claro; cores com matiz (vermelho de erro,
        // verde de sucesso) mantêm o matiz e apenas clareiam para contrastar com o fundo escuro.
        // Cinza claro, usado nas telas para indicar campo desabilitado, continua apagado.
        // Aplicar de novo sobre o resultado não muda nada (as telas podem ser retematizadas).
        public static Color CorTextoParaEscuro(Color cor)
        {
            if (cor.IsEmpty)
                return Dark.Texto;
            if (cor.A == 0)
                return cor;
            if (cor.ToArgb() == Dark.TextoSuave.ToArgb() || cor.ToArgb() == Dark.TextoDesabilitado.ToArgb())
                return cor;

            double lum = Luminancia(cor);
            int croma = Croma(cor);
            if (lum >= 225)
                return cor;
            if (lum >= 150)
                return croma < 16 ? Dark.TextoDesabilitado : cor;
            if (croma < 64)
                return lum < 80 ? Dark.Texto : Dark.TextoSuave;

            return Lighten(cor, 0.45f);
        }

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
            Form form = root as Form;
            if (form != null)
                SubLogoManager.ApplyWindowIcon(form, corTemaAtual);
            ApplyRecursive(root, corTemaAtual, false);

            var formulario = root as Form;
            if (formulario != null)
                AplicarBarraDeTitulo(formulario);
        }

        public static void ApplyConfiguredTheme(Control root)
        {
            Color primaryColor = DefaultColor;

            try
            {
                var configuracao = InstituicaoConfiguracaoStore.Carregar();
                if (configuracao != null)
                {
                    Escuro = configuracao.TemaEscuro;
                    if (!string.IsNullOrWhiteSpace(configuracao.CorPrimaria))
                        primaryColor = ColorTranslator.FromHtml(configuracao.CorPrimaria);
                }
            }
            catch
            {
                primaryColor = DefaultColor;
            }

            Apply(root, primaryColor);
        }

        // Para telas com desenho próprio (login, sobre, diálogos montados em código) que não
        // passam pelo Apply: só converte as cores claras do trecho indicado para o modo escuro.
        public static void AplicarModoEscuro(Control raiz, Color primaryColor)
        {
            if (!Escuro || raiz == null)
                return;

            AdaptarParaEscuro(raiz, Normalize(primaryColor));
            foreach (Control filho in raiz.Controls)
                AplicarModoEscuro(filho, primaryColor);
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
            {
                // A seta nativa do Windows é um bloco cinza que destoa dos campos arredondados.
                comboBox.BorderColor = primaryColor;
                comboBox.ArrowColor = Color.FromArgb(96, 104, 116);
                comboBox.HighlightColor = Lighten(primaryColor, 0.84f);
                comboBox.HighlightTextColor = Darken(primaryColor, 0.55f);
            }

            // A barra nativa é sempre verde; no tema ela é plana, na cor da instituição.
            var barraDeProgresso = control as ProgressBar;
            if (barraDeProgresso != null)
            {
                AplicarProgressoPlano(barraDeProgresso);
                if (barraDeProgresso.Style == ProgressBarStyle.Blocks)
                    barraDeProgresso.Style = ProgressBarStyle.Continuous;
                barraDeProgresso.BackColor = Color.FromArgb(226, 231, 238);
                barraDeProgresso.ForeColor = primaryColor;
            }

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

            // No escuro, rótulo que já passou pela conversão tem a cor resolvida (inclusive o cinza
            // de desabilitado e o texto escondido); um novo Apply só repete a conversão.
            var label = control as Label;
            if (label != null && !IsSemanticLabel(label) && !(Escuro && ControlsComTextoAcompanhado.TryGetValue(label, out _)))
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
                    ArredondarBotao(button);
                }
            }

            if (Escuro && !isNavigation)
                AdaptarParaEscuro(control, primaryColor);

            foreach (Control child in control.Controls)
                ApplyRecursive(child, primaryColor, isNavigation);
        }

        private static void AdaptarParaEscuro(Control control, Color primaryColor)
        {
            var formulario = control as Form;
            if (formulario != null)
            {
                formulario.BackColor = Dark.Fundo;
                formulario.ForeColor = Dark.Texto;
                AplicarBarraDeTitulo(formulario);
                return;
            }

            var campo = control as RoundedTextBox;
            if (campo != null)
            {
                campo.BackColor = Dark.Entrada;
                campo.TextColor = Dark.Texto;
                campo.PlaceholderColor = Dark.TextoSuave;
                campo.BorderColor = Dark.Borda;
                campo.BorderFocusColor = Lighten(primaryColor, 0.35f);
                campo.HoverBackColor = Dark.EntradaHover;
                campo.Invalidate();
                return;
            }

            var campoMascara = control as RoundedMaskedTextBox;
            if (campoMascara != null)
            {
                campoMascara.BackColor = Dark.Entrada;
                campoMascara.ForeColor = Dark.Texto;
                campoMascara.MaskTextColor = Dark.Texto;
                campoMascara.BorderColor = Dark.Borda;
                campoMascara.BorderFocusColor = Lighten(primaryColor, 0.35f);
                campoMascara.HoverBorderColor = Lighten(primaryColor, 0.35f);
                campoMascara.HoverBackColor = Dark.EntradaHover;
                campoMascara.Invalidate();
                return;
            }

            var data = control as RoundedDatePicker;
            if (data != null)
            {
                data.BackColor = Dark.Entrada;
                data.ForeColor = Dark.Texto;
                data.HoverBackColor = Dark.EntradaHover;
                data.PlaceholderColor = Dark.TextoSuave;
                data.BorderColor = Dark.Borda;
                data.BorderFocusColor = Lighten(primaryColor, 0.35f);
                data.IconColor = Dark.TextoSuave;
                data.IconHoverColor = Lighten(primaryColor, 0.5f);
                data.IconHoverAreaColor = Dark.Selecao;
                data.SeparatorColor = Dark.Borda;
                data.Invalidate();
                return;
            }

            var combo = control as RoundedComboBox;
            if (combo != null)
            {
                combo.BackColor = Dark.Entrada;
                combo.ForeColor = Dark.Texto;
                combo.BorderColor = Dark.Borda;
                combo.ArrowColor = Dark.TextoSuave;
                combo.PlaceholderColor = Dark.TextoSuave;
                combo.HighlightColor = Dark.Selecao;
                combo.HighlightTextColor = Dark.Texto;
                combo.Invalidate();
                return;
            }

            var rotulo = control as Label;
            if (rotulo != null)
            {
                AdaptarRotuloParaEscuro(rotulo);
                return;
            }

            var painel = control as Panel;
            if (painel != null)
            {
                AdaptarPainelParaEscuro(painel);
                return;
            }

            if (control is TabPage)
            {
                control.BackColor = Dark.Superficie;
                return;
            }

            if (control is GroupBox || control is CheckBox || control is RadioButton)
            {
                // Fundo branco fixado no designer apareceria como uma caixa sobre o tema escuro.
                if (control.BackColor.A == 255 && Luminancia(control.BackColor) > 150)
                    control.BackColor = Color.Transparent;
                control.ForeColor = CorDoTextoNoEscuro(control, control.ForeColor);
                AcompanharCorDoTexto(control);
                if (!(control is GroupBox))
                    AplicarRolagemEscura(control);
                return;
            }

            var barra = control as ProgressBar;
            if (barra != null)
            {
                AplicarProgressoPlano(barra);
                if (barra.Style == ProgressBarStyle.Blocks)
                    barra.Style = ProgressBarStyle.Continuous;
                barra.BackColor = Dark.Entrada;
                barra.ForeColor = Lighten(primaryColor, 0.25f);
                return;
            }

            if (control is TextBoxBase || control is ComboBox || control is ListBox || control is NumericUpDown)
            {
                control.BackColor = Dark.Entrada;
                control.ForeColor = Dark.Texto;
                var caixaDeTexto = control as TextBoxBase;
                if (control is ListBox || (caixaDeTexto != null && caixaDeTexto.Multiline))
                    AplicarRolagemEscura(control);
            }
        }

        private static void AdaptarRotuloParaEscuro(Label rotulo)
        {
            Color fundo = rotulo.BackColor;
            if (EhPilula(fundo))
                return;

            // Fundo claro explícito (branco do designer) deixa de aparecer como uma caixa sobre o tema escuro.
            if (Luminancia(fundo) > 150 && rotulo.Parent != null)
                rotulo.BackColor = Color.Transparent;

            rotulo.ForeColor = CorDoTextoNoEscuro(rotulo, rotulo.ForeColor);
            AcompanharCorDoTexto(rotulo);

            var link = rotulo as LinkLabel;
            if (link != null)
            {
                link.LinkColor = CorTextoParaEscuro(link.LinkColor);
                link.ActiveLinkColor = CorTextoParaEscuro(link.ActiveLinkColor);
                link.VisitedLinkColor = CorTextoParaEscuro(link.VisitedLinkColor);
            }
        }

        // Etiqueta de situação: fundo pastel com texto escuro da mesma cor, legível em qualquer tema.
        private static bool EhPilula(Color fundo)
        {
            return fundo.A == 255 && Luminancia(fundo) > 190 && Croma(fundo) >= 10;
        }

        // Telas que trocam a cor do texto em tempo de execução (rótulo "desabilitado" em cinza claro,
        // destaque em azul-marinho) usam cores pensadas para o fundo claro; no escuro elas são
        // convertidas no momento da troca.
        private static void AcompanharCorDoTexto(Control controle)
        {
            object marcador;
            if (ControlsComTextoAcompanhado.TryGetValue(controle, out marcador))
                return;
            ControlsComTextoAcompanhado.Add(controle, new object());

            controle.ForeColorChanged += (sender, args) =>
            {
                if (!Escuro || adaptandoCorDoTexto || EhPilula(controle.BackColor))
                    return;

                adaptandoCorDoTexto = true;
                try
                {
                    controle.ForeColor = CorDoTextoNoEscuro(controle, controle.ForeColor);
                }
                finally
                {
                    adaptandoCorDoTexto = false;
                }
            };
        }

        // O rótulo desenha o texto na cor GDI mais próxima, e "transparente" vira branco: no tema
        // claro ele some sobre o fundo branco (as telas usam isso para esconder asteriscos), no
        // escuro apareceria. Continua transparente (alfa zero, então converter de novo dá o mesmo
        // resultado), mas com o RGB do fundo visível atrás do controle.
        private static Color CorDoTextoNoEscuro(Control controle, Color cor)
        {
            if (cor.IsEmpty || cor.A != 0)
                return CorTextoParaEscuro(cor);

            for (Control atual = controle.Parent; atual != null; atual = atual.Parent)
            {
                if (atual.BackColor.A == 255)
                    return Color.FromArgb(0, atual.BackColor);
            }
            return Color.FromArgb(0, Dark.Fundo);
        }

        private static void AdaptarPainelParaEscuro(Panel painel)
        {
            if (painel.AutoScroll)
                AplicarRolagemEscura(painel);

            string nome = painel.Name ?? string.Empty;
            var gradiente = painel as GradientPanel;
            if (gradiente != null)
            {
                if (IsLoginGradient(painel))
                {
                    gradiente.BackColor = Dark.Fundo;
                    gradiente.Gradientetop = Dark.Fundo;
                    gradiente.Gradientebottom = Dark.Fundo;
                }
                else if (PreserveDesignerGradient(painel))
                {
                    gradiente.BackColor = Dark.Superficie;
                }
                gradiente.Invalidate();
                return;
            }

            if (painel.BackColor.A == 0)
                return;

            // Só superfícies claras neutras escurecem; cabeçalhos coloridos (laranja, verde dos
            // cartões) continuam com a cor de destaque.
            if (nome.StartsWith("pnlResultados", StringComparison.OrdinalIgnoreCase))
                painel.BackColor = Dark.Borda;
            else if (nome.StartsWith("pnlFiltros", StringComparison.OrdinalIgnoreCase))
                painel.BackColor = Dark.Superficie;
            else if (Luminancia(painel.BackColor) > 150 && Croma(painel.BackColor) < 48)
                painel.BackColor = Dark.Fundo;
        }

        // Barra de título nativa dos diálogos na cor da própria janela, em vez da cor de destaque do
        // Windows (amarelo, verde...), que destoava do tema. As cores são lidas na hora de aplicar,
        // então valem para o modo atual. Sem efeito antes do Windows 11 (exceto o modo escuro).
        public static void AplicarBarraDeTitulo(Form formulario)
        {
            if (!formulario.TopLevel)
                return;

            // A borda é conferida na hora: o assistente de mapeamento nasce sem borda e vira
            // diálogo depois de criado.
            Action aplicar = delegate
            {
                if (formulario.FormBorderStyle == FormBorderStyle.None)
                    return;
                try
                {
                    int escuro = Escuro ? 1 : 0;
                    int fundo = ParaColorRef(Escuro ? Dark.Fundo : formulario.BackColor);
                    int texto = ParaColorRef(Escuro ? Dark.Texto : Color.FromArgb(32, 37, 46));
                    int borda = ParaColorRef(Escuro ? Dark.Borda : Color.FromArgb(206, 212, 220));
                    DwmSetWindowAttribute(formulario.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref escuro, sizeof(int));
                    DwmSetWindowAttribute(formulario.Handle, DWMWA_CAPTION_COLOR, ref fundo, sizeof(int));
                    DwmSetWindowAttribute(formulario.Handle, DWMWA_TEXT_COLOR, ref texto, sizeof(int));
                    DwmSetWindowAttribute(formulario.Handle, DWMWA_BORDER_COLOR, ref borda, sizeof(int));
                }
                catch (DllNotFoundException) { }
                catch (EntryPointNotFoundException) { }
            };

            if (formulario.IsHandleCreated)
                aplicar();

            object marcador;
            if (ControlsComBarraDeTitulo.TryGetValue(formulario, out marcador))
                return;
            ControlsComBarraDeTitulo.Add(formulario, new object());
            formulario.HandleCreated += (s, e) => aplicar();
        }

        private static int ParaColorRef(Color cor)
        {
            return cor.R | (cor.G << 8) | (cor.B << 16);
        }

        // Barras de rolagem e as caixas de seleção/opção do Windows seguem o tema escuro com este nome de tema.
        private static void AplicarRolagemEscura(Control controle)
        {
            object marcador;
            if (ControlsComRolagemEscura.TryGetValue(controle, out marcador))
                return;
            ControlsComRolagemEscura.Add(controle, new object());

            // Ao fechar a tela, a grade ainda recebe VisibleChanged depois de descartar as próprias barras.
            Action aplicar = delegate
            {
                if (controle.IsDisposed || controle.Disposing)
                    return;
                try
                {
                    SetWindowTheme(controle.Handle, "DarkMode_Explorer", null);
                    foreach (Control filho in controle.Controls)
                    {
                        if (filho is ScrollBar && !filho.IsDisposed && !filho.Disposing)
                            SetWindowTheme(filho.Handle, "DarkMode_Explorer", null);
                    }
                }
                catch (DllNotFoundException) { }
                catch (EntryPointNotFoundException) { }
            };

            if (controle.IsHandleCreated)
                aplicar();
            controle.HandleCreated += (s, e) => aplicar();
            controle.VisibleChanged += (s, e) => { if (controle.IsHandleCreated) aplicar(); };
        }

        // A barra do tema do Windows é verde, com trilho sempre claro; sem o estilo visual a barra
        // nativa passa a usar BackColor/ForeColor. Uma alça nova (troca de Style) precisa do ajuste de novo.
        private static void AplicarProgressoPlano(ProgressBar barra)
        {
            object marcador;
            if (ControlsComProgressoEscuro.TryGetValue(barra, out marcador))
                return;
            ControlsComProgressoEscuro.Add(barra, new object());

            Action aplicar = delegate
            {
                try
                {
                    SetWindowTheme(barra.Handle, string.Empty, string.Empty);
                }
                catch (DllNotFoundException) { }
                catch (EntryPointNotFoundException) { }
            };

            if (barra.IsHandleCreated)
                aplicar();
            barra.HandleCreated += (s, e) => aplicar();
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
                : (isSecondary ? (Escuro ? Dark.SuperficieAlt : Lighten(primaryColor, 0.86f)) : primaryColor);

            button.UseVisualStyleBackColor = false;
            button.BackColor = baseColor;
            button.ForeColor = isSecondary ? GetLabelColor(primaryColor) : GetContrastColor(baseColor);
            button.FlatAppearance.BorderColor = isSecondary
                ? (Escuro ? Dark.Borda : Lighten(primaryColor, 0.62f))
                : Darken(baseColor, 0.12f);
            button.FlatAppearance.MouseOverBackColor = isSecondary
                ? (Escuro ? Dark.EntradaHover : Lighten(primaryColor, 0.76f))
                : Darken(baseColor, 0.1f);
            button.FlatAppearance.MouseDownBackColor = isSecondary
                ? (Escuro ? Dark.Selecao : Lighten(primaryColor, 0.68f))
                : Darken(baseColor, 0.2f);
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

            Color fundoDesabilitado = Escuro ? Dark.Desabilitado : Color.FromArgb(226, 229, 234);
            if (!button.Enabled)
            {
                if (!cores.Guardadas)
                {
                    cores.Fundo = button.BackColor;
                    cores.Borda = button.FlatAppearance.BorderColor;
                    cores.Guardadas = true;
                }
                button.BackColor = fundoDesabilitado;
                button.FlatAppearance.BorderColor = Escuro ? Dark.BordaDesabilitado : Color.FromArgb(210, 214, 220);
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

        private sealed class EstadoDoBotao
        {
            public bool Sobre;
            public bool Pressionado;
        }

        private static readonly ConditionalWeakTable<Button, EstadoDoBotao> BotoesArredondados =
            new ConditionalWeakTable<Button, EstadoDoBotao>();

        // Botão plano do Windows é um retângulo reto; campos, cartões e botões das grades já são
        // arredondados. Redesenha por cima com cantos suaves, mantendo as cores do FlatAppearance
        // (passar o mouse, clicar, desabilitado). Botões com imagem ficam com o desenho original.
        private static void ArredondarBotao(Button button)
        {
            EstadoDoBotao estado;
            if (BotoesArredondados.TryGetValue(button, out estado))
                return;
            estado = new EstadoDoBotao();
            BotoesArredondados.Add(button, estado);

            button.MouseEnter += (s, e) => { estado.Sobre = true; button.Invalidate(); };
            button.MouseLeave += (s, e) => { estado.Sobre = false; estado.Pressionado = false; button.Invalidate(); };
            button.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) { estado.Pressionado = true; button.Invalidate(); } };
            button.MouseUp += (s, e) => { estado.Pressionado = false; button.Invalidate(); };
            button.Paint += (s, e) => DesenharBotaoArredondado(button, estado, e);
        }

        private static void DesenharBotaoArredondado(Button button, EstadoDoBotao estado, PaintEventArgs e)
        {
            if (button.FlatStyle != FlatStyle.Flat || button.Image != null || button.BackgroundImage != null ||
                button.Width < 8 || button.Height < 8)
                return;

            Color fundo = button.BackColor;
            if (button.Enabled && estado.Pressionado && !button.FlatAppearance.MouseDownBackColor.IsEmpty)
                fundo = button.FlatAppearance.MouseDownBackColor;
            else if (button.Enabled && estado.Sobre && !button.FlatAppearance.MouseOverBackColor.IsEmpty)
                fundo = button.FlatAppearance.MouseOverBackColor;

            Color borda = button.FlatAppearance.BorderSize > 0 && !button.FlatAppearance.BorderColor.IsEmpty
                ? button.FlatAppearance.BorderColor
                : fundo;
            Color texto = button.Enabled
                ? button.ForeColor
                : (Escuro ? Color.FromArgb(126, 134, 148) : Color.FromArgb(140, 147, 158));

            ButtonRenderer.DrawParentBackground(e.Graphics, button.ClientRectangle, button);
            int raio = Math.Min(8, button.Height / 3);
            var area = new Rectangle(0, 0, button.Width - 1, button.Height - 1);
            var suavizacao = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var caminho = CaminhoArredondado(area, raio))
            using (var pincel = new SolidBrush(fundo))
            using (var caneta = new Pen(borda))
            {
                e.Graphics.FillPath(pincel, caminho);
                e.Graphics.DrawPath(caneta, caminho);
            }
            e.Graphics.SmoothingMode = suavizacao;

            var alinhamento = TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            if (button.TextAlign == ContentAlignment.MiddleLeft || button.TextAlign == ContentAlignment.TopLeft || button.TextAlign == ContentAlignment.BottomLeft)
                alinhamento |= TextFormatFlags.Left;
            else if (button.TextAlign == ContentAlignment.MiddleRight || button.TextAlign == ContentAlignment.TopRight || button.TextAlign == ContentAlignment.BottomRight)
                alinhamento |= TextFormatFlags.Right;
            else
                alinhamento |= TextFormatFlags.HorizontalCenter;
            var areaTexto = Rectangle.Inflate(button.ClientRectangle, -button.Padding.Horizontal / 2 - 2, -2);
            TextRenderer.DrawText(e.Graphics, button.Text, button.Font, areaTexto, texto, alinhamento);
        }

        private static GraphicsPath CaminhoArredondado(Rectangle area, int raio)
        {
            int diametro = raio * 2;
            var caminho = new GraphicsPath();
            caminho.AddArc(area.X, area.Y, diametro, diametro, 180, 90);
            caminho.AddArc(area.Right - diametro, area.Y, diametro, diametro, 270, 90);
            caminho.AddArc(area.Right - diametro, area.Bottom - diametro, diametro, diametro, 0, 90);
            caminho.AddArc(area.X, area.Bottom - diametro, diametro, diametro, 90, 90);
            caminho.CloseFigure();
            return caminho;
        }

        private static void ApplyCompactUtilityButton(Button button, Color primaryColor)
        {
            button.UseVisualStyleBackColor = false;
            if (Escuro)
            {
                // O botão padrão do Windows mantém a moldura clara mesmo com fundo escuro.
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 1;
                button.BackColor = Dark.SuperficieAlt;
                button.ForeColor = Dark.Texto;
                button.FlatAppearance.BorderColor = Dark.Borda;
                button.FlatAppearance.MouseOverBackColor = Dark.EntradaHover;
                button.FlatAppearance.MouseDownBackColor = Dark.Selecao;
                return;
            }

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
                   name.IndexOf("pdf", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   name.IndexOf("tutorial", StringComparison.OrdinalIgnoreCase) >= 0;
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
            return Escuro ? Dark.Texto : Darken(primaryColor, 0.55f);
        }

        // Rótulo de campo ainda não liberado (ex.: antes de escolher o tipo de usuário). O cinza claro
        // usado antes mal se lia; este continua apagado. No claro é cinza puro de propósito: o Apply
        // trata cinzas como cor de situação e não os troca pela cor dos rótulos.
        public static Color CorRotuloDesabilitado
        {
            get { return Escuro ? Dark.TextoDesabilitado : Color.FromArgb(156, 156, 156); }
        }

        // Listas de sugestão (autocompletar) desenhadas pelas próprias telas.
        public static Color FundoDaSugestao(bool emDestaque)
        {
            if (Escuro)
                return emDestaque ? Dark.EntradaHover : Dark.Entrada;
            return emDestaque ? Color.FromArgb(235, 235, 235) : Color.White;
        }

        public static Color TextoDaSugestao
        {
            get { return Escuro ? Dark.Texto : Color.FromArgb(60, 60, 60); }
        }

        public static Color DivisoriaDaSugestao
        {
            get { return Escuro ? Dark.Borda : Color.FromArgb(220, 220, 220); }
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
            Color surface = Escuro ? Dark.Superficie : Color.FromArgb(250, 251, 253);
            Color alternateSurface = Escuro ? Dark.SuperficieAlt : Lighten(primaryColor, 0.91f);
            Color selected = Escuro ? Dark.Selecao : Lighten(primaryColor, 0.88f);
            Color texto = Escuro ? Dark.Texto : Color.FromArgb(35, 42, 50);
            Color selectedText = texto;
            Color contrast = GetContrastColor(primaryColor);

            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = surface;
            grid.GridColor = Escuro ? Dark.Borda : Lighten(primaryColor, 0.58f);
            grid.DefaultCellStyle.BackColor = surface;
            grid.DefaultCellStyle.ForeColor = texto;
            grid.DefaultCellStyle.SelectionBackColor = selected;
            grid.DefaultCellStyle.SelectionForeColor = selectedText;
            grid.AlternatingRowsDefaultCellStyle.BackColor = alternateSurface;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = texto;
            grid.ColumnHeadersDefaultCellStyle.BackColor = primaryColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = contrast;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = primaryColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = contrast;
            grid.RowHeadersDefaultCellStyle.BackColor = Escuro ? Dark.Superficie : Lighten(primaryColor, 0.82f);
            grid.RowHeadersDefaultCellStyle.ForeColor = texto;
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
                    column.DefaultCellStyle.ForeColor = texto;
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

            // Na formatação o ajuste chega também ao controle de edição; na pintura ele vem depois
            // dos handlers de formatação que as telas registram depois do tema.
            grid.CellFormatting += (sender, args) =>
            {
                if (Escuro && args.CellStyle != null)
                    AdaptarCelulaParaEscuro(args.CellStyle, args.RowIndex);
            };
            grid.CellPainting += (sender, args) =>
            {
                if (!Escuro || args.RowIndex < 0 || args.CellStyle == null)
                    return;
                AdaptarCelulaParaEscuro(args.CellStyle, args.RowIndex);
                PintarCelulaDeListaEscura(grid, args);
            };

            // Telas que estilizam a grade depois do tema devolvem fundo e linhas brancas.
            grid.BackgroundColorChanged += (sender, args) =>
            {
                if (Escuro && Luminancia(grid.BackgroundColor) > 150)
                    grid.BackgroundColor = Dark.Superficie;
            };
            grid.GridColorChanged += (sender, args) =>
            {
                if (Escuro && Luminancia(grid.GridColor) > 150)
                    grid.GridColor = Dark.Borda;
            };
            if (Escuro)
                AplicarRolagemEscura(grid);
        }

        // Ajuste feito na hora de pintar, então vale também para cores que as telas fixam por linha
        // ou no estilo padrão depois do tema (branco, cinza de indisponível, seleção azul-clara).
        // Cores de situação (vermelho de atraso, verde de ativo) clareiam; células com fundo pastel
        // próprio mantêm o par original.
        private static void AdaptarCelulaParaEscuro(DataGridViewCellStyle estilo, int linha)
        {
            Color fundo = estilo.BackColor;
            if (!fundo.IsEmpty && Luminancia(fundo) > 170)
            {
                if (Croma(fundo) >= 24)
                    return;
                if (Luminancia(fundo) >= 235)
                    estilo.BackColor = linha % 2 == 1 ? Dark.SuperficieAlt : Dark.Superficie;
                else
                    estilo.BackColor = Dark.Desabilitado;
            }

            Color selecao = estilo.SelectionBackColor;
            if (!selecao.IsEmpty && Luminancia(selecao) > 170 && Croma(selecao) < 24)
                estilo.SelectionBackColor = Dark.Selecao;

            Color frente = estilo.ForeColor;
            if (!frente.IsEmpty && Luminancia(frente) < 150)
                estilo.ForeColor = CorTextoParaEscuro(frente);
            Color frenteSelecionada = estilo.SelectionForeColor;
            if (!frenteSelecionada.IsEmpty && Luminancia(frenteSelecionada) < 150)
                estilo.SelectionForeColor = CorTextoParaEscuro(frenteSelecionada);
        }

        // O botão de lista das células de combo é pintado sempre claro pelo Windows; a célula é
        // pintada sem ele e recebe uma seta no lugar.
        private static void PintarCelulaDeListaEscura(DataGridView grid, DataGridViewCellPaintingEventArgs args)
        {
            if (args.ColumnIndex < 0)
                return;
            var coluna = grid.Columns[args.ColumnIndex] as DataGridViewComboBoxColumn;
            if (coluna == null || coluna.DisplayStyle == DataGridViewComboBoxDisplayStyle.Nothing)
                return;

            args.Paint(args.CellBounds, args.PaintParts & ~DataGridViewPaintParts.ContentBackground);

            float centroX = args.CellBounds.Right - 14f;
            float centroY = args.CellBounds.Top + args.CellBounds.Height / 2f;
            var suavizacao = args.Graphics.SmoothingMode;
            args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var seta = new Pen(Dark.TextoSuave, 1.6f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                args.Graphics.DrawLine(seta, centroX - 4f, centroY - 2f, centroX, centroY + 2f);
                args.Graphics.DrawLine(seta, centroX, centroY + 2f, centroX + 4f, centroY - 2f);
            }
            args.Graphics.SmoothingMode = suavizacao;
            args.Handled = true;
        }

    }
}

