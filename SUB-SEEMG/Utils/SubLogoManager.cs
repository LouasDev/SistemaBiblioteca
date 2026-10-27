using SUB.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SUB.Utils
{
    public enum SubLogoVariant
    {
        Full,
        Square
    }

    public static class SubLogoManager
    {
        private static readonly object SyncRoot = new object();
        private static readonly Dictionary<string, Bitmap> Cache = new Dictionary<string, Bitmap>(StringComparer.OrdinalIgnoreCase);

        public static Bitmap Get(SubLogoVariant variant, Color primaryColor, Color backgroundColor)
        {
            return Get(variant, primaryColor, backgroundColor, Size.Empty);
        }

        public static Bitmap Get(SubLogoVariant variant, Color primaryColor, Color backgroundColor, Size renderSize)
        {
            primaryColor = ThemeManager.Normalize(primaryColor);
            backgroundColor = backgroundColor.IsEmpty ? Color.White : backgroundColor;
            bool redimensionar = renderSize.Width > 0 && renderSize.Height > 0;
            string tamanho = redimensionar ? string.Format(":{0}x{1}", renderSize.Width, renderSize.Height) : string.Empty;
            string key = string.Format("{0}:{1}:{2}{3}", variant, ColorTranslator.ToHtml(primaryColor), ColorTranslator.ToHtml(backgroundColor), tamanho);

            lock (SyncRoot)
            {
                Bitmap cached;
                if (Cache.TryGetValue(key, out cached))
                    return cached;

                Bitmap themed;
                using (Bitmap original = GetOriginal(variant))
                {
                    if (redimensionar)
                    {
                        using (Bitmap reduzido = Redimensionar(original, renderSize))
                            themed = CreateVariant(reduzido, primaryColor, backgroundColor);
                    }
                    else
                    {
                        themed = CreateVariant(original, primaryColor, backgroundColor);
                    }
                }

                Cache[key] = themed;
                return themed;
            }
        }

        public static void ClearCache()
        {
            lock (SyncRoot)
            {
                foreach (Bitmap image in Cache.Values)
                    image.Dispose();
                Cache.Clear();
            }
        }

        private static Bitmap GetOriginal(SubLogoVariant variant)
        {
            Bitmap source = variant == SubLogoVariant.Square ? Resources.sub_200x200px : Resources.sub_full;
            return new Bitmap(source);
        }

        private static readonly Color AzulOriginal = Color.FromArgb(31, 72, 126);

        private static Bitmap CreateVariant(Bitmap source, Color primaryColor, Color backgroundColor)
        {
            Color logoColor = GetLogoColor(primaryColor, backgroundColor);
            Bitmap result = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    // As bordas suavizadas são mesclas de azul e branco: mantém a proporção na nova paleta.
                    double branco = ProporcaoBranco(pixel);
                    result.SetPixel(x, y, Color.FromArgb(pixel.A,
                        Misturar(logoColor.R, backgroundColor.R, branco),
                        Misturar(logoColor.G, backgroundColor.G, branco),
                        Misturar(logoColor.B, backgroundColor.B, branco)));
                }
            }
            return result;
        }

        private static double ProporcaoBranco(Color pixel)
        {
            double r = (pixel.R - AzulOriginal.R) / (255.0 - AzulOriginal.R);
            double g = (pixel.G - AzulOriginal.G) / (255.0 - AzulOriginal.G);
            double b = (pixel.B - AzulOriginal.B) / (255.0 - AzulOriginal.B);
            return Math.Max(0, Math.Min(1, (r + g + b) / 3));
        }

        private static int Misturar(int de, int para, double proporcao)
        {
            return (int)Math.Round(de + (para - de) * proporcao);
        }

        private static Bitmap Redimensionar(Bitmap source, Size tamanho)
        {
            Bitmap resultado = new Bitmap(tamanho.Width, tamanho.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics grafico = Graphics.FromImage(resultado))
            {
                grafico.CompositingQuality = CompositingQuality.HighQuality;
                grafico.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grafico.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grafico.SmoothingMode = SmoothingMode.HighQuality;
                grafico.DrawImage(source, new Rectangle(Point.Empty, tamanho));
            }

            return resultado;
        }

        private static Color GetLogoColor(Color primaryColor, Color backgroundColor)
        {
            double luminance = (0.299 * backgroundColor.R) +
                               (0.587 * backgroundColor.G) +
                               (0.114 * backgroundColor.B);

            return luminance >= 180
                ? primaryColor
                : ThemeManager.GetContrastColor(backgroundColor);
        }

        public static void SetImage(PictureBox pictureBox, SubLogoVariant variant, Color primaryColor, Color backgroundColor)
        {
            if (pictureBox == null)
                return;

            // Entrega no tamanho exato: a redução da PictureBox (Zoom) serrilha o logo.
            int lado = Math.Min(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
            Bitmap next = lado > 0
                ? Get(variant, primaryColor, backgroundColor, new Size(lado, lado))
                : Get(variant, primaryColor, backgroundColor);
            Image previous = pictureBox.Image;
            pictureBox.Image = new Bitmap(next);
            if (previous != null)
                previous.Dispose();
        }
    }
}

