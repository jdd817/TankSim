using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.DemonHunter
{
    public class SigilOfFlame:Ability
    {
        public SigilOfFlame()
        {
        }

        public override AbilityType AbilityType { get { return AbilityType.Spell; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = 0,
            };
        }
    }
}
