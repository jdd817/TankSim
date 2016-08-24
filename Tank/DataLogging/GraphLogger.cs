using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.DataLogging
{
    public class GraphLogger : IDataLogger
    {
        public GraphLogger()
        {
            Lines = new Dictionary<decimal, Dictionary<string, int>>();
            Runs = new List<string>();
        }

        private string _runName;

        public string RunName
        {
            get { return _runName; }
            set
            {
                _runName = value;
                Runs.Add(value);
            }
        }

        public List<string> Runs { get; set; }

        public Dictionary<decimal, Dictionary<string, int>> Lines
        { get; set; }

        public void LogBuff(decimal Time, BuffAction buffAction, Buff buff)
        {
        }

        public void LogEvent(DamageEvent Event)
        {
        }

        public void LogHealth(decimal Time, int Health)
        {
            if (!Lines.ContainsKey(Time))
                Lines.Add(Time, new Dictionary<string, int>());
            Lines[Time].Add(RunName, Health);
        }

        public void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
        }
    }
}
