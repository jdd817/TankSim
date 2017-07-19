using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.Warrior
{
    public class ShieldBlock : Ability
    {
        public ShieldBlock()
        {
            MaxCharges = 2;
            Cooldown = 13m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 15,
                CasterBuffsApplied = new List<Buff>() { new Buffs.Warrior.ShieldBlock() }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
