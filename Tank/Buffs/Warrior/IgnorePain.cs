using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.DataLogging;

namespace Tank.Buffs.Warrior
{
    [EffectPriority(1)]
    public class IgnorePain : Buff, IDamageTakenEffectStack
    {
        public IgnorePain(int DamageAbsorbed)
        {
            TimeRemaining = Durration;
            DamageRemaining = DamageAbsorbed;
        }

        public override decimal Durration { get { return 15.0m; } }

        public int DamageRemaining
        { get; set; }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            DamageRemaining += (NewBuff as IgnorePain).DamageRemaining;
            if (DamageRemaining > Target.MaxHealth)
                DamageRemaining = Target.MaxHealth;
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageRemaining,
                    TimeRemaining);
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            var BarrierHit = Math.Min(DamageRemaining, damageEvent.DamageTaken);
            int Absorbed = (int)(BarrierHit * 0.90m);
            damageEvent.DamageTaken = damageEvent.DamageTaken - Absorbed;
            damageEvent.DamageAbsorbed += Absorbed;
            DamageRemaining -= BarrierHit;
        }
    }
}
