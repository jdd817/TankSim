﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class VampiricBlood : Ability
    {
        public VampiricBlood()
        {
            Cooldown = 60m;
        }

        public override bool OnGCD
        {
            get { return false; }
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buff>() { new Buffs.DeathKnight.VampiricBlood() }
            };
        }
    }
}
