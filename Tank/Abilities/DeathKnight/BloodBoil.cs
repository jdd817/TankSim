using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class BloodBoil : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                TargetBuffsApplied = new[] { new Buffs.DeathKnight.BloodPlague() }
            };
        }
    }
}
