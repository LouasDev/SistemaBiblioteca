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

        private static Bitmap CreateVariant(Bitmap source, Color primaryColor, Color backgroundColor)
        {
            Color logoColor = GetLogoColor(primaryColor, backgroundColor);
            Bitmap result = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color pixel = source.GetPixel(x, y);
                    if (IsWhite(pixel))
                        result.SetPixel(x, y, Color.FromArgb(pixel.A, backgroundColor));
                    else if (IsBlue(pixel))
                        result.SetPixel(x, y, Color.FromArgb(pixel.A, logoColor));
                    else
                        result.SetPixel(x, y, pixel);
                }
            }
            return result;
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

        private static bool IsWhite(Color color)
        {
            return color.A > 0 && color.R > 235 && color.G > 235 && color.B > 235;
        }

        private static bool IsBlue(Color color)
        {
            return color.A > 0 && color.B > color.R + 18 && color.B > color.G + 8;
        }

        public static void SetImage(PictureBox pictureBox, SubLogoVariant variant, Color primaryColor, Color backgroundColor)
        {
            if (pictureBox == null)
                return;

            Bitmap next = Get(variant, primaryColor, backgroundColor);
            Image previous = pictureBox.Image;
            pictureBox.Image = new Bitmap(next);
            if (previous != null)
                previous.Dispose();
        }
    }
}

