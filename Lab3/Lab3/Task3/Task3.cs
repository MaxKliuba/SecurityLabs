using Lab3.Model;
using Lab3.Utils;
using System;

namespace Lab3
{
    class Task3
    {
        public static void Run(Account account)
        {
            Console.WriteLine();
            Console.WriteLine("***** [TASK 3] *****");

            uint[] randomNums = new uint[624];

            for (int i = 0; i < randomNums.Length; i++)
            {
                Result result = CasinoRoyale.PlayBetterMt(account.Id, 1, 0);
                account = result.Account;
                randomNums[i] = (uint)result.RealNumber;
                Console.WriteLine($"[{i}] -> {result}");
            }
            //Console.WriteLine($"[{string.Join(", ", randomNums)}]");
            Console.WriteLine();

            MersenneTwister mersenneTwister = new MersenneTwister();
            mersenneTwister.States = RecoverStateMt(randomNums);

            //while (account.Money < 1_000_000)
            {
                Result result = CasinoRoyale.PlayBetterMt(account.Id, account.Money / 2, mersenneTwister.Next());
                account = result.Account;
                Console.WriteLine(result);
            }

            Console.WriteLine("********************");
            Console.WriteLine();
        }

        private static uint[] RecoverStateMt(uint[] numbers)
        {
            uint[] states = new uint[624];

            for (int i = 0; i < states.Length; i++)
            {
                states[i] = Untemper(numbers[i]);
            }

            return states;
        }

        private static uint Untemper(uint inValue)
        {
            const int u = 11;
            const int s = 7;
            const uint b = 0x9D2C5680;
            const int t = 15;
            const uint c = 0xEFC60000;
            const int l = 18;
            const int mask = 0x7F;

            uint y = inValue ^ (inValue >> l);
            y = y ^ ((y << t) & c);

            for (int i = 0; i < 4; i++)
            {
                uint _b = b & (uint)(mask << (s * (i + 1)));
                y = y ^ ((y << s) & _b);
            }

            for (int i = 0; i < 3; i++)
            {
                y = y ^ (y >> u);
            }

            return y;
        }
    }
}
