using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.DemonHunter
{
    [Effect(Class = typeof(Classes.DemonHunter))]
    public class KirelNarak : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.ImmolationAura))
                Result.CooldownReduction.Add(
                    new CooldownReduction
                    {
                        Ability = typeof(Abilities.DemonHunter.FieryBrand),
                        Amount = 2m,
                        ReductionType = ReductionType.By
                    });
        }
    }
}
