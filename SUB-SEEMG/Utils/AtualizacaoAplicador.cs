using SUB.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SUB.Utils
{
    public static class AtualizacaoAplicador
    {
        public static string PastaInstalacao => Application.StartupPath;

        public static string CaminhoUpdaterInstalado =>
            Path.Combine(Application.StartupPath, "SUB-UPDATER.exe");

        public static string CaminhoUpdaterTemp =>
            Path.Combine(AppPaths.UpdateFolder, "updater", "SUB-UPDATER.exe");

        public static bool PastaInstalacaoGravavel()
        {
            try
            {
                var teste = Path.Combine(Application.StartupPath, ".sub-gravavel-" + Guid.NewGuid().ToString("N"));
                File.WriteAllText(teste, "ok");
                File.Delete(teste);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ValidarPacoteParaAplicar(string caminhoZip, AtualizacaoManifesto manifesto)
        {
            if (manifesto == null)
                return "Manifesto de atualização indisponível.";

            if (string.IsNullOrWhiteSpace(caminhoZip) || !File.Exists(caminhoZip))
                return "O pacote baixado não foi encontrado. Verifique novamente as atualizações.";

            var info = new FileInfo(caminhoZip);
            if (info.Length != manifesto.Tamanho)
                return "O tamanho do pacote não confere mais com o manifesto. Baixe novamente.";

            var hash = AtualizacaoService.CalcularSha256(caminhoZip);
            if (!string.Equals(hash, (manifesto.Sha256 ?? string.Empty).Trim(), StringComparison.OrdinalIgnoreCase))
                return "O hash SHA-256 do pacote não confere mais. Baixe novamente.";

            if (!File.Exists(CaminhoUpdaterInstalado))
                return "O atualizador (SUB-UPDATER.exe) não foi encontrado na pasta do programa.";

            return null;
        }

        public static string FazerBackupDados()
        {
            return FazerBackupDados(AppPaths.AppDataFolder);
        }

        internal static string FazerBackupDados(string dadosFolder)
        {
            try
            {
                var carimbo = DateTime.Now.ToString("yyyyMMdd-HHmmss");
                var destino = Path.Combine(dadosFolder, "backups", carimbo);
                Directory.CreateDirectory(destino);

                var pastaBanco = Path.Combine(dadosFolder, "SUB");
                if (Directory.Exists(pastaBanco))
                {
                    foreach (var arquivo in Directory.GetFiles(pastaBanco, "sub.sdf*"))
                        File.Copy(arquivo, Path.Combine(destino, Path.GetFileName(arquivo)), true);
                }

                var config = Path.Combine(dadosFolder, "instituicao.json");
                if (File.Exists(config))
                    File.Copy(config, Path.Combine(destino, "instituicao.json"), true);

                PodarBackups(Path.Combine(dadosFolder, "backups"), 3);
                return null;
            }
            catch (Exception ex)
            {
                return "Falha ao fazer o backup dos dados: " + ex.Message;
            }
        }

        internal static void PodarBackups(string pastaBackups, int manter)
        {
            if (!Directory.Exists(pastaBackups))
                return;

            var pastas = Directory.GetDirectories(pastaBackups);
            Array.Sort(pastas, StringComparer.Ordinal);
            for (int i = 0; i < pastas.Length - manter; i++)
                Directory.Delete(pastas[i], true);
        }

        public static string IniciarUpdater(AtualizacaoManifesto manifesto, string caminhoZip)
        {
            try
            {
                var destino = CaminhoUpdaterTemp;
                Directory.CreateDirectory(Path.GetDirectoryName(destino));
                File.Copy(CaminhoUpdaterInstalado, destino, true);

                var args = string.Format(
                    "--pid {0} --pasta \"{1}\" --pacote \"{2}\" --versao \"{3}\" --dados \"{4}\"",
                    Process.GetCurrentProcess().Id,
                    PastaInstalacao,
                    caminhoZip,
                    AtualizacaoService.NormalizarVersao(manifesto.Versao),
                    AppPaths.AppDataFolder);

                Process.Start(destino, args);
                return null;
            }
            catch (Exception ex)
            {
                return "Falha ao iniciar o atualizador: " + ex.Message;
            }
        }

        public static void ConcluirAtualizacaoPendente()
        {
            ConcluirAtualizacaoPendente(AppPaths.UpdateFolder);
        }

        internal static void ConcluirAtualizacaoPendente(string pastaUpdate)
        {
            try
            {
                var pendente = Path.Combine(pastaUpdate, "rollback-pendente.txt");
                if (!File.Exists(pendente))
                    return;

                var rollback = (File.ReadAllText(pendente) ?? string.Empty).Trim();
                var rollbackFull = Path.GetFullPath(rollback);
                var baseFull = Path.GetFullPath(pastaUpdate) + Path.DirectorySeparatorChar;

                File.WriteAllText(
                    Path.Combine(pastaUpdate, "atualizacao-ok.txt"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - versao " +
                    Assembly.GetExecutingAssembly().GetName().Version);

                if (rollbackFull.StartsWith(baseFull, StringComparison.OrdinalIgnoreCase) &&
                    Directory.Exists(rollbackFull))
                {
                    Directory.Delete(rollbackFull, true);
                }

                File.Delete(pendente);
            }
            catch
            {
            }
        }
    }
}
