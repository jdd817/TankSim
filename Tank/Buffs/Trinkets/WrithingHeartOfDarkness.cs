using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Trinkets
{
    [Effect]
    public class WrithingHeartOfDarkness : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Result.AttackResult == AttackResult.Crit)
                tank.Buffs.AddBuff(new Tank.Buffs.Trinkets.WrithingHeartOfDarkness_Buff());
        }
    }

    public class WrithingHeartOfDarkness_Buff : Buff
    {
        public override decimal Durration
        { get { return 6.0m; } }

        public override int MaxStacks
        { get { return 3; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return Stacks * 0.022m;
            return 0m;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }
    }
}
