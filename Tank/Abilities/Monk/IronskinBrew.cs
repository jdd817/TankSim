﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Monk
{
    public class IronskinBrew : Ability
    {
        public IronskinBrew()
        {
            Cooldown = 21m;
            MaxCharges = 3;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buff> { new Buffs.Monk.IronskinBrew() }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
