﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Druid
{
    public class BristlingFur : Ability
    {
        public BristlingFur()
        {
            Cooldown = 45m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buff>() { new Buffs.Druid.BristlingFur() }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
