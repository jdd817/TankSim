using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Warrior
{
    [EffectPriority(-10)]
    public class ShieldBlock : Buff, IDamageTakenEffectStack
    {
        public ShieldBlock()
        {
            TimeRemaining = 6.0m;
        }

        public override decimal Durration { get { return 6.0m; } }

        public override int MaxStacks
        {
            get { return 1; }
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.Result == AttackResult.Hit || damageEvent.Result == AttackResult.Crit)
                damageEvent.Result = AttackResult.Block;
        }
    }
}
