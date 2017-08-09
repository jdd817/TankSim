using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    [EffectPriority(-1)]
    public class DampenHarm:Buff, IDamageTakenEffectStack
    {
        public override decimal Durration { get { return 10m; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            //no real data here.  i'm modelling it based on 50% reduction @ 20% of max health hit, increasing linearly.  If that's wrong, will fix

            var hitPercentage = ((decimal)damageEvent.DamageTaken + damageEvent.DamageAbsorbed) / tank.MaxHealth;
            var dmgReduction = hitPercentage >= 0.20m ? 0.50m : (hitPercentage * 1.5m + 0.2m);

            damageEvent.DamageTaken -= (int)((damageEvent.DamageTaken + damageEvent.DamageAbsorbed) * dmgReduction);

        }
    }
}
