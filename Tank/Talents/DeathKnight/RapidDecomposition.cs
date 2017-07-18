using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 2, 1)]
    public class RapidDecomposition : Buffs.PermanentBuff
    {
        public override void Ticked()
        {
            if (Target.Buffs.GetBuff<Buffs.DeathKnight.DeathAndDecay>() != null)
                (Target as Classes.DeathKnight).RunicPower += 1;
        }
    }
}
