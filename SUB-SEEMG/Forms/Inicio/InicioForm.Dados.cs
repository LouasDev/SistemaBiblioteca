using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SUB.Utils;

namespace SUB.Forms.Inicio
{
    public partial class InicioForm
    {
        private class EmprestimoAtrasadoInfo
        {
            public int Id { get; set; }
            public int UsuarioId { get; set; } // <-- novo: id do usuário (PK)
            public string Nome { get; set; }
            public string Turma { get; set; }
            public string Livro { get; set; }
            public DateTime DataDevolucao { get; set; }
            public int DiasAtraso { get; set; }
        }

        private class ItemComunicadoAtraso
        {
            public int EmprestimoId { get; set; }
            public int LivroId { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public DateTime DataDevolucaoPrevista { get; set; }
            public int DiasAtraso { get; set; }
        }

        private List<EmprestimoAtrasadoInfo> ObterEmprestimosAtrasados()
        {
            var lista = new List<EmprestimoAtrasadoInfo>();
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    // agora trazemos também u.Id (UsuarioId)
                    string sql = @"
SELECT 
    e.Id,
    u.Id AS UsuarioId,
    u.Nome,
    u.Turma,
    l.Nome AS Livro,
    COALESCE(e.DataProrrogacao, e.DataDevolucao) AS DataLimite,
    DATEDIFF(day, COALESCE(e.DataProrrogacao, e.DataDevolucao), GETDATE()) AS DiasAtraso
FROM Emprestimo e
INNER JOIN Usuarios u ON e.Alocador = u.Id
INNER JOIN Livros l ON e.LivroId = l.Id
WHERE e.Status <> 'Devolvido'
  AND COALESCE(e.DataProrrogacao, e.DataDevolucao) < GETDATE()
ORDER BY DiasAtraso DESC, DataLimite ASC";
                    using (var cmd = new SqlCeCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new EmprestimoAtrasadoInfo { Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0), UsuarioId = reader.IsDBNull(1) ? 0 : reader.GetInt32(1), Nome = reader.IsDBNull(2) ? "" : reader.GetString(2), Turma = reader.IsDBNull(3) ? "" : reader.GetString(3), Livro = reader.IsDBNull(4) ? "" : reader.GetString(4), DataDevolucao = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5), DiasAtraso = reader.IsDBNull(6) ? 0 : reader.GetInt32(6) });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logDir = Path.Combine(Application.StartupPath, "logs");
                    Directory.CreateDirectory(logDir);
                    File.AppendAllText(Path.Combine(logDir, "inicio_obter_devedores.log"), DateTime.Now + " - " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                }
                catch
                {
                }
            }

