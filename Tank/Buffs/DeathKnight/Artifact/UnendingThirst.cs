using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class UnendingThirst : PermanentBuff
    {
        public override int MaxStacks { get { return 1; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Leech && Target.Buffs.GetBuff<Buffs.DeathKnight.BloodShield>()!=null)
                return 0.25m;
            return 0;
        }
    }
}
