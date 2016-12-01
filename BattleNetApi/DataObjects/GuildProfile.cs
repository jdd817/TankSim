using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class GuildProfile
    {
        public long lastModified { get; set; }
        public string name { get; set; }
        public string realm { get; set; }
        public string battlegroup { get; set; }
        public int level { get; set; }
        public int side { get; set; }
        public int achievementPoints { get; set; }
        public AchievementData achievements { get; set; }
        public Emblem emblem { get; set; }
        public List<ChallengeMode> challenge { get; set; }
        public List<GuildMember> members { get; set; }
        public List<News> news { get; set; }
    }
}
