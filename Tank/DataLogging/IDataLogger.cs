using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public interface IDataLogger
    {
        void LogEvent(DamageEvent Event);

        void UsedAbility(decimal Time, string AbilityName, AbilityResult Result);
        void LogHealth(decimal Time, int Health);

        void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff);
    }

    public enum BuffAction
    {
        Applied,
        Refreshed,
        Faded,
        Ticked
    }
}
