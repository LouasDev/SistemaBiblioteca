using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaApp.Utils
{
    public class ConfiguracaoApp
    {
        public string NomeEscola { get; set; } = "Nome da Escola";
        public string NomeBiblioteca { get; set; } = "Minha Biblioteca";
        public string CaminhoLogo { get; set; } = "";
        
        /// <summary>
        /// E-mail institucional da escola (usado como remetente dos e-mails).
        /// </summary>
        public string EmailInstitucional { get; set; } = "";
        
        /// <summary>
        /// Identificador único da escola (usado na API central de e-mails).
        /// Exemplo: "ES-0123"
        /// </summary>
        public string EscolaId { get; set; } = "";
        
        public DateTime UltimaAlteracao { get; set; } = DateTime.Now;
    }
}
