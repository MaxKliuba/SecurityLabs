using System;
using System.Security.Cryptography;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Lab4
{
    class MD5Hasher
    {
        public static string CreateSalt()
        {
            return Utils.FromStringToHex(BCryptNet.GenerateSalt());
        }

        public static Hash CreateHash(string text)
        {
            return CreateHash(text, CreateSalt());
        }

        public static Hash CreateHash(string text, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(salt + text);
            byte[] hash = MD5.Create().ComputeHash(bytes);
            string hashedText = Utils.FromStringToHex(Convert.ToBase64String(hash));

            return new Hash(hashedText, salt);
        }

        public static bool Verify(string text, string hash, string salt)
        {
            return CreateHash(text, salt).HashText.Equals(hash);
        }
    }
}
