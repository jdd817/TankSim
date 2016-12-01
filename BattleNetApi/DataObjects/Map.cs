using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Map
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public bool hasChallengeMode { get; set; }
        public ChallengeModeCriteria bronzeCriteria { get; set; }
        public ChallengeModeCriteria silverCriteria { get; set; }
        public ChallengeModeCriteria goldCriteria { get; set; }
    }
}
