using System.Security.Cryptography;
using System.Text;

namespace Qualification.Service.Helpers
{
    public class PasswordHelper
    {
        public static string GenerateHash(string password)
        {
            // SHA256 is disposable by inheritance.
            var sha256 = SHA256.Create();
            // Send a sample text to hash.  
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            // Get the hashed string.  
            var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            // Print the string.   
            return hash;
        }
    }
}
