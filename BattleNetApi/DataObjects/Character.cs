using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Character
    {
        public string name { get; set; }
        public string realm { get; set; }
        public string battlegroup { get; set; }
        public int @class { get; set; }
        public int race { get; set; }
        public int gender { get; set; }
        public int level { get; set; }
        public int achievementPoints { get; set; }
        public string thumbnail { get; set; }
        public string calcClass { get; set; }
        public int faction { get; set; }
        public int totalHonorableKills { get; set; }
        public string guildRealm { get; set; }
        public long lastModified { get; set; }
        public Specialization spec { get; set; }
        public AchievementData achievements { get; set; }
        public Appearance appearance { get; set; }
        public List<News> feed { get; set; }
        public GuildSummary guild { get; set; }
        public ItemList items { get; set; }
        public PlayerStats stats { get; set; }
        public List<TalentData> talents { get; set; }
    }
}
