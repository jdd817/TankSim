using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class GuildSummary
    {
        public string name { get; set; }
        public string realm { get; set; }
        public string battlegroup { get; set; }
        public int members { get; set; }
        public int achievementPoints { get; set; }
        public Emblem emblem { get; set; }
    }
}
