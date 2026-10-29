using System;
using System.Text.RegularExpressions;

namespace SUB.Utils
{
    public static class VersaoAplicativo
    {
        public static bool FormatoValido(string valor)
        {
            return valor != null && Regex.IsMatch(valor, @"\A[0-9]+\.[0-9]+\.[0-9]+\z") && Formatar(valor) == valor;
        }

        // Os metadados do Windows podem acrescentar uma revisão zero.
        // A versão pública sempre contém somente três componentes.
        public static string Formatar(string valor)
        {
            if (valor == null || !Regex.IsMatch(valor, @"\A[0-9]+\.[0-9]+\.[0-9]+(?:\.0)?\z"))
                return null;
            Version versao;
            if (!Version.TryParse(valor, out versao))
                return null;
            return new Version(versao.Major, versao.Minor, versao.Build).ToString(3);
        }

        public static int Comparar(string primeira, string segunda)
        {
            string a = Formatar(primeira);
            string b = Formatar(segunda);
            if (a == null || b == null)
                throw new ArgumentException("Use versões no formato 0.0.0.");
            return new Version(a).CompareTo(new Version(b));
        }
    }
}
