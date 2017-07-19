using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class Bonebreaker:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 6; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.Marrowrend))
                Result.DamageDealt = (int)(Result.DamageDealt * (1m + Stacks * .08m));
        }
    }
}
