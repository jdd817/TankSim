using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.SetBonuses
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class T19_4Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public T19_4Pc(IRng rng)
        {
            _rng = rng;
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

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player Tank, Mob Mob)
        {
            if(Ability.GetType()==typeof(Tank.Abilities.DeathKnight.DeathStrike) && _rng.NextDouble()<=0.25)
            {
                Result.SecondaryResourceCost = -1;
            }
        }
    }
}
