using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid.Artifact
{
    public class IronClaws:PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Armor)
                return 0.05m;
            return 0;
        }
    }
}
