using BibliotecaApp.Utils.Dtos;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaApp.Services
{
    public static class EmailService
    {
        /// <summary>
        /// HttpClient estático reutilizável. Singleton pattern previne socket exhaustion.
        /// </summary>
        private static readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Define se a API central de e-mails está habilitada.
        /// Se false, mantém tentativa com SMTP (legado).
        /// </summary>
        private static bool _apiEmailEnabled = GetApiEmailEnabled();

        /// <summary>
        /// URL da API central para envio de e-mails.
        /// Carregada do app.config via chave "ApiEmailUrl".
        /// </summary>
        private static string _apiEmailUrl = System.Configuration.ConfigurationManager.AppSettings["ApiEmailUrl"] ?? "";

        /// <summary>
        /// Tempo limite (em segundos) para requisições à API.
        /// </summary>
        private static readonly int _timeoutSeconds = int.TryParse(
            System.Configuration.ConfigurationManager.AppSettings["ApiEmailTimeoutSeconds"],
            out var timeout) ? timeout : 30;

        #region Inicialização

        /// <summary>
        /// Lê a chave "ApiEmailEnabled" do app.config para determinar se usa a API central.
        /// </summary>
        private static bool GetApiEmailEnabled()
        {
            var setting = System.Configuration.ConfigurationManager.AppSettings["ApiEmailEnabled"] ?? "true";
            return bool.TryParse(setting, out var result) && result;
        }

        #endregion

        /// <summary>
        /// Envia um e-mail via API central ou SMTP (fallback).
        /// 
        /// Se ApiEmailEnabled=true no app.config e a API estiver configurada:
        ///   - Chama a API central via HTTP POST (recomendado)
        /// Senão:
        ///   - Tenta SMTP como fallback (não implementado nesta versão; poderia ser legado)
        /// </summary>
        /// <param name="destinatario">E-mail do destinatário</param>
        /// <param name="assunto">Assunto do e-mail</param>
        /// <param name="mensagem">Corpo do e-mail (HTML ou texto simples)</param>
        /// <param name="caminhoAnexo">Caminho opcional para um arquivo a anexar</param>
        public static void Enviar(string destinatario, string assunto, string mensagem, string caminhoAnexo = null)
        {
            try
            {
                if (_apiEmailEnabled && !string.IsNullOrEmpty(_apiEmailUrl))
                {
                    // Executa assincronamente mas aguarda o resultado (sincronamente)
                    var task = EnviarViaApiAsync(destinatario, assunto, mensagem, caminhoAnexo);
                    task.Wait(_timeoutSeconds * 1000);
                }
                else
                {
                    // Fallback: tente SMTP (implementar se necessário, por enquanto apenas alerta)
                    MessageBox.Show(
                        "Aviso: API de e-mail não está configurada. Verifique app.config.\n\n" +
                        "Configurações necessárias:\n" +
                        "  ApiEmailEnabled = true\n" +
                        "  ApiEmailUrl = https://sua-api.com/email",
                        "Sistema de E-mail Desconfigured",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (AggregateException aex) when (aex.InnerException is TaskCanceledException)
            {
                MessageBox.Show(
                    "Timeout ao enviar e-mail. A API central pode estar indisponível.\n\n" +
                    "Tente novamente em alguns momentos.",
                    "Timeout na Requisição",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao enviar e-mail:\n\n" + ex.Message,
                    "Erro ao Enviar E-mail",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Envia o e-mail via API central (HTTP POST).
        /// </summary>
        private static async Task EnviarViaApiAsync(
            string destinatario,
            string assunto,
            string mensagem,
            string caminhoAnexo)
        {
            // Carrega a configuração da escola (remetente e identificador)
            var config = ConfigService.Carregar();

            // Define o remetente: usa EmailInstitucional se disponível, senão fallback
            var remetente = !string.IsNullOrEmpty(config.EmailInstitucional)
                ? config.EmailInstitucional
                : "no-reply@escola.local";

            // Define o ID da escola se disponível
            var escolaId = !string.IsNullOrEmpty(config.EscolaId)
                ? config.EscolaId
                : "ESCOLA-PADRAO";

            // Processa anexo se fornecido
            string anexoBase64 = null;
            string nomeAnexo = null;

            if (!string.IsNullOrEmpty(caminhoAnexo) && File.Exists(caminhoAnexo))
            {
                try
                {
                    byte[] conteudoArquivo = File.ReadAllBytes(caminhoAnexo);
                    anexoBase64 = Convert.ToBase64String(conteudoArquivo);
                    nomeAnexo = Path.GetFileName(caminhoAnexo);
                }
                catch (Exception ex)
                {
                    // Aviso: não impede envio, apenas não anexa
                    System.Diagnostics.Debug.WriteLine($"Erro ao ler anexo: {ex.Message}");
                }
            }

            // Monta o DTO de requisição
            var request = new EmailRequest
            {
                EscolaId = escolaId,
                Remetente = remetente,
                Destinatario = destinatario,
                Assunto = assunto,
                Mensagem = mensagem,
                AnexoBase64 = anexoBase64,
                NomeAnexo = nomeAnexo
            };

            // Serializa para JSON
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Configura timeout
            using (var cts = new System.Threading.CancellationTokenSource(_timeoutSeconds * 1000))
            {
                // Faz a chamada HTTP POST
                var response = await _httpClient.PostAsync(_apiEmailUrl, content, cts.Token);

                // Verifica sucesso
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception(
                        $"API retornou erro {(int)response.StatusCode}:\n{errorContent}");
                }
            }
        }
    }
}