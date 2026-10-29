using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SUB.Models;
using SUB.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SUB.Forms.Inicio
{
    public partial class InicioForm
    {
        private void BtnImprimirCarta_Click(object sender, EventArgs e)
        {
            // Localiza o DataGrid de Devedores
            var tabControl = panel1.Controls.Find("tabEstatisticas", true).FirstOrDefault() as TabControl;
            if (tabControl == null)
                return;
            var dgvDevedores = tabControl.TabPages[0].Controls.Find("dgvDevedores", true).FirstOrDefault() as DataGridView;
            if (dgvDevedores == null || dgvDevedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um empréstimo atrasado para gerar o comunicado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var emprestimo = dgvDevedores.SelectedRows[0].DataBoundItem as EmprestimoAtrasadoInfo;
            if (emprestimo == null)
            {
                MessageBox.Show("Não foi possível obter os dados do empréstimo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Buscar os livros em atraso do aluno
            var itens = ObterItensAtrasadosPorUsuario(emprestimo.UsuarioId);
            GerarComunicadoAtrasoPdf(emprestimo, itens);
        }

        private void GerarComunicadoAtrasoPdf(EmprestimoAtrasadoInfo devedor, List<ItemComunicadoAtraso> itens)
        {
            if (devedor == null || itens == null || itens.Count == 0)
            {
                MessageBox.Show("Não há itens em atraso para gerar o comunicado.", "Comunicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string safeName = string.IsNullOrWhiteSpace(devedor.Nome) ? "Aluno" : string.Concat(devedor.Nome.Split(Path.GetInvalidFileNameChars()));
            var dlg = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"Comunicado_atraso_{safeName}.pdf"};
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            var configuracao = InstituicaoConfiguracaoStore.Carregar();
            string instituicao = string.IsNullOrWhiteSpace(configuracao?.Nome) ? "Biblioteca Escolar" : configuracao.Nome;
            DateTime agora = DateTime.Now;
            Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
            using (var fs = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(doc, fs);
                doc.Open();
                var titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 15);
                var destaque = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
                var normal = FontFactory.GetFont(FontFactory.HELVETICA, 10.5f);
                var pequeno = FontFactory.GetFont(FontFactory.HELVETICA, 8.5f);
                var cabecalho = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 22f
                };
                cabecalho.SetWidths(new[] { 0.55f, 6.45f });
                var selo = new PdfPCell
                {
                    Border = iTextSharp.text.Rectangle.BOTTOM_BORDER,
                    BorderColorBottom = new BaseColor(203, 213, 225),
                    BorderWidthBottom = 0.5f,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    PaddingBottom = 7f
                };
                try
                {
                    string caminhoMarca = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "sub-monocromatico.png");
                    var marca = iTextSharp.text.Image.GetInstance(caminhoMarca);
                    marca.ScaleToFit(25f, 25f);
                    selo.AddElement(marca);
                }
                catch
                {
                }

                cabecalho.AddCell(selo);
                var assinatura = new Phrase();
                assinatura.Add(new Chunk("Sistema Único Bibliotecário\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10.5f, new BaseColor(31, 41, 55))));
                assinatura.Add(new Chunk(instituicao, FontFactory.GetFont(FontFactory.HELVETICA, 9.5f, new BaseColor(75, 85, 99))));
                cabecalho.AddCell(new PdfPCell(assinatura) { Border = iTextSharp.text.Rectangle.BOTTOM_BORDER, BorderColorBottom = new BaseColor(203, 213, 225), BorderWidthBottom = 0.5f, VerticalAlignment = Element.ALIGN_MIDDLE, PaddingLeft = 8f, PaddingBottom = 7f });
                doc.Add(cabecalho);
                doc.Add(new Paragraph("NOTIFICAÇÃO DE DEVOLUÇÃO EM ATRASO", titulo) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 16f });
                doc.Add(new Paragraph("Data: " + agora.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR")), normal) { SpacingAfter = 14f });
                doc.Add(new Paragraph("À família ou ao(à) responsável legal pelo(a) estudante " + devedor.Nome + (string.IsNullOrWhiteSpace(devedor.Turma) ? "." : " — Turma: " + devedor.Turma + "."), normal) { SpacingAfter = 14f });
                var aviso = new PdfPCell(new Phrase("PENDÊNCIA REGISTRADA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(52, 58, 64))))
                {
                    BackgroundColor = new BaseColor(243, 244, 246),
                    BorderColor = new BaseColor(209, 213, 219),
                    Padding = 7f
                };
                var caixaAviso = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 14f
                };
                caixaAviso.AddCell(aviso);
                doc.Add(caixaAviso);
                doc.Add(new Paragraph("Esta notificação formal registra pendência de devolução dos itens relacionados abaixo. Solicitamos a regularização junto à Biblioteca Escolar com prioridade. A pendência permanecerá registrada até a devolução dos itens ou a tratativa pela biblioteca.", normal) { SpacingAfter = 14f });
                var item = itens[0];
                var pendencia = new PdfPTable(1)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 22f
                };
                var conteudoPendencia = new PdfPCell
                {
                    BorderColor = new BaseColor(209, 213, 219),
                    Padding = 14f
                };
                conteudoPendencia.AddElement(new Paragraph("ITEM COM DEVOLUÇÃO EM ATRASO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 9, new BaseColor(75, 85, 99))) { SpacingAfter = 8f });
                conteudoPendencia.AddElement(new Paragraph(item.Titulo ?? "Livro não identificado", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, new BaseColor(31, 41, 55))) { SpacingAfter = 3f });
                if (!string.IsNullOrWhiteSpace(item.Autor))
                    conteudoPendencia.AddElement(new Paragraph("Autor: " + item.Autor, normal) { SpacingAfter = 14f });
                var dadosItem = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                dadosItem.SetWidths(new[] { 1f, 1f });
                dadosItem.AddCell(new PdfPCell(new Phrase("DEVOLUÇÃO PREVISTA\n" + item.DataDevolucaoPrevista.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, new BaseColor(52, 58, 64)))) { Border = iTextSharp.text.Rectangle.NO_BORDER, BackgroundColor = new BaseColor(243, 244, 246), Padding = 10f });
                dadosItem.AddCell(new PdfPCell(new Phrase("DIAS EM ATRASO\n" + item.DiasAtraso, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, new BaseColor(52, 58, 64)))) { Border = iTextSharp.text.Rectangle.NO_BORDER, BackgroundColor = new BaseColor(243, 244, 246), Padding = 10f, HorizontalAlignment = Element.ALIGN_CENTER });
                conteudoPendencia.AddElement(dadosItem);
                pendencia.AddCell(conteudoPendencia);
                doc.Add(pendencia);
                doc.Add(new Paragraph("Atenciosamente,\nBiblioteca Escolar de " + instituicao, normal) { SpacingAfter = 24f });
                doc.Add(new Paragraph("Em caso de dúvidas, procure a Biblioteca Escolar.\nGerado pelo SUB — Sistema Único Bibliotecário", pequeno) { Alignment = Element.ALIGN_CENTER });
                doc.Close();
            }

            RegistrarComunicadoAtraso(devedor.UsuarioId, instituicao, agora, itens);
            MessageBox.Show("Comunicado de atraso gerado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                Process.Start(dlg.FileName);
            }
            catch
            {
            }
        }

        private List<ItemComunicadoAtraso> ObterItensAtrasadosPorUsuario(int usuarioId)
        {
            var itens = new List<ItemComunicadoAtraso>();
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    string sql = @"
SELECT e.Id, l.Id, l.Nome, l.Autor, COALESCE(e.DataProrrogacao, e.DataDevolucao), DATEDIFF(day, COALESCE(e.DataProrrogacao, e.DataDevolucao), GETDATE())
FROM Emprestimo e
INNER JOIN Livros l ON e.LivroId = l.Id
WHERE e.Alocador = @usuarioId
  AND e.Status <> 'Devolvido'
  AND COALESCE(e.DataProrrogacao, e.DataDevolucao) < GETDATE()";
                    using (var cmd = new SqlCeCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itens.Add(new ItemComunicadoAtraso { EmprestimoId = reader.GetInt32(0), LivroId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1), Titulo = reader.IsDBNull(2) ? "" : reader.GetString(2), Autor = reader.IsDBNull(3) ? "" : reader.GetString(3), DataDevolucaoPrevista = reader.GetDateTime(4), DiasAtraso = Math.Max(1, reader.GetInt32(5)) });
                            }
                        }
                    }
                }
            }
            catch
            {
            }

            return itens;
        }

        private void RegistrarComunicadoAtraso(int usuarioId, string instituicao, DateTime geradoEm, List<ItemComunicadoAtraso> itens)
        {
            using (var conexao = Conexao.ObterConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    int comunicadoId;
                    using (var inserir = new SqlCeCommand(@"INSERT INTO ComunicadosAtraso (UsuarioId, Bibliotecaria, Instituicao, GeradoEm, VersaoModelo)
VALUES (@usuario, @bibliotecaria, @instituicao, @geradoEm, @versao)", conexao, transacao))
                    {
                        inserir.Parameters.AddWithValue("@usuario", usuarioId);
                        inserir.Parameters.AddWithValue("@bibliotecaria", (object)(Sessao.NomeBibliotecariaLogada ?? "") ?? DBNull.Value);
                        inserir.Parameters.AddWithValue("@instituicao", instituicao);
                        inserir.Parameters.AddWithValue("@geradoEm", geradoEm);
                        inserir.Parameters.AddWithValue("@versao", 1);
                        inserir.ExecuteNonQuery();
                    }

                    using (var id = new SqlCeCommand("SELECT @@IDENTITY", conexao, transacao))
                        comunicadoId = Convert.ToInt32(id.ExecuteScalar());
                    foreach (var item in itens)
                        using (var inserirItem = new SqlCeCommand(@"INSERT INTO ComunicadosAtrasoItens
(ComunicadoId, EmprestimoId, LivroId, Titulo, Autor, DataDevolucaoPrevista, DiasAtraso)
VALUES (@comunicado, @emprestimo, @livro, @titulo, @autor, @devolucao, @dias)", conexao, transacao))
                        {
                            inserirItem.Parameters.AddWithValue("@comunicado", comunicadoId);
                            inserirItem.Parameters.AddWithValue("@emprestimo", item.EmprestimoId);
                            inserirItem.Parameters.AddWithValue("@livro", item.LivroId == 0 ? (object)DBNull.Value : item.LivroId);
                            inserirItem.Parameters.AddWithValue("@titulo", item.Titulo ?? "");
                            inserirItem.Parameters.AddWithValue("@autor", item.Autor ?? "");
                            inserirItem.Parameters.AddWithValue("@devolucao", item.DataDevolucaoPrevista);
                            inserirItem.Parameters.AddWithValue("@dias", item.DiasAtraso);
                            inserirItem.ExecuteNonQuery();
                        }

                    transacao.Commit();
                }
            }
        }
    }
}
