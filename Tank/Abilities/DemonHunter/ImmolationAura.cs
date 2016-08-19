using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.DemonHunter
{
    public class ImmolationAura:Ability
    {
        public ImmolationAura()
        {
            ResourceCost = 0;
        }

        public override AbilityType AbilityType { get { return AbilityType.Spell; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = 0,
                CasterBuffsApplied = new[] { new Buffs.DemonHunter.ImmolationAura() }
            };
        }
    }
}
