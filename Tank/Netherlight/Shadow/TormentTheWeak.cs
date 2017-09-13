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
    public class TormentTheWeak : RPPMBuff, IPlayerAbilityEffectStack
    {
        public TormentTheWeak(IRng rng) : base(rng)
        {
            ProcsPerMinute = 4;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            //only applies a dot.  as the engine doesnt support dots that dont leech, just adding the damage.
            //  will slightly overvalue this ability, but as it will likely be the weakest defensively regardless, not worried about it
            if (Result.DamageDealt > 0 && DidProc(CurrentTime,tank))
            {
                Result.DamageDealt += 80000;
            }
        }
    }
}
