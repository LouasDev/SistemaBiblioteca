using System;
using Newtonsoft.Json;

namespace SUB.Models
{
    // Nullable na configuração para que arquivos criados antes deste campo
    // continuem usando TemaEscuro sem qualquer migração obrigatória.
    public enum ModoAparencia
    {
        Claro,
        Escuro,
        PadraoDoSistema
    }

    public class InstituicaoConfiguracao
    {
        public string Nome { get; set; }
        public string CorPrimaria { get; set; }
        public bool TemaEscuro { get; set; }
        public ModoAparencia? Aparencia { get; set; }
        // Credenciais da primeira bibliotecária pertencem exclusivamente ao banco.
        // Estas propriedades permanecem apenas para leitura de configurações antigas
        // e não devem voltar a gerar chaves nulas no instituicao.json.
        public string CodigoMestreHash { get; set; }
        public string CodigoMestreSalt { get; set; }
        public DateTime ConfiguradaEm { get; set; }
        public DateTime AtualizadaEm { get; set; }
    }
}

