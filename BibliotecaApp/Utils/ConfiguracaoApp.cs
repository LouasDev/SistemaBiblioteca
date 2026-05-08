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
        public DateTime UltimaAlteracao { get; set; } = DateTime.Now;
    }
}
