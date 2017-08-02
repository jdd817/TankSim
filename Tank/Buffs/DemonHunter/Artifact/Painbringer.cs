using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class Painbringer:PermanentBuff
    {
    }

    public class Painbringer_Buff:Buff
    {
        public override int MaxStacks { get { return 100; } }
        public override decimal Durration { get { return 4m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.03m * Stacks;
            return 0;
        }
    }
}
