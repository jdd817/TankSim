using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class HealingOrb : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 30.0m;
            }
        }

        public override int MaxStacks
        {
            get
            {
                return 100;
            }
        }

        public bool GreaterOrb { get; set; }
    }
}
