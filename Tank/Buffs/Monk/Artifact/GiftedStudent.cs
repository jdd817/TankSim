using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Monk.Artifact
{
    public class GiftedStudent : PermanentBuff, IDamageTakenEffectStack, IHealingReceivedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.Result == AttackResult.Dodge)
                tank.Buffs.AddBuff(new GiftedStudent_Buff(0.01m * Stacks));
        }

        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        {
            if (healingEvent.Name == "GiftOfTheOx")
                tank.Buffs.AddBuff(new GiftedStudent_Buff(0.01m * Stacks));
        }
    }

    public class GiftedStudent_Buff:Buff
    {
        public GiftedStudent_Buff(decimal critGain)
        {
            CritGain = critGain;
        }

        public decimal CritGain { get; set; }

        public override decimal Durration { get { return 3m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Crit)
                return CritGain;
            return 0;
        }
    }
}
