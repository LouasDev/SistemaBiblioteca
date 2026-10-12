using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text.RegularExpressions;

namespace SUB.Utils
{
    /// <summary>
    /// Fonte única das turmas da instituição. O SUB não inclui turmas pré-cadastradas:
    /// cada instalação começa vazia e a própria escola define sua estrutura.
    /// </summary>
    public static class TurmasUtil
    {
        public static List<string> TurmasPermitidas
        {
            get { return Carregar(); }
        }

        public static List<string> Carregar()
        {
            var turmas = new List<string>();

            using (var conexao = Conexao.ObterConexao())
            using (var comando = new SqlCeCommand("SELECT Nome FROM Turmas ORDER BY Nome", conexao))
            {
                conexao.Open();
                using (var leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                        turmas.Add(leitor.GetString(0));
                }
            }

            return turmas;
        }

        public static void Salvar(IEnumerable<string> turmas, IDictionary<string, string> renomeacoes = null)
        {
            var lista = NormalizarLista(turmas);

            using (var conexao = Conexao.ObterConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        foreach (var renomeacao in renomeacoes ?? new Dictionary<string, string>())
                        {
                            if (string.Equals(renomeacao.Key, renomeacao.Value, StringComparison.OrdinalIgnoreCase))
                                continue;

                            using (var atualizarUsuarios = new SqlCeCommand(
                                "UPDATE usuarios SET Turma = @novaTurma WHERE Turma = @turmaAnterior", conexao, transacao))
                            {
                                atualizarUsuarios.Parameters.AddWithValue("@novaTurma", renomeacao.Value);
                                atualizarUsuarios.Parameters.AddWithValue("@turmaAnterior", renomeacao.Key);
                                atualizarUsuarios.ExecuteNonQuery();
                            }
                        }

                        using (var limpar = new SqlCeCommand("DELETE FROM Turmas", conexao, transacao))
                            limpar.ExecuteNonQuery();

                        foreach (string turma in lista)
                        {
                            using (var inserir = new SqlCeCommand("INSERT INTO Turmas (Nome) VALUES (@nome)", conexao, transacao))
                            {
                                inserir.Parameters.AddWithValue("@nome", turma);
                                inserir.ExecuteNonQuery();
                            }
                        }

                        transacao.Commit();
                    }
                    catch
                    {
                        try { transacao.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        public static bool Existe(string turma)
        {
            return !string.IsNullOrWhiteSpace(turma) &&
                   Carregar().Contains(turma.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        public static bool EmUso(string turma)
        {
            if (string.IsNullOrWhiteSpace(turma))
                return false;

            using (var conexao = Conexao.ObterConexao())
            using (var comando = new SqlCeCommand("SELECT COUNT(*) FROM usuarios WHERE Turma = @turma", conexao))
            {
                comando.Parameters.AddWithValue("@turma", turma.Trim());
                conexao.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        public static List<string> BuscarSugestoes(string texto)
        {
            var turmas = Carregar();
            if (string.IsNullOrWhiteSpace(texto))
                return new List<string>();

            string busca = Normalizar(texto);
            return turmas
                .Select(turma => new { Turma = turma, Normalizada = Normalizar(turma) })
                .Where(item => item.Normalizada.StartsWith(busca, StringComparison.OrdinalIgnoreCase) ||
                               item.Normalizada.IndexOf(busca, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(item => item.Normalizada.StartsWith(busca, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                .ThenBy(item => item.Turma, StringComparer.OrdinalIgnoreCase)
                .Select(item => item.Turma)
                .ToList();
        }

        private static List<string> NormalizarLista(IEnumerable<string> turmas)
        {
            return (turmas ?? Enumerable.Empty<string>())
                .Where(turma => !string.IsNullOrWhiteSpace(turma))
                .Select(turma => turma.Trim().ToUpperInvariant())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(turma => turma, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
        }

        private static string Normalizar(string valor)
        {
            string texto = (valor ?? string.Empty).Trim().ToLowerInvariant()
                .Normalize(System.Text.NormalizationForm.FormD);
            texto = Regex.Replace(texto, @"\p{M}", string.Empty);
            return Regex.Replace(texto, @"\s+", " ");
        }
    }
}
