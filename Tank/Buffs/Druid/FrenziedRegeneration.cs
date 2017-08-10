using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class FrenziedRegeneration : HealOverTime
    {
        public FrenziedRegeneration(int healAmount)
        {
            HealingPerTick = (int)( healAmount / 3m);
            Tick = 1.0m;
        }

        public override decimal Durration
        {
            get
            {
                return 3m;
            }
        }
    }
}
