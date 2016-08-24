using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class EyeOfTheTiger : HealOverTime
    {
        public EyeOfTheTiger(int healingPerTick)
        {
            HealingPerTick = healingPerTick;
            Tick = 1;
        }

        public override decimal Durration
        {
            get { return 8; }
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
