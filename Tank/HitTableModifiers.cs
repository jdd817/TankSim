using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public class HitTableModifiers
    {
        public HitTableModifiers()
        {
            HitModifiers = 0;
            DodgeModifiers = 0;
            ParryModifiers = 0;
            CritModifiers = 0;
        }

        public decimal HitModifiers
        { get; set; }

        public decimal DodgeModifiers
        { get; set; }

        public decimal ParryModifiers
        { get; set; }

        public decimal CritModifiers
        { get; set; }
    }
}
