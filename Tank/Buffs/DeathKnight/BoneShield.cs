using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight
{
    public class BoneShield:Buff
    {
        public BoneShield()
        {
            TimeRemaining = Durration;
        }

        public override decimal Durration { get { return 30.0m; } }

        public override int MaxStacks
        {
            get { return 10; }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Haste)
                return 0.10m;
            if (Stat == StatType.Stamina)
                return 0.016m * Stacks;
            return 0;
        }
    }
}
