using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Trinkets
{
    //[Effect]  NYI
    public class RecompiledGuardianModule : RPPMBuff, IDamageTakenEffectStack
    {
        public RecompiledGuardianModule(IRng rng) : base(rng)
        {
            ProcsPerMinute = 3;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(DidProc(currentTime, tank))
            { }
        }
    }
}
