using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int quality { get; set; }
        public int itemLevel { get; set; }
        public Tooltip tooltipParams { get; set; }
        public List<Stat> stats { get; set; }
        public int armor { get; set; }
        public string context { get; set; }
        public List<int> bonusLists { get; set; }
        public int artifactId { get; set; }
        public int displayInfoId { get; set; }
        public List<ArtifactTrait> artifactTraits { get; set; }
        public List<ArtifactRelic> relics { get; set; }
        public ItemAppearance appearance { get; set; }
    }
}
