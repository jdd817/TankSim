using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class FueledByPain : RPPMBuff, IBuffFadedEffectStack
    {
        public FueledByPain(IRng rng) : base(rng)
        {
            ProcsPerMinute = 1;
        }

        public void BuffFaded(Buff buff)
        {
            if (buff.GetType() == typeof(LesserSoulFragment) && DidProc(DataLogging.DataLogManager.CurrentTime, Target as Player))
                Target.Buffs.AddBuff(new Metamorphosis() { TimeRemaining = 5.0m });
        }
    }
}
