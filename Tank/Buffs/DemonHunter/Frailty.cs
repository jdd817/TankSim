using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter
{
    public class Frailty : Buff
    {
        public override decimal Durration { get { return 20m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Leech)
                return 0.20m;
            return 0;
        }
    }
}
