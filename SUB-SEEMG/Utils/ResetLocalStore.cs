using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SUB.Utils
{
    internal sealed class PedidoReset
    {
        public string Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime CriadoEmUtc { get; set; }
        public bool Iniciado { get; set; }
    }

    // A produção usa somente AppPaths.AppDataFolder. O caminho injetável
    // permite testar a limpeza exclusivamente em instalações temporárias.
    internal sealed class ResetLocalStore
    {
        private const string NomePedido = ".reset-autorizado.dat";
        private readonly string pasta;
        private string ArquivoPedido
        {
            get
            {
                return Path.Combine(pasta, NomePedido);
            }
        }

        internal bool Pendente
        {
            get
            {
                return File.Exists(ArquivoPedido);
            }
        }

        internal ResetLocalStore(string pastaDados)
        {
            pasta = Path.GetFullPath(pastaDados).TrimEnd(Path.DirectorySeparatorChar);
            if (!string.Equals(Path.GetFileName(pasta), "SUB", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("A limpeza só pode atingir uma pasta de dados SUB.");
            ValidarPasta();
        }

        private void ValidarPasta()
        {
            for (var diretorio = new DirectoryInfo(pasta); diretorio != null; diretorio = diretorio.Parent)
                if (diretorio.Exists && (diretorio.Attributes & FileAttributes.ReparsePoint) != 0)
                    throw new IOException("A pasta de dados contém um atalho ou redirecionamento. Reset cancelado.");
        }

        internal void Preparar(PedidoReset pedido)
        {
            ValidarPasta();
            if (Pendente)
                throw new InvalidOperationException("Já existe um reset autorizado aguardando conclusão.");
            if (pedido == null || string.IsNullOrEmpty(pedido.Id) || pedido.UsuarioId <= 0 || pedido.Iniciado)
                throw new ArgumentException("Autorização de reset inválida.");
            Directory.CreateDirectory(pasta);
            Gravar(pedido);
        }

        private void Gravar(PedidoReset pedido)
        {
            byte[] dados = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pedido));
            byte[] protegidos = ProtectedData.Protect(dados, null, DataProtectionScope.CurrentUser);
            string temporario = ArquivoPedido + ".tmp";
            File.WriteAllBytes(temporario, protegidos);
            if (File.Exists(ArquivoPedido))
                File.Replace(temporario, ArquivoPedido, null);
            else
                File.Move(temporario, ArquivoPedido);
        }

        private PedidoReset Ler()
        {
            ValidarPasta();
            if ((File.GetAttributes(ArquivoPedido) & FileAttributes.ReparsePoint) != 0)
                throw new IOException("O pedido de reset não pode ser um atalho.");
            byte[] dados = ProtectedData.Unprotect(File.ReadAllBytes(ArquivoPedido), null, DataProtectionScope.CurrentUser);
            var pedido = JsonConvert.DeserializeObject<PedidoReset>(Encoding.UTF8.GetString(dados));
            Guid id;
            if (pedido == null || !Guid.TryParse(pedido.Id, out id) || pedido.UsuarioId <= 0 || pedido.CriadoEmUtc == default(DateTime) || pedido.CriadoEmUtc > DateTime.UtcNow.AddMinutes(1))
                throw new InvalidOperationException("Pedido de reset inválido. Nenhum dado foi apagado.");
            return pedido;
        }

        internal void Cancelar(string id)
        {
            if (Pendente && Ler().Id == id)
                File.Delete(ArquivoPedido);
        }

        internal bool ConcluirSePendente()
        {
            if (!Pendente)
                return false;
            PedidoReset pedido = Ler();
            if (!pedido.Iniciado && DateTime.UtcNow - pedido.CriadoEmUtc > TimeSpan.FromMinutes(10))
            {
                File.Delete(ArquivoPedido);
                throw new InvalidOperationException("A autorização do reset expirou. Entre novamente e repita as duas verificações. Nenhum dado foi apagado.");
            }

            var arquivos = new List<string>();
            var diretorios = new List<string>();
            Enumerar(pasta, arquivos, diretorios);
            // Detecta arquivos ocupados ou sem permissão antes da primeira exclusão.
            var bloqueios = new List<FileStream>();
            try
            {
                foreach (string arquivo in arquivos.Where(a => !string.Equals(a, ArquivoPedido, StringComparison.OrdinalIgnoreCase)))
                    bloqueios.Add(new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.None));
            }
            finally
            {
                foreach (var bloqueio in bloqueios)
                    bloqueio.Dispose();
            }

            pedido.Iniciado = true;
            Gravar(pedido);
            foreach (string arquivo in arquivos.Where(a => !string.Equals(a, ArquivoPedido, StringComparison.OrdinalIgnoreCase)))
            {
                ValidarEntrada(arquivo);
                File.Delete(arquivo);
            }

            foreach (string diretorio in diretorios.OrderByDescending(d => d.Length))
            {
                ValidarEntrada(diretorio);
                Directory.Delete(diretorio, false);
            }

            // O pedido permanece até a limpeza terminar: uma falha pode ser
            // retomada no próximo início, antes de abrir login ou primeiro uso.
            if (Directory.GetFileSystemEntries(pasta).Any(a => !string.Equals(a, ArquivoPedido, StringComparison.OrdinalIgnoreCase)))
                throw new IOException("Ainda existem arquivos de dados. Reabra o SUB para concluir o reset autorizado.");
            File.Delete(ArquivoPedido);
            return true;
        }

        private void ValidarEntrada(string caminho)
        {
            string absoluto = Path.GetFullPath(caminho);
            if (!absoluto.StartsWith(pasta + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) || (File.Exists(absoluto) || Directory.Exists(absoluto)) && (File.GetAttributes(absoluto) & FileAttributes.ReparsePoint) != 0)
                throw new IOException("Foi encontrado um caminho fora dos dados locais ou um redirecionamento. Reset interrompido.");
        }

        private void Enumerar(string atual, List<string> arquivos, List<string> diretorios)
        {
            ValidarPasta();
            foreach (string entrada in Directory.GetFileSystemEntries(atual))
            {
                ValidarEntrada(entrada);
                if (Directory.Exists(entrada))
                {
                    diretorios.Add(entrada);
                    Enumerar(entrada, arquivos, diretorios);
                }
                else
                    arquivos.Add(entrada);
            }
        }
    }
}
