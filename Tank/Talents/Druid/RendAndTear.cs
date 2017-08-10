using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 7, 1)]
    public class RendAndTear : PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Damage || Stat == StatType.DamageReduction)
                return Target.Buffs.GetStacks<Buffs.Druid.Thrash>() * 0.02m;
            return 0;
        }
    }
}
