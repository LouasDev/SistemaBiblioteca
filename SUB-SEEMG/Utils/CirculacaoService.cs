using System;
using System.Data.SqlServerCe;

namespace SUB.Utils
{
    internal static class CirculacaoService
    {
        // Só os devolvidos integralmente antes da semana atual saem do painel.
        // Dados incompletos ou contraditórios permanecem visíveis para conferência.
        internal const string FiltroPainelRapido = @"(r.Status IS NULL OR r.Status <> 'Devolvido'
            OR r.Quantidade IS NULL OR r.Quantidade <> 0
            OR r.DataHoraDevolucaoReal IS NULL OR r.DataHoraDevolucaoReal >= @InicioSemana)";
        internal static DateTime InicioSemana(DateTime data)
        {
            return data.Date.AddDays(-(((int)data.DayOfWeek + 6) % 7));
        }

        internal static SqlCeCommand CriarConsultaPainelRapido(SqlCeConnection c, DateTime data)
        {
            var cmd = new SqlCeCommand(@"SELECT r.Id, COALESCE(u.Nome, 'Usuário não localizado') AS Professor,
                COALESCE(l.Nome, r.LivroNome) AS Livro, r.Turma, r.Quantidade,
                r.DataHoraEmprestimo, r.DataHoraDevolucaoReal, r.Bibliotecaria, r.Status, r.LivroId
                FROM EmprestimoRapido r LEFT JOIN Usuarios u ON r.ProfessorId = u.Id
                LEFT JOIN Livros l ON r.LivroId = l.Id WHERE " + FiltroPainelRapido + " ORDER BY r.Id DESC", c);
            cmd.Parameters.AddWithValue("@InicioSemana", InicioSemana(data));
            return cmd;
        }

        internal static void EmprestarNormal(SqlCeConnection c, int usuarioId, int livroId, int responsavelId, DateTime prazo, DateTime agora, DateTime? dataEmprestimoInformada = null)
        {
            if (!RegrasCirculacao.PrazoValido(dataEmprestimoInformada ?? agora, prazo))
                throw new InvalidOperationException(RegrasCirculacao.ErroPrazo);
            using (var tx = c.BeginTransaction())
            {
                string perfil = PerfilAtivo(c, tx, usuarioId);
                if (PerfilAtivo(c, tx, responsavelId) != RegrasCirculacao.PerfilBibliotecario)
                    throw new InvalidOperationException("Selecione uma bibliotecária responsável válida.");
                if (perfil == RegrasCirculacao.PerfilAluno && RegrasCirculacao.AlunoAtingiuLimite(perfil, Contar(c, tx, "SELECT COUNT(*) FROM Emprestimo WHERE Alocador = @Id AND Status = 'Ativo'", usuarioId)))
                    throw new InvalidOperationException("Este aluno já possui um empréstimo ativo.");
                RetirarEstoque(c, tx, livroId, 1);
                using (var cmd = new SqlCeCommand(@"INSERT INTO Emprestimo
                    (UsuarioId, LivroId, Alocador, Responsavel, DataEmprestimo, DataDevolucao, Status)
                    VALUES (@Usuario, @Livro, @Usuario, @Responsavel, @Agora, @Prazo, 'Ativo')", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Usuario", usuarioId);
                    cmd.Parameters.AddWithValue("@Livro", livroId);
                    cmd.Parameters.AddWithValue("@Responsavel", responsavelId);
                    cmd.Parameters.AddWithValue("@Agora", agora);
                    cmd.Parameters.AddWithValue("@Prazo", prazo);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
            }
        }

        internal static void EmprestarRapido(SqlCeConnection c, string professor, string livro, string turma, int quantidade, string bibliotecaria, DateTime agora)
        {
            if (!RegrasCirculacao.QuantidadeRetiradaValida(quantidade) || string.IsNullOrWhiteSpace(turma) || string.IsNullOrWhiteSpace(bibliotecaria))
                throw new InvalidOperationException("Preencha os campos e informe uma quantidade maior que zero.");
            using (var tx = c.BeginTransaction())
            {
                int professorId = BuscarId(c, tx, "SELECT Id FROM Usuarios WHERE Nome = @Nome AND TipoUsuario LIKE '%Professor%' AND (Ativo = 1 OR Ativo IS NULL)", professor);
                int livroId = BuscarId(c, tx, "SELECT Id FROM Livros WHERE Nome = @Nome AND Genero = @Genero AND (Ativo = 1 OR Ativo IS NULL)", livro, RegrasCirculacao.GeneroRapido);
                if (RegrasCirculacao.AtingiuLimitePendencias(Contar(c, tx, "SELECT COUNT(*) FROM EmprestimoRapido WHERE ProfessorId = @Id AND Status = 'Faltando'", professorId)))
                    throw new InvalidOperationException("Este professor já possui duas devoluções faltando. Regularize uma pendência antes de um novo empréstimo rápido.");
                RetirarEstoque(c, tx, livroId, quantidade);
                using (var cmd = new SqlCeCommand(@"INSERT INTO EmprestimoRapido
                    (ProfessorId, LivroId, LivroNome, Turma, Quantidade, DataHoraEmprestimo, Bibliotecaria, Status)
                    VALUES (@Professor, @Livro, @Nome, @Turma, @Quantidade, @Agora, @Bibliotecaria, 'Ativo')", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Professor", professorId);
                    cmd.Parameters.AddWithValue("@Livro", livroId);
                    cmd.Parameters.AddWithValue("@Nome", livro);
                    cmd.Parameters.AddWithValue("@Turma", turma);
                    cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@Agora", agora);
                    cmd.Parameters.AddWithValue("@Bibliotecaria", bibliotecaria);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
            }
        }

        internal static void DevolverNormal(SqlCeConnection c, int emprestimoId, DateTime agora)
        {
            using (var tx = c.BeginTransaction())
            {
                int livroId;
                using (var cmd = new SqlCeCommand("SELECT LivroId FROM Emprestimo WHERE Id = @Id", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Id", emprestimoId);
                    var valor = cmd.ExecuteScalar();
                    if (valor == null || valor == DBNull.Value)
                        throw new InvalidOperationException("Empréstimo ou livro não localizado. Atualize a lista.");
                    livroId = Convert.ToInt32(valor);
                }

                using (var cmd = new SqlCeCommand(@"UPDATE Emprestimo
                    SET Status = 'Devolvido', DataRealDevolucao = @Agora
                    WHERE Id = @Id AND DataRealDevolucao IS NULL AND Status IN ('Ativo', 'Atrasado')", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Agora", agora);
                    cmd.Parameters.AddWithValue("@Id", emprestimoId);
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("Este empréstimo já foi devolvido ou não está mais em aberto. Atualize a lista.");
                }

                ReporEstoque(c, tx, livroId, 1);
                tx.Commit();
            }
        }

        internal static void DevolverRapido(SqlCeConnection c, int emprestimoId, int saldoConfirmado, int devolvidos, DateTime agora)
        {
            if (!RegrasCirculacao.QuantidadeDevolvidaValida(saldoConfirmado, devolvidos))
                throw new InvalidOperationException(RegrasCirculacao.ErroQuantidadeDevolvida);
            using (var tx = c.BeginTransaction())
            {
                int livroId, professorId, saldo;
                string status;
                using (var cmd = new SqlCeCommand("SELECT LivroId, ProfessorId, Quantidade, Status FROM EmprestimoRapido WHERE Id = @Id", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Id", emprestimoId);
                    using (var r = cmd.ExecuteReader())
                    {
                        if (!r.Read() || r.IsDBNull(0) || r.IsDBNull(1) || r.IsDBNull(2))
                            throw new InvalidOperationException("Empréstimo incompleto ou não localizado. Atualize a lista.");
                        livroId = r.GetInt32(0);
                        professorId = r.GetInt32(1);
                        saldo = r.GetInt32(2);
                        status = r.IsDBNull(3) ? "" : r.GetString(3);
                    }
                }

                if (!RegrasCirculacao.EmprestimoRapidoAberto(status, saldo))
                    throw new InvalidOperationException("Este empréstimo já foi devolvido ou não está em aberto.");
                if (saldo != saldoConfirmado)
                    throw new InvalidOperationException("O saldo foi alterado após a consulta. Atualize a lista antes de devolver.");
                int restante = RegrasCirculacao.SaldoAposDevolucao(saldo, devolvidos);
                // Não impede a regularização de uma pendência que já estava como Faltando.
                if (restante > 0 && status != RegrasCirculacao.Faltando && RegrasCirculacao.AtingiuLimitePendencias(Contar(c, tx, "SELECT COUNT(*) FROM EmprestimoRapido WHERE ProfessorId = @Id AND Status = 'Faltando'", professorId)))
                    throw new InvalidOperationException("O professor já possui duas devoluções faltando.");
                using (var cmd = new SqlCeCommand(@"UPDATE EmprestimoRapido
                    SET Status = @Status, DataHoraDevolucaoReal = @Agora, Quantidade = @Restante
                    WHERE Id = @Id AND Quantidade = @Saldo AND Status = @StatusAnterior", c, tx))
                {
                    cmd.Parameters.AddWithValue("@Status", RegrasCirculacao.SituacaoSaldoRapido(restante));
                    cmd.Parameters.AddWithValue("@Agora", agora);
                    cmd.Parameters.AddWithValue("@Restante", restante);
                    cmd.Parameters.AddWithValue("@Id", emprestimoId);
                    cmd.Parameters.AddWithValue("@Saldo", saldo);
                    cmd.Parameters.AddWithValue("@StatusAnterior", status);
                    if (cmd.ExecuteNonQuery() != 1)
                        throw new InvalidOperationException("O empréstimo foi alterado. Atualize a lista antes de devolver.");
                }

                ReporEstoque(c, tx, livroId, devolvidos);
                tx.Commit();
            }
        }

        private static void RetirarEstoque(SqlCeConnection c, SqlCeTransaction tx, int livroId, int quantidade)
        {
            using (var cmd = new SqlCeCommand(@"UPDATE Livros SET Quantidade = Quantidade - @Qtd,
                Disponibilidade = CASE WHEN Quantidade - @Qtd > 0 THEN 1 ELSE 0 END
                WHERE Id = @Id AND Quantidade >= @Qtd AND (Ativo = 1 OR Ativo IS NULL)", c, tx))
            {
                cmd.Parameters.AddWithValue("@Qtd", quantidade);
                cmd.Parameters.AddWithValue("@Id", livroId);
                if (cmd.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Livro indisponível ou quantidade insuficiente no estoque atual. Atualize a consulta.");
            }
        }

        private static void ReporEstoque(SqlCeConnection c, SqlCeTransaction tx, int livroId, int quantidade)
        {
            using (var cmd = new SqlCeCommand(@"UPDATE Livros SET Quantidade = Quantidade + @Qtd,
                Disponibilidade = CASE WHEN Quantidade + @Qtd > 0 THEN 1 ELSE 0 END
                WHERE Id = @Id AND Quantidade >= 0 AND Quantidade <= @Maximo", c, tx))
            {
                cmd.Parameters.AddWithValue("@Qtd", quantidade);
                cmd.Parameters.AddWithValue("@Id", livroId);
                cmd.Parameters.AddWithValue("@Maximo", int.MaxValue - quantidade);
                if (cmd.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("Não foi possível repor o estoque do livro. A devolução não foi salva.");
            }
        }

        private static string PerfilAtivo(SqlCeConnection c, SqlCeTransaction tx, int usuarioId)
        {
            using (var cmd = new SqlCeCommand("SELECT TipoUsuario FROM Usuarios WHERE Id = @Id AND (Ativo = 1 OR Ativo IS NULL)", c, tx))
            {
                cmd.Parameters.AddWithValue("@Id", usuarioId);
                var perfil = cmd.ExecuteScalar();
                if (perfil == null || perfil == DBNull.Value)
                    throw new InvalidOperationException("Usuário não localizado ou inativo.");
                return Convert.ToString(perfil);
            }
        }

        private static int Contar(SqlCeConnection c, SqlCeTransaction tx, string sql, int id)
        {
            using (var cmd = new SqlCeCommand(sql, c, tx))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static int BuscarId(SqlCeConnection c, SqlCeTransaction tx, string sql, string nome, string genero = null)
        {
            using (var cmd = new SqlCeCommand(sql, c, tx))
            {
                cmd.Parameters.AddWithValue("@Nome", nome);
                if (genero != null)
                    cmd.Parameters.AddWithValue("@Genero", genero);
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        throw new InvalidOperationException("Professor ou livro didático não localizado ou inativo.");
                    int id = r.GetInt32(0);
                    if (r.Read())
                        throw new InvalidOperationException("Há cadastros com o mesmo nome. Diferencie os nomes antes de registrar o empréstimo rápido.");
                    return id;
                }
            }
        }
    }
}
