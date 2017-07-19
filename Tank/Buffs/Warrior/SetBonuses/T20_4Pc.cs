using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior.SetBonuses
{
    [Effect(Class = typeof(Classes.Warrior))]
    public class T20_4Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.ShieldSlam))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Warrior.BerserkerRage),
                    Amount = 3.0m,
                    ReductionType = ReductionType.By
                });
            }
        }
    }
}
