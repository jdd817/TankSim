using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior
{
    public class DemoralizingShout : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 10m;
            }
        }
    }
}
