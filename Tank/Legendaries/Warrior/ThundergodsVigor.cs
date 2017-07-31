using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.Warrior
{
    [Effect(Class = typeof(Classes.Warrior))]
    public class ThundergodsVigor : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.ThunderClap))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Warrior.DemoralizingShout),
                    Amount = 3m,
                    ReductionType = ReductionType.By
                });
            }
        }
    }
}
