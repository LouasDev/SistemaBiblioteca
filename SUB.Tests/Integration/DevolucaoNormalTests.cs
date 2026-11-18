using System;
using System.Data.SqlServerCe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUB.Tests.Infrastructure;
using SUB.Utils;

namespace SUB.Tests.Integration
{
    [TestClass]
    [TestCategory("P1")]
    public class DevolucaoNormalTests : DatabaseTestBase
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
        public void DevolverNormal_Sucesso_RepoeEstoqueEMarcaDevolvido()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int emprestimoId = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));
                int estoqueEmprestado = SeedData.ReadQuantity(conexao, ctx.LivroNormalId);

                CirculacaoService.DevolverNormal(conexao, emprestimoId, Agora.AddDays(1));

                Assert.AreEqual(estoqueEmprestado + 1, SeedData.ReadQuantity(conexao, ctx.LivroNormalId));
                Assert.AreEqual("Devolvido", SeedData.ReadStatus(conexao, emprestimoId));
                Assert.AreEqual(Agora.AddDays(1), Convert.ToDateTime(Db.Scalar(
                    "SELECT DataRealDevolucao FROM Emprestimo WHERE Id = " + emprestimoId)));
            }
        }

        [TestMethod]
        public void DevolverNormal_EmprestimoAtrasado_MarcaDevolvido()
        {
            using (var conexao = Db.Open())
            {
                int id = SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Agora.AddDays(-10), Agora.AddDays(-3), "Atrasado");
                int antes = SeedData.ReadQuantity(conexao, ctx.LivroNormalId);

                CirculacaoService.DevolverNormal(conexao, id, Agora);

                Assert.AreEqual("Devolvido", SeedData.ReadStatus(conexao, id));
                Assert.AreEqual(antes + 1, SeedData.ReadQuantity(conexao, ctx.LivroNormalId));
            }
        }

        [TestMethod]
        public void DevolverNormal_DuplaDevolucao_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));
                CirculacaoService.DevolverNormal(conexao, id, Agora);

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverNormal(conexao, id, Agora));
            }
        }

        [TestMethod]
        public void DevolverNormal_EmprestimoInexistente_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverNormal(conexao, 999999, Agora));
            }
        }

        [TestMethod]
        public void DevolverNormal_EmprestimoSemLivro_Rejeita()
        {
            using (var conexao = Db.Open())
            {
                int id = SeedData.InsertLoan(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Agora, Prazo, "Ativo");
                using (var comando = new SqlCeCommand("UPDATE Emprestimo SET LivroId = NULL WHERE Id = @Id", conexao))
                {
                    comando.Parameters.AddWithValue("@Id", id);
                    comando.ExecuteNonQuery();
                }

                Assert.ThrowsException<InvalidOperationException>(() =>
                    CirculacaoService.DevolverNormal(conexao, id, Agora));
            }
        }

        [TestMethod]
        public void DevolverNormal_LivroComEmprestimoNaoPodeSerRemovido()
        {
            using (var conexao = Db.Open())
            {
                CirculacaoService.EmprestarNormal(conexao, ctx.AlunoId, ctx.LivroNormalId,
                    ctx.BibliotecariaId, Prazo, Agora);
                int id = Convert.ToInt32(Db.Scalar("SELECT Id FROM Emprestimo"));

                using (var comando = new SqlCeCommand("DELETE FROM Livros WHERE Id = @Id", conexao))
                {
                    comando.Parameters.AddWithValue("@Id", ctx.LivroNormalId);
                    Assert.ThrowsException<SqlCeException>(() => comando.ExecuteNonQuery());
                }

                Assert.AreEqual("Ativo", Db.Scalar("SELECT Status FROM Emprestimo WHERE Id = " + id).ToString());
                CirculacaoService.DevolverNormal(conexao, id, Agora);
                Assert.AreEqual("Devolvido", Db.Scalar("SELECT Status FROM Emprestimo WHERE Id = " + id).ToString());
            }
        }
    }
}
