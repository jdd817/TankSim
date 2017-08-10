using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 3, 3)]
    public class RestorationAffinity : PermanentBuff
    {
        public RestorationAffinity()
        {
            Tick = 5m;
        }

        public override void Ticked()
        {
            var healingAmount = (int)(Target.MaxHealth * 0.03m);
            Target.CurrentHealth += healingAmount;

            DataLogging.DataLogManager.LogHeal(new DataLogging.HealingEvent
            {
                Name = "YserasGift",
                Amount = healingAmount,
                Time = DataLogging.DataLogManager.CurrentTime
            });
        }

        public override void Reset()
        {
            TickTimer = 0;
        }
    }
}
