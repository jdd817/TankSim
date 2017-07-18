using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class SouldrinkerActive : PermanentBuff, IPlayerAbilityEffectStack
    {
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

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.DeathStrike) || Ability.GetType() == typeof(Abilities.DeathKnight.Consumption))
            {
                var overHeal = (tank.CurrentHealth + Result.SelfHealing) - tank.MaxHealth;
                if (overHeal > 0)
                    tank.Buffs.AddBuff(new Artifact.Souldrinker(overHeal, tank.MaxHealth));
            }
        }
    }
}
