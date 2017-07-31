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
    [EffectPriority(2)]
    public class MannorothsBloodlettingManacles : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.ResourceCost>0)
            {
                Result.SelfHealing += (int)(0.001m * Result.ResourceCost * tank.MaxHealth);
            }
        }
    }
}
