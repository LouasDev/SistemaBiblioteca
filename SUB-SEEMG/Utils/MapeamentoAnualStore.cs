using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Globalization;
using System.IO;
using System.Linq;
using SUB.Models;

namespace SUB.Utils
{
    /// <summary>Fonte transacional do ciclo anual de uma instalação.</summary>
    public sealed class MapeamentoAnualStore
    {
        public const string Pendente = "Pendente";
        public const string RequerRevisao = "RequerRevisao";
        public const string Concluido = "Concluido";
        public const int VersaoRegraAtual = 2;
        public EstadoMapeamentoAnual ObterEstadoDoAno(int ano)
        {
            MigrarLegadoSeNecessario();
            using (var conexao = Conexao.ObterConexao())
            {
                conexao.Open();
                int anoBase = ObterOuCriarAnoBase(conexao, ano);
                EstadoMapeamentoAnual estado = LerEstado(conexao, ano);
                // Um registro legado pendente ou corrompido nunca pode ser ocultado
                // apenas por coincidir com o ano-base da instalação.
                if (estado != null)
                {
                    estado.DeveLembrar = !string.Equals(estado.Status, Concluido, StringComparison.OrdinalIgnoreCase);
                    return estado;
                }

                if (ano <= anoBase)
                    return new EstadoMapeamentoAnual
                    {
                        Ano = ano,
                        DeveLembrar = false
                    };
                if (estado == null)
                {
                    InserirEstado(conexao, null, ano, Pendente, null);
                    estado = LerEstado(conexao, ano);
                }

                estado.DeveLembrar = !string.Equals(estado.Status, Concluido, StringComparison.OrdinalIgnoreCase);
                return estado;
            }
        }

