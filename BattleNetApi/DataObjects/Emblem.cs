using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class Emblem
    {
        public int icon { get; set; }
        public string iconColor { get; set; }
        public int iconColorId { get; set; }
        public int border { get; set; }
        public string borderColor { get; set; }
        public int borderColorId { get; set; }
        public string backgroundColor { get; set; }
        public int backgroundColorId { get; set; }
    }
}
