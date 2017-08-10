using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid
{
    public class GuardianOfUrsoc : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 30m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Armor)
                return 0.15m;
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            var ability = Ability.GetType();
            if (ability == typeof(Abilities.Druid.Thrash)
                || ability == typeof(Abilities.Druid.Swipe)
                || ability == typeof(Abilities.Druid.Mangle)
                || ability == typeof(Abilities.Druid.Maul))
            {
                Result.CooldownReduction.Add(
                    new CooldownReduction
                    {
                        Ability = ability,
                        ReductionType = ReductionType.By,
                        Amount = 1.5m
                    });
            }
        }
    }
}
