using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class ItemClass
    {
        public int @class { get; set; }
        public string name { get; set; }
        public List<ItemSubClass> sublcasses { get; set; } 
    }

    public class ItemSubClass
    {
        public int subclass { get; set; }
        public string name { get; set; }
    }
}
