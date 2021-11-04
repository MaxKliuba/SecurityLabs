using System;

namespace Lab3.Utils
{
    class MersenneTwister
    {
        private const int w = 32;
        private const int n = 624;
        private const int m = 397;
        private const int r = 31;
        private const uint a = 0x9908B0DF;
        private const int u = 11;
        private const uint d = 0xFFFFFFFF;
        private const int s = 7;
        private const uint b = 0x9D2C5680;
        private const int t = 15;
        private const uint c = 0xEFC60000;
        private const int l = 18;
        private const int f = 1812433253;

        private uint[] mt = new uint[624];
        private int index = n + 1;
        private const long lowerMask = (1 << r) - 1L;
        private const long upperMask = ~lowerMask;

        public MersenneTwister(uint seed)
        {
            index = n;
            mt[0] = seed;

            for (int i = 1; i < n; i++)
            {
                mt[i] = (uint)(f * (mt[i - 1] ^ (mt[i - 1] >> (w - 2))) + i);
            }
        }

        private void Twist()
        {
            for (int i = 0; i < n; i++)
            {
                uint temp = (uint)((mt[i] & upperMask) + (mt[(i + 1) % n] & lowerMask));
                uint tempShift = temp >> 1;

                if (temp % 2 != 0)
                {
                    tempShift = tempShift ^ a;
                }
                mt[i] = mt[(i + m) % n] ^ tempShift;
            }
            index = 0;
        }

        private uint Temper(uint inValue)
        {
            uint y = inValue;
            y = y ^ ((y >> u) & d);
            y = y ^ ((y << s) & b);
            y = y ^ ((y << t) & c);
            y = y ^ (y >> l);

            return y;
        }

        public uint Next()
        {
            if (index >= n)
            {
                if (index > n)
                {
                    throw new ArithmeticException("Generator was never seeded");
                }

                Twist();
            }

            uint output = Temper(mt[index]);
            index++;

            return output;
        }
    }
}
