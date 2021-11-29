using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Part1
    {
        public static void Run()
        {
            List<string> top100Passwords = DocumentReader.ReadList("..\\..\\..\\res\\top_100_passwords.txt");
            List<string> top1MPasswords = DocumentReader.ReadList("..\\..\\..\\res\\top_1M_passwords.txt");
            Console.WriteLine($"Result (Top 100 & 1M) -> {ComparePasswordLists(top100Passwords, top1MPasswords)}%");

            int limit = 100_000;
            List<string> passwordsMd5 = new List<string>();
            List<string> passwordsSha1 = new List<string>();
            Console.Write("Passwords generating");
            for (int i = 0; i < limit; i++)
            {
                passwordsMd5.Add(PasswordGenerator.GenerateLikeHuman());
                passwordsSha1.Add(PasswordGenerator.GenerateLikeHuman());

                if ((i % (limit / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");

            //DocumentReader.WriteList("..\\..\\..\\out\\passwordsMd5.txt", passwords);
            //DocumentReader.WriteList("..\\..\\..\\out\\passwordsSha1.txt", passwords);

            Console.WriteLine($"Result (MD5 passwords & Top 100) -> {ComparePasswordLists(passwordsMd5, top100Passwords)}%");
            //Console.WriteLine($"Result (MD5 passwords & Top 1M) -> {ComparePasswordLists(passwordsMd5, top1MPasswords)}%");
            Console.WriteLine($"Result (SHA1 passwords & Top 100) -> {ComparePasswordLists(passwordsSha1, top100Passwords)}%");
            //Console.WriteLine($"Result (SHA1 passwords & Top 1M) -> {ComparePasswordLists(passwordsSha1, top1MPasswords)}%");


            Console.Write("MD5 hashing");
            List<Hash> hashesMd5 = new List<Hash>();
            for (int i = 0; i < limit; i++)
            {
                Hash hash = MD5Hasher.CreateHash(passwordsMd5[i]);
                hashesMd5.Add(hash);

                if ((i % (limit / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");
            DocumentReader.WriteHashList("..\\..\\..\\out\\hashes_md5.csv", hashesMd5);
            DocumentReader.WriteList("..\\..\\..\\out\\passwords_md5.txt", hashesMd5.Select(h => h.ToString()).ToList());

            Console.Write("SHA1 hashing");
            List<Hash> hashesSha1 = new List<Hash>();
            for (int i = 0; i < limit; i++)
            {
                Hash hash = SHA1Hasher.CreateHash(passwordsSha1[i]);
                hashesSha1.Add(hash);

                if ((i % (limit / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");
            DocumentReader.WriteHashList("..\\..\\..\\out\\hashes_sha1.csv", hashesSha1);
            DocumentReader.WriteList("..\\..\\..\\out\\passwords_sha1.txt", hashesSha1.Select(h => h.ToString()).ToList());
        }

        public static double ComparePasswordLists(List<string> list, List<string> baseList)
        {
            int count = 0;

            Console.Write("Comparison");
            for (int i = 0; i < list.Count; i++)
            {
                if (baseList.Contains(list[i]))
                {
                    count++;
                }

                if ((i % (list.Count / 10)) == 0)
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine("DONE");

            return (double)count / list.Count * 100.0;
        }
    }
}
