using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.DeathKnight
{
    [EffectPriority(10)]
    public class DeathKnightMastery : PermanentBuff, IHealingReceivedEffectStack
    {
        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        {
            if(ability!=null && ability.GetType()==typeof(Abilities.DeathKnight.DeathStrike))
            {
                var shieldModifier = 0.12m;
                shieldModifier += tank.Mastery * 0.60m;

                tank.Buffs.AddBuff(new Buffs.DeathKnight.BloodShield((int)(healingEvent.Amount * shieldModifier)));
            }
        }
    }
}
