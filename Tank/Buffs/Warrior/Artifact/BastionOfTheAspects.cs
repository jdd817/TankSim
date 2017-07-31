using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Warrior.Artifact
{
    [EffectPriority(-4)]
    public class BastionOfTheAspects : PermanentBuff, IDamageTakenEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.Result==AttackResult.Block)
            {
                var blockDelta = (int)(damageEvent.DamageBlocked * Stacks * 0.02m);
                damageEvent.DamageBlocked += blockDelta;
                damageEvent.DamageTaken -= blockDelta;
            }
        }
    }
}
