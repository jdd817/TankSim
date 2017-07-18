using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Talents
{
    public class TalentAttribute:Attribute
    {
        public TalentAttribute() { }
        public TalentAttribute(Type characterClass, int tier, int possition)
        {
            Class = characterClass;
            Tier = tier;
            Possition = possition;
        }

        public Type Class { get; set; }
        public int Tier { get; set; }
        public int Possition { get; set; }
    }
}
