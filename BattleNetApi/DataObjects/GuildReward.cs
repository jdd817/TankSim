using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class GuildReward
    {
        public int minGuildLevel { get; set; }
        public int minGuildRepLevel { get; set; }
        public Achievement achievement { get; set; }
        public Item item { get; set; }
    }
}
