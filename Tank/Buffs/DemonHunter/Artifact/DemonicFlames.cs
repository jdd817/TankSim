using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class DemonicFlames : PermanentBuff, IBuffAppliedEffectStack
    {
        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(FieryBrand))
                buff.TimeRemaining += 2.0m;
        }
    }
}
