﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SoulBarrier : Ability
    {
        public SoulBarrier()
        {
            Cooldown = 30m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;
            
            var soulFragments = Caster.Buffs.GetBuff<LesserSoulFragment>();
            var healingDone = soulFragments.GetTotalHealing();
            Caster.Buffs.ClearBuff(typeof(LesserSoulFragment));

            return new AbilityResult
            {
                ResourceCost = 60,
                DamageDealt = (int)(7.42m * (Caster as Player).WeaponDamage),
                SelfHealing = healingDone,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.SoulBarrier(dh, soulFragments.Stacks) }
            };
        }
    }
}
