using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public class Character
    {
        public Dictionary<Slot,Item> EquippedItems { get; set; }
        public List<Item> Inventory { get; set; }
    }
}
