using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Web.Models
{
    public class SimulationResult
    {
        public List<Plot> Results { get; set; }
    }

    public class Plot
    {
        public string label { get; set; }
        public decimal[][] data { get; set; }
    }
}