using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Druid.Artifact
{
    [EffectPriority(1)]
    public class ScintillatingMoonlight : PermanentBuff, IBuffAppliedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.Druid.Moonfire))
                Target.Buffs.AddBuff(new ScintillatingMoonlight_Buff(Stacks * 0.01m) { TimeRemaining = buff.TimeRemaining });
        }
    }

    public class ScintillatingMoonlight_Buff : Buff
    {
        public ScintillatingMoonlight_Buff(decimal damageReduction)
        {
            DamageReduction = damageReduction;
        }

        public decimal DamageReduction { get; set; }

        public override decimal Durration { get { return 10m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return DamageReduction;
            return 0;
        }
    }
}
