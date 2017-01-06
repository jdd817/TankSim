using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class AchievementCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Achievement> acheivements { get; set; }
        public List<AchievementCategory> categories { get; set; }
    }
}
