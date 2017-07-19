using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    [EffectPriority(1)]
    public class Veinrender : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks
        { get { return 6; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.HeartStrike))
                Result.DamageDealt = (int)(Result.DamageDealt * (1 + Stacks * 0.03m));
        }
    }
}
