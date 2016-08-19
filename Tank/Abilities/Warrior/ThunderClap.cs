using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class ThunderClap : Ability
    {
        public override AbilityType AbilityType { get { return AbilityType.Spell; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
            };
        }
    }
}
