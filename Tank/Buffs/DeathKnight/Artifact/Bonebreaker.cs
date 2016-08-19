using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class Bonebreaker:PermanentBuff
    {
        public override int MaxStacks { get { return 3; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
