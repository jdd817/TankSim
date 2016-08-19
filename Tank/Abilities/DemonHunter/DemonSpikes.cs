﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.DemonHunter
{
    public class DemonSpikes:Ability
    {
        public DemonSpikes()
        {
            ResourceCost = 20;
        }

        public override AbilityType AbilityType { get { return AbilityType.Melee; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;
            return new AbilityResult
            {
                ResourceCost = ResourceCost,
                DamageDealt = 0,
                CasterBuffsApplied = new[] { new Buffs.DemonHunter.DemonSpikes(dh.Mastery / 1.125m + .12m) }
            };
        }
    }
}
