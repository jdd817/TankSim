using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class ShieldSlam:Ability
    {
        public ShieldSlam()
        {
            ResourceGain = 20;
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
