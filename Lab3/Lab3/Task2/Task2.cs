using Lab3.Model;
using Lab3.Utils;
using System;

namespace Lab3
{
    class Task2
    {
        public static void Run(Account account)
        {
            Console.WriteLine();
            Console.WriteLine("***** [TASK 2] *****");

            uint seed = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            MersenneTwister mersenneTwister = new MersenneTwister(seed - 1);
            Result result = CasinoRoyale.PlayMt(account.Id, 1, mersenneTwister.Next());
            account = result.Account;

            uint i = 0;
            while (i <= 10)
            {
                seed += i;
                mersenneTwister = new MersenneTwister(seed);

                if (mersenneTwister.Next() == result.RealNumber)
                {
                    Console.WriteLine($"seed = {seed}");
                    break;
                }
                i++;
            }

            //while (account.Money < 1_000_000)
            {
                result = CasinoRoyale.PlayMt(account.Id, account.Money / 2, mersenneTwister.Next());
                account = result.Account;
                Console.WriteLine(result);
            }

            Console.WriteLine("********************");
            Console.WriteLine();
        }
    }
}
