using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Spell
    {
        public int id { get; set; }
        public string name { get; set; }
        public string subtext { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string castTime { get; set; }
    }
}
