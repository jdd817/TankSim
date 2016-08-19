using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.DataLogging
{
    public class DamageEvent
    {
        public decimal Time
        { get; set; }

        public AttackResult Result
        { get; set; }

        public int DamageTaken
        { get; set; }

        public int DamageAbsorbed
        { get; set; }

        public int DamageBlocked
        { get; set; }

        public int DamageHealed
        {
            get; set;
        }
    }
}
