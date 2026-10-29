using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SUB.Utils
{
    public static class FormHelper
    {
        // As importações de DLL devem ficar no topo da classe, nunca dentro de métodos
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamanho);
        private const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        private const int DWMWA_BORDER_COLOR = 34;
        private const int DWMWCP_ROUND = 2;
        // Formulários cujos cantos são arredondados pelo DWM (Windows 11).
        private static readonly ConditionalWeakTable<Form, object> cantosNativos = new ConditionalWeakTable<Form, object>();
        // Cor atual da borda de cada formulário; os handlers de DesenharBorda leem daqui
        // para que AlterarCorDaBorda possa trocá-la depois (ex.: tema escuro).
        private sealed class Borda
        {
            public Color Cor;
        }

        private static readonly ConditionalWeakTable<Form, Borda> bordas = new ConditionalWeakTable<Form, Borda>();
        // Método de Extensão para Arredondar
        public static void ArredondarCantos(this Form form, int raio)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            // O Windows 11 arredonda com antialiasing; a Region de GDI deixa os cantos serrilhados
            // e fica como alternativa para versões que não suportam o atributo.
            form.HandleCreated += (sender, e) =>
            {
                if (AplicarCantosNativos(form))
                    form.Region = null;
            };
            form.Resize += (sender, e) =>
            {
                if (!cantosNativos.TryGetValue(form, out _))
                    form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, raio, raio));
            };
            // Aplica o arredondamento inicial
            if (!(form.IsHandleCreated && AplicarCantosNativos(form)))
                form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, raio, raio));
        }

        private static bool AplicarCantosNativos(Form form)
        {
            try
            {
                int preferencia = DWMWCP_ROUND;
                bool aplicado = DwmSetWindowAttribute(form.Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preferencia, sizeof(int)) == 0;
                if (aplicado && !cantosNativos.TryGetValue(form, out _))
                    cantosNativos.Add(form, new object ());
                return aplicado;
            }
            catch (DllNotFoundException)
            {
                return false;
            }
            catch (EntryPointNotFoundException)
            {
                return false;
            }
        }

        // Método de Extensão para Mover
        public static void HabilitarMovimentacao(this Form form)
        {
            form.MouseDown += (sender, e) =>
            {
                ReleaseCapture();
                SendMessage(form.Handle, 0x112, 0xf012, 0);
            };
        }

        public static void DesenharBorda(this Form form, Color cor, int raio, int espessura)
        {
            var borda = bordas.GetValue(form, f => new Borda());
            borda.Cor = cor;
            form.HandleCreated += (s, e) => DefinirCorDaBordaNativa(form, borda.Cor);
            if (form.IsHandleCreated)
                DefinirCorDaBordaNativa(form, cor);
            form.Paint += (s, e) =>
            {
                // Com cantos nativos, o DWM desenha a borda acompanhando a curva.
                if (cantosNativos.TryGetValue(form, out _))
                    return;
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
                    using (Pen pen = new Pen(borda.Cor, espessura))
                    {
                        // Desenha o caminho completo (curvas + retas)
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
        }

        // Janela maior que a área de trabalho (ex.: 1200×800 numa tela de 1366×768) perde os botões
        // de baixo. Reduz para caber e centraliza; a ancoragem dos controles acompanha.
        public static void CaberNaTela(this Form form)
        {
            Rectangle area = Screen.FromControl(form.Owner ?? form).WorkingArea;
            if (form.Width <= area.Width && form.Height <= area.Height)
                return;
            int largura = Math.Min(form.Width, area.Width);
            int altura = Math.Min(form.Height, area.Height);
            form.StartPosition = FormStartPosition.Manual;
            form.Bounds = new Rectangle(area.Left + (area.Width - largura) / 2, area.Top + (area.Height - altura) / 2, largura, altura);
        }

        // Troca a cor de uma borda já registrada com DesenharBorda, sem registrar handlers novos.
        public static void AlterarCorDaBorda(this Form form, Color cor)
        {
            Borda borda;
            if (!bordas.TryGetValue(form, out borda))
                return;
            borda.Cor = cor;
            if (form.IsHandleCreated)
                DefinirCorDaBordaNativa(form, cor);
            form.Invalidate();
        }

        private static void DefinirCorDaBordaNativa(Form form, Color cor)
        {
            if (!cantosNativos.TryGetValue(form, out _))
                return;
            try
            {
                int colorRef = cor.R | (cor.G << 8) | (cor.B << 16);
                DwmSetWindowAttribute(form.Handle, DWMWA_BORDER_COLOR, ref colorRef, sizeof(int));
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }

        // "mailto:" depende de haver um cliente de e-mail configurado como padrão no Windows;
        // em muitas máquinas não há, e Process.Start tanto pode falhar quanto "ter sucesso"
        // abrindo um app (ex.: Mail do Windows) que fica preso numa tela de configuração de
        // conta — nos dois casos o clique pareceria não fazer nada. Copiar o endereço e avisar
        // funciona sempre, sem depender de nenhum programa externo.
        public static void AbrirSuporte(string email)
        {
            try
            {
                Clipboard.SetText(email);
                MessageBox.Show(
                    "O endereço de suporte foi copiado para a área de transferência:\n" + email,
                    "Suporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show(
                    "Entre em contato com o suporte pelo endereço:\n" + email,
                    "Suporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
