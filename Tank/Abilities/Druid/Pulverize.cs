using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Pulverize : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var thrash = Caster.Buffs.GetBuff<Buffs.Druid.Thrash>();
            thrash.Stacks -= 2;

            return new AbilityResult
            {
                DamageDealt = (int)(8.5m * (Caster as Player).WeaponDamage),
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Druid.Pulverize() }
            };
        }
    }
}
