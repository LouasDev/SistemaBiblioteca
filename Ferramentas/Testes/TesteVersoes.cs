// csc /out:TesteVersoes.exe TesteVersoes.cs SUB-SEEMG/Utils/VersaoAplicativo.cs
// Uso: TesteVersoes.exe <pasta bin do SUB e SUB-UPDATER>
using System;
using System.IO;
using System.Reflection;
using SUB.Utils;

class TesteVersoes
{
    static void Verificar(bool condicao, string mensagem)
    {
        if (!condicao) throw new Exception(mensagem);
        Console.WriteLine("PASS " + mensagem);
    }

    static void Main(string[] args)
    {
        Verificar(VersaoAplicativo.Formatar("1.0.1.0") == "1.0.1", "metadados Windows exibidos com tres componentes");
        Verificar(VersaoAplicativo.Comparar("1.0.1.0", "1.0.1") == 0, "compatibilidade com executaveis anteriores");
        Verificar(VersaoAplicativo.Comparar("1.0.10", "1.0.9") > 0, "comparacao numerica de patch");
        Verificar(VersaoAplicativo.Comparar("1.10.0", "1.9.99") > 0, "comparacao numerica de minor");
        foreach (string invalida in new[] { "1", "1.0", "1.0.1.0", "1.0.1.2", "v1.0.1", "1.0.1-beta", "1.0.1+build", "-1.0.0", "1.0.x", "01.0.1", "1.0.1\n" })
            Verificar(!VersaoAplicativo.FormatoValido(invalida), "manifesto rejeita formato diferente de 0.0.0: " + invalida.Trim());
        Verificar(VersaoAplicativo.FormatoValido("0.0.0") && VersaoAplicativo.FormatoValido("1.0.2"), "formato publico valido");
        Verificar(VersaoAplicativo.Formatar("1.0.1.2") == null, "nao descarta revisao diferente de zero");

        string pasta = Path.GetFullPath(args[0]);
        AppDomain.CurrentDomain.AssemblyResolve += (s,e) => {
            foreach (string ext in new[] { ".dll", ".exe" }) {
                string arquivo = Path.Combine(pasta, new AssemblyName(e.Name).Name + ext);
                if (File.Exists(arquivo)) return Assembly.LoadFrom(arquivo);
            }
            return null;
        };
        var sub = Assembly.LoadFrom(Path.Combine(pasta, "SUB.exe"));
        var servico = sub.GetType("SUB.Utils.AtualizacaoService");
        string atual = (string)servico.GetMethod("VersaoAtual").Invoke(null, null);
        Verificar(VersaoAplicativo.FormatoValido(atual), "servico retorna versao atual no formato 0.0.0");
        var sobre = Activator.CreateInstance(sub.GetType("SUB.Forms.Login.AboutForm"));
        try {
            var rotulo = sobre.GetType().GetField("label5", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sobre);
            Verificar((string)rotulo.GetType().GetProperty("Text").GetValue(rotulo, null) == "Versão " + atual,
                "tela Sobre usa a versao de distribuicao com tres componentes");
        } finally { ((IDisposable)sobre).Dispose(); }
        var manifesto = Activator.CreateInstance(sub.GetType("SUB.Models.AtualizacaoManifesto"));
        manifesto.GetType().GetProperty("Versao").SetValue(manifesto, "1.0.1.0", null);
        var validar = servico.GetMethod("ValidarManifesto", BindingFlags.NonPublic | BindingFlags.Static);
        Verificar(((string)validar.Invoke(null, new[] { manifesto })).Contains("0.0.0"), "servico rejeita manifesto com quarto componente");

        var updater = Assembly.LoadFrom(Path.Combine(pasta, "SUB-UPDATER.exe")).GetType("SUB.Updater.Program");
        var comparar = updater.GetMethod("CompararVersoes", BindingFlags.NonPublic | BindingFlags.Static);
        Verificar((int)comparar.Invoke(null, new object[] { "1.0.1.0", "1.0.1" }) == 0, "updater reconhece metadados Windows");
        Verificar((int)comparar.Invoke(null, new object[] { "1.0.10", "1.0.9" }) > 0, "updater compara tres componentes numericamente");
        var confere = updater.GetMethod("VersaoConfere", BindingFlags.NonPublic | BindingFlags.Static);
        Verificar((bool)confere.Invoke(null, new object[] { Path.Combine(pasta, "SUB.exe"), atual }), "updater valida executavel com versao publica");
        Verificar(!(bool)confere.Invoke(null, new object[] { Path.Combine(pasta, "SUB.exe"), atual + ".0" }), "updater exige versao publica com tres componentes");
    }
}
