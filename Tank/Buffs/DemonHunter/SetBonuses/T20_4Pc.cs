using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.SetBonuses
{
    [Effect(Class = typeof(Classes.DemonHunter))]
    public class T20_4Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.SoulCleave))
                Result.TargetBuffsApplied.Add(new T20_4Pc_Buff());
        }
    }

    public class T20_4Pc_Buff:Buff
    {
        public override decimal Durration { get { return 8m; } }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Versatility)
                return 2000;
            return 0;
        }
    }
}
