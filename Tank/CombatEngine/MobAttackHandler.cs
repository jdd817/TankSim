using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.CombatEngine
{
    public class MobAttackHandler:IAttackHandler
    {
        private ICombatTable _combatTable;

        public MobAttackHandler(ICombatTable combatTable)
        {
            _combatTable = combatTable;
        }

        public void ProcessAttack(Player Tank, Mob Mob, decimal Time, Attack Attack)
        {
            var modifiers = Attack.GetModifiers(Mob.Buffs, Tank.Buffs);
            AttackResult Result = _combatTable.GetAttackResult(Mob, Tank,
                modifiers);

            var damageEvent = new DataLogging.DamageEvent()
            {
                Time = Time,
                Result = Result,
                DamageTaken = Attack.Damage,
                RawDamage = Attack.Damage
            };

            if (Result == AttackResult.Dodge || Result == AttackResult.Parry)
            {
                damageEvent.DamageTaken = 0;
                damageEvent.RawDamage = 0;
            }
            else
            {
                if (Tank.Armor > 0)
                    damageEvent.DamageTaken = (int)(damageEvent.DamageTaken * (1m - Tank.ArmorDamageReduction));

                damageEvent.DamageTaken = (int)(damageEvent.DamageTaken * (1m - Tank.VersatilityDamageReduction) * (1m - Tank.Buffs.GetPercentageAdjustment(StatType.DamageReduction)));
            }

            var damageEffectStack = Tank.Buffs.GetEffectStack<IDamageTakenEffectStack>();

            foreach (var effectStack in damageEffectStack)
                effectStack.DamageTaken(Time, damageEvent, Tank);

            damageEvent = Tank.UpdateFromMobAttack(damageEvent);
            Tank.CurrentHealth -= damageEvent.DamageTaken;
            DataLogging.DataLogManager.LogEvent(damageEvent);
        }
    }
}
