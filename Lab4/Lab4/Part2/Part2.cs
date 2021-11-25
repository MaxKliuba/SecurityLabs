using System;
using System.Collections.Generic;

namespace Lab4
{
    class Part2
    {
        private static readonly List<string> PASSWORDS = DocumentReader.ReadList("..\\..\\..\\res\\top_1M_passwords.txt", false);

        public static void Run()
        {
            List<Hash> hashesMd5 = DocumentReader.ReadHashList("..\\..\\..\\out\\hashes_md5.csv", false);
            int count = 0;

            Console.Write($"Decryption");
            for (int i = 0; i < hashesMd5.Count; i++)
            {
                if (DecryptMd5(hashesMd5[i]))
                {
                    count++;
                }

                if ((i % (hashesMd5.Count / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");

            Console.WriteLine($"Deciphered -> {(double)count / hashesMd5.Count * 100}%");
        }

        public static bool DecryptMd5(Hash hash)
        {
            foreach (string password in PASSWORDS)
            {
                if (MD5Hasher.Verify(password, hash.HashText, hash.Salt))
                {
                    hash.Text = password;

                    return true;
                }
            }

            return false;
        }
    }
}