        public void Aplicar(int ano, IEnumerable<MapeamentoRegistro> registros, string bibliotecaria)
        {
            List<MapeamentoRegistro> alteracoes = (registros ?? Enumerable.Empty<MapeamentoRegistro>()).Where(r => !string.Equals(r.TurmaAtual, r.NovaTurma, StringComparison.OrdinalIgnoreCase)).ToList();
            using (var conexao = Conexao.ObterConexao())
            {
                conexao.Open();
                using (SqlCeTransaction transacao = conexao.BeginTransaction())
                {
                    EstadoMapeamentoAnual existente = LerEstado(conexao, ano, transacao);
                    if (existente != null && string.Equals(existente.Status, Concluido, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("O mapeamento deste ano já foi concluído e não pode ser aplicado novamente.");
                    foreach (MapeamentoRegistro registro in alteracoes)
                    {
                        using (var atualizar = new SqlCeCommand("UPDATE usuarios SET Turma = @turma WHERE Id = @id", conexao, transacao))
                        {
                            atualizar.Parameters.AddWithValue("@turma", registro.NovaTurma ?? string.Empty);
                            atualizar.Parameters.AddWithValue("@id", registro.UsuarioId);
                            atualizar.ExecuteNonQuery();
                        }
                    }

                    using (var limparItens = new SqlCeCommand("DELETE FROM MapeamentoAnualItens WHERE Ano = @ano", conexao, transacao))
                    {
                        limparItens.Parameters.AddWithValue("@ano", ano);
                        limparItens.ExecuteNonQuery();
                    }

                    foreach (MapeamentoRegistro registro in registros ?? Enumerable.Empty<MapeamentoRegistro>())
                    {
                        using (var inserirItem = new SqlCeCommand(@"INSERT INTO MapeamentoAnualItens
                            (Ano, UsuarioId, TurmaOrigem, TurmaDestino, Observacao)
                            VALUES (@ano, @usuarioId, @origem, @destino, @observacao)", conexao, transacao))
                        {
                            inserirItem.Parameters.AddWithValue("@ano", ano);
                            inserirItem.Parameters.AddWithValue("@usuarioId", registro.UsuarioId);
                            inserirItem.Parameters.AddWithValue("@origem", registro.TurmaAtual ?? string.Empty);
                            inserirItem.Parameters.AddWithValue("@destino", registro.NovaTurma ?? string.Empty);
                            inserirItem.Parameters.AddWithValue("@observacao", registro.Observacao ?? string.Empty);
                            inserirItem.ExecuteNonQuery();
                        }
                    }

                    UpsertEstado(conexao, transacao, ano, Concluido, bibliotecaria, DateTime.Now);
                    transacao.Commit();
                }
            }
        }

        private void MigrarLegadoSeNecessario()
        {
            if (!Directory.Exists(AppPaths.MappingFolder))
                return;
            foreach (string caminho in Directory.GetFiles(AppPaths.MappingFolder, "mapeamento_*.txt"))
            {
                int ano;
                if (!TentarExtrairAno(caminho, out ano))
                    continue;
                using (var conexao = Conexao.ObterConexao())
                {
                    conexao.Open();
                    if (LerEstado(conexao, ano) != null)
                        continue;
                    string status;
                    try
                    {
                        status = File.ReadLines(caminho).FirstOrDefault(l => l.StartsWith("STATUS=", StringComparison.OrdinalIgnoreCase));
                    }
                    catch
                    {
                        status = null;
                    }

                    if (string.Equals(status, "STATUS=concluido", StringComparison.OrdinalIgnoreCase))
                    {
                        InserirEstado(conexao, null, ano, Concluido, null);
                    }
                    else if (string.Equals(status, "STATUS=pendente", StringComparison.OrdinalIgnoreCase))
                    {
                        InserirEstado(conexao, null, ano, Pendente, null);
                    }
                    else
                    {
                        PreservarArquivoCorrompido(caminho);
                        InserirEstado(conexao, null, ano, RequerRevisao, null);
                    }
                }
            }
        }

        private static void PreservarArquivoCorrompido(string caminho)
        {
            string backup = caminho + ".corrompido-" + DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture);
            if (!File.Exists(backup))
                File.Copy(caminho, backup, false);
        }

        private static bool TentarExtrairAno(string caminho, out int ano)
        {
            string nome = Path.GetFileNameWithoutExtension(caminho);
            string valor = nome.Substring("mapeamento_".Length);
            return int.TryParse(valor, out ano) && ano >= 2000 && ano <= 2100;
        }

        private static int ObterOuCriarAnoBase(SqlCeConnection conexao, int anoAtual)
        {
            using (var consultar = new SqlCeCommand("SELECT AnoBase FROM MapeamentoAnualControle WHERE Id = 1", conexao))
            {
                object valor = consultar.ExecuteScalar();
                if (valor != null && valor != DBNull.Value)
                    return Convert.ToInt32(valor);
            }

            int anoBase = anoAtual;
            string marcador = Path.Combine(AppPaths.MappingFolder, "app_instalado_em.txt");
            if (File.Exists(marcador) && int.TryParse(File.ReadAllText(marcador).Trim(), out int legado) && legado >= 2000 && legado <= 2100)
                anoBase = legado;
            using (var inserir = new SqlCeCommand("INSERT INTO MapeamentoAnualControle (Id, AnoBase, CriadoEm) VALUES (1, @ano, @agora)", conexao))
            {
                inserir.Parameters.AddWithValue("@ano", anoBase);
                inserir.Parameters.AddWithValue("@agora", DateTime.Now);
                inserir.ExecuteNonQuery();
            }

            return anoBase;
        }

        private static EstadoMapeamentoAnual LerEstado(SqlCeConnection conexao, int ano, SqlCeTransaction transacao = null)
        {
            using (var comando = new SqlCeCommand("SELECT Ano, Status, CriadoEm, ConcluidoEm FROM MapeamentosAnuais WHERE Ano = @ano", conexao, transacao))
            {
                comando.Parameters.AddWithValue("@ano", ano);
                using (var leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                        return null;
                    return new EstadoMapeamentoAnual
                    {
                        Ano = leitor.GetInt32(0),
                        Status = leitor.GetString(1),
                        CriadoEm = leitor.GetDateTime(2),
                        ConcluidoEm = leitor.IsDBNull(3) ? (DateTime? )null : leitor.GetDateTime(3)
                    };
                }
            }
        }

        private static void InserirEstado(SqlCeConnection conexao, SqlCeTransaction transacao, int ano, string status, string bibliotecaria)
        {
            using (var inserir = new SqlCeCommand(@"INSERT INTO MapeamentosAnuais
                (Ano, Status, CriadoEm, ConcluidoEm, Bibliotecaria, VersaoRegra)
                VALUES (@ano, @status, @criado, @concluido, @bibliotecaria, @versao)", conexao, transacao))
            {
                inserir.Parameters.AddWithValue("@ano", ano);
                inserir.Parameters.AddWithValue("@status", status);
                inserir.Parameters.AddWithValue("@criado", DateTime.Now);
                inserir.Parameters.AddWithValue("@concluido", string.Equals(status, Concluido, StringComparison.OrdinalIgnoreCase) ? (object)DateTime.Now : DBNull.Value);
                inserir.Parameters.AddWithValue("@bibliotecaria", (object)bibliotecaria ?? DBNull.Value);
                inserir.Parameters.AddWithValue("@versao", VersaoRegraAtual);
                inserir.ExecuteNonQuery();
            }
        }

        private static void UpsertEstado(SqlCeConnection conexao, SqlCeTransaction transacao, int ano, string status, string bibliotecaria, DateTime concluidoEm)
        {
            using (var atualizar = new SqlCeCommand(@"UPDATE MapeamentosAnuais SET
                Status = @status, ConcluidoEm = @concluido, Bibliotecaria = @bibliotecaria, VersaoRegra = @versao
                WHERE Ano = @ano", conexao, transacao))
            {
                atualizar.Parameters.AddWithValue("@ano", ano);
                atualizar.Parameters.AddWithValue("@status", status);
                atualizar.Parameters.AddWithValue("@concluido", concluidoEm);
                atualizar.Parameters.AddWithValue("@bibliotecaria", (object)bibliotecaria ?? DBNull.Value);
                atualizar.Parameters.AddWithValue("@versao", VersaoRegraAtual);
                if (atualizar.ExecuteNonQuery() > 0)
                    return;
            }

            InserirEstado(conexao, transacao, ano, status, bibliotecaria);
        }
    }
}
