using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SUB.Utils
{
    internal static class CodigoMestreArquivo
    {
        internal static string Ler(string caminho)
        {
            if (!string.Equals(Path.GetExtension(caminho), ".txt", StringComparison.OrdinalIgnoreCase))
                throw new FormatException("Selecione o arquivo TXT de recuperação salvo no primeiro uso.");
            string conteudo;
            using (var arquivo = new FileStream(caminho, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (arquivo.Length == 0 || arquivo.Length > 16 * 1024)
                    throw new FormatException("O arquivo está vazio ou não corresponde ao TXT de recuperação do SUB.");
                using (var leitor = new StreamReader(arquivo, Encoding.UTF8, true))
                    conteudo = leitor.ReadToEnd();
            }

            // Formato exportado pelo PrimeiroUsoForm; não usa o nome da escola
            // para autorizar a operação: o código será conferido contra o hash.
            var codigos = Regex.Matches(conteudo, @"^[ \t]*C[óo]digo[ \t]*:[ \t]*([0-9a-f]{48})[ \t]*\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (codigos.Count == 1)
                return codigos[0].Groups[1].Value.ToUpperInvariant();
            string somenteCodigo = conteudo.Trim();
            if (codigos.Count == 0 && Regex.IsMatch(somenteCodigo, @"\A[0-9a-fA-F]{48}\z"))
                return somenteCodigo.ToUpperInvariant();
            throw new FormatException("Não foi encontrado um único código válido. Selecione o TXT de recuperação gerado pelo SUB.");
        }
    }
}
