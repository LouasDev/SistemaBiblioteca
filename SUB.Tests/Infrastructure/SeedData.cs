using System;
using System.Data.SqlServerCe;

namespace SUB.Tests.Infrastructure
{
    public sealed class SeedContext
    {
        public int BibliotecariaId { get; set; }
        public int ProfessorId { get; set; }
        public int AlunoId { get; set; }
        public int AlunoInativoId { get; set; }
        public int LivroNormalId { get; set; }
        public int LivroDidaticoId { get; set; }
        public int LivroEsgotadoId { get; set; }
        public int LivroDidaticoDoisId { get; set; }
    }

    public static class SeedData
    {
        public static SeedContext SeedBasic(SqlCeConnection conexao)
        {
            InsertTurma(conexao, "6º Ano A");
            InsertTurma(conexao, "7º Ano A");
            InsertTurma(conexao, "8º Ano B");
            InsertTurma(conexao, "9º Ano A");

            var ctx = new SeedContext
            {
                BibliotecariaId = InsertUser(conexao, "Bibliotecária Teste", "Bibliotecário(a)", null, "biblio@test.local", true),
                ProfessorId = InsertUser(conexao, "Professor Teste", "Professor(a)", null, "professor@test.local", true),
                AlunoId = InsertUser(conexao, "Aluno Teste", "Aluno(a)", "6º Ano A", "aluno@test.local", true),
                AlunoInativoId = InsertUser(conexao, "Aluno Inativo", "Aluno(a)", "6º Ano A", "inativo@test.local", false)
            };

            ctx.LivroNormalId = InsertBook(conexao, "Livro Normal", "Literatura", 3, "1234567890123");
            ctx.LivroDidaticoId = InsertBook(conexao, "Livro Didático", "Didático", 2, "2234567890123");
            ctx.LivroEsgotadoId = InsertBook(conexao, "Livro Esgotado", "Literatura", 0, "3234567890123");
            ctx.LivroDidaticoDoisId = InsertBook(conexao, "Livro Didático Dois", "Didático", 5, "4234567890123");
            return ctx;
        }

        public static void InsertTurma(SqlCeConnection conexao, string nome)
        {
            using (var comando = new SqlCeCommand("INSERT INTO Turmas (Nome) VALUES (@Nome)", conexao))
            {
                comando.Parameters.AddWithValue("@Nome", nome);
                comando.ExecuteNonQuery();
            }
        }

