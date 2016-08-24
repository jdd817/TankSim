using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class RendAndTear : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 15m;
            }
        }

        public override int MaxStacks
        {
            get
            {
                return 3;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.02m * Stacks;
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
