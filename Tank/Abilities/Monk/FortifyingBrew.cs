﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class FortifyingBrew : Ability
    {
        public FortifyingBrew()
        {
            Cooldown = 7 * 60m;
        }
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Monk.FortifyingBrew() }
            };
        }
    }
}
