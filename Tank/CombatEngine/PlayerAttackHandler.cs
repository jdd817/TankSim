using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

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

            Tank.UpdateAbilityResults(Time, Attack, actionResult);
        }
    }
}
