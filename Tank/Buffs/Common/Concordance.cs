using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Common
{
    public class Concordance : RPPMBuff, IPlayerAbilityEffectStack
    {
        public Concordance(IRng rng) : base(rng)
        {
            ProcsPerMinute = 2;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(DidProc(CurrentTime,tank))
            {
                tank.Buffs.AddBuff(new CorcordanceOfTheLegionfall { Stacks = Stacks });
            }
        }
    }

    public class CorcordanceOfTheLegionfall:Buff
    {
        public override decimal Durration { get { return 10m; } }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Versatility)
                return 4000 + 300 * (Stacks - 1);
            return 0;
        }
    }
}
