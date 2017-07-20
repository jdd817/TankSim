using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public static class DataLogManager
    {
        private static List<DamageEvent> _events;
        private static decimal _currentTime;

        static DataLogManager()
        {
            Loggers = new List<IDataLogger>();
            _events = new List<DamageEvent>();
        }

        public static List<IDataLogger> Loggers
        { get; set; }

        public static void Reset()
        {
            _events = new List<DamageEvent>();
            _currentTime = 0;
        }

        public static void LogEvent(DamageEvent Event)
        {
            _currentTime = Event.Time;
            _events.Add(Event);
            foreach (IDataLogger Logger in Loggers)
                Logger.LogEvent(Event);
        }

        public static void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
            _currentTime = Time;
            foreach (IDataLogger Logger in Loggers)
                Logger.UsedAbility(Time, AbilityName, Result);
        }

        public static void LogHealth(decimal Time, int Health)
        {
            _currentTime = Time;
            foreach (IDataLogger Logger in Loggers)
                Logger.LogHealth(Time, Health);
        }

        public static void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff)
        {
            _currentTime = Time;
            foreach (IDataLogger Logger in Loggers)
                Logger.LogBuff(Time, buffAction, buff);
        }

        public static void LogHeal(HealingEvent healingEvent)
        {
            foreach (IDataLogger Logger in Loggers)
                Logger.LogHeal(healingEvent);
        }

        public static decimal CurrentTime { get { return _currentTime; } }

        public static int DamageSince(decimal Time)
        {
            return _events.Where(de => de.Time >= Time).Sum(de => de.DamageTaken + de.DamageAbsorbed);
        }
    }
}
