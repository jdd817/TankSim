using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    [EffectPriority(5)]
    public class ShacklesOfBryndaor : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.DeathStrike))
            {
                if (Result.SelfHealing * (1m + tank.VersatilityDamageIncrease) > tank.MaxHealth * 0.10m)
                    Result.ResourceCost = (int)(Result.ResourceCost * 0.85m);
            }
        }
    }
}
