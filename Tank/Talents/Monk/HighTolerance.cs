using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.Monk
{
    [Talent(typeof(Classes.Monk), 7, 3)]
    public class HighTolerance : PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.StaggerAmount)
                return 0.10m;
            if (Stat == StatType.Haste)
            {
                var stagger = Target.Buffs.GetBuff<Buffs.Monk.Stagger>();
                if (stagger == null)
                    return 0;
                var tickPercentage = ((decimal)stagger.TickDamage) / Target.MaxHealth;
                if (tickPercentage >= 0.06m)
                    return 0.15m;
                if (tickPercentage >= 0.03m)
                    return 0.12m;
                return 0.08m;
            }

            return 0;
        }
    }
}
