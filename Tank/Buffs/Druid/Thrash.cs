using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class Thrash : Buff
    {
        public Thrash(int damage)
        {
            Tick = 3m;
            DamagePerTick = (int)(damage * Tick / Durration);
        }

        public int DamagePerTick { get; set; }

        public override int MaxStacks { get { return 3; } }

        public override decimal Durration { get { return 15; } }
    }
}
