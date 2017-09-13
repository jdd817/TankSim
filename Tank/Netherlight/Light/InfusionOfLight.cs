using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Netherlight.Light
{
    [Effect]
    public class InfusionOfLight : RPPMBuff, IPlayerAbilityEffectStack
    {
        public InfusionOfLight(IRng rng) : base(rng)
        {
            ProcsPerMinute = 4;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if((Result.DamageDealt>0 || Result.SelfHealing>0||Result.TartgetHealing>0) && DidProc(CurrentTime,tank))
            {
                if (Result.DamageDealt > 0)
                    Result.DamageDealt += 101000;
                if (Result.SelfHealing > 0)
                    Result.SelfHealing += 101000;
                if (Result.TartgetHealing > 0)
                    Result.TartgetHealing += 101000;
            }
        }
    }
}
