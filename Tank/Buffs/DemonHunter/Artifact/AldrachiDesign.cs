using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class AldrachiDesign:PermanentBuff
    {
        public override int MaxStacks
        { get { return 7; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Parry)
                return Stacks * 0.01m;

            return 0;
        }
    }
}
