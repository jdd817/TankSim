using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization.BattleNetAdapters
{
    public class StatAdapter : IAdapter<int, StatType>
    {
        public StatType Convert(int source)
        {
            switch (source)
            {
                case 7: return StatType.Stam;
                case 32: return StatType.Crit;
                case 36: return StatType.Haste;
                case 40: return StatType.Vers;
                case 49: return StatType.Mastery;
                case 62: return StatType.Leech;
                case 4:
                case 71:
                case 74: return StatType.Str;
                default: return StatType.Other;
            }
        }
    }
}
