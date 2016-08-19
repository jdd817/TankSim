using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class DeathAndDecay : Ability
    {
        public DeathAndDecay()
        {
            SecondaryResourceCost = 1;
            ResourceGain = 10;
        }

        public override AbilityType AbilityType { get { return AbilityType.Spell; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new[] { new Buffs.DeathKnight.DeathAndDecay() },
                SecondaryResourceCost = 1,
                ResourceCost = -10
            };
        }
    }
}
