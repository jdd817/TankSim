using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tank
{
    public class Weapon
    {
        public Weapon()
        {
            TwoHander = false;
        }

        public int Damage
        {
            get
            {
                return (LowDamage + HighDamage) / 2;
            }
        }

        public int LowDamage
        { get; set; }

        public int HighDamage
        { get; set; }

        public decimal Speed
        { get; set; }

        public decimal SwingTimer
        { get; set; }

        public bool TwoHander { get; set; }
    }
}
