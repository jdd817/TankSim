using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Appearance
    {
        public int faceVariation { get; set; }
        public int skinColor { get; set; }
        public int hairVariation { get; set; }
        public int hairColor { get; set; }
        public int featureVariation { get; set; }
        public bool showHelm { get; set; }
        public bool showCloak { get; set; }
        public List<int> customDisplayOptions { get; set; }
    }
}
