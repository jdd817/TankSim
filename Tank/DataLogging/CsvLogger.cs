using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public class CsvLogger : IDataLogger, IDisposable
    {
        private TextWriter _writer;

        public CsvLogger(string file)
        {
            _writer = new StreamWriter(file);
        }

        public void LogEvent(DamageEvent Event)
        {
        }

        public void LogHealth(decimal Time, int Health)
        {
            _writer.WriteLine("{0},{1}", Time, Health);
        }

        public void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
        }
        public void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff)
        { }

        public void Dispose()
        {
            _writer.Flush();
            _writer.Close();
            _writer.Dispose();
        }
    }
}
