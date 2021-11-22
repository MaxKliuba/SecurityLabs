using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab4
{
    class Part1
    {
        public static void Run()
        {
            int limit = 100_000;

            Console.Write("MD5 hashing");
            List<Hash> hashesMd5 = new List<Hash>();
            for (int i = 0; i < limit; i++)
            {
                Hash hash = MD5Hasher.CreateHash(PasswordGenerator.GenerateLikeHuman());
                hashesMd5.Add(hash);

                if ((i % (limit / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");
            WriteHashList("..\\..\\..\\Res\\hashes_md5.csv", hashesMd5);

            Console.Write("SHA1 hashing");
            List<Hash> hashesBCrypt = new List<Hash>();
            for (int i = 0; i < limit; i++)
            {
                Hash hash = SHA1Hasher.CreateHash(PasswordGenerator.GenerateLikeHuman());
                hashesBCrypt.Add(hash);

                if ((i % (limit / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");
            WriteHashList("..\\..\\..\\Res\\hashes_sha1.csv", hashesBCrypt);
        }

        private static void WriteHashList(string path, List<Hash> hashes)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.Default))
            {
                Console.Write($"Writing to [{path}]");
                for (int i = 0; i < hashes.Count; i++)
                {
                    streamWriter.WriteLine(hashes[i]);

                    if ((i % (hashes.Count / 10)) == 0)
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine("DONE");
            }
        }
    }
}
