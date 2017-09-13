using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Netherlight.Light
{
    [Effect]
    public class Shocklight : PermanentBuff, IBuffAppliedEffectStack
    {
        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.Common.CorcordanceOfTheLegionfall))
                Target.Buffs.AddBuff(new Shocklight_Buff());
        }
    }

    public class Shocklight_Buff : Buff
    {
        public override decimal Durration { get { return 10m; } }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Crit)
                return 1500;
            return 0;
        }
    }
}
