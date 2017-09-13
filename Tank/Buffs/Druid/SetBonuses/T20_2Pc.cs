using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.SetBonuses
{
    [Effect(Class = typeof(Classes.Druid))]
    public class T20_2Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            //cant find any information on how this scales.  assuming linearly, with 0% reduction at 100 health and 40% and 0 health
            if(Ability.GetType()==typeof(Abilities.Druid.FrenziedRegeneration))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Druid.FrenziedRegeneration),
                    ReductionType = ReductionType.By,
                    Amount = (1m - tank.HealthPercentage) * 0.40m * Ability.Cooldown
                });
            }
        }
    }
}
