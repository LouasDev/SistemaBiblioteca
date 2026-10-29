using System;

namespace SUB.Utils
{
    // Políticas compartilhadas pela interface e pelos serviços, sem acesso ao banco ou aos Forms.
    internal static class RegrasCirculacao
    {
        internal const int PrazoPadraoDias = 7;
        internal const int MaximoDiasProrrogacao = 14;
        internal const int LimitePendenciasRapidas = 2;
        internal const string PerfilAluno = "Aluno(a)";
        internal const string PerfilProfessor = "Professor(a)";
        internal const string PerfilBibliotecario = "Bibliotecário(a)";
        internal const string GeneroRapido = "Didático";
        internal const string Ativo = "Ativo";
        internal const string Atrasado = "Atrasado";
        internal const string Devolvido = "Devolvido";
        internal const string Faltando = "Faltando";
        internal const string ErroPrazo = "A data de devolução não pode ser anterior à data de empréstimo.";
        internal const string ErroQuantidadeDevolvida = "Informe uma quantidade entre zero e o saldo pendente.";
        internal static DateTime PrazoPadrao(DateTime dataEmprestimo)
        {
            return dataEmprestimo.AddDays(PrazoPadraoDias);
        }

        internal static bool PrazoValido(DateTime dataEmprestimo, DateTime prazo)
        {
            return prazo >= dataEmprestimo;
        }

        internal static bool ProfessorTemPrazoPersonalizado(string perfil)
        {
            return perfil == PerfilProfessor;
        }

        internal static bool AlunoAtingiuLimite(string perfil, int ativos)
        {
            return perfil == PerfilAluno && ativos > 0;
        }

        internal static bool AtingiuLimitePendencias(int pendencias)
        {
            return pendencias >= LimitePendenciasRapidas;
        }

        internal static bool EmprestimoNormalAberto(string status)
        {
            return status == Ativo || status == Atrasado;
        }

        internal static bool EmprestimoRapidoAberto(string status, int saldo)
        {
            return saldo > 0 && SituacaoRapidaAberta(status);
        }

        internal static bool SituacaoRapidaAberta(string status)
        {
            return status == Ativo || status == Faltando;
        }

        internal static bool QuantidadeRetiradaValida(int quantidade)
        {
            return quantidade > 0;
        }

        internal static bool QuantidadeDevolvidaValida(int saldo, int devolvidos)
        {
            return saldo > 0 && devolvidos >= 0 && devolvidos <= saldo;
        }

        internal static int SaldoAposDevolucao(int saldo, int devolvidos)
        {
            if (!QuantidadeDevolvidaValida(saldo, devolvidos))
                throw new InvalidOperationException(ErroQuantidadeDevolvida);
            return saldo - devolvidos;
        }

        internal static string SituacaoSaldoRapido(int saldo)
        {
            return saldo == 0 ? Devolvido : Faltando;
        }

        internal static string SituacaoPrazo(DateTime prazo, DateTime agora)
        {
            return prazo.Date >= agora.Date ? Ativo : Atrasado;
        }

        internal static string ErroProrrogacao(DateTime prazoAtual, int dias)
        {
            if (dias < 1 || dias > MaximoDiasProrrogacao)
                return string.Format("Informe uma quantidade de dias entre 1 e {0}.", MaximoDiasProrrogacao);
            if (prazoAtual > DateTime.MaxValue.AddDays(-dias))
                return "A nova data ultrapassa o limite permitido.";
            return null;
        }

        internal static DateTime CalcularProrrogacao(DateTime prazoAtual, int dias)
        {
            string erro = ErroProrrogacao(prazoAtual, dias);
            if (erro != null)
                throw new InvalidOperationException(erro);
            return prazoAtual.AddDays(dias);
        }
    }
}
