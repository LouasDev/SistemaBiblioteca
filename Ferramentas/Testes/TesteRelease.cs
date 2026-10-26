// Compilar com csc /r:SUB.exe /r:System.Windows.Forms.dll /r:System.Drawing.dll.
// Uso: TesteRelease.exe <pasta do pacote novo> <zip antigo> <zip novo> <pasta de teste nova>
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SUB.Forms.Configuracoes;
using SUB.Forms.Inicio;
using SUB.Models;

class TesteRelease
{
    static string pacote;
    const BindingFlags Metodos = BindingFlags.NonPublic | BindingFlags.Static;
    const BindingFlags Campos = BindingFlags.NonPublic | BindingFlags.Instance;

    [STAThread]
    static void Main(string[] args)
    {
        pacote = Path.GetFullPath(args[0]);
        AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
            string nome = new AssemblyName(e.Name).Name;
            foreach (string extensao in new[] { ".dll", ".exe" }) {
                string caminho = Path.Combine(pacote, nome + extensao);
                if (File.Exists(caminho)) return Assembly.LoadFrom(caminho);
            }
            return null;
        };
        Executar(args);
    }

    static void Conferir(bool condicao, string mensagem)
    {
        if (!condicao) throw new Exception(mensagem);
        Console.WriteLine("PASS " + mensagem);
    }

    static void Executar(string[] args)
    {
        using (var form = new ConfiguracaoForm())
        {
            var apresentar = typeof(ConfiguracaoForm).GetMethod("ApresentarResultadoVerificacao", Campos);
            var notas = (TextBox)typeof(ConfiguracaoForm).GetField("txtNotasVersao", Campos).GetValue(form);
            var manifesto = new AtualizacaoManifesto { Versao = "1.0.1", Notas = "Notas da versao 1.0.1" };
            apresentar.Invoke(form, new object[] { new ResultadoVerificacaoAtualizacao {
                Sucesso = true, AtualizacaoDisponivel = true, Manifesto = manifesto } });
            Conferir(notas.Text == manifesto.Notas, "notas exibidas para nova versao");
            apresentar.Invoke(form, new object[] { new ResultadoVerificacaoAtualizacao {
                Sucesso = true, AtualizacaoDisponivel = false, Manifesto = manifesto,
                Mensagem = "Voce ja esta usando a versao mais recente." } });
            Conferir(notas.Text == manifesto.Notas, "notas mantidas para versao ja atualizada");
            Conferir(typeof(ConfiguracaoForm).GetField("manifestoDisponivel", Campos).GetValue(form) == null,
                "versao atual nao fica disponivel para reinstalacao");
            apresentar.Invoke(form, new object[] { new ResultadoVerificacaoAtualizacao {
                Sucesso = false, Manifesto = manifesto, Mensagem = "Assinatura invalida" } });
            Conferir(notas.Text == "", "erro limpa notas de manifesto nao confiavel");
        }

        using (var inicio = new InicioForm())
        {
            var faixas = inicio.Controls.Find("lblAtualizacaoTeste", true);
            Conferir(faixas.Length == 1 && faixas[0].Text.Contains("SUB 1.0.1"), "faixa de teste na tela Inicio");
            Conferir(faixas[0].Parent.ClientRectangle.Contains(faixas[0].Bounds), "faixa dentro do cabecalho");
        }

        string raiz = Path.GetFullPath(args[3]);
        if (Directory.Exists(raiz)) throw new Exception("Use uma pasta de teste nova.");
        Directory.CreateDirectory(raiz);
        var tipo = Assembly.LoadFrom(Path.Combine(pacote, "SUB-UPDATER.exe")).GetType("SUB.Updater.Program");
        string instalacao = Path.Combine(raiz, "instalacao");
        string staging = Path.Combine(raiz, "staging");
        string rollback = Path.Combine(raiz, "rollback");
        var extrair = tipo.GetMethod("ExtrairPacoteValidado", Metodos);
        extrair.Invoke(null, new object[] { Path.GetFullPath(args[1]), instalacao });
        extrair.Invoke(null, new object[] { Path.GetFullPath(args[2]), staging });
        var ler = tipo.GetMethod("LerListaArquivos", Metodos);
        object antiga = ler.Invoke(null, new object[] { Path.Combine(instalacao, "arquivos.txt") });
        object nova = ler.Invoke(null, new object[] { Path.Combine(staging, "arquivos.txt") });
        object resultado = tipo.GetMethod("AplicarTroca", Metodos).Invoke(null,
            new object[] { staging, instalacao, nova, antiga, rollback });
        Conferir((bool)resultado.GetType().GetField("Sucesso").GetValue(resultado), "aplicacao isolada do pacote real");
        string exe = Path.Combine(instalacao, "SUB.exe");
        Conferir(System.Diagnostics.FileVersionInfo.GetVersionInfo(exe).FileVersion == "1.0.1.0", "troca 1.0.0.0 para 1.0.1.0");
        tipo.GetMethod("RestaurarRollback", Metodos).Invoke(null, new object[] { rollback, resultado, instalacao });
        Conferir(System.Diagnostics.FileVersionInfo.GetVersionInfo(exe).FileVersion == "1.0.0.0", "rollback restaura 1.0.0.0");
    }
}
