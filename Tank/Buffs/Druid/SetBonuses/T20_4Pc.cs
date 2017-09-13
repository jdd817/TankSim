using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.SetBonuses
{
    [Effect(Class = typeof(Classes.Druid))]
    public class T20_4Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.ResourceCost>0)
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Druid.FrenziedRegeneration),
                    ReductionType = ReductionType.By,
                    Amount = Result.ResourceCost / 20m
                });
            }
        }
    }
}
