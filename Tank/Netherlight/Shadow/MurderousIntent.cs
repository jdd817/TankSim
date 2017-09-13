using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Netherlight.Shadow
{
    [Effect]
    public class MurderousIntent : PermanentBuff, IBuffAppliedEffectStack
    {
        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.Common.CorcordanceOfTheLegionfall))
                Target.Buffs.AddBuff(new MurderousIntent_Buff());
        }
    }

    public class MurderousIntent_Buff:Buff
    {
        public override decimal Durration { get { return 10m; } }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Versatility)
                return 1500;
            return 0;
        }
    }
}
