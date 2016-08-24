using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SoulCleave : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;

            var healingDone = (int)(12.75m * dh.AttackPower);// * (1.0m + dh.VersatilityDamageIncrease));

            var soulFragments = Caster.Buffs.GetStacks(typeof(SoulFragment));
            Caster.Buffs.ClearBuff(typeof(SoulFragment));

            healingDone += (int)(soulFragments * 2.5m * dh.AttackPower);

            return new AbilityResult
            {
                ResourceCost = 60,
                DamageDealt = 0,
                SelfHealing = healingDone,
                CasterBuffsApplied = new[] { new Buffs.DemonHunter.FeastOfSouls((int)(3.9m * dh.AttackPower * dh.HealthPercentage)) },
                CooldownReduction = new[] { new CooldownReduction { Ability = typeof(Abilities.DemonHunter.DemonSpikes), ReductionType = ReductionType.By, Amount = soulFragments } }
            };
        }
    }
}
