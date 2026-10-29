using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SUB.Utils
{
    /// <summary>
    /// Regra institucional de progressão para turmas no formato "Nº Ano M".
    /// A mesma instância alimenta sugestão, opções e validação do mapeamento.
    /// </summary>
    public sealed class RegrasMapeamentoTurmas
    {
        public const string Egresso = "EGRESSO";
        public const string Transferido = "TRANSFERIDO";
        public const string Desistente = "DESISTENTE";
        private static readonly Regex PadraoTurma = new Regex(@"^\s*(?<serie>[1-9])\s*(?:[°º]\s*)?ano(?:\s+(?<identificador>\d+))?\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private readonly List<TurmaPadronizada> turmas;
        public RegrasMapeamentoTurmas(IEnumerable<string> turmasConfiguradas)
        {
            turmas = (turmasConfiguradas ?? Enumerable.Empty<string>()).Select(CriarTurma).Where(t => t != null).OrderBy(t => t.Nome, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public string Sugerir(string turmaAtual)
        {
            return OpcoesDeProgressao(turmaAtual).FirstOrDefault();
        }

        public List<string> OpcoesDeProgressao(string turmaAtual)
        {
            if (EhStatusEspecial(turmaAtual))
                return new List<string>
                {
                    turmaAtual.Trim().ToUpperInvariant()
                };
            TurmaPadronizada atual = CriarTurma(turmaAtual);
            if (atual == null)
                return new List<string>();
            if (atual.Serie == 3)
                return new List<string>
                {
                    Egresso
                };
            int proximaSerie = atual.Serie == 9 ? 1 : atual.Serie + 1;
            return OrdenarPreferindoIdentificador(turmas.Where(t => t.Serie == proximaSerie), atual.Identificador).Select(t => t.Nome).ToList();
        }

        public List<string> OpcoesDeAjusteIndividual(string turmaAtual)
        {
            if (EhStatusEspecial(turmaAtual))
                return new List<string>
                {
                    turmaAtual.Trim().ToUpperInvariant()
                };
            TurmaPadronizada atual = CriarTurma(turmaAtual);
            if (atual == null)
                return new List<string>();
            var opcoes = OpcoesDeProgressao(turmaAtual);
            opcoes.AddRange(OrdenarPreferindoIdentificador(turmas.Where(t => t.Serie == atual.Serie), atual.Identificador).Select(t => t.Nome));
            opcoes.Add(Desistente);
            opcoes.Add(Transferido);
            return opcoes.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        }

        public bool EhEscolhaDeProgressaoValida(string turmaAtual, string destino)
        {
            return !string.IsNullOrWhiteSpace(destino) && OpcoesDeProgressao(turmaAtual).Contains(destino.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        public bool EhEscolhaIndividualValida(string turmaAtual, string destino)
        {
            return !string.IsNullOrWhiteSpace(destino) && OpcoesDeAjusteIndividual(turmaAtual).Contains(destino.Trim(), StringComparer.OrdinalIgnoreCase);
        }

        public int ExtrairSerie(string turma)
        {
            TurmaPadronizada padronizada = CriarTurma(turma);
            return padronizada == null ? 0 : padronizada.Serie;
        }

        private static IEnumerable<TurmaPadronizada> OrdenarPreferindoIdentificador(IEnumerable<TurmaPadronizada> candidatas, string identificador)
        {
            return candidatas.OrderBy(t => string.Equals(t.Identificador, identificador, StringComparison.OrdinalIgnoreCase) ? 0 : 1).ThenBy(t => t.Nome, StringComparer.OrdinalIgnoreCase);
        }

        private static bool EhStatusEspecial(string turma)
        {
            string valor = (turma ?? string.Empty).Trim();
            return string.Equals(valor, Egresso, StringComparison.OrdinalIgnoreCase) || string.Equals(valor, Transferido, StringComparison.OrdinalIgnoreCase) || string.Equals(valor, Desistente, StringComparison.OrdinalIgnoreCase);
        }

        private static TurmaPadronizada CriarTurma(string turma)
        {
            Match match = PadraoTurma.Match(turma ?? string.Empty);
            if (!match.Success)
                return null;
            return new TurmaPadronizada
            {
                Nome = turma.Trim(),
                Serie = int.Parse(match.Groups["serie"].Value),
                Identificador = match.Groups["identificador"].Success ? match.Groups["identificador"].Value : string.Empty
            };
        }

        private sealed class TurmaPadronizada
        {
            public string Nome { get; set; }
            public int Serie { get; set; }
            public string Identificador { get; set; }
        }
    }
}
