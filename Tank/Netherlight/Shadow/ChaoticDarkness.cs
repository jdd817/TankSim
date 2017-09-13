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
    public class ChaoticDarkness : RPPMBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public ChaoticDarkness(IRng rng) : base(rng)
        {
            _rng = rng;
            ProcsPerMinute = 2;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.DamageDealt>0 && DidProc(CurrentTime,tank))
            {
                var damageIncrease = _rng.Next(60000, 300000);
                Result.DamageDealt += damageIncrease;
                Result.SelfHealing += damageIncrease;
            }
        }
    }
}
