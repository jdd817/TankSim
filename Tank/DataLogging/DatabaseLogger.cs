using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Tank.Abilities;

namespace Tank.DataLogging
{
    public class DatabaseLogger : IDataLogger, IDisposable
    {
        public DatabaseLogger(string connectionString, string RunName = "")
        {
            Conn = new SqlConnection(connectionString);
            RunID = Guid.NewGuid();

            StartRun(RunName);
        }

        SqlConnection Conn;

        public Guid RunID
        { get; private set; }

        private void StartRun(string RunName)
        {
            SqlCommand Cmd = new SqlCommand("StartRun", Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("RunID", RunID));
            Cmd.Parameters.Add(new SqlParameter("RunName", RunName));

            if (Conn.State != ConnectionState.Open)
                Conn.Open();
            Cmd.ExecuteNonQuery();
        }

        #region IDataLogger Members

        public void LogEvent(DamageEvent Event)
        {
            SqlCommand Cmd = new SqlCommand("LogDamageEvent", Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("RunID", RunID));
            Cmd.Parameters.Add(new SqlParameter("Time", Event.Time));
            Cmd.Parameters.Add(new SqlParameter("Result", Event.Result.ToString()));
            Cmd.Parameters.Add(new SqlParameter("DamageTaken", Event.DamageTaken));
            Cmd.Parameters.Add(new SqlParameter("DamageBlocked", Event.DamageBlocked));
            Cmd.Parameters.Add(new SqlParameter("DamageAbsorbed", Event.DamageAbsorbed));

            if (Conn.State != ConnectionState.Open)
                Conn.Open();
            Cmd.ExecuteNonQuery();
        }

        public void UsedAbility(decimal Time, string AbilityName, AbilityResult Result)
        {
            SqlCommand Cmd = new SqlCommand("LogAbilityUsage", Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("RunID", RunID));
            Cmd.Parameters.Add(new SqlParameter("Time", Time));
            Cmd.Parameters.Add(new SqlParameter("Ability", AbilityName));

            if (Conn.State != ConnectionState.Open)
                Conn.Open();
            Cmd.ExecuteNonQuery();
        }

        public void LogHealth(decimal Time, int Health)
        {
            SqlCommand Cmd = new SqlCommand("LogHealth", Conn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add(new SqlParameter("RunID", RunID));
            Cmd.Parameters.Add(new SqlParameter("Time", Time));
            Cmd.Parameters.Add(new SqlParameter("Health", Health));

            if (Conn.State != ConnectionState.Open)
                Conn.Open();
            Cmd.ExecuteNonQuery();
        }

        public void LogBuff(decimal Time, BuffAction buffAction, Buffs.Buff buff)
        { }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Conn.Dispose();
        }

        #endregion
    }
}
