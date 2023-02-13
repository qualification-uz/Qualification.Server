using System.Security.Cryptography;
using System.Text;

namespace Qualification.Service.Helpers
{
    public class PasswordHelper
    {
        private const int KeySize = 32;
        private const int IterationsCount = 1000;
        private const int SaltSize = 64;

        public static string Encrypt(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
                password: password,
                saltSize: SaltSize,
                iterations: IterationsCount,
                hashAlgorithm: HashAlgorithmName.SHA256))
            {
                return Convert.ToBase64String(algorithm.GetBytes(KeySize));
            }
        }

        public static bool Verify(string hash, string password)
        {
            return Encrypt(password).SequenceEqual(hash);
        }
    }
}
