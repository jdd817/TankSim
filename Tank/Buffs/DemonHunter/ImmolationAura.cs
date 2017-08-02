using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DemonHunter
{
    public class ImmolationAura : Buff
    {
        public ImmolationAura()
        {
            TimeRemaining = Durration;
            Tick = 0.6m;
        }

        public override decimal Durration { get { return 6.0m; } }

        public override int MaxStacks
        {
            get { return 1; }
        }
    }
}
