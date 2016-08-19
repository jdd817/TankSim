using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class IgnorePain : Ability
    {
        public int DamageAbsorbed
        { get; set; }


        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = ResourceCost,
                CasterBuffsApplied = new[] { new Buffs.Warrior.IgnorePain(this.DamageAbsorbed) }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
