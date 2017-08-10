using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class BearForm:PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Armor)
                return 2m;
            if (Stat == StatType.MaxHealth)
                return 0.55m;
            return 0;
        }
    }
}
