using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class Moonfire : Buff
    {
        public override decimal Durration
        { get { return 16.0m; } }

        public override int MaxStacks
        { get { return 1; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0m;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
