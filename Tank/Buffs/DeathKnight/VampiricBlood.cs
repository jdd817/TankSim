using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.DeathKnight
{
    public class VampiricBlood : Buff, IHealingReceivedEffectStack
    {
        public VampiricBlood()
        {
            TimeRemaining = Durration;
            PercentageGain = 0.30m;
        }

        public override decimal Durration { get { return 10.0m; } }

        public decimal PercentageGain { get; set; }

        public override int MaxStacks
        {
            get { return 1; }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
                return PercentageGain;
            return 0;
        }

        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        {
            healingEvent.Amount = (int)(healingEvent.Amount * (1m + PercentageGain));
        }

        public override void Applied()
        {
            var oldMaxHealth = (int)(Target.MaxHealth / (1m + PercentageGain));
            Target.CurrentHealth += Target.MaxHealth - oldMaxHealth;
        }
    }
}
