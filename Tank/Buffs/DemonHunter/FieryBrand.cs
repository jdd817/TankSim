using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter
{
    public class FieryBrand : Buff
    {
        public override decimal Durration { get { return 8m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.40m;
            return 0;
        }
    }
}
