using Lab3.Model;
using Lab3.Utils;
using System;

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
            long m = (long)Math.Pow(2, 32);
            long modInv;

            do
            {
                for (int i = 0; i < states.Length; i++)
                {
                    result = CasinoRoyale.PlayLcg(account.Id, 1, 1);
                    account = result.Account;
                    states[i] = result.RealNumber;
                }
            } while (!TryModInverse(states[1] - states[0], m, out modInv));

            Console.WriteLine($"[{string.Join(", ", states)}]");

            int a = CrackUnknownMultiplier(states, m, modInv);
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

        private static int CrackUnknownMultiplier(long[] states, long modulus, long modInv)
        {
            return (int)((states[2] - states[1]) * modInv % modulus);
        }

        public static bool TryModInverse(long number, long modulo, out long result)
        {
            if (number < 1 || modulo < 2)
            {
                result = default;

                return false;
            }

            long n = number;
            long m = modulo, v = 0, d = 1;

            while (n > 0)
            {
                long t = m / n, x = n;
                n = m % x;
                m = x;
                x = d;
                d = checked(v - t * x);
                v = x;
            }
            result = v % modulo;

            if (result < 0)
            {
                result += modulo;
            }

            if (number * result % modulo == 1L)
            {
                return true;
            }

            result = default;

            return false;
        }
    }
}
