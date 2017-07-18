using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs
{
    public class EffectAttribute:Attribute
    {
        public Type Class { get; set; }
    }
}
