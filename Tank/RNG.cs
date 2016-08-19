using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public static class RNG
    {
        private static Random Rand = new Random();

        public static void Reseed(int seed=0)
        {
            if (seed == 0)
                Rand = new Random();
            else
                Rand = new Random(seed);
        }

        public static double NextDouble()
        {
            return Rand.NextDouble();
        }

        public static int Next(int minValue, int maxValue)
        {
            return Rand.Next(minValue, maxValue);
        }
    }
}
