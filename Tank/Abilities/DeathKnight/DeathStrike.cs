using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class DeathStrike : Ability
    {
        public DeathStrike()
        {
            ResourceCost = 45;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var healingDone = HealingAmount(Caster);

            var shieldModifier = 0.12m;
            var tank = Caster as Player;
            if (tank != null)
            {
                var mastery = RatingConverter.GetRating(StatType.Mastery, tank.MasteryRating);
                shieldModifier += mastery * 0.60m;
            }

            var resourceCost = 45;
            var boneSheild = Caster.Buffs.GetBuff(typeof(Buffs.DeathKnight.BoneShield));
            if (boneSheild != null && boneSheild.Stacks >= 5)
                resourceCost -= 10;

            return new AbilityResult
            {
                ResourceCost = resourceCost,
                DamageDealt = 0,
                SelfHealing = healingDone,
                CasterBuffsApplied = new[] { new Buffs.DeathKnight.BloodShield((int)(healingDone * shieldModifier)) }
            };
        }

        public static int HealingAmount(Actor Caster)
        {
            return (int)Math.Max(DataLogging.DataLogManager.DamageSince(DataLogging.DataLogManager.CurrentTime - 5) * 0.20m,
                Caster.MaxHealth * 0.10m);
        }
    }
}
