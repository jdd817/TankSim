using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class CarrionFeast : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks
        {
            get
            {
                return 6;
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
            if (Ability.GetType() == typeof(Tank.Abilities.DeathKnight.DeathStrike))
            {
                Result.SelfHealing = (int)(Result.SelfHealing * (1m + Stacks * 0.05m));
            }
        }
    }
}
