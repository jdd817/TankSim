using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior),3,5)]
    public class Indomitable:Buffs.PermanentBuff
    {
        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
                return 0.20m;
            return 0;
        }
    }
}
