using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class FieryBrand : Ability
    {
        public FieryBrand()
        {
            Cooldown = 60m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                TargetBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.FieryBrand() }
            };
        }
    }
}
