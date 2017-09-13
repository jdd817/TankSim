using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Netherlight.Shadow
{
    [Effect]
    public class MasterOfShadows:PermanentBuff
    {
        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Mastery)
                return 1000;
            return 0;
        }
    }
}
