using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.CombatEngine
{
    public class PlayerAttackHandler:IAttackHandler
    {
        private ICombatTable _combatTable;

        public PlayerAttackHandler(ICombatTable combatTable)
        {
            _combatTable = combatTable;
        }

        public void ProcessAttack(Player Tank, Mob Mob, decimal Time, Attack Attack)
        {
            var modifiers = Attack.GetModifiers(Tank.Buffs, Mob.Buffs);
            AttackResult Result = _combatTable.GetAttackResult(Tank, Mob,
                modifiers);

            var actionResult = Attack.GetAbilityResult(Result, Tank, Mob);
            actionResult.AttackResult = Result;
            actionResult.Time = Time;

            foreach (var effectStack in Tank.Buffs.GetEffectStack<IPlayerAbilityEffectStack>())
                effectStack.ProcessAbilityUsed(Time, Attack, actionResult, Tank, Mob);

            //leech - applying vers to it.  need to find a source confirming or denying that vers applies to leech
            var leechAmount = (int)(actionResult.DamageDealt * Tank.LeechPercentage * (1m + Tank.VersatilityDamageIncrease));
            if (leechAmount > 0)
            {
                var healingEvent = new DataLogging.HealingEvent
                {
                    Name = "Leech",
                    Amount = leechAmount,
                    Time = Time
                };
                foreach (var effect in Tank.Buffs.GetEffectStack<IHealingReceivedEffectStack>())
                    effect.HealingReceived(healingEvent, Tank, null);
                Tank.ApplyHealing(healingEvent.Amount);
                DataLogging.DataLogManager.LogHeal(healingEvent);
            }

            Tank.UpdateAbilityResults(Time, Attack, actionResult);
        }
    }
}
