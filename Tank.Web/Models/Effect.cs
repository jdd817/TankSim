using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Web.Models
{
    public class Effect
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<EffectParameter> Parameters { get; set; }
    }

    public class EffectParameter
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public string Value { get; set; }
    }
}