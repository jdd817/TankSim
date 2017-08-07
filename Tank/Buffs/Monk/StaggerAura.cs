using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    public class StaggerAura : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0)
            {
                var staggerAmount = 0.45m + tank.Buffs.GetPercentageAdjustment(StatType.StaggerAmount);

                var damageStaggered = (int)(damageEvent.DamageTaken * staggerAmount);
                
                damageEvent.DamageTaken = damageEvent.DamageTaken - damageStaggered;

                tank.Buffs.AddBuff(new Buffs.Monk.Stagger(damageStaggered));
            }
        }
    }
}
