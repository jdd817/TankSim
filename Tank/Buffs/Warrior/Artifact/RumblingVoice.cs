using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Warrior.Artifact
{
    public class RumblingVoice : PermanentBuff, IBuffAppliedEffectStack
    {
        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(DemoralizingShout))
            {
                buff.TimeRemaining = buff.TimeRemaining * 1.5m;
            }
        }
    }
}
