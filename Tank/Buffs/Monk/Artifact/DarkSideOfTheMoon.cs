using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Monk.Artifact
{
    public class DarkSideOfTheMoon : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.BlackoutStrike))
                Result.CasterBuffsApplied.Add(new DarkSideOfTheMoon_Buff(0.02m * Stacks));
        }
    }

    public class DarkSideOfTheMoon_Buff:Buff, IDamageTakenEffectStack
    {
        public DarkSideOfTheMoon_Buff(decimal reduction)
        {
            DamageReduction = reduction;
        }

        public decimal DamageReduction { get; set; }

        public override decimal Durration { get { return 10m; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.DamageTaken>0)
            {
                damageEvent.DamageTaken -= (int)(damageEvent.DamageTaken * DamageReduction);
                TimeRemaining = 0;
            }
        }
    }
}
