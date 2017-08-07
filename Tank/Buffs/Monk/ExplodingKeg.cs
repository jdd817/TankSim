using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    [EffectPriority(-5)]
    public class ExplodingKeg : Buff, IDamageTakenEffectStack
    {
        public override decimal Durration { get { return 3m; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            damageEvent.Result = AttackResult.Miss;
            damageEvent.DamageTaken = 0;
            damageEvent.RawDamage = 0;
        }
    }
}
