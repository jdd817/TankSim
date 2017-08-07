using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.SetBonuses
{
    [Effect(Class = typeof(Classes.DemonHunter))]
    public class T20_2Pc : PermanentBuff, IBuffFadedEffectStack
    {
        public void BuffFaded(Buff buff)
        {
            if (buff.GetType() == typeof(LesserSoulFragment))
                Target.Buffs.AddBuff(new T20_2Pc_Buff());
        }
    }

    public class T20_2Pc_Buff : Buff
    {
        public override decimal Durration { get { return 6m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.05m;
            return 0;
        }
    }
}
