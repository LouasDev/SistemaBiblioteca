using System;
using System.Data;
using System.Data.SqlServerCe;

namespace SUB.Utils
{
    internal enum ResultadoExclusaoCadastro { Excluido, Desativado }

    internal static class ExclusaoCadastroService
    {
        internal static ResultadoExclusaoCadastro ExcluirLivro(SqlCeConnection connection, int id)
        {
            return Excluir(connection, id, false);
        }

        internal static ResultadoExclusaoCadastro ExcluirUsuario(SqlCeConnection connection, int id)
        {
            return Excluir(connection, id, true);
        }

        private static ResultadoExclusaoCadastro Excluir(SqlCeConnection connection, int id, bool usuario)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));
            if (id <= 0)
                throw new InvalidOperationException("Selecione um cadastro válido.");
            string tabela = usuario ? "usuarios" : "Livros";
            string normal = usuario ? "(UsuarioId = @Id OR Alocador = @Id OR Responsavel = @Id)" : "LivroId = @Id";
            string rapido = usuario ? "(UsuarioId = @Id OR ProfessorId = @Id)" : "(LivroId = @Id OR (LivroId IS NULL AND LivroNome IN (SELECT Nome FROM Livros WHERE Id = @Id)))";
            using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
            {
                if (!Existe(connection, transaction, "SELECT COUNT(*) FROM " + tabela + " WHERE Id = @Id", id))
                    throw new InvalidOperationException("Cadastro não localizado. Atualize a lista.");

                bool pendente = Existe(connection, transaction, "SELECT COUNT(*) FROM Emprestimo WHERE " + normal +
                    " AND (Status IS NULL OR Status <> 'Devolvido' OR DataRealDevolucao IS NULL)", id)
                    || Existe(connection, transaction, "SELECT COUNT(*) FROM EmprestimoRapido WHERE " + rapido +
                    " AND (Status IS NULL OR Status <> 'Devolvido' OR Quantidade IS NULL OR Quantidade <> 0 OR DataHoraDevolucaoReal IS NULL)", id);
                if (pendente)
                    throw new InvalidOperationException("Este cadastro está vinculado a empréstimos pendentes ou devoluções incompletas. Regularize os registros antes de excluir ou desativar.");

                bool historico = Existe(connection, transaction, "SELECT COUNT(*) FROM Emprestimo WHERE " + normal, id)
                    || Existe(connection, transaction, "SELECT COUNT(*) FROM EmprestimoRapido WHERE " + rapido, id)
                    || Existe(connection, transaction, "SELECT COUNT(*) FROM NotificacoesDisponibilidade WHERE " + (usuario ? "UsuarioId" : "LivroId") + " = @Id", id);
                if (usuario)
                {
                    historico = historico
                        || Existe(connection, transaction, "SELECT COUNT(*) FROM LogProrrogacoes WHERE UsuarioId = @Id", id)
                        || Existe(connection, transaction, "SELECT COUNT(*) FROM MapeamentoAnualItens WHERE UsuarioId = @Id", id)
                        || Existe(connection, transaction, "SELECT COUNT(*) FROM ComunicadosAtraso WHERE UsuarioId = @Id", id);
                }
                else
                {
                    historico = historico || Existe(connection, transaction, "SELECT COUNT(*) FROM ComunicadosAtrasoItens WHERE LivroId = @Id", id);
                }

                string sql = historico ? "UPDATE " + tabela + " SET Ativo = 0 WHERE Id = @Id" : "DELETE FROM " + tabela + " WHERE Id = @Id";
                using (var command = new SqlCeCommand(sql, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    if (command.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("O cadastro foi alterado. Atualize a lista.");
                }
                transaction.Commit();
                return historico ? ResultadoExclusaoCadastro.Desativado : ResultadoExclusaoCadastro.Excluido;
            }
        }

        private static bool Existe(SqlCeConnection connection, SqlCeTransaction transaction, string sql, int id)
        {
            using (var command = new SqlCeCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", id);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }
    }
}
