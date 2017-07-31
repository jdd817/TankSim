using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior.Artifact
{
    public class VrykulShieldTraining:PermanentBuff
    {
        public override int MaxStacks { get { return 7; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if(Stat==StatType.Armor)
                return 0.02m * Stacks;

            return 0;
        }
    }
}
