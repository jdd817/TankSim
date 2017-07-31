using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior.Artifact
{
    public class DragonSkin : PermanentBuff, IBuffAppliedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            if(buff.GetType()==typeof(IgnorePain))
            {
                var ignorePain = buff as IgnorePain;
                ignorePain.DamageRemaining = (int)(ignorePain.DamageRemaining * (1m + Stacks * .02m));
            }
        }
    }
}
