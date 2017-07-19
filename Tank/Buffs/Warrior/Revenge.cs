using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior
{
    public class Revenge : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration
        { get { return 15m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.Revenge))
            {
                Result.ResourceCost = 0;
                this.TimeRemaining = 0;
            }
        }
    }
}
