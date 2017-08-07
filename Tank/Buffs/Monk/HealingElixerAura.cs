using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    public class HealingElixerAura : IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0
                && tank.CurrentHealth + damageEvent.DamageTaken > tank.MaxHealth * 0.35m
                && tank.CurrentHealth < tank.MaxHealth * 0.35m
                && tank.Cooldowns.ChargesAvailable<Abilities.Monk.HealingElixer>() > 0)
            {
                var elixer = new Abilities.Monk.HealingElixer();
                var result = elixer.GetAbilityResult(AttackResult.Hit, tank, null);
                result.SelfHealing = tank.ApplyHealing(result.SelfHealing);
                tank.Cooldowns.AbilityUsed(elixer, result);
                DataLogging.DataLogManager.UsedAbility(damageEvent.Time, elixer.GetType().Name, result);
            }
        }
    }
}
