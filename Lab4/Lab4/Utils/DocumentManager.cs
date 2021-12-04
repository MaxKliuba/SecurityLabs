using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab4
{
    class DocumentManager
    {
        public static List<Hash> ReadHashList(string path, bool log = true)
        {
            List<Hash> hashes = new List<Hash>();

            using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
            {
                if (log) Console.Write($"Reading [{path}]...");
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] values = line.Split(';');
                    hashes.Add(new Hash(values[0], values[1], null));
                }
                if (log) Console.WriteLine("DONE");
            }

            return hashes;
        }

        public static void WriteHashList(string path, List<Hash> hashes)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.Default))
            {
                Console.Write($"Writing to [{path}]");
                for (int i = 0; i < hashes.Count; i++)
                {
                    streamWriter.WriteLine(hashes[i].PrivateWrite());

                    if ((i % (hashes.Count / 10)) == 0)
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine("DONE");
            }
        }

        public static List<string> ReadList(string path, bool log = true)
        {
            List<string> list = new List<string>();

            using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
            {
                if (log) Console.Write($"Reading [{path}]...");
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
                if (log) Console.WriteLine("DONE");
            }

            return list;
        }

        public static void WriteList(string path, List<string> list)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.Default))
            {
                Console.Write($"Writing to [{path}]");
                for (int i = 0; i < list.Count; i++)
                {
                    streamWriter.WriteLine(list[i]);

                    if ((i % (list.Count / 10)) == 0)
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine("DONE");
            }
        }
    }
}
