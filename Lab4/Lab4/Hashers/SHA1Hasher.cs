using System;
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
            byte[] bytes = Encoding.UTF8.GetBytes(salt + text);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            string hashedText = Utils.FromStringToHex(Convert.ToBase64String(hash));

            return new Hash(hashedText, Utils.FromStringToHex(salt), text);
        }

        public static bool Verify(string text, string hash, string salt)
        {
            return CreateHash(text, Utils.FromHexToString(salt)).HashText.Equals(hash);
        }
    }
}
