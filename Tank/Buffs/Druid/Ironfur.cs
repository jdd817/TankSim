using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid
{
    public class Ironfur : Buff
    {
        public Ironfur()
        {
            ArmorIncrease = 0.65m;
            buffId = _buffCounter++;
        }

        private static int _buffCounter = 0;
        private int buffId;

        public override decimal Durration
        {
            get
            {
                return 6m;
            }
        }

        public decimal ArmorIncrease { get; set; }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Armor)
                return ArmorIncrease;
            return 0m;
        }

        public override string Name
        {
            get { return this.GetType().Name + buffId; }
        }
    }
}
