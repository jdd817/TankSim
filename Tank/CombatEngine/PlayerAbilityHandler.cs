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

            Tank.Cooldowns.AbilityUsed(PlayerAction, actionResult);
            
            foreach (var effectStack in Tank.Buffs.GetEffectStack<IPlayerAbilityEffectStack>().ToList())
            {
                effectStack.ProcessAbilityUsed(Time, PlayerAction, actionResult, Tank, Mob);
            }

            DataLogging.DataLogManager.UsedAbility(Time, PlayerAction.GetType().Name, actionResult);

            Tank.UpdateAbilityResults(Time, PlayerAction, actionResult);
            foreach (Buff B in actionResult.CasterBuffsApplied)
                Tank.Buffs.AddBuff(B);
            foreach (Buff B in actionResult.TargetBuffsApplied)
                Mob.Buffs.AddBuff(B);
        }
    }
}
