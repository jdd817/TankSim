using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class RattlingBones:PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public RattlingBones(IRng rng)
        {
            _rng = rng;
        }

        public override int MaxStacks { get { return 1; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.Marrowrend))
            {
                if(_rng.NextDouble()<=0.30)
                {
                    var boneShield = Result.CasterBuffsApplied.OfType<Buffs.DeathKnight.BoneShield>().First();
                    boneShield.Stacks++;
                }
            }
        }
    }
}
