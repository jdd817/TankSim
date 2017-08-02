using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class VampiricAura : Buff
    {
        public override decimal Durration
        {
            get
            {
                return 15m;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Leech)
                return 0.25m;
            return 0m;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
