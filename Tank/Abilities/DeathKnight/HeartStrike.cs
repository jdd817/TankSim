using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class HeartStrike : Ability
    {
        public HeartStrike()
        {
            SecondaryResourceCost = 1;
            ResourceGain = 18;
        }

        public override AbilityType AbilityType { get { return AbilityType.Melee; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var healingPercentage = 0.0m;
            var bloodFeast = Caster.Buffs.GetBuff(typeof(Buffs.DeathKnight.Artifact.BloodFeast));
            if (bloodFeast != null)
                healingPercentage = 0.25m;

            return new AbilityResult
            {
                SecondaryResourceCost = 1,
                ResourceCost = -18,
                DamageDealt = (int)(Caster.Weapons[0].Damage * 2.58m * (1 + Caster.Buffs.GetStacks(typeof(Buffs.DeathKnight.Artifact.Veinrender)) * 0.4m)),
                SelfHealing = (int)(Caster.Weapons[0].Damage * 2.58m * healingPercentage * (1 + Caster.Buffs.GetStacks(typeof(Buffs.DeathKnight.Artifact.Veinrender)) * 0.4m))
            };
        }
    }
}
