// Auxiliar de teste da Etapa D - interface de atualizacao no ConfiguracaoForm.
// Compilado pelo harness test-etapaD.ps1 (csc do framework, C# 5, sem NuGet).
// Uso: TesteFormD.exe <caminho completo de SUB.exe>
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SUB.Forms.Configuracoes;
using SUB.Models;
using SUB.Utils;

class TesteFormD
{
    static int falhas;
    static string dirBin;
    static ConfiguracaoForm form;

    // O JIT resolve as referencias do Main no momento da compilacao do metodo,
    // antes de qualquer instrucao executar. Por isso o Main so registra o
    // AssemblyResolve e delega: os tipos do SUB so sao resolvidos quando
    // Executar for compilado (ao ser chamado), ja com o handler ativo.
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("FALHOU uso: TesteFormD.exe <caminho SUB.exe>");
            Environment.Exit(2);
        }

        dirBin = Path.GetDirectoryName(Path.GetFullPath(args[0]));
        AppDomain.CurrentDomain.AssemblyResolve += Resolver;
        Executar();
    }

    static void Executar()
    {
        Application.EnableVisualStyles();

        try
        {
            TestarConstrutor();
            TestarControlesDoDesigner();
            TestarEstadoInicial();
            TestarHierarquiaDoLayout();
            TestarOnShownEVersao();
            TestarDimensoesEBinding();
            TestarTemaAplicado();
            TestarHelpers();

            string chave = LerChavePrivada();
            if (chave == null)
            {
                Console.WriteLine("SKIP verificar-novo/verificar-atual: chave privada ausente em ~/.sub-releases");
            }
            else
            {
                TestarVerificarVersaoNova(chave);
                TestarVerificarVersaoAtual(chave);
            }
            TestarVerificarAssinaturaInvalida();
            TestarVerificarSemInternet();
            TestarVerificar404Amigavel();
        }
        catch (Exception ex)
        {
            falhas++;
            Console.WriteLine("FALHOU erro-inesperado: " + ex.Message);
        }
        finally
        {
            try { if (form != null) form.Close(); } catch { }
            Application.DoEvents();
        }

        Console.WriteLine(falhas == 0 ? "TODOS OS TESTES PASSARAM" : falhas + " TESTE(S) FALHARAM");
        Environment.Exit(falhas);
    }

    static Assembly Resolver(object sender, ResolveEventArgs e)
    {
        try
        {
            string nome = new AssemblyName(e.Name).Name;
            string dll = Path.Combine(dirBin, nome + ".dll");
            if (File.Exists(dll))
                return Assembly.LoadFrom(dll);
            string exe = Path.Combine(dirBin, nome + ".exe");
            if (File.Exists(exe))
                return Assembly.LoadFrom(exe);
        }
        catch { }
        return null;
    }

    static void Ok(string nome)
    {
        Console.WriteLine("PASS " + nome);
    }

    static void Falhou(string nome, string motivo)
    {
        falhas++;
        Console.WriteLine("FALHOU " + nome + ": " + motivo);
    }

    static void Verificar(string nome, bool condicao, string motivo)
    {
        if (condicao) Ok(nome);
        else Falhou(nome, motivo);
    }

    static object Campo(string nome)
    {
        var info = typeof(ConfiguracaoForm).GetField(nome,
            BindingFlags.NonPublic | BindingFlags.Instance);
        if (info == null)
            throw new Exception("campo " + nome + " nao encontrado");
        return info.GetValue(form);
    }

    static T Campo<T>(string nome)
    {
        return (T)Campo(nome);
    }

    static void TestarConstrutor()
    {
        try
        {
            form = new ConfiguracaoForm();
            form.ShowInTaskbar = false;
            Ok("construtor-sem-banco-e-sem-rede");
        }
        catch (Exception ex)
        {
            Falhou("construtor-sem-banco-e-sem-rede", ex.Message);
            throw;
        }
    }

    static void TestarControlesDoDesigner()
    {
        string[] nomes =
        {
            "pnlRaiz", "lblTitulo", "grpInstituicao", "tblInstituicao",
            "lblNome", "txtNome", "lblCor", "pnlLinhaCor", "cbTemas",
            "pnlCorPreview", "lblCorHex", "grpAtualizacao", "tblAtualizacao",
            "lblVersaoAtual", "lblStatusAtualizacao", "pnlVerificar",
            "btnVerificar", "pbVerificando", "lblNotasTitulo", "txtNotasVersao",
            "pnlDownload", "btnAtualizar", "btnCancelarDownload", "pbDownload",
            "pnlBotoes", "btnSalvar", "btnCancelar"
        };

        int ausentes = 0;
        foreach (string nome in nomes)
        {
            object valor = null;
            try { valor = Campo(nome); } catch { }
            if (valor == null)
            {
                ausentes++;
                Console.WriteLine("   campo ausente: " + nome);
            }
        }
        Verificar("controles-no-designer", ausentes == 0, ausentes + " campo(s) ausente(s)");

        Verificar("tipos-dos-botoes-de-atualizacao",
            Campo("btnVerificar") is CustomButton &&
            Campo("btnAtualizar") is CustomButton &&
            Campo("btnCancelarDownload") is CustomButton,
            "botoes da secao de atualizacao devem ser CustomButton");
        Verificar("notas-em-textbox-padrao", Campo("txtNotasVersao") is TextBox,
            "txtNotasVersao deve ser TextBox padrao (Multiline)");
    }

    static void TestarEstadoInicial()
    {
        var btnAtualizar = Campo<Button>("btnAtualizar");
        var btnCancelarDownload = Campo<Button>("btnCancelarDownload");
        var pbDownload = Campo<ProgressBar>("pbDownload");
        var pbVerificando = Campo<ProgressBar>("pbVerificando");
        var txtNotas = Campo<TextBox>("txtNotasVersao");
        var txtNome = Campo<TextBox>("txtNome");
        var cbTemas = Campo<ComboBox>("cbTemas");
        var btnVerificar = Campo<Button>("btnVerificar");

        Verificar("estado-inicial-oculto",
            !btnAtualizar.Visible && !btnCancelarDownload.Visible &&
            !pbDownload.Visible && !pbVerificando.Visible,
            "btnAtualizar/btnCancelarDownload/pbDownload/pbVerificando devem iniciar ocultos");

        Verificar("notas-readonly-multiline",
            txtNotas.ReadOnly && txtNotas.Multiline &&
            txtNotas.ScrollBars == ScrollBars.Vertical && txtNotas.MaxLength == 4000,
            "txtNotasVersao deve ser ReadOnly+Multiline+VScroll+MaxLength 4000");

        Verificar("txt-nome-maxlength", txtNome.MaxLength == 150, "txtNome.MaxLength deve ser 150");
        Verificar("combo-temas-dropdownlist", cbTemas.DropDownStyle == ComboBoxStyle.DropDownList,
            "cbTemas deve ser DropDownList");
        Verificar("verificar-habilitado", btnVerificar.Enabled, "btnVerificar deve iniciar habilitado");
        Verificar("cancel-button", form.CancelButton == Campo("btnCancelar"),
            "CancelButton deve ser btnCancelar");
        Verificar("textos-botoes",
            btnVerificar.Text.StartsWith("Verificar") &&
            btnAtualizar.Text == "Atualizar agora" &&
            btnCancelarDownload.Text == "Cancelar download",
            "textos dos botoes da secao de atualizacao");
    }

    static void TestarHierarquiaDoLayout()
    {
        var pnlRaiz = Campo<Control>("pnlRaiz");
        var grpInstituicao = Campo<Control>("grpInstituicao");
        var grpAtualizacao = Campo<Control>("grpAtualizacao");
        var txtNome = Campo<Control>("txtNome");
        var txtNotas = Campo<Control>("txtNotasVersao");
        var btnAtualizar = Campo<Control>("btnAtualizar");
        var pnlDownload = Campo<Control>("pnlDownload");

        Verificar("layout-dock-fill",
            pnlRaiz.Dock == DockStyle.Fill &&
            form.Controls.Contains(pnlRaiz) &&
            txtNome.Dock == DockStyle.Fill &&
            txtNotas.Dock == DockStyle.Fill,
            "pnlRaiz/txtNome/txtNotasVersao devem usar Dock=Fill e pnlRaiz deve estar no form");

        Verificar("secoes-em-groupbox",
            pnlRaiz.Controls.Contains(grpInstituicao) &&
            pnlRaiz.Controls.Contains(grpAtualizacao) &&
            grpAtualizacao.Controls.ContainsKey("tblAtualizacao"),
            "grpInstituicao/grpAtualizacao devem estar em pnlRaiz e tblAtualizacao dentro de grpAtualizacao");

        Verificar("botoes-atualizacao-em-painel",
            pnlDownload.Controls.Contains(btnAtualizar) &&
            pnlDownload.Controls.Contains(Campo<Control>("btnCancelarDownload")) &&
            pnlDownload.Controls.Contains(Campo<Control>("pbDownload")),
            "btnAtualizar/btnCancelarDownload/pbDownload devem estar em pnlDownload");

        var tblAtualizacao = Campo<Control>("tblAtualizacao");
        int ordenado =
            tblAtualizacao.Controls.Find("lblVersaoAtual", false)[0].TabIndex;
        int status = tblAtualizacao.Controls.Find("lblStatusAtualizacao", false)[0].TabIndex;
        int verificar = tblAtualizacao.Controls.Find("pnlVerificar", false)[0].TabIndex;
        int notas = tblAtualizacao.Controls.Find("txtNotasVersao", false)[0].TabIndex;
        int download = tblAtualizacao.Controls.Find("pnlDownload", false)[0].TabIndex;
        Verificar("tabindex-logico",
            ordenado < status && status < verificar && verificar < notas && notas < download,
            "TabIndex de lblVersaoAtual < lblStatus < pnlVerificar < txtNotas < pnlDownload");
    }

    static void TestarOnShownEVersao()
    {
        form.Opacity = 0;
        form.Show();
        Application.DoEvents();

        var lblVersao = Campo<Label>("lblVersaoAtual");
        string esperado = FileVersionInfo.GetVersionInfo(Path.Combine(dirBin, "SUB.exe")).FileVersion;
        Verificar("onshoud-preenche-versao",
            lblVersao.Text.Contains(esperado),
            "lblVersaoAtual deveria conter o FileVersion (" + lblVersao.Text + ")");
    }

    static void TestarDimensoesEBinding()
    {
        var grpInst = Campo<Control>("grpInstituicao");
        var grpAtual = Campo<Control>("grpAtualizacao");
        var txtNome = Campo<Control>("txtNome");
        var cbTemas = Campo<Control>("cbTemas");
        var pnlCor = Campo<Control>("pnlCorPreview");
        var btnSalvar = Campo<Control>("btnSalvar");

        // Regressao da Etapa D: GroupBox.AutoSize nao cresce com filho Dock=Fill,
        // o que colapsava a secao Instituicao e escondia nome/tema.
        Verificar("dimensoes-secao-instituicao",
            grpInst.Height >= 140 && txtNome.Height >= 20 && cbTemas.Height >= 20 &&
            pnlCor.Height >= 30 && txtNome.Visible && btnSalvar.Visible,
            "grpInst=" + grpInst.Height + " txtNome=" + txtNome.Height +
            " cbTemas=" + cbTemas.Height + " pnlCor=" + pnlCor.Height);

        Verificar("dimensoes-secao-atualizacao",
            grpAtual.Height >= 150 && grpAtual.Bottom <= form.ClientSize.Height,
            "grpAtual=" + grpAtual.Height + " bottom=" + grpAtual.Bottom);

        string esperado = "";
        try
        {
            var cfg = InstituicaoConfiguracaoStore.Carregar();
            if (cfg != null)
                esperado = cfg.Nome ?? "";
        }
        catch { }
        Verificar("binding-nome-carregado",
            txtNome.Text == esperado,
            "txtNome='" + txtNome.Text + "' esperado='" + esperado + "'");
    }

    static void TestarTemaAplicado()
    {
        var primaria = Color.FromArgb(31, 72, 126);
        var lblNome = Campo<Label>("lblNome");
        var corAntes = lblNome.ForeColor;
        ThemeManager.Apply(form, primaria);
        Application.DoEvents();

        var btnSalvar = Campo<Button>("btnSalvar");
        var btnCancelarDownload = Campo<CustomButton>("btnCancelarDownload");
        var btnVerificar = Campo<CustomButton>("btnVerificar");

        // Comportamento real do ThemeManager (aprovado no projeto): labels de cor
        // neutra/escura (hue semantico) preservam a ForeColor; os botoes e o
        // CustomButton recebem as cores do tema.
        Verificar("tema-labels-preservados",
            lblNome.ForeColor == corAntes,
            "lblNome (cor neutra) deve manter a ForeColor herdada");

        Verificar("tema-botao-primario",
            btnSalvar.BackColor == primaria,
            "btnSalvar deve receber a cor primaria");

        // O ThemeManager trata CustomButton sempre com a cor primaria (sem a
        // regra "secundario por nome" que vale para Button padrao).
        Verificar("tema-custombutton",
            btnVerificar.BaseColor == primaria &&
            btnCancelarDownload.BaseColor == primaria,
            "CustomButtons (btnVerificar/btnCancelarDownload) devem receber a cor primaria");
    }

    static void TestarHelpers()
    {
        var t = typeof(ConfiguracaoForm);
        var truncar = t.GetMethod("TruncarNotas", BindingFlags.NonPublic | BindingFlags.Static);
        var percentual = t.GetMethod("CalcularPercentual", BindingFlags.NonPublic | BindingFlags.Static);
        var mensagem = t.GetMethod("MontarMensagemVersaoNova", BindingFlags.NonPublic | BindingFlags.Static);

        Verificar("helpers-existem", truncar != null && percentual != null && mensagem != null,
            "TruncarNotas/CalcularPercentual/MontarMensagemVersaoNova devem existir");

        string r1 = (string)truncar.Invoke(null, new object[] { null });
        string r2 = (string)truncar.Invoke(null, new object[] { "abc" });
        string longa = new string('x', 5000);
        string r3 = (string)truncar.Invoke(null, new object[] { longa });
        Verificar("truncar-notas",
            r1 == "" && r2 == "abc" && r3.Length >= 4000 && r3.Length < 4200 && r3.Contains("[notas truncadas]"),
            "TruncarNotas: null->vazio, curto->igual, longo->4000 + marcador");

        int p0 = (int)percentual.Invoke(null, new object[] { 0L, 0L });
        int p1 = (int)percentual.Invoke(null, new object[] { 50L, 100L });
        int p2 = (int)percentual.Invoke(null, new object[] { 150L, 100L });
        Verificar("calcular-percentual", p0 == 0 && p1 == 50 && p2 == 100,
            "CalcularPercentual: 0/0->0, 50/100->50, 150/100->100");

        var resultado = new ResultadoVerificacaoAtualizacao
        {
            Manifesto = new AtualizacaoManifesto { Versao = "9.9.9.9", Data = "20/10/2026" }
        };
        string msg = (string)mensagem.Invoke(null, new object[] { resultado });
        Verificar("mensagem-versao-nova",
            msg.Contains("9.9.9.9") && msg.Contains("20/10/2026"),
            "MontarMensagemVersaoNova deve citar versao e data");
    }

    static string LerChavePrivada()
    {
        try
        {
            string caminho = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".sub-releases", "chave-privada.xml");
            if (!File.Exists(caminho))
                return null;
            return File.ReadAllText(caminho);
        }
        catch
        {
            return null;
        }
    }

    static string MontarJsonManifesto(string versao, string notas)
    {
        return "{\"versao\":\"" + versao + "\",\"data\":\"20/10/2026\"," +
               "\"url\":\"http://127.0.0.1:1/pacote.zip\"," +
               "\"sha256\":\"" + new string('a', 64) + "\",\"tamanho\":1234," +
               "\"notas\":" + EscaparJson(notas) + "}";
    }

    static string EscaparJson(string valor)
    {
        return "\"" + valor.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
    }

    static byte[] Assinar(byte[] dados, string xmlChavePrivada)
    {
        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(xmlChavePrivada);
            return rsa.SignData(dados, "SHA256");
        }
    }

    static int PortaLivre()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        int porta = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return porta;
    }

    class Servidor : IDisposable
    {
        HttpListener listener;
        Thread thread;
        volatile bool rodando = true;
        public byte[] Corpo;
        public byte[] Assinatura;
        public int StatusCode = 200;

        public Servidor(int porta)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:" + porta + "/");
            listener.Start();
            thread = new Thread(Loop);
            thread.IsBackground = true;
            thread.Start();
        }

        void Loop()
        {
            while (rodando)
            {
                HttpListenerContext ctx = null;
                try { ctx = listener.GetContext(); }
                catch { break; }
                if (ctx == null) break;
                try
                {
                    ctx.Response.StatusCode = StatusCode;
                    if (StatusCode != 200)
                    {
                        ctx.Response.OutputStream.Close();
                        continue;
                    }
                    bool sig = ctx.Request.Url.AbsolutePath.EndsWith(".sig");
                    byte[] dados = sig ? Assinatura : Corpo;
                    ctx.Response.ContentLength64 = dados.Length;
                    ctx.Response.OutputStream.Write(dados, 0, dados.Length);
                    ctx.Response.OutputStream.Close();
                }
                catch { }
            }
        }

        public void Dispose()
        {
            rodando = false;
            try { listener.Stop(); } catch { }
            try { listener.Close(); } catch { }
        }
    }

    static void InvocarVerificar()
    {
        var metodo = typeof(ConfiguracaoForm).GetMethod("btnVerificar_Click",
            BindingFlags.NonPublic | BindingFlags.Instance);
        metodo.Invoke(form, new object[] { form, EventArgs.Empty });
    }

    static string SemAcento(string valor)
    {
        if (valor == null)
            return "";
        var sb = new StringBuilder();
        foreach (char c in valor.Normalize(NormalizationForm.FormD))
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) !=
                System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    static void AguardarVerificacaoConcluir(int timeoutMs)
    {
        // Espera o status final por comparacao de texto (sem depender de acentos
        // nem do ciclo do pbVerificando, que tem race quando a verificacao
        // local termina entre o Invoke e o primeiro DoEvents).
        var lbl = Campo<Label>("lblStatusAtualizacao");
        var pb = Campo<ProgressBar>("pbVerificando");
        string inicial = SemAcento(lbl.Text);
        var inicio = DateTime.Now;
        while (true)
        {
            Application.DoEvents();
            string atual = SemAcento(lbl.Text);
            bool terminou = atual != inicial &&
                atual != "verificando atualizacoes..." &&
                !pb.Visible;
            if (terminou)
                break;
            if ((DateTime.Now - inicio).TotalMilliseconds >= timeoutMs)
                throw new Exception("verificacao nao concluiu no tempo limite; status='" + lbl.Text + "'");
            Thread.Sleep(50);
        }
        Application.DoEvents();
    }

    static void TestarVerificarVersaoNova(string chave)
    {
        int porta = PortaLivre();
        string json = MontarJsonManifesto("99.0.0.0", "correcao de bugs - nota de teste");
        byte[] corpo = Encoding.UTF8.GetBytes(json);

        using (var servidor = new Servidor(porta))
        {
            servidor.Corpo = corpo;
            servidor.Assinatura = Encoding.ASCII.GetBytes(Convert.ToBase64String(Assinar(corpo, chave)));

            string url = "http://127.0.0.1:" + porta + "/version.json";
            ConfigurarUrl(url);
            InvocarVerificar();
            AguardarVerificacaoConcluir(40000);

            var lbl = Campo<Label>("lblStatusAtualizacao");
            var btnAtualizar = Campo<Button>("btnAtualizar");
            var txtNotas = Campo<TextBox>("txtNotasVersao");
            var manifesto = Campo<AtualizacaoManifesto>("manifestoDisponivel");

            Verificar("verificar-versao-nova",
                lbl.Text.Contains("99.0.0.0") && btnAtualizar.Visible &&
                txtNotas.Text.Contains("correcao de bugs") && manifesto != null &&
                manifesto.Versao == "99.0.0.0",
                "status='" + lbl.Text + "' btnAtualizar.Visible=" + btnAtualizar.Visible);
        }
    }

    static void TestarVerificarVersaoAtual(string chave)
    {
        int porta = PortaLivre();
        string versaoAtual = AtualizacaoService.VersaoAtual();
        string json = MontarJsonManifesto(versaoAtual, "nao deveria aparecer");
        byte[] corpo = Encoding.UTF8.GetBytes(json);

        using (var servidor = new Servidor(porta))
        {
            servidor.Corpo = corpo;
            servidor.Assinatura = Encoding.ASCII.GetBytes(Convert.ToBase64String(Assinar(corpo, chave)));

            string url = "http://127.0.0.1:" + porta + "/version.json";
            ConfigurarUrl(url);
            InvocarVerificar();
            AguardarVerificacaoConcluir(40000);

            var lbl = Campo<Label>("lblStatusAtualizacao");
            var btnAtualizar = Campo<Button>("btnAtualizar");
            var txtNotas = Campo<TextBox>("txtNotasVersao");

            Verificar("verificar-versao-ja-atual",
                lbl.Text.Contains("mais recente") && !btnAtualizar.Visible &&
                txtNotas.Text == "",
                "status='" + lbl.Text + "'");
        }
    }

    static void TestarVerificarAssinaturaInvalida()
    {
        int porta = PortaLivre();
        string json = MontarJsonManifesto("99.0.0.0", "x");
        byte[] corpo = Encoding.UTF8.GetBytes(json);

        using (var servidor = new Servidor(porta))
        {
            servidor.Corpo = corpo;
            servidor.Assinatura = Encoding.ASCII.GetBytes("assinatura-invalida");

            string url = "http://127.0.0.1:" + porta + "/version.json";
            ConfigurarUrl(url);
            InvocarVerificar();
            AguardarVerificacaoConcluir(40000);

            var lbl = Campo<Label>("lblStatusAtualizacao");
            var btnAtualizar = Campo<Button>("btnAtualizar");

            Verificar("verificar-assinatura-invalida",
                lbl.Text.ToLowerInvariant().Contains("assinatura") && !btnAtualizar.Visible,
                "status='" + lbl.Text + "'");
        }
    }

    static void TestarVerificarSemInternet()
    {
        int porta = PortaLivre(); // porta sem servidor: connection refused
        string url = "http://127.0.0.1:" + porta + "/version.json";
        ConfigurarUrl(url);
        InvocarVerificar();
        AguardarVerificacaoConcluir(40000);

        var lbl = Campo<Label>("lblStatusAtualizacao");
        var btnAtualizar = Campo<Button>("btnAtualizar");
        Verificar("verificar-sem-internet",
            lbl.Text.ToLowerInvariant().Contains("conex") && !btnAtualizar.Visible,
            "status='" + lbl.Text + "'");
    }

    static void TestarVerificar404Amigavel()
    {
        int porta = PortaLivre();
        using (var servidor = new Servidor(porta))
        {
            servidor.StatusCode = 404;

            string url = "http://127.0.0.1:" + porta + "/version.json";
            ConfigurarUrl(url);
            InvocarVerificar();
            AguardarVerificacaoConcluir(40000);

            var lbl = Campo<Label>("lblStatusAtualizacao");
            var btnAtualizar = Campo<Button>("btnAtualizar");
            Verificar("verificar-404-amigavel",
                lbl.Text.Contains("HTTP 404") && SemAcento(lbl.Text).Contains("versao publicada") &&
                !btnAtualizar.Visible,
                "status='" + lbl.Text + "'");
        }
    }

    static void ConfigurarUrl(string url)
    {
        var campo = typeof(AtualizacaoService).GetField("UrlManifestoOverrideParaTestes",
            BindingFlags.NonPublic | BindingFlags.Static);
        if (campo == null)
            throw new Exception("UrlManifestoOverrideParaTestes nao encontrado no AtualizacaoService");
        campo.SetValue(null, url);
    }
}
