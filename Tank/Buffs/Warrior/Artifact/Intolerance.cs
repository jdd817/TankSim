using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior.Artifact
{
    public class Intolerance:PermanentBuff
    {
        public override int MaxStacks { get { return 7; } }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.ResourceCap)
                return Stacks * 10;
            return 0;
        }
    }
}
