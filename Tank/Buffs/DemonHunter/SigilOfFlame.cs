using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter
{
    public class SigilOfFlame:DamageOverTime
    {
        public SigilOfFlame(int damagePerTick)
        {
            Tick = 1;
            DamagePerTick = damagePerTick;
        }

        public override decimal Durration { get { return 6m; } }
    }
}
