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
            Cooldown = 9m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = -15
            };
        }
    }
}
