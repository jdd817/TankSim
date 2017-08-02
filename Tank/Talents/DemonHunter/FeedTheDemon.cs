using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.DemonHunter
{
    [Talent(typeof(Classes.DemonHunter), 4, 1)]
    public class FeedTheDemon : PermanentBuff, IBuffFadedEffectStack
    {
        public void BuffFaded(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.DemonHunter.LesserSoulFragment))
                (Target as Player).Cooldowns.ReduceTimers(new Abilities.CooldownReduction
                {
                    Ability = typeof(Abilities.DemonHunter.DemonSpikes),
                    Amount = buff.Stacks,
                    ReductionType = Abilities.ReductionType.By
                });
        }
    }
}
