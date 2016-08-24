using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.Warrior
{
    public class WeakenedBlows : Buff
    {
        public WeakenedBlows()
        {
            TimeRemaining = 30m;
        }

        public override decimal Durration { get { return 30.0m; } }

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
            if (Stat == StatType.Damage)
                return -0.10m;
            else
                return 0;
        }
    }
}
