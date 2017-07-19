using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.Warrior
{
    public class IgnorePain : Ability
    {
        public int DamageAbsorbed
        { get; set; }


        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var war = Caster as Player;
            return new AbilityResult
            {
                ResourceCost = 60,
                CasterBuffsApplied = new List<Buff>() { new Buffs.Warrior.IgnorePain((int)(28.0m * war.AttackPower)) }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
