using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class ItemList
    {
        public int averageItemLevel { get; set; }
        public int averageItemLevelEquiped { get; set; }
        public Item head { get; set; }
    }
}
