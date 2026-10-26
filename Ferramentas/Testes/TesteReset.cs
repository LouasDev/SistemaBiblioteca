// Compilar junto de ResetLocalStore.cs e ResetService.cs; referenciar SUB.exe,
// Newtonsoft.Json.dll, System.Data.SqlServerCe.dll e assemblies do Framework.
// Uso: TesteReset.exe <bin do SUB> <pasta nova de testes>
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using SUB.Forms.Configuracoes;
using SUB.Models;
using SUB.Utils;

class TesteReset
{
    static string bin;
    static string raiz;
    static int testes;

    [STAThread]
    static void Main(string[] args)
    {
        bin = Path.GetFullPath(args[0]);
        raiz = Path.GetFullPath(args[1]);
        if (Directory.Exists(raiz)) throw new Exception("Use uma pasta de teste nova.");
        Directory.CreateDirectory(raiz);
        AppDomain.CurrentDomain.AssemblyResolve += (s,e) => {
            foreach (string ext in new[] { ".dll", ".exe" }) {
                string caminho = Path.Combine(bin, new AssemblyName(e.Name).Name + ext);
                if (File.Exists(caminho)) return Assembly.LoadFrom(caminho);
            }
            return null;
        };
        Executar();
        Console.WriteLine("TODOS OS " + testes + " TESTES PASSARAM");
    }

    static void Verificar(bool condicao, string nome)
    {
        if (!condicao) throw new Exception(nome);
        testes++;
        Console.WriteLine("PASS " + nome);
    }

    static PedidoReset Pedido()
    {
        return new PedidoReset { Id = Guid.NewGuid().ToString("N"), UsuarioId = 7, CriadoEmUtc = DateTime.UtcNow };
    }

    static string CriarDados(string caso)
    {
        string pasta = Path.Combine(raiz, caso, "SUB");
        foreach (string sub in new[] { "SUB", "backups/antigo", "mapeamentoanual", "Relatorios", "update" })
            Directory.CreateDirectory(Path.Combine(pasta, sub));
        foreach (string arquivo in new[] { "instituicao.json", "SUB/sub.sdf", "backups/antigo/sub.sdf", "mapeamentoanual/ano.txt", "Relatorios/relatorio.pdf", "update/pacote.zip" })
            File.WriteAllText(Path.Combine(pasta, arquivo), "DADOS FICTICIOS DE TESTE");
        return pasta;
    }

    static void DeveFalhar(Action acao, string nome)
    {
        bool falhou = false;
        try { acao(); } catch (Exception) { falhou = true; }
        Verificar(falhou, nome);
    }

