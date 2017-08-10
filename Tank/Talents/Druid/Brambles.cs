using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 1, 1)]
    public class Brambles : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0)
                damageEvent.DamageTaken = Math.Max(0, (int)(damageEvent.DamageTaken - tank.AttackPower * 0.24m));
        }
    }
}
