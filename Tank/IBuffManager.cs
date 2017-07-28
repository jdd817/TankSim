using System;
using System.Collections.Generic;
using Tank.Buffs;

namespace Tank
{
    public interface IBuffManager
    {
        Actor Target { get; set; }
        void AddBuff(Buff NewBuff);
        void ClearAll();
        void ClearAllNonPermanent();
        void ClearBuff(Type BuffType);
        void ClearBuff(string BuffName);
        void ClearBuff<T>() where T : Buff;
        IEnumerable<Buff> DecrementBuffTimers(decimal DeltaTime);
        Buff GetBuff(Type BuffType);
        Buff GetBuff(string BuffName);
        T GetBuff<T>() where T : Buff;
        decimal GetPercentageAdjustment(StatType ChanceType);
        int GetRatingAdjustment(StatType RatingType);
        int GetStacks(Type BuffType);
        int GetStacks<T>() where T : Buff;
        List<T> GetEffectStack<T>() where T : class, IEffectStack;
    }
}