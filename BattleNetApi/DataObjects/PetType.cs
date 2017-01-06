using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class PetType
    {
        int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public int typeAbilityId { get; set; }
        public int strongAgainstId { get; set; }
        public int weakAgainstId { get; set; }
    }
}
