using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Netherlight.Light
{
    [Effect]
    public class LightsEmbrace : RPPMBuff, IDamageTakenEffectStack
    {
        public LightsEmbrace(IRng rng) : base(rng)
        {
            ProcsPerMinute = 8;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(DidProc(currentTime,tank))
            {
                tank.Buffs.AddBuff(new LightsEmbrace_Buff());
            }
        }
    }

    public class LightsEmbrace_Buff: HealOverTime
    {
        public override decimal Durration { get { return 6m; } }

        public override int MaxStacks { get { return 5; } }

        public override int HealingPerTick
        {
            get
            {
                return Stacks * 45000;
            }

            set
            {
            }
        }
    }
}
