using Newtonsoft.Json;

namespace SUB.Models
{
    public class AtualizacaoManifesto
    {
        [JsonProperty("versao")]
        public string Versao { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("tamanho")]
        public long Tamanho { get; set; }

        [JsonProperty("notas")]
        public string Notas { get; set; }
    }

    public class ResultadoVerificacaoAtualizacao
    {
        public bool Sucesso { get; set; }
        public bool AtualizacaoDisponivel { get; set; }
        public bool SemInternet { get; set; }
        public bool ManifestoInvalido { get; set; }
        public bool AssinaturaInvalida { get; set; }
        public string Mensagem { get; set; }
        public string VersaoAtual { get; set; }
        public string VersaoNova { get; set; }
        public AtualizacaoManifesto Manifesto { get; set; }
    }

    public class ProgressoDownload
    {
        public long BytesBaixados { get; set; }
        public long TotalBytes { get; set; }
    }

    public class ResultadoDownloadPacote
    {
        public bool Sucesso { get; set; }
        public bool Cancelado { get; set; }
        public bool HashDivergente { get; set; }
        public bool HostNaoPermitido { get; set; }
        public bool EspacoInsuficiente { get; set; }
        public string Mensagem { get; set; }
        public string CaminhoArquivo { get; set; }
        public long Tamanho { get; set; }
    }
}
