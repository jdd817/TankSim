using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Druid
{
    public class Maul : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 20,
                CasterBuffsApplied = new List<Buff> { new Buffs.Druid.GuardianOfElune() }
            };
        }
    }
}
