using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public class StreamLogger:IDataLogger, IDisposable
    {
        private TextWriter _eventWriter;
        private TextWriter _abilityWriter;
        private TextWriter _healthWriter;
        private TextWriter _buffWriter;

        public StreamLogger(TextWriter outputWriter)
        {
            _eventWriter = outputWriter;
            _abilityWriter = outputWriter;
            _healthWriter = outputWriter;
            _buffWriter = outputWriter;
        }

        public StreamLogger(TextWriter eventWriter,
            TextWriter abilityWriter,
            TextWriter healthWriter,
            TextWriter buffWriter)
        {
            _eventWriter = eventWriter;
            _abilityWriter = abilityWriter;
            _healthWriter = healthWriter;
            _buffWriter = buffWriter;
        }

        public void Dispose()
        {
            var writers = (new[] { _eventWriter, _abilityWriter, _healthWriter, _buffWriter }).Where(w => w != null).Distinct();

            foreach (var writer in writers)
            {
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
        }

        #region IDataLogger Members

        public void LogEvent(DamageEvent Event)
        {
            if (_eventWriter != null)
                _eventWriter.WriteLine("[{0:0.00}] Mob {1} for {2} Damage {3} {4}",
                    Event.Time,
                    Event.Result,
                    Event.DamageTaken,
                    Event.DamageBlocked > 0 ? String.Format("({0} blocked)", Event.DamageBlocked) : "",
                    Event.DamageAbsorbed > 0 ? String.Format("({0} absorbed)", Event.DamageAbsorbed) : "");
        }

        public void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
            if (_abilityWriter != null)
                _abilityWriter.WriteLine("[{0:0.00}] Used {1} {2}",
                    Result.Time,
                    AbilityName,
                    Result.SelfHealing > 0 ? String.Format("(+{0} Healed)", Result.SelfHealing) : ""
                    );
        }

        private int _lastHealth = -1;

        public void LogHealth(decimal Time, int Health)
        {
            if (_healthWriter != null)
            {
                if (Health != _lastHealth)
                {
                    _healthWriter.WriteLine("[{0:0.00}] At {1} Health", Time, Health);
                    _lastHealth = Health;
                }
            }
        }

        public void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff)
        {
            if (_buffWriter != null)
            {
                _buffWriter.WriteLine("[{0:0.00}] {2} {1}",
                    Time,
                    buff,
                    buffAction);
            }
        }

        #endregion
    }
}
