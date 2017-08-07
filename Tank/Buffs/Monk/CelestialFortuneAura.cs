using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    public class CelestialFortuneAura : PermanentBuff, IHealingReceivedEffectStack
    {
        private IRng _rng;

        public CelestialFortuneAura(IRng rng)
        {
            _rng = rng;
        }

        public void HealingReceived(HealingEvent healingEvent, Player tank, Ability ability)
        { 
            if (_rng.NextDouble() < (double)tank.CritChance)
                healingEvent.Amount = (int)(healingEvent.Amount * 1.65m);
        }
    }
}
