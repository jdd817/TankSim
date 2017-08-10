using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Druid
{
    public class BristlingFur : Buff, IDamageTakenEffectStack
    {
        public override decimal Durration { get { return 8m; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            //get rage
            //from blue: 50 * DamageTaken / MaxHealth
            //from icy: BristlingFurRage = 100 * Damage / ExpectedMaxHealth
            //using icy as its likely more recent than the blue i found 
            (tank as Classes.Druid).Rage += (int)(100m * (damageEvent.RawDamage / tank.MaxHealth));
        }
    }
}
