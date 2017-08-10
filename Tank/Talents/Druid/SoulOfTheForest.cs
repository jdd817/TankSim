using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 5, 1)]
    public class SoulOfTheForest : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Mangle))
            {
                Result.ResourceCost -= 5;
                Result.DamageDealt += (int)(Result.DamageDealt * 0.25m);
            }
        }
    }
}
