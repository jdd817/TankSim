using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk.Artifact
{
    public class HotBlooded:PermanentBuff
    {
        public override int MaxStacks { get { return 7; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction && Target.Buffs.BuffActive<BreathOfFire>())
                return 0.01m * Stacks;

            return 0;
        }
    }
}
