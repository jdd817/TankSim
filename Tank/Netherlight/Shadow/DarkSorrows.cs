using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Netherlight.Shadow
{
    [Effect]
    public class DarkSorrows : RPPMBuff, IPlayerAbilityEffectStack
    {
        public DarkSorrows(IRng rng) : base(rng)
        {
            ProcsPerMinute = 1;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.DamageDealt>0 && DidProc(CurrentTime,tank))
            {
                Result.DamageDealt += 1000;
            }
        }
    }
}
