using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Part2
    {
        public static void Run()
        {
            List<Hash> hashes = DocumentManager.ReadHashList("..\\..\\..\\..\\input\\hashes.csv");
            DocumentManager.WriteList("..\\..\\..\\..\\input\\_hashes.txt", hashes.Select(h => h.PrivateWrite(':')).ToList());
        }
    }
}
