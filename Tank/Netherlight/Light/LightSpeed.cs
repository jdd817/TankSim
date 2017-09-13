using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Netherlight.Light
{
    [Effect]
    public class LightSpeed : PermanentBuff
    {
        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Haste)
                return 500;
            return 0;
        }
    }
}
