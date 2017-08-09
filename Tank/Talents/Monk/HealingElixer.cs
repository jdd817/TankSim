using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.Monk
{
    [Talent(typeof(Classes.Monk), 5, 1)]
    public class HealingElixer : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DataLogging.DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0
                && tank.CurrentHealth + damageEvent.DamageTaken > tank.MaxHealth * 0.35m
                && tank.CurrentHealth < tank.MaxHealth * 0.35m
                && tank.Cooldowns.ChargesAvailable<Abilities.Monk.HealingElixer>() > 0)
            {
                var elixer = new Abilities.Monk.HealingElixer();
                var result = elixer.GetAbilityResult(AttackResult.Hit, tank, null);
                var healingEvent = new DataLogging.HealingEvent
                {
                    Name = "HealingElixer",
                    Amount = result.SelfHealing,
                    Time = DataLogging.DataLogManager.CurrentTime
                };

                foreach (var effect in tank.Buffs.GetEffectStack<IHealingReceivedEffectStack>())
                    effect.HealingReceived(healingEvent, tank, elixer);

                result.SelfHealing = tank.ApplyHealing(healingEvent.Amount);
                tank.Cooldowns.AbilityUsed(elixer, result);

                DataLogging.DataLogManager.UsedAbility(damageEvent.Time, elixer.GetType().Name, result);
                DataLogging.DataLogManager.LogHeal(healingEvent);
            }
        }
    }
}