            return lista;
        }

        private async Task CarregarEstatisticasAsync()
        {
            try
            {
                SetStatus("Carregando estatísticas...");
                var stats = await Task.Run(() => ObterEstatisticas());
                var topUser = await Task.Run(() => ObterTopUsuarioMaisEmprestimos());
                topUsuarioMaisEmprestimos = topUser;
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    BeginInvoke(new Action(() =>
                    {
                        AtualizarCards(stats);
                        // Atualiza o card do Top Usuário
                        // Atualiza o card do Top Usuário
                        // Atualiza o card do Top Usuário
                        var card = flowCards.Controls.OfType<Panel>().FirstOrDefault(p => (string)p.Tag == "TopUsuario");
                        if (card != null)
                        {
                            var lblName = card.Controls.Find("lblTopName", true).FirstOrDefault() as Label;
                            var lblTurma = card.Controls.Find("lblTopTurma", true).FirstOrDefault() as Label;
                            if (lblName != null && lblTurma != null)
                            {
                                if (!string.IsNullOrWhiteSpace(topUsuarioMaisEmprestimos.Nome))
                                {
                                    // Pega os dois primeiros nomes
                                    var matches = System.Text.RegularExpressions.Regex.Matches(topUsuarioMaisEmprestimos.Nome.Trim(), @"\S+");
                                    var primeiros = matches.Cast<System.Text.RegularExpressions.Match>().Select(m => m.Value).Take(2).ToArray();
                                    var doisNomes = string.Join(" ", primeiros);
                                    // Normaliza a turma: remove quebras de linha e espaços duplicados
                                    string turmaDisplay = topUsuarioMaisEmprestimos.Turma?.Trim() ?? "";
                                    turmaDisplay = turmaDisplay.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
                                    turmaDisplay = System.Text.RegularExpressions.Regex.Replace(turmaDisplay, @"\s+", " ");
                                    // Trunca visualmente se muito longa
                                    if (turmaDisplay.Length > 25)
                                        turmaDisplay = turmaDisplay.Substring(0, 25) + "...";
                                    // Exibe os valores
                                    lblName.Text = doisNomes;
                                    lblTurma.Text = string.IsNullOrWhiteSpace(turmaDisplay) ? "" : turmaDisplay;
                                    lblTurma.Visible = !string.IsNullOrWhiteSpace(turmaDisplay);
                                    // Tooltip detalhado (nome completo + turma + qtd)
                                    try
                                    {
                                        if (formToolTip == null)
                                            formToolTip = new ToolTip();
                                        string tip = $"{topUsuarioMaisEmprestimos.Nome}";
                                        if (!string.IsNullOrWhiteSpace(topUsuarioMaisEmprestimos.Turma))
                                            tip += $" • Turma: {topUsuarioMaisEmprestimos.Turma}";
                                        tip += $" • {topUsuarioMaisEmprestimos.Qtd} empréstimos";
                                        formToolTip.SetToolTip(lblName, tip);
                                        formToolTip.SetToolTip(lblTurma, tip);
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    lblName.Text = "-";
                                    lblTurma.Text = "";
                                    lblTurma.Visible = false;
                                }
                            }
                        }
                    }));
                }

                var estatisticasEmprestimos = await Task.Run(() => ObterEstatisticasEmprestimos());
                var livrosPopulares = await Task.Run(() => ObterLivrosPopulares(10));
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    BeginInvoke(new Action(() =>
                    {
                        TabControl tabControl = panel1.Controls.Find("tabEstatisticas", true).FirstOrDefault() as TabControl;
                        if (tabControl != null && tabControl.TabPages.Count >= 3)
                        {
                            DataGridView dgvDevedores = tabControl.TabPages[0].Controls.Find("dgvDevedores", true).FirstOrDefault() as DataGridView;
                            DataGridView dgvEstatisticas = tabControl.TabPages[1].Controls.Find("dgvEstatisticasEmprestimos", true).FirstOrDefault() as DataGridView;
                            DataGridView dgvLivros = tabControl.TabPages[2].Controls.Find("dgvLivrosPopulares", true).FirstOrDefault() as DataGridView;
                            if (dgvDevedores != null)
                            {
                                var emprestimosAtrasados = ObterEmprestimosAtrasados();
                                dgvDevedores.DataSource = null;
                                dgvDevedores.DataSource = emprestimosAtrasados;
                                dgvDevedores.ClearSelection();
                                dgvDevedores.Refresh();
                                EnsureEmptyOverlay(dgvDevedores, "Nenhum empréstimo atrasado no momento.");
                            }

                            if (dgvEstatisticas != null)
                            {
                                dgvEstatisticas.DataSource = null;
                                dgvEstatisticas.DataSource = estatisticasEmprestimos;
                                dgvEstatisticas.ClearSelection();
                                dgvEstatisticas.Refresh();
                            }

                            if (dgvLivros != null)
                            {
                                // Uma consulta vazia transitória não deve apagar uma
                                // lista válida que já esteja visível durante a atualização.
                                if (livrosPopulares.Count > 0 || dgvLivros.Rows.Count == 0)
                                {
                                    dgvLivros.DataSource = null;
                                    dgvLivros.DataSource = livrosPopulares;
                                }

                                dgvLivros.ClearSelection();
                                dgvLivros.Refresh();
                                EnsureEmptyOverlay(dgvLivros, "Nenhum livro encontrado por enquanto.");
                            }
                        }

                        SetStatus($"Última atualização: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    }));
                }
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() => SetStatus($"Erro ao carregar: {ex.Message}")));
            }
        }

        /// <summary>
        /// Retorna o usuário (exceto professores) que mais realizou empréstimos (apenas tabela Emprestimo).
        /// </summary>
        private (int Id, string Nome, string Turma, int Qtd) ObterTopUsuarioMaisEmprestimos()
        {
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    string sql = @"
SELECT TOP 1 
    u.Id, 
    u.Nome, 
    COALESCE(u.Turma, '') AS Turma, 
    COUNT(e.Id) AS Qtd,
    MAX(e.DataEmprestimo) AS UltimoEmprestimo
FROM Emprestimo e
INNER JOIN Usuarios u ON e.Alocador = u.Id
WHERE (u.TipoUsuario IS NULL OR UPPER(u.TipoUsuario) NOT LIKE 'PROFESSOR%')
GROUP BY u.Id, u.Nome, u.Turma
ORDER BY Qtd DESC, UltimoEmprestimo DESC;
";
                    using (var cmd = new SqlCeCommand(sql, conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            string nome = reader.IsDBNull(1) ? "-" : reader.GetString(1);
                            string turma = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            int qtd = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3));
                            return (id, nome, turma, qtd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logDir = Path.Combine(Application.StartupPath, "logs");
                    Directory.CreateDirectory(logDir);
                    File.AppendAllText(Path.Combine(logDir, "inicio_obter_top_usuario.log"), DateTime.Now + " - " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                }
                catch
                {
                }
            }

            return (0, "-", "", 0);
        }

        private List<EstatisticaEmprestimo> ObterEstatisticasEmprestimos()
        {
            var lista = new List<EstatisticaEmprestimo>();
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo", conexao))
                        lista.Add(new EstatisticaEmprestimo { Categoria = "Empréstimos Totais", Valor = Convert.ToInt32(cmd.ExecuteScalar() ?? 0), Detalhes = "Desde o início do sistema" });
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status <> 'Devolvido'", conexao))
                        lista.Add(new EstatisticaEmprestimo { Categoria = "Empréstimos Ativos", Valor = Convert.ToInt32(cmd.ExecuteScalar() ?? 0), Detalhes = "Aguardando devolução" });
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status = 'Atrasado'", conexao))
                        lista.Add(new EstatisticaEmprestimo { Categoria = "Empréstimos Atrasados", Valor = Convert.ToInt32(cmd.ExecuteScalar() ?? 0), Detalhes = "Fora do prazo de devolução" });
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status = 'Devolvido' AND DataRealDevolucao <= DataDevolucao", conexao))
                    {
                        int noPrazo = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        int totalDevolvidos = 0;
                        using (var cmd2 = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status = 'Devolvido'", conexao))
                        {
                            totalDevolvidos = Convert.ToInt32(cmd2.ExecuteScalar() ?? 0);
                        }

                        double taxa = totalDevolvidos > 0 ? (noPrazo * 100.0 / totalDevolvidos) : 0;
                        lista.Add(new EstatisticaEmprestimo { Categoria = "Devolução no Prazo", Valor = Math.Round(taxa, 1), Detalhes = $"{noPrazo} de {totalDevolvidos} empréstimos devolvidos" });
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logDir = Path.Combine(Application.StartupPath, "logs");
                    Directory.CreateDirectory(logDir);
                    File.AppendAllText(Path.Combine(logDir, "inicio_obter_estatisticas.log"), DateTime.Now + " - " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                }
                catch
                {
                }
            }

            return lista;
        }

        private List<LivroPopular> ObterLivrosPopulares(int topN)
        {
            var lista = new List<LivroPopular>();
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    // Janela de popularidade (ajuste se quiser outro período)
                    DateTime inicioJanela = DateTime.Now.AddMonths(-12);
                    string sql = $@"
SELECT TOP {topN}
    l.Nome,
    l.Autor,
    COUNT(e.Id) AS TotalEmprestimos,
    l.Quantidade
FROM Livros l
INNER JOIN Emprestimo e 
    ON l.Id = e.LivroId
    AND e.DataEmprestimo >= @inicio
WHERE 
    (l.Genero IS NULL OR (l.Genero NOT LIKE 'Didático%' AND l.Genero NOT LIKE 'Didatico%'))
GROUP BY l.Id, l.Nome, l.Autor, l.Quantidade
ORDER BY TotalEmprestimos DESC, l.Nome";
                    using (var cmd = new SqlCeCommand(sql, conexao))
                    {
                        cmd.Parameters.AddWithValue("@inicio", inicioJanela);
                        using (var reader = cmd.ExecuteReader())
                        {
                            int posicao = 1;
                            while (reader.Read())
                            {
                                var nome = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                var autor = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                int emprestimos = 0;
                                int quantidade = 0;
                                try
                                {
                                    emprestimos = reader.IsDBNull(2) ? 0 : Convert.ToInt32(reader.GetValue(2));
                                }
                                catch
                                {
                                }

                                try
                                {
                                    quantidade = reader.IsDBNull(3) ? 0 : Convert.ToInt32(reader.GetValue(3));
                                }
                                catch
                                {
                                }

                                string disponibilidade = quantidade > 0 ? "Disponível" : "Indisponível";
                                if (quantidade > 0 && quantidade < 5)
                                    disponibilidade = "Poucas unidades";
                                lista.Add(new LivroPopular { Posicao = posicao++, Titulo = nome, Autor = autor, Emprestimos = emprestimos, Disponibilidade = disponibilidade });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    var logDir = Path.Combine(Application.StartupPath, "logs");
                    Directory.CreateDirectory(logDir);
                    File.AppendAllText(Path.Combine(logDir, "inicio_obter_livros.log"), DateTime.Now + " - " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                }
                catch
                {
                }
            }

            return lista;
        }

        public class EstatisticaEmprestimo
        {
            public string Categoria { get; set; }
            public double Valor { get; set; }
            public string Detalhes { get; set; }
        }

        public class LivroPopular
        {
            public int Posicao { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int Emprestimos { get; set; }
            public string Disponibilidade { get; set; }
        }

        private Dictionary<string, int> ObterEstatisticas()
        {
            var dict = new Dictionary<string, int>
            {
                ["Usuarios"] = 0,
                ["Livros"] = 0,
                ["EmpAtivos"] = 0,
                ["Atrasados"] = 0,
                ["RapidosHoje"] = 0,
                ["MediaMes"] = 0 // novo campo
            };
            try
            {
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Usuarios", conexao))
                        dict["Usuarios"] = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    using (var cmd = new SqlCeCommand("SELECT COALESCE(SUM(Quantidade), 0) FROM Livros", conexao))
                    {
                        var result = cmd.ExecuteScalar();
                        dict["Livros"] = result != null ? Convert.ToInt32(result) : 0;
                    }

                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status <> 'Devolvido'", conexao))
                        dict["EmpAtivos"] = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE Status = 'Atrasado'", conexao))
                        dict["Atrasados"] = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    using (var cmd = new SqlCeCommand("SELECT Id, DataHoraEmprestimo FROM EmprestimoRapido WHERE Status = 'Ativo'", conexao))
                    using (var reader = cmd.ExecuteReader())
                    {
                        int cont = 0;
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(1))
                            {
                                var dt = reader.GetDateTime(1);
                                if (dt.Date == DateTime.Now.Date)
                                    cont++;
                            }
                        }

                        dict["RapidosHoje"] = cont;
                    }

                    // Média de empréstimos do mês atual
                    var primeiroDia = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
                    using (var cmd = new SqlCeCommand("SELECT COUNT(*) FROM Emprestimo WHERE DataEmprestimo >= @inicio AND DataEmprestimo <= @fim", conexao))
                    {
                        cmd.Parameters.AddWithValue("@inicio", primeiroDia);
                        cmd.Parameters.AddWithValue("@fim", ultimoDia);
                        int totalMes = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                        int dias = DateTime.Now.Day;
                        dict["MediaMes"] = dias > 0 ? (int)Math.Round(totalMes / (double)dias) : 0;
                    }
                }
            }
            catch
            { /*silent*/
            }

            return dict;
        }
    }
}
