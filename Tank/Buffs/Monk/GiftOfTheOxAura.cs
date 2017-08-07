using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    public class GiftOfTheOxAura : PermanentBuff, IDamageTakenEffectStack
    {
        private IRng _rng;

        public GiftOfTheOxAura(IRng rng)
        {
            _rng = rng;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.DamageTaken>0)
            {
                var healingOrbChance = (0.75m * damageEvent.DamageTaken / tank.MaxHealth) * (3 - 2m * tank.HealthPercentage);

                if (_rng.NextDouble() <= (double)healingOrbChance)
                    tank.Buffs.AddBuff(new Buffs.Monk.HealingOrb());
            }
        }
    }
}
