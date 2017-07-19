using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.SetBonuses
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class T20_2Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.BloodBoil))
            {
                Result.CasterBuffsApplied.Add(new GraveWarden());
            }
        }
    }

    public class GraveWarden:Buff
    {
        public override decimal Durration
        {
            get
            {
                return 10m;
            }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Versatility)
                return 2500;
            return 0;
        }
    }
}
