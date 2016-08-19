using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class ElusiveBrawler : Buff
    {

        public ElusiveBrawler(decimal dodgeChance)
        {
            DodgeChance = dodgeChance;
        }

        public decimal DodgeChance { get; set; }

        public override decimal Durration { get { return 60.0m; } }

        public override int MaxStacks { get { return 100; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Dodge)
                return DodgeChance * Stacks;  //how does mastery interact?
            if (Stat == StatType.AttackPower)
                return 0.08m;
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
