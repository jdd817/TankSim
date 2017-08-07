using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class FortifyingBrew : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Monk.FortifyingBrew() }
            };
        }
    }
}
