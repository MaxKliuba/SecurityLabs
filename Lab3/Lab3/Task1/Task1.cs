using Lab3.Model;
using Lab3.Utils;
using System;
using System.Numerics;

namespace Lab3
{
    class Task1
    {
        public static void Run(Account account)
        {
            Console.WriteLine();
            Console.WriteLine("***** [TASK 1] *****");

            Result result = null;
            long[] states = new long[3];
            for (int i = 0; i < states.Length; i++)
            {
                result = CasinoRoyale.PlayLcg(account.Id, 1, 1);
                account = result.Account;
                states[i] = result.RealNumber;
            }
            Console.WriteLine($"[{string.Join(", ", states)}]");

            long m = (long)Math.Pow(2, 32);
            int a = CrackUnknownMultiplier(states, m);
            int c = CrackUnknownIncrement(states, m, a);
            Console.WriteLine("a = " + a);
            Console.WriteLine("c = " + c);

            //while (account.Money < 1_000_000)
            {
                long bet = account.Money / 2;
                int next = Next(result.RealNumber, a, c, m);
                result = CasinoRoyale.PlayLcg(account.Id, bet, next);
                account = result.Account;
                Console.WriteLine(result);
            }

            Console.WriteLine("********************");
            Console.WriteLine();
        }

        public static int Next(long last, int a, int c, long m)
        {
            return (int)((a * last + c) % m);
        }

        private static int CrackUnknownIncrement(long[] states, long modulus, int multiplier)
        {
            return (int)((states[1] - states[0] * multiplier) % modulus);
        }

        private static int CrackUnknownMultiplier(long[] states, long modulus)
        {
            return (int)((states[2] - states[1]) * ModInverse(states[1] - states[0], modulus) % modulus);
        }

        private static long ModInverse(long b, long n)
        {
            (long g, long x, long _) = Egcd(b, n);

            return x % n;

            if (g == 1)
            {
                return x % n;
            }

            throw new ArgumentException("Invalid arguments");
        }

        private static (long, long, long) Egcd(long a, long b)
        {
            if (a == 0)
            {
                return (b, 0, 1);
            }
            else
            {
                (long g, long x, long y) = Egcd(b % a, a);

                return (g, y - (b / a) * x, x);
            }
        }
    }
}
