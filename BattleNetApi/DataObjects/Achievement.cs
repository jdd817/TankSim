using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Achievement
    {
        public int id { get; set; }
        public string title { get; set; }
        public int points { get; set; }
        public string description { get; set; }
        public List<int> rewardItems { get; set; }
        public string icon { get; set; }
        public List<AchievementCriteria> criteria { get; set; }
        public bool accountWide { get; set; }
        public int factionId { get; set; }
    }
}
