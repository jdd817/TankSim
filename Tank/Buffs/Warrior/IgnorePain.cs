using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.Warrior
{
    public class IgnorePain : Buff
    {
        public IgnorePain(int DamageAbsorbed)
        {
            TimeRemaining = 6.0m;
            DamageRemaining = DamageAbsorbed;
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

        public int DamageRemaining
        { get; set; }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageRemaining,
                    TimeRemaining);
        }
    }
}
