using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.DataLogging
{
    public class HealingEvent
    {
        public string Name { get; set; }
        public decimal Time { get; set; }
        public int Amount { get; set; }
    }
}
