﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Barkskin : Ability
    {
        public Barkskin()
        {
            Cooldown = 60m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Druid.Barkskin() }
            };
        }
    }
}