        public static int InsertUser(SqlCeConnection conexao, string nome, string tipo, string turma, string email, bool? ativo)
        {
            using (var comando = new SqlCeCommand(@"
INSERT INTO usuarios (Nome, Email, Senha_Hash, Senha_Salt, CPF, DataNascimento, Turma, Telefone, TipoUsuario, Ativo)
VALUES (@Nome, @Email, NULL, NULL, NULL, NULL, @Turma, NULL, @Tipo, @Ativo)", conexao))
            {
                comando.Parameters.AddWithValue("@Nome", nome);
                comando.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Turma", (object)turma ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Tipo", tipo);
                comando.Parameters.AddWithValue("@Ativo", ativo.HasValue ? (object)(ativo.Value ? 1 : 0) : DBNull.Value);
                comando.ExecuteNonQuery();
                return LastIdentity(conexao);
            }
        }

        public static int InsertBook(SqlCeConnection conexao, string nome, string genero, int quantidade, string codigoBarras)
        {
            using (var comando = new SqlCeCommand(@"
INSERT INTO Livros (Nome, Autor, Genero, Quantidade, CodigoBarras, Disponibilidade, Ativo)
VALUES (@Nome, 'Autor Teste', @Genero, @Quantidade, @Codigo, @Disponibilidade, 1)", conexao))
            {
                comando.Parameters.AddWithValue("@Nome", nome);
                comando.Parameters.AddWithValue("@Genero", genero);
                comando.Parameters.AddWithValue("@Quantidade", quantidade);
                comando.Parameters.AddWithValue("@Codigo", codigoBarras);
                comando.Parameters.AddWithValue("@Disponibilidade", quantidade > 0 ? 1 : 0);
                comando.ExecuteNonQuery();
                return LastIdentity(conexao);
            }
        }

        public static int InsertLoan(SqlCeConnection conexao, int usuarioId, int livroId, int responsavelId, DateTime dataEmprestimo, DateTime? dataDevolucao, string status)
        {
            using (var comando = new SqlCeCommand(@"
INSERT INTO Emprestimo (UsuarioId, LivroId, Alocador, Responsavel, DataEmprestimo, DataDevolucao, Status)
VALUES (@Usuario, @Livro, @Usuario, @Responsavel, @DataEmprestimo, @DataDevolucao, @Status)", conexao))
            {
                comando.Parameters.AddWithValue("@Usuario", usuarioId);
                comando.Parameters.AddWithValue("@Livro", livroId);
                comando.Parameters.AddWithValue("@Responsavel", responsavelId);
                comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimo);
                comando.Parameters.AddWithValue("@DataDevolucao", dataDevolucao.HasValue ? (object)dataDevolucao.Value : DBNull.Value);
                comando.Parameters.AddWithValue("@Status", status);
                comando.ExecuteNonQuery();
                return LastIdentity(conexao);
            }
        }

        public static int InsertQuickLoan(SqlCeConnection conexao, int professorId, int livroId, string livroNome, string turma, int quantidade, DateTime dataEmprestimo, string status)
        {
            using (var comando = new SqlCeCommand(@"
INSERT INTO EmprestimoRapido (ProfessorId, LivroId, LivroNome, Turma, Quantidade, DataHoraEmprestimo, Status)
VALUES (@Professor, @Livro, @LivroNome, @Turma, @Quantidade, @Data, @Status)", conexao))
            {
                comando.Parameters.AddWithValue("@Professor", professorId);
                comando.Parameters.AddWithValue("@Livro", livroId);
                comando.Parameters.AddWithValue("@LivroNome", livroNome);
                comando.Parameters.AddWithValue("@Turma", turma);
                comando.Parameters.AddWithValue("@Quantidade", quantidade);
                comando.Parameters.AddWithValue("@Data", dataEmprestimo);
                comando.Parameters.AddWithValue("@Status", status);
                comando.ExecuteNonQuery();
                return LastIdentity(conexao);
            }
        }

        private static int LastIdentity(SqlCeConnection conexao)
        {
            using (var comando = new SqlCeCommand("SELECT @@IDENTITY", conexao))
                return Convert.ToInt32(comando.ExecuteScalar());
        }

        public static string ReadBookQuantity(SqlCeConnection conexao, int livroId, out int disponibilidade)
        {
            using (var comando = new SqlCeCommand("SELECT Quantidade, Disponibilidade FROM Livros WHERE Id = @Id", conexao))
            {
                comando.Parameters.AddWithValue("@Id", livroId);
                using (var leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                        throw new InvalidOperationException("Livro não encontrado.");
                    disponibilidade = leitor.IsDBNull(1) ? -1 : Convert.ToInt32(leitor.GetValue(1));
                    return leitor.GetInt32(0).ToString();
                }
            }
        }

        public static int ReadQuantity(SqlCeConnection conexao, int livroId)
        {
            using (var comando = new SqlCeCommand("SELECT Quantidade FROM Livros WHERE Id = @Id", conexao))
            {
                comando.Parameters.AddWithValue("@Id", livroId);
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        public static string ReadStatus(SqlCeConnection conexao, int emprestimoId)
        {
            using (var comando = new SqlCeCommand("SELECT Status FROM Emprestimo WHERE Id = @Id", conexao))
            {
                comando.Parameters.AddWithValue("@Id", emprestimoId);
                return Convert.ToString(comando.ExecuteScalar());
            }
        }
    }
}