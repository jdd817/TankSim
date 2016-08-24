using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tank
{
    public class Weapon
    {
        public int Damage(IRng rng)
        {
            return rng.Next(LowDamage, HighDamage);
        }

        public int LowDamage
        { get; set; }

        public int HighDamage
        { get; set; }

        public decimal Speed
        { get; set; }

        public decimal SwingTimer
        { get; set; }
    }
}
