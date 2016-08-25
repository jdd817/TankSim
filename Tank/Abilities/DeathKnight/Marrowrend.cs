using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class Marrowrend : Ability
    {
        private IRng _rng;

        public Marrowrend(IRng rng)
        {
            _rng = rng;
        }


        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var boneCharges = 3;
            if (Caster.Buffs.GetBuff(typeof(Buffs.DeathKnight.Artifact.RattlingBones)) != null && _rng.NextDouble() <= 0.30)
                boneCharges++;
            return new AbilityResult
            {
                CasterBuffsApplied = new[]
                {
                    new Buffs.DeathKnight.BoneShield(Caster.Buffs.GetBuff<Buffs.DeathKnight.Artifact.SkeletalShattering>()!=null,Caster.CritChance,_rng)
                    { Stacks = boneCharges }
                },
                SecondaryResourceCost = 2,
                ResourceCost = -20
            };
        }
    }
}
