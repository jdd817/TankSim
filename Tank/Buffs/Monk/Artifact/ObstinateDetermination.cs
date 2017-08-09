using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk.Artifact
{
    public class ObstinateDetermination : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0
               && tank.CurrentHealth + damageEvent.DamageTaken > tank.MaxHealth * 0.35m
               && tank.CurrentHealth < tank.MaxHealth * 0.35m)
            {
                tank.Buffs.AddBuff(new HealingOrb());
            }
        }
    }
}
