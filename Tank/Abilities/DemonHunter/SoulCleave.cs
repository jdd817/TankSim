using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SoulCleave : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;

            var healingDone = (int)(12.75m * dh.AttackPower * (1.0m + dh.VersatilityDamageIncrease));

            var soulFragments = Caster.Buffs.GetBuff<LesserSoulFragment>();
            healingDone += soulFragments != null ? soulFragments.GetTotalHealing() : 0;
            Caster.Buffs.ClearBuff(typeof(LesserSoulFragment));

            return new AbilityResult
            {
                ResourceCost = 60,
                DamageDealt = (int)(7.42m*(Caster as Player).WeaponDamage),
                SelfHealing = healingDone,
                CooldownReduction = new List<CooldownReduction>() {
                    new CooldownReduction
                    {
                        Ability = typeof(Abilities.DemonHunter.DemonSpikes),
                        ReductionType = ReductionType.By,
                        Amount = soulFragments!=null? soulFragments.Stacks:0
                    }
                }
            };
        }
    }
}
