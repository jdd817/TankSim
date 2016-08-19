using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.Warrior
{
    public class ShieldBlock : Buff
    {
        public ShieldBlock()
        {
            TimeRemaining = 6.0m;
        }

        public override decimal Durration { get { return 6.0m; } }

        public override int MaxStacks
        {
            get { return 1; }
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
