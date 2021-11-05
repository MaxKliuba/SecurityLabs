using Lab3.Model;
using Lab3.Utils;
using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = CasinoRoyale.CreateAcc();
            Console.WriteLine(account);
            Console.WriteLine();

            Task1.Run(account);
            Task2.Run(account);
            Task3.Run(account);
        }
    }
}
