using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SoulCleave : Ability
    {
        public SoulCleave()
        {
            ResourceCost = 60;
        }

        public override AbilityType AbilityType { get { return AbilityType.Melee; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;

            var healingDone = (int)(2.0m * dh.AttackPower * 4.5m * (1.0m + dh.VersatilityDamageIncrease));

            var soulFragments = Caster.Buffs.GetStacks(typeof(SoulFragment));
            Caster.Buffs.ClearBuff(typeof(SoulFragment));

            healingDone += (int)(soulFragments * 21.25m * dh.AttackPower);

            return new AbilityResult
            {
                ResourceCost = ResourceCost,
                DamageDealt = 0,
                SelfHealing = healingDone,
                CasterBuffsApplied = new[] { new Buffs.DemonHunter.FeastOfSouls((int)(3.9m * dh.AttackPower * (dh.CurrentHealth*1m / dh.MaxHealth))) }
            };
        }
    }
}
