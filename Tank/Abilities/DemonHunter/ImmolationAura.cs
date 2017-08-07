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
            Cooldown = 15m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = (int)((Caster as Player).AttackPower * 2.43m),
                DamageType = DamageType.Fire,
                CasterBuffsApplied = new List<Buffs.Buff>() { new Buffs.DemonHunter.ImmolationAura((Caster as Player).AttackPower) }
            };
        }
    }
}
