using System;
using System.Security.Cryptography;

namespace SUB.Utils
{
    public static class CriptografiaSenha
    {
        public static void CriarHash(string senha, out string hash, out string salt)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                salt = Convert.ToBase64String(saltBytes);
                using (var pbkdf2 = new Rfc2898DeriveBytes(senha, saltBytes, 10000))
                {
                    byte[] hashBytes = pbkdf2.GetBytes(32);
                    hash = Convert.ToBase64String(hashBytes);
                }
            }
        }

        public static bool VerificarSenha(string senhaDigitada, string hashSalvo, string saltSalvo)
        {
            if (senhaDigitada == null || string.IsNullOrWhiteSpace(hashSalvo) || string.IsNullOrWhiteSpace(saltSalvo))
                return false;

            byte[] saltBytes;
            byte[] hashSalvoBytes;
            try
            {
                saltBytes = Convert.FromBase64String(saltSalvo);
                hashSalvoBytes = Convert.FromBase64String(hashSalvo);
            }
            catch (FormatException)
            {
                return false;
            }

            if (saltBytes.Length != 16 || hashSalvoBytes.Length != 32)
                return false;

            using (var pbkdf2 = new Rfc2898DeriveBytes(senhaDigitada, saltBytes, 10000))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(32);
                int diferenca = 0;
                for (int i = 0; i < hashCalculado.Length; i++)
                    diferenca |= hashCalculado[i] ^ hashSalvoBytes[i];
                return diferenca == 0;
            }
        }
    }
}
