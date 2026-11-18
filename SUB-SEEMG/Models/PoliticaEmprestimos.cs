using System;
using SUB.Utils;

namespace SUB.Models
{
    public class PoliticaEmprestimos
    {
        public int LimiteAluno { get; set; } = 1;
        public int LimiteProfessor { get; set; } = -1;
        public int LimiteBibliotecario { get; set; } = -1;
        public int PrazoDias { get; set; } = 7;
        public int MaximoProrrogacoes { get; set; } = -1;

        public PoliticaEmprestimos Copiar() => (PoliticaEmprestimos)MemberwiseClone();

        public void Validar()
        {
            if ((LimiteAluno != -1 && (LimiteAluno < 1 || LimiteAluno > 100)) ||
                (LimiteProfessor != -1 && (LimiteProfessor < 1 || LimiteProfessor > 100)) ||
                (LimiteBibliotecario != -1 && (LimiteBibliotecario < 1 || LimiteBibliotecario > 100)) ||
                MaximoProrrogacoes < -1 || MaximoProrrogacoes > 1000 || PrazoDias < 1 || PrazoDias > 365)
                throw new InvalidOperationException("Regras de empréstimo inválidas. Confira os limites e o prazo nas Configurações.");
        }

        public int LimitePara(string perfil)
        {
            Validar();
            if (perfil == RegrasCirculacao.PerfilAluno) return LimiteAluno;
            if (perfil == RegrasCirculacao.PerfilProfessor) return LimiteProfessor;
            if (perfil == RegrasCirculacao.PerfilBibliotecario) return LimiteBibliotecario;
            throw new InvalidOperationException("Este tipo de pessoa não possui uma regra de empréstimo.");
        }

        public DateTime CalcularPrazo(DateTime inicio)
        {
            Validar();
            if (inicio.Date > DateTime.MaxValue.AddDays(-PrazoDias))
                throw new InvalidOperationException("A data de devolução ultrapassa o limite permitido.");
            return inicio.Date.AddDays(PrazoDias);
        }

        public static PoliticaEmprestimos Obter(InstituicaoConfiguracao configuracao)
        {
            return configuracao?.RegrasEmprestimo?.Copiar() ?? new PoliticaEmprestimos();
        }

        // Somente a interface lê configurações; serviços recebem uma cópia explícita.
        public static PoliticaEmprestimos Carregar() => Obter(InstituicaoConfiguracaoStore.Carregar());
    }
}
