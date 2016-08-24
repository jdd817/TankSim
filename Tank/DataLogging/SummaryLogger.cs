using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public class SummaryLogger : IDataLogger
    {
        public SummaryLogger()
        {
            DamageEvents = new List<int>();
            HealEvents = new List<int>();
            AbsorbedEvents = new List<int>();
        }

        public int DamageHealed
        { get { return HealEvents.Sum(); } }

        public int DamageAbsorbed
        { get { return AbsorbedEvents.Sum(); } }

        public int DamageTaken
        { get { return DamageEvents.Sum(); } }

        public int MaxDamage
        { get { return DamageEvents.Max(); } }

        public int MinDamage
        { get { return DamageEvents.Min(); } }

        public int DamagedCount
        { get { return DamageEvents.Count; } }

        public decimal Average
        { get { return DamageEvents.Average(x => (decimal)x); } }

        public decimal StandardDeviation
        {
            get
            {
                double Avg = (double)Average;
                double[] Squares = DamageEvents.Select(x => (x - Avg) * (x - Avg)).ToArray();
                return (decimal)Math.Sqrt(Squares.Sum() / Squares.Average());
            }
        }

        private List<int> DamageEvents;
        private List<int> HealEvents;
        private List<int> AbsorbedEvents;
        
        #region IDataLogger Members

        public void LogEvent(DamageEvent Event)
        {
            if (Event.DamageTaken > 0)
                DamageEvents.Add(Event.DamageTaken);
            if (Event.DamageAbsorbed > 0)
                AbsorbedEvents.Add(Event.DamageAbsorbed);
            if (Event.DamageHealed > 0)
                HealEvents.Add(Event.DamageHealed);
        }

        public void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
            if (Result.SelfHealing > 0)
                HealEvents.Add(Result.SelfHealing);
        }

        public void LogHealth(decimal Time, int Health)
        { }

        public void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff)
        { }

        #endregion
    }
}
