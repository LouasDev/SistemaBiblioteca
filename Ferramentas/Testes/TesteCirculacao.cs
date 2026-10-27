using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;
using System.Threading;
using SUB.Utils;

class TesteCirculacao
{
    static string bin, raiz;
    static int testes;
    static readonly DateTime Agora = new DateTime(2026, 10, 27, 10, 0, 0);

    static void Main(string[] args)
    {
        bin = Path.GetFullPath(args[0]);
        raiz = Path.GetFullPath(args[1]);
        if (Directory.Exists(raiz)) throw new Exception("Use uma pasta nova para os testes.");
        Directory.CreateDirectory(raiz);
        AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
            string path = Path.Combine(bin, new AssemblyName(e.Name).Name + ".exe");
            if (!File.Exists(path)) path = Path.ChangeExtension(path, ".dll");
            return File.Exists(path) ? Assembly.LoadFrom(path) : null;
        };
        RegrasCompartilhadas();
        FluxoNormal();
        FluxoRapido();
        Falhas();
        Painel();
        DisputaUltimoExemplar();
        Console.WriteLine("TODOS OS " + testes + " TESTES PASSARAM");
    }

    static void Verificar(bool ok, string nome)
    {
        if (!ok) throw new Exception(nome);
        testes++;
        Console.WriteLine("PASS " + nome);
    }

    static void Sql(SqlCeConnection c, string sql)
    {
        using (var cmd = new SqlCeCommand(sql, c)) cmd.ExecuteNonQuery();
    }

    static object Valor(SqlCeConnection c, string sql)
    {
        using (var cmd = new SqlCeCommand(sql, c)) return cmd.ExecuteScalar();
    }

    static int Numero(SqlCeConnection c, string sql) { return Convert.ToInt32(Valor(c, sql)); }
    static int Estoque(SqlCeConnection c) { return Numero(c, "SELECT Quantidade FROM Livros WHERE Id = 1"); }
    static int Id(SqlCeConnection c) { return Numero(c, "SELECT @@IDENTITY"); }

    static SqlCeConnection Banco(string nome, int estoque)
    {
        string caminho = Path.Combine(raiz, nome + ".sdf");
        string cs = "Data Source=" + caminho;
        using (var engine = new SqlCeEngine(cs)) engine.CreateDatabase();
        var c = new SqlCeConnection(cs);
        c.Open();
        // Somente a conexão fictícia chega ao inicializador. Nunca chama EnsureCreated.
        typeof(DatabaseInitializer).GetMethod("CreateSchema", BindingFlags.Static | BindingFlags.NonPublic)
            .Invoke(null, new object[] { c });
        Sql(c, "INSERT INTO Usuarios (Nome, TipoUsuario, Ativo) VALUES ('Bibliotecaria Teste', 'Bibliotecário(a)', 1)");
        Sql(c, "INSERT INTO Usuarios (Nome, TipoUsuario, Ativo) VALUES ('Professor Teste', 'Professor(a)', 1)");
        Sql(c, "INSERT INTO Usuarios (Nome, TipoUsuario, Ativo) VALUES ('Aluno Teste', 'Aluno(a)', 1)");
        Sql(c, "INSERT INTO Livros (Nome, Genero, Quantidade, Disponibilidade, Ativo) VALUES ('Didatico Teste', 'Didático', " + estoque + ", " + (estoque > 0 ? 1 : 0) + ", 1)");
        return c;
    }

    static void Recusar(Action acao, string nome)
    {
        bool falhou = false;
        try { acao(); } catch (InvalidOperationException) { falhou = true; }
        Verificar(falhou, nome);
    }

    static void FalharSql(Action acao, string nome)
    {
        bool falhou = false;
        try { acao(); } catch (SqlCeException) { falhou = true; }
        Verificar(falhou, nome);
    }

    static void Emprestar(SqlCeConnection c, int usuario)
    {
        CirculacaoService.EmprestarNormal(c, usuario, 1, 1, Agora.AddDays(7), Agora);
    }

    static int Rapido(SqlCeConnection c, int quantidade)
    {
        CirculacaoService.EmprestarRapido(c, "Professor Teste", "Didatico Teste", "Turma A", quantidade, "Bibliotecaria Teste", Agora);
        return Numero(c, "SELECT MAX(Id) FROM EmprestimoRapido");
    }

    static void FluxoNormal()
    {
        using (var c = Banco("normal", 2))
        {
            Recusar(() => CirculacaoService.EmprestarNormal(c, 2, 1, 1, Agora.AddMinutes(-1), Agora),
                "servico recusa prazo anterior mesmo sem passar pelo formulario");
            Recusar(() => CirculacaoService.EmprestarNormal(c, 2, 1, 1, Agora.AddDays(1), Agora, Agora.AddDays(2)),
                "servico usa a mesma data informada que a validacao do formulario");
            Verificar(Estoque(c) == 2 && Numero(c, "SELECT COUNT(*) FROM Emprestimo") == 0,
                "validacao de datas falha antes de movimentar estoque ou criar registro");
            Emprestar(c, 3);
            int aluno = Numero(c, "SELECT MAX(Id) FROM Emprestimo");
            Verificar(Estoque(c) == 1 && Numero(c, "SELECT COUNT(*) FROM Emprestimo") == 1,
                "emprestimo normal grava registro e retira uma unidade");
            Recusar(() => Emprestar(c, 3), "limite existente de aluno conferido na transacao");
            Verificar(Estoque(c) == 1, "recusa nao altera estoque");
            Emprestar(c, 2);
            int professor = Numero(c, "SELECT MAX(Id) FROM Emprestimo");
            Verificar(Estoque(c) == 0 && Numero(c, "SELECT Disponibilidade FROM Livros WHERE Id = 1") == 0,
                "ultima unidade torna livro indisponivel");
            Recusar(() => Emprestar(c, 2), "consulta antiga nao permite emprestar sem saldo");
            Verificar(Estoque(c) == 0 && Numero(c, "SELECT COUNT(*) FROM Emprestimo") == 2,
                "estoque nao fica negativo nem cria emprestimo extra");
            CirculacaoService.DevolverNormal(c, aluno, Agora);
            Verificar(Estoque(c) == 1 && Numero(c, "SELECT Disponibilidade FROM Livros WHERE Id = 1") == 1
                && (string)Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + aluno) == "Devolvido",
                "devolucao normal atualiza estoque disponibilidade e situacao");
            Recusar(() => CirculacaoService.DevolverNormal(c, aluno, Agora), "devolucao normal repetida bloqueada no banco");
            Verificar(Estoque(c) == 1, "devolucao repetida nao duplica exemplares");
            Sql(c, "UPDATE Emprestimo SET Status = 'Atrasado' WHERE Id = " + professor);
            CirculacaoService.DevolverNormal(c, professor, Agora);
            Verificar(Estoque(c) == 2, "emprestimo atrasado tambem pode ser devolvido");
            Recusar(() => CirculacaoService.DevolverNormal(c, 999, Agora), "devolucao inexistente bloqueada");
            Sql(c, "UPDATE Usuarios SET Ativo = 0 WHERE Id = 2");
            Recusar(() => Emprestar(c, 2), "usuario inativo nao gera retirada");
            Recusar(() => CirculacaoService.EmprestarNormal(c, 3, 1, 3, Agora.AddDays(7), Agora),
                "responsavel precisa ser bibliotecario");
        }
    }

    static void RegrasCompartilhadas()
    {
        Verificar(RegrasCirculacao.PrazoPadrao(new DateTime(2028, 2, 26)) == new DateTime(2028, 3, 4),
            "prazo padrao de sete dias atravessa fevereiro bissexto");
        Verificar(RegrasCirculacao.PrazoValido(Agora, Agora)
            && !RegrasCirculacao.PrazoValido(Agora, Agora.AddTicks(-1)),
            "datas iguais sao aceitas e prazo anterior continua recusado");
        Verificar(RegrasCirculacao.ProfessorTemPrazoPersonalizado("Professor(a)")
            && !RegrasCirculacao.ProfessorTemPrazoPersonalizado("Aluno(a)"),
            "prazo personalizado obrigatorio continua exclusivo do perfil professor atual");
        Verificar(!RegrasCirculacao.AlunoAtingiuLimite("Aluno(a)", 0)
            && RegrasCirculacao.AlunoAtingiuLimite("Aluno(a)", 1)
            && !RegrasCirculacao.AlunoAtingiuLimite("Professor(a)", 2),
            "limite de um ativo aplica-se ao aluno sem limitar professor");
        Verificar(!RegrasCirculacao.AtingiuLimitePendencias(1)
            && RegrasCirculacao.AtingiuLimitePendencias(2) && RegrasCirculacao.AtingiuLimitePendencias(3),
            "duas pendencias permanecem o limite do fluxo rapido");
        Verificar(RegrasCirculacao.EmprestimoNormalAberto("Ativo")
            && RegrasCirculacao.EmprestimoNormalAberto("Atrasado")
            && !RegrasCirculacao.EmprestimoNormalAberto("Devolvido")
            && !RegrasCirculacao.EmprestimoNormalAberto(null),
            "emprestimo normal encerrado ou sem situacao nao permite movimentacao");
        Verificar(RegrasCirculacao.EmprestimoRapidoAberto("Faltando", 2)
            && !RegrasCirculacao.EmprestimoRapidoAberto("Devolvido", 2)
            && !RegrasCirculacao.EmprestimoRapidoAberto("Ativo", 0),
            "fluxo rapido exige situacao aberta e saldo positivo");
        Verificar(!RegrasCirculacao.QuantidadeRetiradaValida(0)
            && !RegrasCirculacao.QuantidadeRetiradaValida(-1)
            && RegrasCirculacao.QuantidadeRetiradaValida(1),
            "retirada exige quantidade positiva");
        Verificar(RegrasCirculacao.SaldoAposDevolucao(5, 0) == 5
            && RegrasCirculacao.SaldoAposDevolucao(5, 2) == 3
            && RegrasCirculacao.SaldoAposDevolucao(5, 5) == 0,
            "saldo compartilhado trata falta total devolucao parcial e total");
        Recusar(() => RegrasCirculacao.SaldoAposDevolucao(5, 6), "regra de saldo nao permite devolucao excedente");
        Verificar(!RegrasCirculacao.QuantidadeDevolvidaValida(0, 0)
            && !RegrasCirculacao.QuantidadeDevolvidaValida(5, -1),
            "devolucao exige pendencia e nao aceita valor negativo");
        Verificar(RegrasCirculacao.SituacaoSaldoRapido(0) == "Devolvido"
            && RegrasCirculacao.SituacaoSaldoRapido(1) == "Faltando",
            "situacao acompanha saldo restante");
        Verificar(RegrasCirculacao.SituacaoPrazo(Agora.Date, Agora.Date.AddHours(23)) == "Ativo"
            && RegrasCirculacao.SituacaoPrazo(Agora.AddDays(-1), Agora) == "Atrasado",
            "prorrogacao preserva classificacao pelo dia de vencimento");
        Verificar(RegrasCirculacao.CalcularProrrogacao(new DateTime(2026, 12, 31), 1) == new DateTime(2027, 1, 1)
            && RegrasCirculacao.CalcularProrrogacao(Agora, 14) == Agora.AddDays(14),
            "prorrogacao soma ao prazo atual e aceita limites de um e quatorze dias");
        Verificar(RegrasCirculacao.ErroProrrogacao(Agora, 0) != null
            && RegrasCirculacao.ErroProrrogacao(Agora, 15) != null,
            "limites invalidos sao recusados na previa e na gravacao");
        Verificar(RegrasCirculacao.CalcularProrrogacao(DateTime.MaxValue.AddDays(-14), 14) == DateTime.MaxValue
            && RegrasCirculacao.ErroProrrogacao(DateTime.MaxValue, 1) != null,
            "previa e servico protegem contra estouro da data maxima");
    }

    static void FluxoRapido()
    {
        using (var c = Banco("rapido", 12))
        {
            int id = Rapido(c, 10);
            Verificar(Estoque(c) == 2 && Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + id) == 10,
                "professor retira varios didaticos em uma operacao");
            Recusar(() => Rapido(c, 3), "retirada rapida maior que saldo bloqueada");
            Recusar(() => Rapido(c, 0), "retirada de zero exemplares bloqueada");
            CirculacaoService.DevolverRapido(c, id, 10, 6, Agora);
            Verificar(Estoque(c) == 8 && Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + id) == 4
                && (string)Valor(c, "SELECT Status FROM EmprestimoRapido WHERE Id = " + id) == "Faltando",
                "devolucao parcial repoe apenas recebidos e mantem saldo faltante");
            Recusar(() => CirculacaoService.DevolverRapido(c, id, 10, 6, Agora), "confirmacao rapida com saldo antigo bloqueada");
            Verificar(Estoque(c) == 8, "repetir devolucao parcial nao duplica estoque");
            Recusar(() => CirculacaoService.DevolverRapido(c, id, 4, 5, Agora), "nao aceita devolver acima do saldo");
            Recusar(() => CirculacaoService.DevolverRapido(c, id, 4, -1, Agora), "devolucao negativa bloqueada");
            CirculacaoService.DevolverRapido(c, id, 4, 4, Agora);
            Verificar(Estoque(c) == 12 && Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + id) == 0,
                "regularizacao total zera saldo e restaura estoque");
            Recusar(() => CirculacaoService.DevolverRapido(c, id, 4, 4, Agora), "devolucao rapida encerrada nao reabre");

            int primeiro = Rapido(c, 3);
            CirculacaoService.DevolverRapido(c, primeiro, 3, 0, Agora);
            int segundo = Rapido(c, 3);
            CirculacaoService.DevolverRapido(c, segundo, 3, 0, Agora);
            Recusar(() => Rapido(c, 1), "duas pendencias mantem bloqueio de novas retiradas");
            CirculacaoService.DevolverRapido(c, primeiro, 3, 1, Agora);
            Verificar(Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + primeiro) == 2,
                "limite de pendencias nao impede regularizar parcialmente registro existente");
            CirculacaoService.DevolverRapido(c, primeiro, 2, 2, Agora);
            Verificar(Rapido(c, 1) > segundo, "regularizar pendencia libera retirada e preserva sequencia de identificadores");

            Sql(c, "UPDATE Livros SET Genero = 'Romance' WHERE Id = 1");
            Recusar(() => Rapido(c, 1), "fluxo rapido continua exclusivo para didaticos");
            Sql(c, "UPDATE Livros SET Genero = 'Didático' WHERE Id = 1");
            CirculacaoService.EmprestarRapido(c, "Professor Teste", "Didatico Teste", "A", Estoque(c), "Teste", Agora);
            int ultimo = Numero(c, "SELECT MAX(Id) FROM EmprestimoRapido");
            Verificar(Numero(c, "SELECT Disponibilidade FROM Livros WHERE Id = 1") == 0, "retirada rapida da ultima unidade desativa disponibilidade");
            CirculacaoService.DevolverRapido(c, ultimo, Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + ultimo), 1, Agora);
            Verificar(Numero(c, "SELECT Disponibilidade FROM Livros WHERE Id = 1") == 1, "devolucao rapida reativa disponibilidade");
        }
    }

    static void Falhas()
    {
        using (var c = Banco("falha-emprestimo-normal", 5))
        {
            Sql(c, "ALTER TABLE Emprestimo DROP COLUMN Responsavel");
            FalharSql(() => Emprestar(c, 2), "falha SQL depois da retirada normal e detectada");
            Verificar(Estoque(c) == 5 && Numero(c, "SELECT COUNT(*) FROM Emprestimo") == 0,
                "falha ao inserir emprestimo normal desfaz retirada");
        }
        using (var c = Banco("falha-emprestimo-rapido", 5))
        {
            Sql(c, "ALTER TABLE EmprestimoRapido DROP COLUMN Bibliotecaria");
            FalharSql(() => Rapido(c, 3), "falha SQL depois da retirada rapida e detectada");
            Verificar(Estoque(c) == 5 && Numero(c, "SELECT COUNT(*) FROM EmprestimoRapido") == 0,
                "falha ao inserir emprestimo rapido desfaz retirada");
        }
        using (var c = Banco("falha-devolucao-normal", 5))
        {
            Emprestar(c, 2);
            int id = Numero(c, "SELECT MAX(Id) FROM Emprestimo");
            Sql(c, "ALTER TABLE Livros DROP COLUMN Disponibilidade");
            FalharSql(() => CirculacaoService.DevolverNormal(c, id, Agora), "falha SQL depois de marcar devolucao normal e detectada");
            Verificar(Estoque(c) == 4 && (string)Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + id) == "Ativo"
                && Valor(c, "SELECT DataRealDevolucao FROM Emprestimo WHERE Id = " + id) == DBNull.Value,
                "falha na reposicao desfaz encerramento e data da devolucao normal");
        }
        using (var c = Banco("falha-devolucao-rapida", 5))
        {
            int id = Rapido(c, 3);
            Sql(c, "ALTER TABLE Livros DROP COLUMN Disponibilidade");
            FalharSql(() => CirculacaoService.DevolverRapido(c, id, 3, 2, Agora), "falha SQL depois de atualizar saldo rapido e detectada");
            Verificar(Estoque(c) == 2 && Numero(c, "SELECT Quantidade FROM EmprestimoRapido WHERE Id = " + id) == 3
                && (string)Valor(c, "SELECT Status FROM EmprestimoRapido WHERE Id = " + id) == "Ativo"
                && Valor(c, "SELECT DataHoraDevolucaoReal FROM EmprestimoRapido WHERE Id = " + id) == DBNull.Value,
                "falha na reposicao desfaz saldo status e data da devolucao rapida");
        }
        using (var c = Banco("livro-ausente", 5))
        {
            int id = Rapido(c, 3);
            Sql(c, "DELETE FROM Livros WHERE Id = 1");
            Recusar(() => CirculacaoService.DevolverRapido(c, id, 3, 3, Agora), "livro ausente impede confirmar devolucao sem reposicao");
            Verificar((string)Valor(c, "SELECT Status FROM EmprestimoRapido WHERE Id = " + id) == "Ativo",
                "livro ausente preserva pendencia");
        }
    }

    static int RegistroPainel(SqlCeConnection c, string status, int? saldo, DateTime? devolucao)
    {
        using (var cmd = new SqlCeCommand(@"INSERT INTO EmprestimoRapido
            (ProfessorId, LivroId, LivroNome, Turma, Quantidade, DataHoraEmprestimo, DataHoraDevolucaoReal, Bibliotecaria, Status)
            VALUES (2, 1, 'Didatico Teste', 'A', @Saldo, @Emp, @Dev, 'Teste', @Status)", c))
        {
            cmd.Parameters.AddWithValue("@Saldo", saldo.HasValue ? (object)saldo.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Emp", Agora.AddDays(-30));
            cmd.Parameters.AddWithValue("@Dev", devolucao.HasValue ? (object)devolucao.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", status == null ? (object)DBNull.Value : status);
            cmd.ExecuteNonQuery();
        }
        return Numero(c, "SELECT MAX(Id) FROM EmprestimoRapido");
    }

    static HashSet<int> PainelIds(SqlCeConnection c, DateTime data)
    {
        var ids = new HashSet<int>();
        using (var cmd = CirculacaoService.CriarConsultaPainelRapido(c, data))
        using (var r = cmd.ExecuteReader()) while (r.Read()) ids.Add(r.GetInt32(0));
        return ids;
    }

    static void Painel()
    {
        using (var c = Banco("painel-semanal", 5))
        {
            DateTime segunda = new DateTime(2026, 10, 26);
            int antigo = RegistroPainel(c, "Devolvido", 0, segunda.AddSeconds(-1));
            int atual = RegistroPainel(c, "Devolvido", 0, segunda);
            int ativo = RegistroPainel(c, "Ativo", 4, null);
            int faltando = RegistroPainel(c, "Faltando", 2, segunda.AddDays(-20));
            int semData = RegistroPainel(c, "Devolvido", 0, null);
            int contraditorio = RegistroPainel(c, "Devolvido", 2, segunda.AddDays(-20));
            int semSaldo = RegistroPainel(c, "Devolvido", null, segunda.AddDays(-20));
            int semStatus = RegistroPainel(c, null, 1, null);
            var ids = PainelIds(c, Agora);
            Verificar(!ids.Contains(antigo) && ids.Contains(atual), "painel oculta somente devolvidos de semanas anteriores");
            Verificar(ids.Contains(ativo) && ids.Contains(faltando), "pendentes antigos continuam no painel");
            Verificar(ids.Contains(semData) && ids.Contains(contraditorio) && ids.Contains(semSaldo) && ids.Contains(semStatus),
                "dados incompletos ou contraditorios nao sao ocultados");
            Verificar(PainelIds(c, segunda.AddDays(6)).Contains(atual), "domingo pertence a mesma semana");
            Verificar(!PainelIds(c, segunda.AddDays(7)).Contains(atual), "proxima segunda oculta devolvido sem excluir registro");
            Verificar(Numero(c, "SELECT COUNT(*) FROM EmprestimoRapido") == 8, "consulta semanal nao apaga historico");
            Verificar(PainelIds(c, new DateTime(2027, 1, 4)).Contains(faltando), "virada do ano nao elimina pendencias");
            Sql(c, "DELETE FROM Usuarios WHERE Id = 2");
            Verificar(PainelIds(c, Agora).Contains(ativo), "cadastro do professor ausente nao esconde pendencia");
            Verificar(CirculacaoService.InicioSemana(new DateTime(2027, 1, 3)) == new DateTime(2026, 12, 28),
                "limite semanal atravessa o ano corretamente");
        }
    }

    static void DisputaUltimoExemplar()
    {
        using (var c = Banco("concorrencia", 1))
        using (var largada = new ManualResetEvent(false))
        {
            string cs = c.ConnectionString;
            int sucessos = 0, recusas = 0;
            Exception inesperada = null;
            ThreadStart retirar = () => {
                try {
                    using (var outra = new SqlCeConnection(cs)) {
                        outra.Open();
                        largada.WaitOne();
                        Emprestar(outra, 2);
                        Interlocked.Increment(ref sucessos);
                    }
                } catch (InvalidOperationException) { Interlocked.Increment(ref recusas); }
                catch (SqlCeException) { Interlocked.Increment(ref recusas); }
                catch (Exception ex) { inesperada = ex; }
            };
            var a = new Thread(retirar);
            var b = new Thread(retirar);
            a.Start(); b.Start(); largada.Set();
            a.Join(); b.Join();
            if (inesperada != null) throw inesperada;
            Verificar(sucessos == 1 && recusas == 1 && Estoque(c) == 0
                && Numero(c, "SELECT COUNT(*) FROM Emprestimo") == 1,
                "duas conexoes disputando ultima unidade nao vendem exemplar inexistente");
        }
    }
}
