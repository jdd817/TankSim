using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 1, 3)]
    public class BloodFrenzy : PermanentBuff, IBuffTickedEffectStack
    {
        public void BuffTicked(Buff buff)
        {
            if(buff.GetType()==typeof(Buffs.Druid.Thrash))
                (Target as Classes.Druid).Rage += 2;
        }
    }
}
