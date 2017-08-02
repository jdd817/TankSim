using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DemonHunter
{
    public class DemonSpikes : Buff
    {
        public DemonSpikes(Player tank)
        {
            TimeRemaining = 6.0m;
            DamageReduction = tank.Mastery / 1.125m + .12m;
        }

        public decimal DamageReduction
        { get; set; }

        public override decimal Durration { get { return 6.0m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Parry)
                return 0.20m;
            if (Stat == StatType.DamageReduction)
                return DamageReduction;
            return 0;
        }

        public override string ToString()
        {
            return String.Format("{0}<{1:0.00}%> ({2:0.00})",
                    Name,
                    DamageReduction * 100m,
                    TimeRemaining);
        }
    }
}
