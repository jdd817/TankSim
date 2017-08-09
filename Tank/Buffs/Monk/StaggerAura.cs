using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    [EffectPriority(5)]
    public class StaggerAura : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.Name == "Stagger")
                return;
            if (damageEvent.DamageTaken > 0)
            {
                var staggerAmount = 0.40m + tank.Buffs.GetPercentageAdjustment(StatType.StaggerAmount);

                var damageStaggered = (int)(damageEvent.DamageTaken * staggerAmount);
                
                damageEvent.DamageTaken = damageEvent.DamageTaken - damageStaggered;

                tank.Buffs.AddBuff(new Buffs.Monk.Stagger(damageStaggered));
            }
        }
    }
}
