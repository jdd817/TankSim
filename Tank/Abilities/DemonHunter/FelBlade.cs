﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class FelBlade : Ability
    {
        public FelBlade()
        {
            Cooldown = 15m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = (int)(5.6m * (Caster as Player).WeaponDamage),
                DamageType = DamageType.Fire,
                ResourceCost = -20
            };
        }
    }
}
