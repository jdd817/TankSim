using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class EmbraceThePain:PermanentBuff, IBuffAppliedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            if(buff.GetType()==typeof(Metamorphosis))
                (buff as Metamorphosis).HealthGain += Stacks * 0.05m;
        }
    }
}
