using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class News
    {
        public string type { get; set; }
        public string character { get; set; }
        public long timestamp { get; set; }
        public int itemId { get; set; }
        public string context { get; set; }
        public List<int> bonusLists { get; set; }
        public Achievement achievement { get; set; }
        public bool featOfStrength { get; set; }
        public List<AchievementCriteria> criteria { get; set; }
        public int quantity { get; set; }
        public string name { get; set; }
    }
}