    static void Executar()
    {
        string codigo = "CODIGO-MESTRE-FICTICIO";
        string senha = "SENHA-FICTICIA";
        string hash, salt, hashCodigo, saltCodigo;
        CriptografiaSenha.CriarHash(senha, out hash, out salt);
        CriptografiaSenha.CriarHash(codigo, out hashCodigo, out saltCodigo);
        var config = new InstituicaoConfiguracao { CodigoMestreHash = hashCodigo, CodigoMestreSalt = saltCodigo };
        Verificar(ResetService.ValidarCredenciais(config, "Bibliotecário(a)", true, hash, salt, codigo, senha), "duas credenciais corretas autorizam");
        Verificar(!ResetService.ValidarCredenciais(config, "Bibliotecário(a)", true, hash, salt, "errado", senha), "senha correta nao substitui codigo mestre");
        Verificar(!ResetService.ValidarCredenciais(config, "Bibliotecário(a)", true, hash, salt, codigo, "errada"), "codigo correto nao substitui senha");
        Verificar(!ResetService.ValidarCredenciais(config, "Aluno(a)", true, hash, salt, codigo, senha), "perfil sem permissao rejeitado");
        Verificar(!ResetService.ValidarCredenciais(config, "Bibliotecário(a)", false, hash, salt, codigo, senha), "bibliotecario inativo rejeitado");
        Verificar(!ResetService.ValidarCredenciais(null, "Bibliotecário(a)", true, hash, salt, codigo, senha), "configuracao ausente rejeitada");
        Verificar(!ResetService.ValidarCredenciais(config, "Bibliotecário(a)", true, "hash", "salt invalido", codigo, senha), "hash corrompido nao autoriza");
        Sessao.Limpar();
        ResetService.Autorizacao autorizacao;
        string erro;
        Verificar(!ResetService.TentarAutorizar(codigo, senha, out autorizacao, out erro) && autorizacao == null,
            "sem sessao nao consulta nem altera dados da instalacao");
        DeveFalhar(() => ResetService.Iniciar(null), "sem autorizacao nao inicia processo de reset");
        Sessao.UsuarioId = 7;
        DeveFalhar(() => ResetService.Iniciar(new ResetService.Autorizacao {
            UsuarioId = 7, CriadaEmUtc = DateTime.UtcNow.AddMinutes(-3) }), "autorizacao de confirmacao expirada rejeitada");
        DeveFalhar(() => ResetService.Iniciar(new ResetService.Autorizacao {
            UsuarioId = 8, CriadaEmUtc = DateTime.UtcNow }), "troca de sessao invalida autorizacao");
        Sessao.Limpar();
        var campoBloqueio = typeof(ResetService).GetField("bloqueadoAteUtc", BindingFlags.Static | BindingFlags.NonPublic);
        campoBloqueio.SetValue(null, DateTime.UtcNow.AddMinutes(5));
        Verificar(!ResetService.TentarAutorizar(codigo, senha, out autorizacao, out erro) && erro.Contains("cinco minutos"),
            "bloqueio de tentativas impede autorizacao");
        campoBloqueio.SetValue(null, default(DateTime));
        DeveFalhar(() => new ResetLocalStore(raiz), "recusa pasta fora do formato de dados SUB");

        string pasta = CriarDados("limpeza");
        string externo = Path.Combine(raiz, "exportacao-externa.txt");
        File.WriteAllText(externo, "PRESERVAR");
        var store = new ResetLocalStore(pasta);
        Verificar(!store.ConcluirSePendente() && File.Exists(Path.Combine(pasta, "SUB/sub.sdf")), "sem pedido nao apaga dados");
        var pedido = Pedido();
        store.Preparar(pedido);
        string marcador = Path.Combine(pasta, ".reset-autorizado.dat");
        Verificar(!File.ReadAllText(marcador).Contains(pedido.Id), "autorizacao persistida e protegida pelo Windows");
        DeveFalhar(() => store.Preparar(Pedido()), "pedido pendente nao pode ser sobrescrito");
        using (var bloqueio = new FileStream(Path.Combine(pasta, "SUB/sub.sdf"), FileMode.Open, FileAccess.ReadWrite, FileShare.None))
        {
            DeveFalhar(() => store.ConcluirSePendente(), "arquivo bloqueado impede a limpeza");
            Verificar(File.Exists(Path.Combine(pasta, "instituicao.json")) &&
                File.Exists(Path.Combine(pasta, "backups/antigo/sub.sdf")), "falha na verificacao preserva todos os dados");
        }
        Verificar(store.ConcluirSePendente() && Directory.GetFileSystemEntries(pasta).Length == 0,
            "limpeza completa remove banco configuracao backups relatorios e pedido");
        Verificar(File.ReadAllText(externo) == "PRESERVAR", "arquivos fora da pasta local preservados");
        Verificar(!store.ConcluirSePendente(), "reset concluido nao se repete");

        pasta = CriarDados("cancelamento");
        store = new ResetLocalStore(pasta);
        pedido = Pedido();
        store.Preparar(pedido);
        store.Cancelar("outro-pedido");
        Verificar(store.Pendente, "cancelamento exige o identificador correto");
        store.Cancelar(pedido.Id);
        Verificar(!store.Pendente && File.Exists(Path.Combine(pasta, "SUB/sub.sdf")), "cancelar pedido nao apaga dados");

        pasta = CriarDados("adulterado");
        store = new ResetLocalStore(pasta);
        store.Preparar(Pedido());
        File.WriteAllText(Path.Combine(pasta, ".reset-autorizado.dat"), "INVALIDO");
        DeveFalhar(() => store.ConcluirSePendente(), "pedido adulterado rejeitado");
        Verificar(File.Exists(Path.Combine(pasta, "SUB/sub.sdf")), "pedido adulterado preserva banco");

        pasta = CriarDados("expirado");
        store = new ResetLocalStore(pasta);
        pedido = Pedido();
        pedido.CriadoEmUtc = DateTime.UtcNow.AddMinutes(-11);
        store.Preparar(pedido);
        DeveFalhar(() => store.ConcluirSePendente(), "pedido nao iniciado expira em dez minutos");
        Verificar(!store.Pendente && File.Exists(Path.Combine(pasta, "SUB/sub.sdf")), "pedido expirado nao apaga dados");

        pasta = CriarDados("retomada");
        store = new ResetLocalStore(pasta);
        pedido = Pedido();
        store.Preparar(pedido);
        pedido.Iniciado = true;
        pedido.CriadoEmUtc = DateTime.UtcNow.AddDays(-1);
        typeof(ResetLocalStore).GetMethod("Gravar", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(store, new object[] { pedido });
        File.Delete(Path.Combine(pasta, "instituicao.json"));
        Verificar(store.ConcluirSePendente() && Directory.GetFileSystemEntries(pasta).Length == 0,
            "pedido iniciado retoma limpeza parcial antes do primeiro uso");

        pasta = CriarDados("redirecionamento");
        store = new ResetLocalStore(pasta);
        store.Preparar(Pedido());
        string fora = Path.Combine(raiz, "fora-do-sub");
        Directory.CreateDirectory(fora);
        File.WriteAllText(Path.Combine(fora, "preservar.txt"), "PRESERVAR");
        string link = Path.Combine(pasta, "atalho");
        using (var processo = Process.Start(new ProcessStartInfo("cmd.exe", "/c mklink /J \"" + link + "\" \"" + fora + "\"") {
            UseShellExecute = false, CreateNoWindow = true, RedirectStandardOutput = true, RedirectStandardError = true }))
        {
            processo.WaitForExit();
            Verificar(processo.ExitCode == 0, "preparacao do teste de redirecionamento");
        }
        DeveFalhar(() => store.ConcluirSePendente(), "redirecionamento bloqueia limpeza antes das exclusoes");
        Verificar(File.Exists(Path.Combine(fora, "preservar.txt")) && File.Exists(Path.Combine(pasta, "SUB/sub.sdf")),
            "arquivos externos e internos preservados ao detectar redirecionamento");

        Application.EnableVisualStyles();
        TestarTermoEArquivo();
        using (var form = new ConfiguracaoForm())
        {
            ThemeManager.Apply(form, ThemeManager.DefaultColor);
            // Não carrega a configuração real ao exibir o formulário de teste.
            typeof(ConfiguracaoForm).GetField("valoresCarregados", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(form, true);
            var campoNome = form.Controls.Find("c", true).Single();
            campoNome.Text = "Instituição de teste";
            var temas = (ComboBox)form.Controls.Find("cbTemas", true).Single();
            temas.Items.Add(ThemeManager.Presets[0]);
            temas.SelectedIndex = 0;
            form.ShowInTaskbar = false;
            form.Opacity = 0;
            form.Show();
            Application.DoEvents();
            var layout = typeof(ConfiguracaoForm).GetMethod("AjustarLayout", BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (Size tamanho in new[] { new Size(1280, 845), new Size(800, 600), new Size(640, 480) })
            {
                form.ClientSize = tamanho;
                layout.Invoke(form, null);
                Verificar(!form.AutoScroll && new[] { "Titulo", "grpInstituicao", "grpAtualizacao", "btnSalvar", "btnApagarDados" }
                    .All(nome => form.ClientRectangle.Contains(form.Controls.Find(nome, true).Single().Bounds)),
                    "configuracoes sem scroll e controles dentro da tela " + tamanho);
                var reset = form.Controls.Find("btnApagarDados", true).Single();
                var salvar = form.Controls.Find("btnSalvar", true).Single();
                var grupo = form.Controls.Find("grpAtualizacao", true).Single();
                Verificar(reset.Bottom <= tamanho.Height && !reset.Bounds.IntersectsWith(salvar.Bounds) &&
                    !reset.Bounds.IntersectsWith(grupo.Bounds), "rodape acessivel sem sobreposicao " + tamanho);
                using (var bitmap = new Bitmap(tamanho.Width, tamanho.Height)) {
                    form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, tamanho));
                    bitmap.Save(Path.Combine(raiz, "configuracoes-" + tamanho.Width + "x" + tamanho.Height + ".png"));
                }
            }
        }
    }

    static Control Campo(Control form, string nome)
    {
        return form.Controls.Find(nome, true).Single();
    }

    static void TestarTermoEArquivo()
    {
        string pasta = Path.Combine(raiz, "arquivos-recuperacao");
        Directory.CreateDirectory(pasta);
        string codigo = new string('A', 48);
        string conteudo = "Código de recuperação do SUB\r\nInstituição: Escola de teste\r\nCódigo: " + codigo +
            "\r\n\r\nGuarde este arquivo em local seguro.";
        string valido = Path.Combine(pasta, "recuperacao.txt");
        File.WriteAllText(valido, conteudo, new UTF8Encoding(true));
        Verificar(CodigoMestreArquivo.Ler(valido) == codigo, "le TXT original exportado com UTF-8 BOM");
        string simples = Path.Combine(pasta, "somente-codigo.txt");
        File.WriteAllText(simples, codigo.ToLowerInvariant());
        Verificar(CodigoMestreArquivo.Ler(simples) == codigo, "aceita TXT contendo somente o codigo completo");
        string unicode = Path.Combine(pasta, "unicode.txt");
        File.WriteAllText(unicode, conteudo, Encoding.Unicode);
        Verificar(CodigoMestreArquivo.Ler(unicode) == codigo, "reconhece TXT salvo com BOM Unicode");
        string invalido = Path.Combine(pasta, "invalido.txt");
        File.WriteAllText(invalido, "nao e um codigo de recuperacao");
        DeveFalhar(() => CodigoMestreArquivo.Ler(invalido), "TXT sem codigo nao e aceito");
        string duplicado = Path.Combine(pasta, "duplicado.txt");
        File.WriteAllText(duplicado, "Código: " + codigo + "\r\nCódigo: " + new string('B', 48));
        DeveFalhar(() => CodigoMestreArquivo.Ler(duplicado), "arquivo com dois codigos e rejeitado");
        string vazio = Path.Combine(pasta, "vazio.txt");
        File.WriteAllText(vazio, "");
        DeveFalhar(() => CodigoMestreArquivo.Ler(vazio), "TXT vazio rejeitado");
        string grande = Path.Combine(pasta, "grande.txt");
        File.WriteAllText(grande, new string('X', 16385));
        DeveFalhar(() => CodigoMestreArquivo.Ler(grande), "arquivo acima do limite rejeitado");
        string extensao = Path.Combine(pasta, "codigo.csv");
        File.WriteAllText(extensao, codigo);
        DeveFalhar(() => CodigoMestreArquivo.Ler(extensao), "extensao diferente de TXT rejeitada");
        DeveFalhar(() => CodigoMestreArquivo.Ler(Path.Combine(pasta, "inexistente.txt")), "arquivo inexistente nao autoriza");

        string hash, salt;
        CriptografiaSenha.CriarHash(codigo, out hash, out salt);
        var config = new InstituicaoConfiguracao { CodigoMestreHash = hash, CodigoMestreSalt = salt };
        string outro = Path.Combine(pasta, "outra-instalacao.txt");
        File.WriteAllText(outro, "Código: " + new string('B', 48));
        Verificar(!ResetService.ValidarCodigoMestre(config, CodigoMestreArquivo.Ler(outro)), "codigo de outra instalacao nao corresponde ao hash");
        Verificar(ResetService.ValidarCodigoMestre(config, CodigoMestreArquivo.Ler(valido)), "codigo do TXT validado contra hash da instalacao");

        var registrar = typeof(ResetService).GetMethod("RegistrarFalha", BindingFlags.Static | BindingFlags.NonPublic);
        for (int i = 0; i < 5; i++) registrar.Invoke(null, new object[] { "falha de teste" });
        DeveFalhar(() => ResetService.ValidarArquivoCodigo(valido), "cinco falhas bloqueiam tambem a importacao do TXT");
        typeof(ResetService).GetField("bloqueadoAteUtc", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, default(DateTime));
        Sessao.Limpar();
        DeveFalhar(() => ResetService.ValidarArquivoCodigo(valido), "importacao sem sessao nao consulta dados reais");

        Func<string, string> validar = caminho => {
            string valor = CodigoMestreArquivo.Ler(caminho);
            if (!ResetService.ValidarCodigoMestre(config, valor))
                throw new InvalidOperationException("TXT de outra instalação.");
            return valor;
        };
        var importar = typeof(ResetTermoForm).GetMethod("ImportarArquivo", BindingFlags.NonPublic | BindingFlags.Instance);
        var validador = typeof(ResetTermoForm).GetField("ValidarArquivo", BindingFlags.NonPublic | BindingFlags.Instance);
        var codigoForm = typeof(ResetTermoForm).GetProperty("CodigoMestre", BindingFlags.NonPublic | BindingFlags.Instance);
        using (var form = new ResetTermoForm())
        {
            validador.SetValue(form, validar);
            form.Resumo = new ResumoReset { Usuarios = 42, Titulos = 18, Emprestimos = 24, EmprestimosRapidos = 5, Turmas = 6 };
            form.Opacity = 0;
            form.Show();
            Application.DoEvents();
            Verificar(Campo(form, "lblUsuariosValor").Text == "42" && Campo(form, "lblTitulosValor").Text == "18" &&
                Campo(form, "lblTurmasValor").Text == "6", "termo mostra o resumo fornecido pela instalacao");
            var continuar = (Button)Campo(form, "btnContinuar");
            var concordo = (CheckBox)Campo(form, "chkConcordo");
            Verificar(!continuar.Enabled && !concordo.Checked, "termo inicia sem aceite e sem autorizacao");
            concordo.Checked = true;
            Verificar(!continuar.Enabled, "aceite sozinho nao libera proxima etapa");
            Verificar((bool)importar.Invoke(form, new object[] { valido }) && continuar.Enabled, "aceite e TXT correto liberam etapa da senha");
            Verificar(Campo(form, "lblArquivo").Text == "recuperacao.txt" &&
                !Campo(form, "lblStatusArquivo").Text.Contains(codigo), "codigo mestre nao e exibido no popup");
            concordo.Checked = false;
            Verificar(!continuar.Enabled, "retirar aceite desabilita proxima etapa");
            concordo.Checked = true;
            Verificar(!(bool)importar.Invoke(form, new object[] { outro }) && !continuar.Enabled && codigoForm.GetValue(form, null) == null,
                "trocar para TXT incorreto limpa validacao anterior");
            importar.Invoke(form, new object[] { valido });
            File.WriteAllText(valido, "arquivo alterado depois da selecao");
            continuar.PerformClick();
            Verificar(form.DialogResult != DialogResult.OK && !continuar.Enabled,
                "arquivo alterado e conferido novamente antes de pedir senha");
            File.WriteAllText(valido, conteudo, new UTF8Encoding(true));
            importar.Invoke(form, new object[] { valido });
            foreach (var tema in ThemeManager.Presets)
            {
                form.AplicarTema(tema.PrimaryColor);
                Verificar(Campo(form, "pnlHeader").BackColor == tema.PrimaryColor &&
                    Campo(form, "btnSelecionarTxt").BackColor == tema.PrimaryColor && continuar.BackColor == tema.PrimaryColor,
                    "popup acompanha tema " + tema.Name);
                Verificar(Campo(form, "lblAviso").ForeColor == Color.FromArgb(150, 40, 40), "aviso definitivo preservado no tema " + tema.Name);
                using (var bitmap = new Bitmap(form.Width, form.Height))
                {
                    form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
                    bitmap.Save(Path.Combine(raiz, "termo-reset-" + tema.Name.Split(' ')[0] + ".png"));
                }
            }
            Verificar(!form.AutoScroll && form.Controls.Cast<Control>().All(c => form.ClientRectangle.Contains(c.Bounds)), "popup sem controles cortados");
            ((Button)Campo(form, "btnCancelar")).PerformClick();
            Verificar(form.DialogResult == DialogResult.Cancel, "cancelar termo nao autoriza reset");
        }

        using (var form = new ResetTermoForm())
        {
            validador.SetValue(form, validar);
            form.Opacity = 0;
            form.Shown += (s,e) => {
                importar.Invoke(form, new object[] { valido });
                ((CheckBox)Campo(form, "chkConcordo")).Checked = true;
                ((Button)Campo(form, "btnContinuar")).PerformClick();
            };
            Verificar(form.ShowDialog() == DialogResult.OK && (string)codigoForm.GetValue(form, null) == codigo,
                "popup modal retorna codigo validado para o PasswordForm");
        }

        using (var superficie = new DesignSurface())
        {
            superficie.BeginLoad(typeof(ResetTermoForm));
            Verificar(superficie.IsLoaded && superficie.View is Control, "formulario pode ser carregado no Designer WinForms");
        }
        using (var formulario = new ResetTermoForm())
        {
            var introducao = Campo(formulario, "lblIntroducao");
            introducao.Text = "Texto personalizado pelo Designer";
            introducao.Location = new Point(40, 104);
            string textoPersonalizado = introducao.Text;
            Point posicaoPersonalizada = introducao.Location;
            formulario.AplicarTema(ThemeManager.Presets[1].PrimaryColor);
            Verificar(introducao.Text == textoPersonalizado && introducao.Location == posicaoPersonalizada,
                "troca de tema preserva texto e posicao personalizados no Designer");
        }
    }
}
