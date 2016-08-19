﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class ShieldBlock : Ability
    {
        public override AbilityType AbilityType { get { return AbilityType.Self; } }


        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 10,
                CasterBuffsApplied = new[] { new Buffs.Warrior.ShieldBlock() }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
