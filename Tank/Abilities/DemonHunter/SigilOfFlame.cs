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
            Cooldown = 30m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = (int)((Caster as Player).AttackPower * 1.86m),
                DamageType = DamageType.Fire,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.SigilOfFlame((int)(2.04m / 6m * (Caster as Player).AttackPower)) }
            };
        }
    }
}
