using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DemonHunter
{
    public class SoulFragment:Buff
    {
        public SoulFragment()
        {
            TimeRemaining = Durration;
            Stacks = 1;
        }

        public override decimal Durration { get { return 20.0m; } }

        public override int MaxStacks
        {
            get { return 100; }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }
    }
}
