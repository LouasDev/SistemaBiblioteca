using System;
using System.Data.SqlServerCe;

namespace SUB.Utils
{
    internal static class ProrrogacaoService
    {
        // A conexão é fornecida pelo chamador; os testes usam somente bancos fictícios.
        internal static void Prorrogar(SqlCeConnection conexao, int emprestimoId, int dias,
            DateTime prazoConfirmado, string bibliotecaria, DateTime agora)
        {
            RegrasCirculacao.CalcularProrrogacao(prazoConfirmado, dias);
            if (string.IsNullOrWhiteSpace(bibliotecaria) || bibliotecaria.Length > 200)
                throw new InvalidOperationException("Não foi possível identificar a bibliotecária. Entre novamente no sistema.");

            using (var transacao = conexao.BeginTransaction())
            {
                DateTime prazoAtual;
                object usuario;
                using (var consulta = new SqlCeCommand(@"
                    SELECT DataDevolucao, Status, DataRealDevolucao, UsuarioId
                    FROM Emprestimo WHERE Id = @Id", conexao, transacao))
                {
                    consulta.Parameters.AddWithValue("@Id", emprestimoId);
                    using (var leitor = consulta.ExecuteReader())
                    {
                        if (!leitor.Read())
                            throw new InvalidOperationException("O empréstimo não foi localizado. Atualize a lista.");
                        string status = leitor.IsDBNull(1) ? "" : leitor.GetString(1).Trim();
                        if (!leitor.IsDBNull(2) || !RegrasCirculacao.EmprestimoNormalAberto(status))
                            throw new InvalidOperationException("Somente empréstimos em aberto podem ser prorrogados. O livro pode já ter sido devolvido.");
                        if (leitor.IsDBNull(0))
                            throw new InvalidOperationException("O empréstimo não possui uma data de devolução válida.");
                        prazoAtual = leitor.GetDateTime(0);
                        usuario = leitor.GetValue(3);
                    }
                }

                if (prazoAtual != prazoConfirmado)
                    throw new InvalidOperationException("O prazo foi alterado após a consulta. Atualize a lista e confira a nova data antes de prorrogar.");
                DateTime novoPrazo = RegrasCirculacao.CalcularProrrogacao(prazoAtual, dias);
                using (var atualizar = new SqlCeCommand(@"
                    UPDATE Emprestimo SET DataDevolucao = @NovaData,
                        DataProrrogacao = @NovaData, Status = @Status
                    WHERE Id = @Id AND DataDevolucao = @PrazoAnterior
                        AND DataRealDevolucao IS NULL AND Status IN ('Ativo', 'Atrasado')", conexao, transacao))
                {
                    atualizar.Parameters.AddWithValue("@NovaData", novoPrazo);
                    atualizar.Parameters.AddWithValue("@Status", RegrasCirculacao.SituacaoPrazo(novoPrazo, agora));
                    atualizar.Parameters.AddWithValue("@Id", emprestimoId);
                    atualizar.Parameters.AddWithValue("@PrazoAnterior", prazoAtual);
                    if (atualizar.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("O empréstimo foi alterado. Atualize a lista antes de tentar novamente.");
                }

                using (var registrar = new SqlCeCommand(@"
                    INSERT INTO LogProrrogacoes
                        (EmprestimoId, UsuarioId, DataHora, DataDaAcao, NovaDataDevolucao, BibliotecariaNome)
                    VALUES (@EmprestimoId, @UsuarioId, @Agora, @Agora, @NovaData, @Bibliotecaria)", conexao, transacao))
                {
                    registrar.Parameters.AddWithValue("@EmprestimoId", emprestimoId);
                    registrar.Parameters.AddWithValue("@UsuarioId", usuario);
                    registrar.Parameters.AddWithValue("@Agora", agora);
                    registrar.Parameters.AddWithValue("@NovaData", novoPrazo);
                    registrar.Parameters.AddWithValue("@Bibliotecaria", bibliotecaria);
                    registrar.ExecuteNonQuery();
                }
                // Dispose sem Commit desfaz a operação inteira, inclusive se o histórico falhar.
                transacao.Commit();
            }
        }
    }
}
