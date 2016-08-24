﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tank.Buffs;

namespace Tank
{
    public class BuffManager : IBuffManager
    {
        public BuffManager()
        {
            Buffs = new Dictionary<string, Buff>();
        }

        private Dictionary<string, Buff> Buffs;

        public void AddBuff(Buff NewBuff)
        {
            if (Buffs.ContainsKey(NewBuff.Name))
            {
                Buffs[NewBuff.Name].Refresh(NewBuff);
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Refreshed, Buffs[NewBuff.Name]);
            }
            else
            {
                Buffs.Add(NewBuff.Name, NewBuff);
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Applied, NewBuff);
            }
        }

        public IEnumerable<Buff> DecrementBuffTimers(decimal DeltaTime)
        {
            List<Buff> ExpiredBuffs = new List<Buff>();
            List<Buff> TickingBuffs = new List<Buff>();
            foreach (Buff B in Buffs.Values.Where(b => !b.Permanent))
            {
                B.TimeRemaining -= DeltaTime;
                if (B.TimeRemaining <= 0 || B.Stacks <= 0)
                    ExpiredBuffs.Add(B);
                if (B.Tick > 0)
                {
                    B.TickTimer -= DeltaTime;
                    if (B.TickTimer <= 0)
                    {
                        B.TickTimer += B.Tick;
                        TickingBuffs.Add(B);
                    }
                }
            }

            foreach (Buff Expired in ExpiredBuffs)
            {
                Buffs.Remove(Expired.Name);
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Faded, Expired);
            }

            return TickingBuffs;
        }

        public int GetRatingAdjustment(StatType RatingType)
        {
            return Buffs.Values.Sum(B => B.GetRatingModifier(RatingType));
        }

        public decimal GetPercentageAdjustment(StatType ChanceType)
        {
            return Buffs.Values.Sum(B => B.GetPercentageModifier(ChanceType));
        }

        public T GetBuff<T>() where T : Buff
        {
            return GetBuff(typeof(T).Name) as T;
        }

        public Buff GetBuff(Type BuffType)
        {
            return GetBuff(BuffType.Name);
        }

        public Buff GetBuff(string BuffName)
        {
            if (Buffs.ContainsKey(BuffName))
                return Buffs[BuffName];
            else
                return null;
        }

        public int GetStacks<T>() where T : Buff
        {
            return GetStacks(typeof(T));
        }

        public int GetStacks(Type BuffType)
        {
            var buff = GetBuff(BuffType);
            return buff != null ? buff.Stacks : 0;
        }

        public void ClearBuff<T>() where T : Buff
        {
            ClearBuff(typeof(T).Name);
        }

        public void ClearBuff(Type BuffType)
        {
            ClearBuff(BuffType.Name);            
        }

        public void ClearBuff(string BuffName)
        {
            if (Buffs.ContainsKey(BuffName))
            {
                var cleared = Buffs[BuffName];
                Buffs.Remove(BuffName);
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Faded, cleared);
            }
        }

        public void ClearAllNonPermanent()
        {
            var nonPermanentBuffs = Buffs.Values.Where(b => !b.Permanent).ToList();
            foreach (var buff in nonPermanentBuffs)
                Buffs.Remove(buff.Name);
        }
    }
}