using System;
using System.Drawing;
using System.Windows.Forms;
using SUB.Forms.Usuario;
using SUB.Models;
using SUB.Utils;

namespace SUB.Forms.Configuracoes
{
    public partial class ConfiguracaoForm
    {
        private void btnApagarDados_Click(object sender, EventArgs e)
        {
            if (operacaoEmAndamento)
                return;
            if (!Sessao.UsuarioAutenticado || Sessao.Perfil != "Bibliotecário(a)")
            {
                MessageBox.Show("Entre com uma conta de bibliotecário para resetar o aplicativo.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DefinirModoOperacao(true);
            try
            {
                Color corTemaSalva = ObterCorTemaSalva();
                string codigo;
                using (var dialogo = new ResetTermoForm
                {
                    Resumo = ResetService.ObterResumo()
                }

                )
                {
                    dialogo.AplicarTema(corTemaSalva);
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;
                    codigo = dialogo.CodigoMestre;
                }

                string senha;
                using (var dialogo = new PasswordForm
                {
                    Titulo = "Reset: etapa 2 de 2",
                    Mensagem = "TXT validado. Digite a senha da bibliotecária logada."
                }

                )
                {
                    ThemeManager.Apply(dialogo, corTemaSalva);
                    if (dialogo.ShowDialog(this) != DialogResult.OK)
                        return;
                    senha = dialogo.SenhaDigitada;
                }

                ResetService.Autorizacao autorizacao;
                string erro;
                if (!ResetService.TentarAutorizar(codigo, senha, out autorizacao, out erro))
                {
                    MessageBox.Show(erro, "Reset não autorizado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("As duas verificações foram concluídas.\n\n" + "Apagar definitivamente todos os dados locais e voltar ao primeiro uso?\n\n" + "Os backups locais também serão apagados. Esta ação não pode ser desfeita.", "Confirmar reset definitivo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;
                ResetService.Iniciar(autorizacao);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível iniciar o reset.\n\n" + ex.Message, "Reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    DefinirModoOperacao(false);
            }
        }
    }
}
