using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior
{
    public class DemoralizingShout : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 8m;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.20m;
            return 0;
        }
    }
}
