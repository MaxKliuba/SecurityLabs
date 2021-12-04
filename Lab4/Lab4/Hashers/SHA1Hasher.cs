using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Lab4
{
    class SHA1Hasher
    {
        public static string CreateSalt()
        {
            return BCryptNet.GenerateSalt();
        }

        public static Hash CreateHash(string text)
        {
            return CreateHash(text, CreateSalt());
        }

        public static Hash CreateHash(string text, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text + salt);
            byte[] hash = new SHA1Managed().ComputeHash(bytes);
            string hashedText = Utils.ByteArrayToHexString(hash);

            return new Hash(hashedText, salt, text);
        }

        public static bool Verify(string text, string hash, string salt)
        {
            return CreateHash(text, salt).HashText.Equals(hash);
        }
    }
}
