using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class HeartStrike : Ability
    {
        private IRng _rng;

        public HeartStrike(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var player = Caster as Player;
            var healingPercentage = 0.0m;
            var bloodFeast = Caster.Buffs.GetBuff(typeof(Buffs.DeathKnight.Artifact.BloodFeast));
            if (bloodFeast != null)
                healingPercentage = 0.25m;

            return new AbilityResult
            {
                SecondaryResourceCost = 1,
                ResourceCost = -18,
                DamageDealt = (int)(player.WeaponDamage * 2.58m * (1 + Caster.Buffs.GetStacks(typeof(Buffs.DeathKnight.Artifact.Veinrender)) * 0.4m)),
                SelfHealing = (int)(player.WeaponDamage * 2.58m * healingPercentage * (1 + Caster.Buffs.GetStacks(typeof(Buffs.DeathKnight.Artifact.Veinrender)) * 0.4m))
            };
        }
    }
}
