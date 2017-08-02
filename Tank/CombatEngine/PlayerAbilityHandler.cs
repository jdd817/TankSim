using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.CombatEngine
{
    public class PlayerAbilityHandler:IAbilityHandler
    {
        private ICombatTable _combatTable;

        public PlayerAbilityHandler(ICombatTable combatTable)
        {
            _combatTable = combatTable;
        }

        public void ProcessAction(Player Tank, Mob Mob, decimal Time, Ability PlayerAction)
        {
            var modifiers = PlayerAction.GetModifiers(Tank.Buffs, Mob.Buffs);

            AttackResult Result = _combatTable.GetAttackResult(Tank, Mob,
                modifiers);

            var actionResult = PlayerAction.GetAbilityResult(Result, Tank, Mob);
            actionResult.AttackResult = Result;
            actionResult.Time = Time;
            
            foreach (var effectStack in Tank.Buffs.GetEffectStack<IPlayerAbilityEffectStack>().ToList())
                effectStack.ProcessAbilityUsed(Time, PlayerAction, actionResult, Tank, Mob);

            Tank.Cooldowns.AbilityUsed(PlayerAction, actionResult);

            DataLogging.DataLogManager.UsedAbility(Time, PlayerAction.GetType().Name, actionResult);

            Tank.UpdateAbilityResults(Time, PlayerAction, actionResult);
            if(actionResult.SelfHealing>0)
            {
                var healingEvent = new DataLogging.HealingEvent
                {
                    Name = PlayerAction.GetType().Name,
                    Amount = (int)(actionResult.SelfHealing * (1m + Tank.VersatilityDamageIncrease)),
                    Time = Time
                };
                foreach (var effect in Tank.Buffs.GetEffectStack<IHealingReceivedEffectStack>())
                    effect.HealingReceived(healingEvent, Tank, PlayerAction);
                Tank.ApplyHealing(healingEvent.Amount);
                DataLogging.DataLogManager.LogHeal(healingEvent);
            }

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

            foreach (Buff B in actionResult.CasterBuffsApplied)
                Tank.Buffs.AddBuff(B);
            foreach (Buff B in actionResult.TargetBuffsApplied)
                Mob.Buffs.AddBuff(B);
        }
    }
}
