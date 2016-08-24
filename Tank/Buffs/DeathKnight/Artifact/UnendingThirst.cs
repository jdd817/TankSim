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
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }

    public class UnendingThirstApplication : PermanentBuff
    {
        public override int MaxStacks { get { return 1; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Leech)
                return 0.25m;
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
