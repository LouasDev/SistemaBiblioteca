using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Reflection;
using SUB.Utils;

class TesteProrrogacao
{
    static int testes;
    static string bin;
    static string raiz;
    static readonly DateTime Hoje = new DateTime(2026, 10, 27, 10, 0, 0);

    static void Main(string[] args)
    {
        bin = Path.GetFullPath(args[0]);
        raiz = Path.GetFullPath(args[1]);
        if (Directory.Exists(raiz)) throw new Exception("Use uma pasta nova para os dados fictícios.");
        Directory.CreateDirectory(raiz);
        AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
            string caminho = Path.Combine(bin, new AssemblyName(e.Name).Name + ".exe");
            if (!File.Exists(caminho)) caminho = Path.ChangeExtension(caminho, ".dll");
            return File.Exists(caminho) ? Assembly.LoadFrom(caminho) : null;
        };
        Executar();
        Console.WriteLine("TODOS OS " + testes + " TESTES PASSARAM");
    }

    static void Verificar(bool resultado, string nome)
    {
        if (!resultado) throw new Exception(nome);
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

    static SqlCeConnection Banco(string nome)
    {
        string caminho = Path.Combine(raiz, nome + ".sdf");
        string connectionString = "Data Source=" + caminho;
        using (var engine = new SqlCeEngine(connectionString)) engine.CreateDatabase();
        var c = new SqlCeConnection(connectionString);
        c.Open();
        return c;
    }

    static void Preparar(SqlCeConnection c)
    {
        // Chama somente a criação do esquema com a conexão fictícia fornecida.
        // Não chama EnsureCreated, que usa o caminho da instalação real.
        var metodo = typeof(DatabaseInitializer).GetMethod("CreateSchema", BindingFlags.Static | BindingFlags.NonPublic);
        metodo.Invoke(null, new object[] { c });
    }

    static int Emprestimo(SqlCeConnection c, string status, DateTime? prazo, bool devolvido)
    {
        using (var cmd = new SqlCeCommand(@"INSERT INTO Emprestimo
            (UsuarioId, LivroId, Alocador, DataEmprestimo, DataDevolucao, DataRealDevolucao, Status)
            VALUES (11, 22, 11, @Data, @Prazo, @Real, @Status)", c))
        {
            cmd.Parameters.AddWithValue("@Data", Hoje.AddDays(-7));
            cmd.Parameters.AddWithValue("@Prazo", prazo.HasValue ? (object)prazo.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@Real", devolvido ? (object)Hoje : DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.ExecuteNonQuery();
        }
        return Convert.ToInt32(Valor(c, "SELECT @@IDENTITY"));
    }

    static void DeveRecusar(SqlCeConnection c, int id, int dias, DateTime prazo, string nome)
    {
        object prazoAntes = Valor(c, "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id);
        object statusAntes = Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + id);
        int logsAntes = Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes"));
        bool recusou = false;
        try { ProrrogacaoService.Prorrogar(c, id, dias, prazo, "Bibliotecaria Teste", Hoje); }
        catch (InvalidOperationException) { recusou = true; }
        Verificar(recusou && Equals(prazoAntes, Valor(c, "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id))
            && Equals(statusAntes, Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + id))
            && logsAntes == Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")), nome);
    }

    static void Executar()
    {
        using (var c = Banco("novo"))
        {
            Preparar(c);
            Preparar(c);
            Verificar(Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM SchemaInfo WHERE Versao = 5")) == 1,
                "migracao repetida registra versao uma unica vez");
            int id = Emprestimo(c, "Ativo", Hoje, false);
            ProrrogacaoService.Prorrogar(c, id, 7, Hoje, "Bibliotecaria Teste", Hoje);
            Verificar((DateTime)Valor(c, "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id) == Hoje.AddDays(7),
                "base nova permite prorrogar por sete dias");
            Verificar((DateTime)Valor(c, "SELECT DataProrrogacao FROM Emprestimo WHERE Id = " + id) == Hoje.AddDays(7),
                "prazo utilizado pelo painel de atrasos acompanha a prorrogacao");
            Verificar((DateTime)Valor(c, "SELECT DataHora FROM LogProrrogacoes") == Hoje
                && (DateTime)Valor(c, "SELECT DataDaAcao FROM LogProrrogacoes") == Hoje,
                "historico preenche as duas datas inclusive campo obrigatorio antigo");
            Verificar((DateTime)Valor(c, "SELECT NovaDataDevolucao FROM LogProrrogacoes") == Hoje.AddDays(7)
                && (string)Valor(c, "SELECT BibliotecariaNome FROM LogProrrogacoes") == "Bibliotecaria Teste"
                && Convert.ToInt32(Valor(c, "SELECT UsuarioId FROM LogProrrogacoes")) == 11,
                "historico guarda prazo usuario e bibliotecaria");
            DeveRecusar(c, id, 7, Hoje, "confirmacao antiga ou clique repetido nao prorroga novamente");
            ProrrogacaoService.Prorrogar(c, id, 14, Hoje.AddDays(7), "Outra Bibliotecaria", Hoje);
            Verificar(Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")) == 2,
                "nova confirmacao permite segunda prorrogacao e preserva primeiro historico");

            int vencido = Emprestimo(c, "Atrasado", Hoje.AddDays(-20), false);
            ProrrogacaoService.Prorrogar(c, vencido, 1, Hoje.AddDays(-20), "Teste", Hoje);
            Verificar((string)Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + vencido) == "Atrasado",
                "somar dias ao vencimento mantem atraso se novo prazo ainda passou");
            int venceHoje = Emprestimo(c, "Atrasado", Hoje.AddDays(-7), false);
            ProrrogacaoService.Prorrogar(c, venceHoje, 7, Hoje.AddDays(-7), "Teste", Hoje);
            Verificar((string)Valor(c, "SELECT Status FROM Emprestimo WHERE Id = " + venceHoje) == "Ativo",
                "novo prazo no dia atual retorna a ativo conforme regra existente");

            DeveRecusar(c, Emprestimo(c, "Devolvido", Hoje, true), 7, Hoje, "livro devolvido nao reabre");
            DeveRecusar(c, Emprestimo(c, "Ativo", Hoje, true), 7, Hoje, "data real preenchida bloqueia mesmo com status ativo");
            DeveRecusar(c, Emprestimo(c, "Finalizado", Hoje, false), 7, Hoje, "status encerrado sem data real tambem bloqueia");
            DeveRecusar(c, Emprestimo(c, "Cancelado", Hoje, false), 7, Hoje, "emprestimo cancelado nao reabre");
            DeveRecusar(c, Emprestimo(c, "Ativo", null, false), 7, Hoje, "prazo ausente nao e inventado");
            DeveRecusar(c, 99999, 7, Hoje, "emprestimo inexistente nao cria historico");
            DeveRecusar(c, id, 0, Hoje.AddDays(21), "zero dias recusado");
            DeveRecusar(c, id, -1, Hoje.AddDays(21), "dias negativos recusados");
            DeveRecusar(c, id, 15, Hoje.AddDays(21), "mais de quatorze dias recusados");

            int antesFalha = Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes"));
            // Provoca uma falha SQL real somente depois do UPDATE do prazo.
            Sql(c, "ALTER TABLE LogProrrogacoes DROP COLUMN NovaDataDevolucao");
            bool falhou = false;
            try { ProrrogacaoService.Prorrogar(c, id, 7, Hoje.AddDays(21), "Nome maior que o campo", Hoje); }
            catch (SqlCeException) { falhou = true; }
            Verificar(falhou && (DateTime)Valor(c, "SELECT DataDevolucao FROM Emprestimo WHERE Id = " + id) == Hoje.AddDays(21)
                && (DateTime)Valor(c, "SELECT DataProrrogacao FROM Emprestimo WHERE Id = " + id) == Hoje.AddDays(21)
                && antesFalha == Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")),
                "falha no historico desfaz prazo e nao cria registro parcial");
        }

        using (var c = Banco("historico-datahora"))
        {
            Sql(c, @"CREATE TABLE LogProrrogacoes (Id int IDENTITY PRIMARY KEY, EmprestimoId int NULL,
                UsuarioId int NULL, DataHora datetime NOT NULL, Observacao nvarchar(500) NULL)");
            Sql(c, "INSERT INTO LogProrrogacoes (EmprestimoId, DataHora, Observacao) VALUES (123, '20200102', 'Preservar')");
            Preparar(c);
            Preparar(c);
            Verificar(Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")) == 1
                && (string)Valor(c, "SELECT Observacao FROM LogProrrogacoes") == "Preservar"
                && (DateTime)Valor(c, "SELECT DataDaAcao FROM LogProrrogacoes") == new DateTime(2020, 1, 2),
                "historico DataHora preservado e visivel na data preferida pelo relatorio");
            Verificar(Valor(c, "SELECT BibliotecariaNome FROM LogProrrogacoes") == DBNull.Value
                && Valor(c, "SELECT NovaDataDevolucao FROM LogProrrogacoes") == DBNull.Value,
                "migracao nao inventa bibliotecaria ou prazo de registros antigos");
            int id = Emprestimo(c, "Ativo", Hoje, false);
            ProrrogacaoService.Prorrogar(c, id, 7, Hoje, "Teste", Hoje);
            Verificar(Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")) == 2,
                "base migrada com DataHora obrigatorio aceita novo registro");
        }

        using (var c = Banco("historico-datadaacao"))
        {
            Sql(c, @"CREATE TABLE LogProrrogacoes (LogId int IDENTITY PRIMARY KEY, EmprestimoId int NULL,
                DataDaAcao datetime NOT NULL, NovaDataDevolucao datetime NULL, BibliotecariaNome nvarchar(200) NULL)");
            Sql(c, @"INSERT INTO LogProrrogacoes (EmprestimoId, DataDaAcao, NovaDataDevolucao, BibliotecariaNome)
                VALUES (456, '20200103', '20200110', 'Nome antigo')");
            Preparar(c);
            Preparar(c);
            Verificar((string)Valor(c, "SELECT BibliotecariaNome FROM LogProrrogacoes") == "Nome antigo"
                && (DateTime)Valor(c, "SELECT NovaDataDevolucao FROM LogProrrogacoes") == new DateTime(2020, 1, 10)
                && (DateTime)Valor(c, "SELECT DataHora FROM LogProrrogacoes") == new DateTime(2020, 1, 3),
                "formato legado com LogId mantem autor prazo e data");
            int id = Emprestimo(c, "Ativo", Hoje, false);
            ProrrogacaoService.Prorrogar(c, id, 7, Hoje, "Teste", Hoje);
            Verificar(Convert.ToInt32(Valor(c, "SELECT COUNT(*) FROM LogProrrogacoes")) == 2,
                "formato legado DataDaAcao aceita novo registro sem trocar identificadores");
        }
    }
}
