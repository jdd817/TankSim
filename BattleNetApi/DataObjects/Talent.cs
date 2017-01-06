using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Talent
    {
        public int tier { get; set; }
        public int column { get; set; }
        public Spell spell { get; set; }
    }
}
