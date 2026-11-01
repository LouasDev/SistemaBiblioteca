using System;
using System.Data.SqlServerCe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class EmprestimoNormalTests : DatabaseTestBase
    {
        private SeedContext ctx;
        private static readonly DateTime Agora = new DateTime(2026, 3, 10, 10, 0, 0);
        private static readonly DateTime Prazo = Agora.Date.AddDays(7);

        protected override void OnSeed(SqlCeTestDatabase db)
        {
            using (var conexao = db.Open())
                ctx = SeedData.SeedBasic(conexao);
        }

        [TestMethod]
        public void EmprestarNormal_Sucesso_DecrementaEstoqueECriaEmprestimoAtivo()
        {
            using (var conexao = Db.Open())
            {
                int antes = SeedData.ReadQuantity(conexao, ctx.LivroNormalId);
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);

                Assert.AreEqual(antes - 1, SeedData.ReadQuantity(conexao, ctx.LivroNormalId));
                Assert.AreEqual(1, Db.Int("SELECT COUNT(*) FROM Emprestimo WHERE Status = 'Ativo'"));
                Assert.AreEqual("Ativo", Db.Scalar("SELECT Status FROM Emprestimo").ToString());
                Assert.AreEqual(Agora, Convert.ToDateTime(Db.Scalar("SELECT DataEmprestimo FROM Emprestimo")));
                Assert.AreEqual(Prazo, Convert.ToDateTime(Db.Scalar("SELECT DataDevolucao FROM Emprestimo")));
            }
        }

        [TestMethod]
        public void EmprestarNormal_UltimoExemplar_AtualizaDisponibilidadeParaZero()
        {
            using (var conexao = Db.Open())
            {
                using (var comando = new SqlCeCommand("UPDATE Livros SET Quantidade = 1, Disponibilidade = 1 WHERE Id = @Id", conexao))
                {
                    comando.Parameters.AddWithValue("@Id", ctx.LivroNormalId);
                    comando.ExecuteNonQuery();
                }

                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);

                Assert.AreEqual(0, SeedData.ReadQuantity(conexao, ctx.LivroNormalId));
                Assert.AreEqual(0, Db.Int("SELECT Disponibilidade FROM Livros WHERE Id = " + ctx.LivroNormalId));
            }
        }

        [TestMethod]
        public void EmprestarNormal_AlunoComEmprestimoAtivo_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId,
                    Agora, Prazo, "Ativo");
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroDidaticoId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_AlunoComEmprestimoAtrasado_DeveRejeitar()
        {
            using (var conexao = Db.Open())
            {
                SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId, ctx.BibliotecariaId,
                    Agora.AddDays(-10), Agora.AddDays(-3), "Atrasado");
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroDidaticoId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_EstoqueInsuficiente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroEsgotadoId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_UsuarioInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, 999999, ctx.LivroNormalId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_UsuarioInativo_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoInativoId, ctx.LivroNormalId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_ResponsavelNaoBibliotecario_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                        ctx.ProfessorId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_PrazoAnteriorAoEmprestimo_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                        ctx.BibliotecariaId, Agora.AddDays(-1), Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_LivroInativo_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                using (var comando = new SqlCeCommand("UPDATE Livros SET Ativo = 0 WHERE Id = @Id", conexao))
                {
                    comando.Parameters.AddWithValue("@Id", ctx.LivroNormalId);
                    comando.ExecuteNonQuery();
                }

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }

        [TestMethod]
        public void EmprestarNormal_LivroInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, 999999,
                        ctx.BibliotecariaId, Prazo, Agora));
            }
        }
    }
}