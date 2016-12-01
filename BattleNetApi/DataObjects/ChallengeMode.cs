using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class ChallengeMode
    {
        public Realm realm { get; set; }
        public Map map { get; set; }
        public List<Group> groups { get; set; }
    }
}
