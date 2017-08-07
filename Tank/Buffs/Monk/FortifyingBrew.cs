using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class FortifyingBrew:Buff
    {
        public override decimal Durration { get { return 15m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
                return 0.20m;
            if (Stat == StatType.StaggerAmount)
                return 0.10m;
            if (Stat == StatType.DamageReduction)
                return 0.20m;
            return 0;
        }

        public override void Applied()
        {
            var oldMaxHealth = (int)(Target.MaxHealth / (1.20m));
            Target.CurrentHealth += Target.MaxHealth - oldMaxHealth;
        }
    }
}
