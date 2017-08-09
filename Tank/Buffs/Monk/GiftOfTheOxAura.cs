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
        public GiftOfTheOxAura()
        {
            DamageCounter = 0;
        }

        public decimal DamageCounter { get; set; }

        /*
         * Blue:
         * "It's no longer a random chance, under the hood. When you are hit, it increments a counter by 
         * (DamageTakenBeforeAbsorbsOrStagger / MaxHealth). It now drops an orb whenever that reaches 1.0, 
         * and decrements it by 1.0. The tooltip still says ‘chance’, to keep it understandable. 
         * Gift of the Mists multiplies that counter increment by 
         * (2 - (HealthBeforeDamage - DamageTakenBeforeAbsorbsOrStagger) / MaxHealth); 
         * ie, a simple linear 0-100% increase based on missing health, counting the hit as a full hit."
         * */

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0)
            {
                decimal damageTaken = damageEvent.DamageTaken + damageEvent.DamageAbsorbed;
                //var healingOrbChance = (0.75m * damageEvent.DamageTaken / tank.MaxHealth) * (3 - 2m * tank.HealthPercentage);
                var multiplier = 1.0m;
                if (tank.Buffs.GetBuff<Talents.Monk.GiftOfTheMists>() != null)
                    multiplier = 2 - (tank.CurrentHealth - damageTaken) / tank.MaxHealth;
                DamageCounter += (damageTaken / tank.MaxHealth) * multiplier;

                if (DamageCounter >= 1.0m)
                {
                    DamageCounter -= 1.0m;
                    tank.Buffs.AddBuff(new HealingOrb());
                }
            }
        }
    }
}
