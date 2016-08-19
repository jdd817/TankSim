using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    class Revenge:Ability
    {
        public Revenge()
        {
            ResourceGain = 15;
        }

        public override AbilityType AbilityType { get { return AbilityType.Melee; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
            };
        }
    }
}
