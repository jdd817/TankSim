using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class WillOfTheIllidari:PermanentBuff
    {
        public override int MaxStacks { get { return 7; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
                return 0.01m * Stacks;
            return 0;
        }
    }
}
