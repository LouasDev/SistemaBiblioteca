using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SUB.Utils
{
    /// <summary>
    /// Normaliza textos usados apenas para comparação de busca, preservando o valor exibido ao usuário.
    /// </summary>
    internal static class TextoBusca
    {
        internal static string Normalizar(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;
            string decomposto = texto.Trim().Normalize(NormalizationForm.FormD);
            var resultado = new StringBuilder(decomposto.Length);
            foreach (char caractere in decomposto)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (categoria != UnicodeCategory.NonSpacingMark && categoria != UnicodeCategory.SpacingCombiningMark && categoria != UnicodeCategory.EnclosingMark)
                {
                    resultado.Append(caractere);
                }
            }

            return resultado.ToString().Normalize(NormalizationForm.FormC).ToUpperInvariant();
        }

        internal static bool Contem(string texto, string termo)
        {
            return Normalizar(texto).Contains(Normalizar(termo));
        }

        internal static bool ComecaCom(string texto, string prefixo)
        {
            return Normalizar(texto).StartsWith(Normalizar(prefixo), StringComparison.Ordinal);
        }

        internal static bool SaoIguais(string primeiro, string segundo)
        {
            return string.Equals(Normalizar(primeiro), Normalizar(segundo), StringComparison.Ordinal);
        }

        internal static bool ContemTodosTokens(string texto, string consulta)
        {
            if (string.IsNullOrWhiteSpace(consulta))
                return true;
            if (string.IsNullOrWhiteSpace(texto))
                return false;
            string textoNormalizado = Normalizar(texto);
            return consulta.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Normalizar).All(token => textoNormalizado.Contains(token));
        }
    }
}
