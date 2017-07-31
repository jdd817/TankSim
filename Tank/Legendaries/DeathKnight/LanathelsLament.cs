using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class LanathelsLament : PermanentBuff, IHealingReceivedEffectStack
    {
        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        {
            if(tank.Buffs.GetBuff<Buffs.DeathKnight.DeathAndDecay>()!=null)
            {
                healingEvent.Amount = (int)(healingEvent.Amount * 1.05m);
            }
        }
    }
}
