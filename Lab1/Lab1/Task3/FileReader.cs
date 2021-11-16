using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab1
{
    class FileReader
    {
        public static List<string> Read(string path)
        {
            List<string> outputs = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    outputs.Add(line);
                }
            }

            return outputs;
        }
    }
}
