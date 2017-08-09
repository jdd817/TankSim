using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk.Artifact
{
    public class PotentKick : PermanentBuff, IBuffAppliedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.Monk.IronskinBrew))
                buff.TimeRemaining += 0.5m * Stacks;
        }
    }
}
