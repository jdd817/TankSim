using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class Marrowrend : Ability
    {
        public Marrowrend()
        {
            SecondaryResourceCost = 2;
            ResourceGain = 20;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var boneCharges = 3;
            if (Caster.Buffs.GetBuff(typeof(Buffs.DeathKnight.Artifact.RattlingBones)) != null && RNG.NextDouble() <= 0.30)
                boneCharges++;
            return new AbilityResult
            {
                CasterBuffsApplied = new[] { new Buffs.DeathKnight.BoneShield() { Stacks = boneCharges } },
                SecondaryResourceCost = 2,
                ResourceCost = -20
            };
        }
    }
}
