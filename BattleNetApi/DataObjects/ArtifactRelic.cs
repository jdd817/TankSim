using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class ArtifactRelic
    {
        public int socket { get; set; }
        public int itemId { get; set; }
        public int context { get; set; }
        public List<int> bonusLists { get; set; }
    }
}
