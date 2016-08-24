using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public interface IRng
    {
        void Reseed(int seed = 0);
        double NextDouble();
        int Next(int minValue, int maxValue);
    }

    public class RNG:IRng
    {
        private Random _rand;

        public RNG()
        {
            _rand = new Random();
        }

        public void Reseed(int seed=0)
        {
            if (seed == 0)
                _rand = new Random();
            else
                _rand = new Random(seed);
        }

        public double NextDouble()
        {
            return _rand.NextDouble();
        }

        public int Next(int minValue, int maxValue)
        {
            return _rand.Next(minValue, maxValue);
        }
    }
}
