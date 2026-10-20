using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SUB.Updater
{
    internal static class Program
    {
        private const string NomeMutex = "Local\\SUB-Updater";

        private static string caminhoLog;
        private static bool semMensagens;

        [STAThread]
        private static int Main(string[] args)
        {
            bool criado;
            using (var mutex = new Mutex(true, NomeMutex, out criado))
            {
                if (!criado)
                {
                    Avisar("Já existe uma atualização em andamento neste computador. Aguarde-a terminar.");
                    return 1;
                }

                try
                {
                    return Executar(args);
                }
                catch (Exception ex)
                {
                    Log("ERRO INESPERADO: " + ex);
                    Avisar("Falha inesperada ao aplicar a atualização.\n\n" + ex.Message);
                    return 10;
                }
            }
        }

        private static int Executar(string[] args)
        {
            var a = LerArgs(args);
            if (a == null)
            {
                Avisar("Argumentos inválidos.\nUso: SUB-UPDATER.exe --pid <pid> --pasta <instalação> --pacote <zip> --versao <x.y.z> --dados <dados> [--timeout <s>] [--sem-mensagens]");
                return 2;
            }

            semMensagens = a.ContainsKey("sem-mensagens");
            caminhoLog = Path.Combine(a["dados"], "update", "updater.log");
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoLog));

            var erro = ValidarPasta(a["pasta"], "pasta de instalação") ??
                       ValidarPasta(a["dados"], "pasta de dados");
            if (erro != null)
            {
                Log(erro);
                Avisar(erro);
                return 2;
            }

            if (File.Exists(Path.Combine(a["pasta"], "sub.sdf")) ||
                File.Exists(Path.Combine(a["pasta"], "instituicao.json")))
            {
                erro = "A pasta de instalação contém arquivos de dados (sub.sdf/instituicao.json). Atualização abortada por segurança.";
                Log(erro);
                Avisar(erro);
                return 2;
            }

            if (!File.Exists(a["pacote"]))
            {
                erro = "O pacote de atualização não foi encontrado: " + a["pacote"];
                Log(erro);
                Avisar(erro);
                return 2;
            }

            int pid;
            if (!int.TryParse(a["pid"], out pid))
            {
                Log("PID inválido.");
                return 2;
            }

            int timeout = 60;
            int timeoutArg;
            if (a.ContainsKey("timeout") && int.TryParse(a["timeout"], out timeoutArg) && timeoutArg > 0)
                timeout = timeoutArg;

            var versao = a["versao"];
            Log("=== Atualização iniciada ===");
            Log("Alvo: " + versao + " | Pasta: " + a["pasta"] + " | Pacote: " + a["pacote"]);

            var resultadoEspera = EsperarProcesso(pid, timeout);
            if (resultadoEspera != 0)
                return resultadoEspera;

            var carimbo = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            var staging = Path.Combine(a["dados"], "update", "staging-" + carimbo);
            var rollbackDir = Path.Combine(a["dados"], "update", "rollback-" + carimbo);

            try
            {
                ExtrairPacoteValidado(a["pacote"], staging);
            }
            catch (Exception ex)
            {
                Log("Pacote rejeitado: " + ex.Message);
                TentarApagarDiretorio(staging);
                Avisar("O pacote de atualização é inválido ou inseguro.\n\n" + ex.Message + "\n\nNada foi alterado.");
                return 3;
            }

            var listaNova = LerListaArquivos(Path.Combine(staging, "arquivos.txt")) ??
                            ListarArquivosDoDiretorio(staging);
            var listaAntiga = LerListaArquivos(Path.Combine(a["pasta"], "arquivos.txt"));

            foreach (var rel in listaNova)
            {
                if (!File.Exists(Path.Combine(staging, rel)))
                {
                    Log("Lista do pacote inconsistente: falta " + rel);
                    TentarApagarDiretorio(staging);
                    Avisar("O pacote de atualização está incompleto (arquivos.txt não confere).\n\nNada foi alterado.");
                    return 3;
                }
            }

            var resultadoTroca = AplicarTroca(staging, a["pasta"], listaNova, listaAntiga, rollbackDir);
            if (!resultadoTroca.Sucesso)
            {
                Log("FALHA NA TROCA: " + resultadoTroca.Erro);
                RestaurarRollback(rollbackDir, resultadoTroca, a["pasta"]);
                Log("Rollback de arquivos concluído. Cópias preservadas em: " + rollbackDir);
                Avisar("Falha ao aplicar a atualização.\n\n" + resultadoTroca.Erro +
                       "\n\nOs arquivos da versão anterior foram restaurados.");
                TentarReabrir(Path.Combine(a["pasta"], "SUB.exe"));
                return 5;
            }

            var exeNovo = Path.Combine(a["pasta"], "SUB.exe");
            if (!File.Exists(exeNovo) || !VersaoConfere(exeNovo, versao))
            {
                var detalhe = !File.Exists(exeNovo)
                    ? "o arquivo SUB.exe não existe após a troca"
                    : "a versão do SUB.exe não corresponde a " + versao;
                Log("VERIFICAÇÃO PÓS-TROCA FALHOU: " + detalhe);
                RestaurarRollback(rollbackDir, resultadoTroca, a["pasta"]);
                Log("Rollback de arquivos concluído. Cópias preservadas em: " + rollbackDir);
                Avisar("A atualização não passou na verificação final.\n\n" + detalhe +
                       "\n\nOs arquivos da versão anterior foram restaurados.");
                TentarReabrir(Path.Combine(a["pasta"], "SUB.exe"));
                return 6;
            }

            TentarApagarDiretorio(staging);
            try { File.Delete(a["pacote"]); } catch (IOException) { Log("Não foi possível remover o pacote: " + a["pacote"]); }

            EscreverRestauracaoManual(rollbackDir, a["pasta"], a["dados"]);
            File.WriteAllText(Path.Combine(a["dados"], "update", "rollback-pendente.txt"), rollbackDir);

            Log("Atualização aplicada com sucesso. Rollback preservado em: " + rollbackDir);
            Log("A limpeza do rollback ocorrerá após o primeiro início bem-sucedido do SUB novo.");

            var erroReabr = TentarReabrir(exeNovo);
            if (erroReabr != null)
            {
                Log("Não foi possível reabrir o SUB: " + erroReabr);
                Avisar("Atualização concluída, mas não foi possível reabrir o SUB automaticamente.\n\nAbra-o manualmente.");
                return 7;
            }

            Log("SUB reaberto. Atualização finalizada.");
            return 0;
        }

        private static Dictionary<string, string> LerArgs(string[] args)
        {
            var mapa = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < args.Length; i++)
            {
                var chave = args[i];
                if (!chave.StartsWith("--", StringComparison.Ordinal))
                    return null;
                chave = chave.Substring(2);

                if (chave.Equals("sem-mensagens", StringComparison.OrdinalIgnoreCase))
                {
                    mapa["sem-mensagens"] = "1";
                    continue;
                }

                if (i + 1 >= args.Length)
                    return null;
                mapa[chave] = args[++i];
            }

            var obrigatorios = new[] { "pid", "pasta", "pacote", "versao", "dados" };
            foreach (var obr in obrigatorios)
            {
                if (!mapa.ContainsKey(obr) || string.IsNullOrWhiteSpace(mapa[obr]))
                    return null;
            }

            return mapa;
        }

        private static string ValidarPasta(string pasta, string rotulo)
        {
            try
            {
                var full = Path.GetFullPath(pasta);
                if (string.Equals(full, Path.GetPathRoot(full), StringComparison.OrdinalIgnoreCase))
                    return "Raiz de disco não é aceita como " + rotulo + ": " + pasta;
                if (!Directory.Exists(full))
                    return "Pasta não encontrada (" + rotulo + "): " + pasta;
                return null;
            }
            catch (Exception)
            {
                return "Caminho inválido (" + rotulo + "): " + pasta;
            }
        }

        private static int EsperarProcesso(int pid, int timeoutSegundos)
        {
            Process proc;
            try
            {
                proc = Process.GetProcessById(pid);
            }
            catch (ArgumentException)
            {
                Log("O SUB (PID " + pid + ") já havia encerrado.");
                return 0;
            }

            using (proc)
            {
                Log("Aguardando o SUB (PID " + pid + ") encerrar (até " + timeoutSegundos + "s)...");
                if (proc.WaitForExit(timeoutSegundos * 1000))
                {
                    Log("O SUB encerrou normalmente.");
                    return 0;
                }

                Log("Timeout: o SUB ainda está em execução.");
                Avisar("O SUB ainda não foi fechado.\n\nFeche o aplicativo e execute a atualização novamente.\n(O processo não será encerrado automaticamente.)");
                return 4;
            }
        }

        private class EntradaZip
        {
            public string CaminhoRel;
            public ZipArchiveEntry Entry;
        }

        private static void ExtrairPacoteValidado(string caminhoZip, string staging)
        {
            var entradas = new List<EntradaZip>();

            using (var zip = ZipFile.OpenRead(caminhoZip))
            {
                foreach (var entry in zip.Entries)
                {
                    var nome = entry.FullName;
                    if (string.IsNullOrEmpty(nome))
                        continue;

                    if (nome.IndexOf(':') >= 0)
                        throw new InvalidDataException("entrada com caminho inválido (ADS): " + nome);

                    if (nome.StartsWith("/", StringComparison.Ordinal) ||
                        nome.StartsWith("\\", StringComparison.Ordinal) ||
                        (nome.Length >= 2 && nome[1] == ':'))
                        throw new InvalidDataException("caminho absoluto não é aceito: " + nome);

                    var partes = nome.Replace('\\', '/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    var ehDiretorio = nome.EndsWith("/", StringComparison.Ordinal) || nome.EndsWith("\\", StringComparison.Ordinal);

                    foreach (var parte in partes)
                    {
                        if (parte == "." || parte == "..")
                            throw new InvalidDataException("entrada com caminho relativo ('..'): " + nome);
                        if (parte != parte.TrimEnd(' ', '.'))
                            throw new InvalidDataException("nome com ponto ou espaço final: " + nome);
                    }

                    if (EhProtegido(partes))
                        throw new InvalidDataException("o pacote contém arquivo de dados protegido: " + nome);

                    if (!ehDiretorio)
                    {
                        entradas.Add(new EntradaZip
                        {
                            CaminhoRel = string.Join(Path.DirectorySeparatorChar.ToString(), partes),
                            Entry = entry
                        });
                    }
                }

                if (entradas.Count == 0)
                    throw new InvalidDataException("o pacote está vazio");

                var stagingFull = Path.GetFullPath(staging) + Path.DirectorySeparatorChar;
                Directory.CreateDirectory(staging);

                foreach (var entrada in entradas)
                {
                    var destino = Path.GetFullPath(Path.Combine(staging, entrada.CaminhoRel));
                    if (!destino.StartsWith(stagingFull, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidDataException("entrada escapa da pasta de destino: " + entrada.Entry.FullName);

                    Directory.CreateDirectory(Path.GetDirectoryName(destino));
                    entrada.Entry.ExtractToFile(destino, true);
                }
            }
        }

        private static bool EhProtegido(string[] partes)
        {
            if (partes == null || partes.Length == 0)
                return false;

            var ultimo = NormalizarNome(partes[partes.Length - 1]);
            if (ultimo.StartsWith("sub.sdf", StringComparison.Ordinal))
                return true;
            if (ultimo.StartsWith("instituicao.json", StringComparison.Ordinal))
                return true;

            foreach (var parte in partes)
            {
                var n = NormalizarNome(parte);
                if (n == "mapeamentoanual" || n == "relatorios" || n == "backups" || n == "update")
                    return true;
            }

            return false;
        }

        private static string NormalizarNome(string valor)
        {
            return (valor ?? string.Empty).Trim().TrimEnd('.', ' ').ToLowerInvariant();
        }

        private static List<string> LerListaArquivos(string caminho)
        {
            if (!File.Exists(caminho))
                return null;

            var lista = new List<string>();
            foreach (var linha in File.ReadAllLines(caminho))
            {
                var t = linha.Trim();
                if (t.Length == 0 || t.StartsWith("#", StringComparison.Ordinal))
                    continue;

                var rel = t.Replace('/', Path.DirectorySeparatorChar)
                           .Replace('\\', Path.DirectorySeparatorChar);

                if (EhCaminhoRelInseguro(rel))
                {
                    Log("Ignorando linha insegura em " + Path.GetFileName(caminho) + ": " + t);
                    continue;
                }

                lista.Add(rel);
            }

            return lista;
        }

        private static bool EhCaminhoRelInseguro(string rel)
        {
            if (string.IsNullOrWhiteSpace(rel))
                return true;
            if (Path.IsPathRooted(rel) || rel.IndexOf(':') >= 0)
                return true;

            var partes = rel.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parte in partes)
            {
                if (parte == "." || parte == "..")
                    return true;
                if (parte != parte.TrimEnd(' ', '.'))
                    return true;
            }

            return EhProtegido(partes);
        }

        private static List<string> ListarArquivosDoDiretorio(string raiz)
        {
            var raizFull = Path.GetFullPath(raiz);
            var lista = new List<string>();
            foreach (var arquivo in Directory.GetFiles(raizFull, "*", SearchOption.AllDirectories))
            {
                lista.Add(arquivo.Substring(raizFull.Length + 1));
            }
            return lista;
        }

        private class ResultadoTroca
        {
            public bool Sucesso;
            public string Erro;
            public readonly List<string> Substituidos = new List<string>();
            public readonly List<string> Adicionados = new List<string>();
            public readonly List<string> Removidos = new List<string>();
        }

        private static ResultadoTroca AplicarTroca(string staging, string pasta, List<string> listaNova, List<string> listaAntiga, string rollbackDir)
        {
            var res = new ResultadoTroca();
            Directory.CreateDirectory(rollbackDir);

            var setNova = new HashSet<string>(listaNova, StringComparer.OrdinalIgnoreCase);
            HashSet<string> setAntiga = null;
            if (listaAntiga != null)
                setAntiga = new HashSet<string>(listaAntiga, StringComparer.OrdinalIgnoreCase);

            foreach (var rel in listaNova)
            {
                try
                {
                    var src = Path.Combine(staging, rel);
                    var dst = Path.Combine(pasta, rel);
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));

                    if (File.Exists(dst))
                    {
                        var rb = Path.Combine(rollbackDir, rel);
                        Directory.CreateDirectory(Path.GetDirectoryName(rb));
                        File.Copy(dst, rb, true);
                        res.Substituidos.Add(rel);
                    }
                    else
                    {
                        res.Adicionados.Add(rel);
                    }

                    File.Copy(src, dst, true);
                    Log("Aplicado: " + rel);
                }
                catch (Exception ex)
                {
                    res.Erro = "Falha ao aplicar '" + rel + "': " + ex.Message;
                    return res;
                }
            }

            if (setAntiga != null)
            {
                foreach (var rel in setAntiga)
                {
                    if (setNova.Contains(rel))
                        continue;

                    try
                    {
                        var dst = Path.Combine(pasta, rel);
                        if (File.Exists(dst))
                        {
                            var rb = Path.Combine(rollbackDir, rel);
                            Directory.CreateDirectory(Path.GetDirectoryName(rb));
                            File.Copy(dst, rb, true);
                            File.Delete(dst);
                            res.Removidos.Add(rel);
                            Log("Removido (obsoleto): " + rel);
                        }
                    }
                    catch (Exception ex)
                    {
                        res.Erro = "Falha ao remover obsoleto '" + rel + "': " + ex.Message;
                        return res;
                    }
                }
            }

            res.Sucesso = true;
            return res;
        }

        private static void RestaurarRollback(string rollbackDir, ResultadoTroca res, string pasta)
        {
            foreach (var rel in res.Adicionados)
            {
                try
                {
                    var dst = Path.Combine(pasta, rel);
                    if (File.Exists(dst))
                        File.Delete(dst);
                }
                catch (Exception ex) { Log("Rollback (remoção de novo): falha em " + rel + ": " + ex.Message); }
            }

            foreach (var rel in res.Substituidos)
            {
                RestaurarUm(rollbackDir, rel, pasta);
            }

            foreach (var rel in res.Removidos)
            {
                RestaurarUm(rollbackDir, rel, pasta);
            }
        }

        private static void RestaurarUm(string rollbackDir, string rel, string pasta)
        {
            try
            {
                var origem = Path.Combine(rollbackDir, rel);
                if (File.Exists(origem))
                {
                    var dst = Path.Combine(pasta, rel);
                    Directory.CreateDirectory(Path.GetDirectoryName(dst));
                    File.Copy(origem, dst, true);
                    Log("Restaurado: " + rel);
                }
            }
            catch (Exception ex) { Log("Rollback: falha ao restaurar " + rel + ": " + ex.Message); }
        }

        private static void EscreverRestauracaoManual(string rollbackDir, string pasta, string dados)
        {
            var texto = new StringBuilder();
            texto.AppendLine("RESTAURAÇÃO MANUAL DA VERSÃO ANTERIOR");
            texto.AppendLine("Gerado em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            texto.AppendLine();
            texto.AppendLine("1. Feche o SUB (se estiver aberto).");
            texto.AppendLine("2. Copie TODO o conteúdo abaixo de volta para a pasta do programa:");
            texto.AppendLine("   " + rollbackDir);
            texto.AppendLine("   Pasta do programa: " + pasta);
            texto.AppendLine("3. (Se o banco tiver sido afetado) substitua os arquivos em:");
            texto.AppendLine("   " + Path.Combine(dados, "SUB") + " (sub.sdf)");
            texto.AppendLine("   " + dados + " (instituicao.json)");
            texto.AppendLine("   pelos arquivos do backup mais recente em:");
            texto.AppendLine("   " + Path.Combine(dados, "backups"));
            texto.AppendLine("4. Abra o SUB novamente.");
            texto.AppendLine();
            texto.AppendLine("OBS: este diretório de rollback só é apagado automaticamente");
            texto.AppendLine("após o SUB iniciar com sucesso uma vez.");

            File.WriteAllText(Path.Combine(rollbackDir, "RESTAURACAO.txt"), texto.ToString());
        }

        private static string TentarReabrir(string exe)
        {
            try
            {
                Process.Start(exe);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static bool VersaoConfere(string exe, string esperada)
        {
            var atual = FileVersionInfo.GetVersionInfo(exe).FileVersion;
            return CompararVersoes(atual, esperada) == 0;
        }

        private static int CompararVersoes(string a, string b)
        {
            var va = ParaVersaoNormalizada(a);
            var vb = ParaVersaoNormalizada(b);
            for (int i = 0; i < 4; i++)
            {
                var cmp = va[i].CompareTo(vb[i]);
                if (cmp != 0)
                    return cmp;
            }
            return 0;
        }

        private static int[] ParaVersaoNormalizada(string versao)
        {
            var resultado = new int[4];
            if (string.IsNullOrWhiteSpace(versao))
                return resultado;

            var cortado = versao.Trim();
            var corte = cortado.IndexOfAny(new[] { '-', '+' });
            if (corte >= 0)
                cortado = cortado.Substring(0, corte);

            var partes = cortado.Split('.');
            for (int i = 0; i < 4; i++)
            {
                int valor;
                if (i < partes.Length && int.TryParse(partes[i], out valor))
                    resultado[i] = valor;
            }
            return resultado;
        }

        private static void TentarApagarDiretorio(string pasta)
        {
            try
            {
                if (Directory.Exists(pasta))
                    Directory.Delete(pasta, true);
            }
            catch (Exception ex) { Log("Não foi possível remover " + pasta + ": " + ex.Message); }
        }

        private static void Log(string mensagem)
        {
            try
            {
                if (string.IsNullOrEmpty(caminhoLog))
                    return;
                Directory.CreateDirectory(Path.GetDirectoryName(caminhoLog));
                File.AppendAllText(caminhoLog,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + mensagem + Environment.NewLine);
            }
            catch (IOException) { }
        }

        private static void Avisar(string mensagem)
        {
            if (semMensagens)
                return;
            try
            {
                MessageBox.Show(mensagem, "Atualização do SUB", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception) { }
        }
    }
}
