using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class IronskinBrew : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 6;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.StaggerAmount)
                return 0.35m;
            return 0;
        }
    }
}
