using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.DemonHunter
{
    public class DemonSpikes:Ability
    {
        public DemonSpikes()
        {
            MaxCharges = 2;
            Cooldown = 15m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;
            return new AbilityResult
            {
                ResourceCost = 20,
                DamageDealt = 0,
                CasterBuffsApplied = new List<Buffs.Buff>() { new Buffs.DemonHunter.DemonSpikes(dh) }
            };
        }
    }
}
