namespace SUB.Models
{
    public static class Sessao
    {
        public static int? UsuarioId { get; set; }
        public static string NomeUsuario { get; set; }
        public static string Perfil { get; set; }
        public static string NomeBibliotecariaLogada { get; set; }
        public static bool UsuarioAutenticado => UsuarioId.HasValue;

        public static void Limpar()
        {
            UsuarioId = null;
            NomeUsuario = null;
            Perfil = null;
            NomeBibliotecariaLogada = null;
        }
    }
}
