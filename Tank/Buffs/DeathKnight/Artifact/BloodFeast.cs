using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    [EffectPriority(2)]
    public class BloodFeast:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 1; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.HeartStrike))
                Result.SelfHealing = (int)(Result.DamageDealt * 0.25m);
        }
    }
}
