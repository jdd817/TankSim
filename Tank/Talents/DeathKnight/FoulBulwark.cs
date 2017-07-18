using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight),6,3)]
    public class FoulBulwark:Buffs.PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
            {
                var boneSheild = Target.Buffs.GetBuff<Buffs.DeathKnight.BoneShield>();
                if (boneSheild != null)
                    return 0.02m * boneSheild.Stacks;
            }
            return 0;
        }
    }
}
