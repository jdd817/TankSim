using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 6, 3)]
    public class SurvivalOfTheFittest : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Barkskin) || Ability.GetType()==typeof(Abilities.Druid.SurvivalInstincts))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = Ability.GetType(),
                    ReductionType = ReductionType.By,
                    Amount = Ability.Cooldown * 0.33m
                });
            }
        }
    }
}
