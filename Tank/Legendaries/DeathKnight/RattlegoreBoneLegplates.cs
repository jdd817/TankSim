using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class RattlegoreBoneLegplates : PermanentBuff, IBuffAppliedEffectStack
    {
        public void BuffApplied(Buff buff)
        {
            if(buff.GetType()==typeof(Buffs.DeathKnight.BoneShield))
            {
                var boneshield = buff as Buffs.DeathKnight.BoneShield;
                boneshield.BaseDamageReduction += 0.02m;
            }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.ResourceCap)
                return 60;
            return 0;
        }
    }
}
