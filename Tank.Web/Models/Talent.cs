using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Web.Models
{
    public class Talent
    {
        public string Class { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }
}