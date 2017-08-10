using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Druid
{
    public class DruidMasteryAura : PermanentBuff, IHealingReceivedEffectStack
    {
        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        {
            healingEvent.Amount += (int)(healingEvent.Amount * (tank.Mastery * 1.25m));
        }
    }
}
