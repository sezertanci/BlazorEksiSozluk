using System.Security.Cryptography;
using System.Text;

namespace BlazorEksiSozluk.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBaytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBaytes);
        }
    }
}
