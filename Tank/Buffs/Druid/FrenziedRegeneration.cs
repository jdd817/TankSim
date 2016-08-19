using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class FrenziedRegeneration : HealOverTime
    {
        public FrenziedRegeneration(int healAmount)
        {
            HealingPerTick = (int)( healAmount / 3m);
            Tick = 1.0m;
        }

        public override decimal Durration
        {
            get
            {
                return 3m;
            }
        }

        public override int MaxStacks
        {
            get
            {
                return 1;
            }
        }

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
