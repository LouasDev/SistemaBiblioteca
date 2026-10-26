using SUB.Models;
using System;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Windows.Forms;

namespace SUB.Utils
{
    internal static class ResetService
    {
        internal sealed class Autorizacao
        {
            internal int UsuarioId;
            internal DateTime CriadaEmUtc;
        }

        private static int falhas;
        private static DateTime bloqueadoAteUtc;

        internal static ResumoReset ObterResumo()
        {
            if (!Sessao.UsuarioAutenticado || Sessao.Perfil != "Bibliotecário(a)")
                throw new InvalidOperationException("Entre com uma conta de bibliotecário para solicitar o reset.");
            using (var conexao = Conexao.ObterConexao())
            {
                conexao.Open();
                return new ResumoReset
                {
                    Usuarios = Contar(conexao, "usuarios"),
                    Titulos = Contar(conexao, "Livros"),
                    Emprestimos = Contar(conexao, "Emprestimo"),
                    EmprestimosRapidos = Contar(conexao, "EmprestimoRapido"),
                    Turmas = Contar(conexao, "Turmas")
                };
            }
        }

        private static int Contar(SqlCeConnection conexao, string tabela)
        {
            using (var comando = new SqlCeCommand("SELECT COUNT(*) FROM " + tabela, conexao))
                return Convert.ToInt32(comando.ExecuteScalar());
        }

        internal static string ValidarArquivoCodigo(string caminho)
        {
            if (DateTime.UtcNow < bloqueadoAteUtc)
                throw new InvalidOperationException("Limite de tentativas atingido. Aguarde cinco minutos antes de tentar novamente.");
            if (!Sessao.UsuarioAutenticado || Sessao.Perfil != "Bibliotecário(a)")
                throw new InvalidOperationException("Entre com uma conta de bibliotecário para solicitar o reset.");

            string codigo;
            try { codigo = CodigoMestreArquivo.Ler(caminho); }
            catch (FormatException ex) { throw new FormatException(RegistrarFalha(ex.Message)); }
            if (!ValidarCodigoMestre(InstituicaoConfiguracaoStore.Carregar(), codigo))
                throw new InvalidOperationException(RegistrarFalha("Este TXT não corresponde ao código mestre desta instalação."));
            return codigo;
        }

        private static string RegistrarFalha(string mensagem)
        {
            if (++falhas < 5)
                return mensagem;
            falhas = 0;
            bloqueadoAteUtc = DateTime.UtcNow.AddMinutes(5);
            return "Limite de tentativas atingido. Aguarde cinco minutos antes de tentar novamente.";
        }

        internal static bool ValidarCodigoMestre(InstituicaoConfiguracao configuracao, string codigo)
        {
            if (configuracao == null || string.IsNullOrWhiteSpace(codigo) ||
                string.IsNullOrWhiteSpace(configuracao.CodigoMestreHash) ||
                string.IsNullOrWhiteSpace(configuracao.CodigoMestreSalt))
                return false;
            try { return CriptografiaSenha.VerificarSenha(codigo.Trim(), configuracao.CodigoMestreHash, configuracao.CodigoMestreSalt); }
            catch (FormatException) { return false; }
            catch (ArgumentException) { return false; }
        }

        internal static bool TentarAutorizar(string codigo, string senha, out Autorizacao autorizacao, out string erro)
        {
            autorizacao = null;
            erro = "O código mestre ou a senha não conferem.";
            if (DateTime.UtcNow < bloqueadoAteUtc)
            {
                erro = "Limite de tentativas atingido. Aguarde cinco minutos antes de tentar novamente.";
                return false;
            }
            if (!Sessao.UsuarioAutenticado)
            {
                erro = "Entre com uma conta de bibliotecário para solicitar o reset.";
                return false;
            }

            var configuracao = InstituicaoConfiguracaoStore.Carregar();
            using (var conexao = Conexao.ObterConexao())
            using (var comando = new SqlCeCommand(
                "SELECT TipoUsuario, Ativo, Senha_Hash, Senha_Salt FROM usuarios WHERE Id = @id", conexao))
            {
                comando.Parameters.AddWithValue("@id", Sessao.UsuarioId.Value);
                conexao.Open();
                using (var leitor = comando.ExecuteReader())
                {
                    if (leitor.Read() && ValidarCredenciais(configuracao, leitor["TipoUsuario"].ToString(),
                        leitor["Ativo"] == DBNull.Value || Convert.ToBoolean(leitor["Ativo"]),
                        leitor["Senha_Hash"].ToString(), leitor["Senha_Salt"].ToString(), codigo, senha))
                    {
                        falhas = 0;
                        autorizacao = new Autorizacao { UsuarioId = Sessao.UsuarioId.Value, CriadaEmUtc = DateTime.UtcNow };
                        return true;
                    }
                }
            }
            erro = RegistrarFalha(erro);
            return false;
        }

        internal static bool ValidarCredenciais(InstituicaoConfiguracao configuracao, string perfil, bool ativo,
            string hash, string salt, string codigo, string senha)
        {
            if (!ativo || perfil != "Bibliotecário(a)" || configuracao == null ||
                string.IsNullOrWhiteSpace(configuracao.CodigoMestreHash) ||
                string.IsNullOrWhiteSpace(configuracao.CodigoMestreSalt) ||
                string.IsNullOrWhiteSpace(codigo) || string.IsNullOrEmpty(senha))
                return false;
            try
            {
                bool mestre = ValidarCodigoMestre(configuracao, codigo);
                bool bibliotecario = CriptografiaSenha.VerificarSenha(senha, hash, salt);
                return mestre && bibliotecario;
            }
            catch (FormatException) { return false; }
            catch (ArgumentException) { return false; }
        }

        internal static void Iniciar(Autorizacao autorizacao)
        {
            if (autorizacao == null || !Sessao.UsuarioAutenticado || autorizacao.UsuarioId != Sessao.UsuarioId.Value ||
                DateTime.UtcNow - autorizacao.CriadaEmUtc > TimeSpan.FromMinutes(2))
                throw new InvalidOperationException("Confirme novamente o código mestre e a senha para resetar.");

            var store = new ResetLocalStore(AppPaths.AppDataFolder);
            var pedido = new PedidoReset { Id = Guid.NewGuid().ToString("N"), UsuarioId = autorizacao.UsuarioId, CriadoEmUtc = DateTime.UtcNow };
            store.Preparar(pedido);
            try
            {
                var processo = Process.Start(new ProcessStartInfo(Application.ExecutablePath,
                    "--aguardar-reset " + Process.GetCurrentProcess().Id)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                if (processo == null)
                    throw new InvalidOperationException("Não foi possível reiniciar o SUB.");
                processo.Dispose();
            }
            catch
            {
                store.Cancelar(pedido.Id);
                throw;
            }
        }

        internal static bool AguardarInstanciaAnterior(string[] args)
        {
            if (args.Length == 0)
                return true;
            int pid;
            if (args.Length != 2 || args[0] != "--aguardar-reset" || !int.TryParse(args[1], out pid) || pid <= 0 ||
                pid == Process.GetCurrentProcess().Id)
                return false;
            try
            {
                using (var processo = Process.GetProcessById(pid))
                    if (!processo.WaitForExit(30000))
                    {
                        MessageBox.Show("Feche o SUB e abra novamente para concluir o reset já autorizado.", "Reset pendente",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
            }
            catch (ArgumentException) { }
            return true;
        }
    }
}
