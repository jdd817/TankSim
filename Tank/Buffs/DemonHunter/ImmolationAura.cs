using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DemonHunter
{
    public class ImmolationAura : DamageOverTime
    {
        public ImmolationAura(int attackPower)
        {
            TimeRemaining = Durration;
            Tick = 0.6m;
            //actually does 69% of attackpower per second.  adjusting the coefficient to account for ticking it every .6 instead of 1 seconds.
            DamagePerTick = (int)(0.414m * attackPower);
        }

        public override decimal Durration { get { return 6.0m; } }

        public override void Ticked()
        {
            (Target as Classes.DemonHunter).Pain += 2;
        }

        public override int MaxStacks
        {
            get { return 1; }
        }
    }
}
