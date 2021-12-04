using BCryptNet = BCrypt.Net.BCrypt;

namespace Lab4
{
    class BCryptHasher
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
            string hashedText = BCryptNet.HashPassword(text, salt);

            return new Hash(hashedText, salt, text);
        }

        public static bool Verify(string text, string hash)
        {
            return BCryptNet.Verify(text, hash);
        }
    }
}