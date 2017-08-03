using System;
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
        public Actor Target { get; set; }

        public void AddBuff(Buff NewBuff)
        {
            NewBuff.Target = Target;
            foreach (var effect in GetEffectStack<IBuffAppliedEffectStack>())
                effect.BuffApplied(NewBuff);
            if (Buffs.ContainsKey(NewBuff.Name))
            {
                Buffs[NewBuff.Name].Refresh(NewBuff);
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Refreshed, Buffs[NewBuff.Name]);
            }
            else
            {
                Buffs.Add(NewBuff.Name, NewBuff);
                NewBuff.Applied();
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Applied, NewBuff);
            }
        }

        public IEnumerable<Buff> DecrementBuffTimers(decimal DeltaTime)
        {
            List<Buff> ExpiredBuffs = new List<Buff>();
            List<Buff> TickingBuffs = new List<Buff>();

            var buffTickedEffects = this.GetEffectStack<IBuffTickedEffectStack>();

            foreach (Buff B in Buffs.Values.ToList())
            {
                B.TimeRemaining -= DeltaTime;
                B.TimerUpdated(DeltaTime);
                if (!B.Permanent && (B.TimeRemaining <= 0 || B.Stacks <= 0))
                    ExpiredBuffs.Add(B);
                if (B.Tick > 0)
                {
                    B.TickTimer -= DeltaTime;
                    if (B.TickTimer <= 0)
                    {
                        B.TickTimer += B.Tick;
                        TickingBuffs.Add(B);
                        B.Ticked();
                        foreach (var effect in buffTickedEffects)
                            effect.BuffTicked(B);
                    }
                }
            }

            var buffExpiredEffects = this.GetEffectStack<IBuffFadedEffectStack>();

            foreach (Buff Expired in ExpiredBuffs)
            {
                Buffs.Remove(Expired.Name);
                foreach (var effect in buffExpiredEffects)
                    effect.BuffFaded(Expired);
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
                
                foreach (var effect in GetEffectStack<IBuffFadedEffectStack>())
                    effect.BuffFaded(cleared);
    
                DataLogging.DataLogManager.LogBuff(DataLogging.DataLogManager.CurrentTime, DataLogging.BuffAction.Faded, cleared);
            }
        }

        public void ClearAll()
        {
            Buffs.Clear();
        }

        public void ClearAllNonPermanent()
        {
            var nonPermanentBuffs = Buffs.Values.Where(b => !b.Permanent).ToList();
            foreach (var buff in nonPermanentBuffs)
                Buffs.Remove(buff.Name);
            foreach (var buff in Buffs.Values.OfBaseType<Buffs.RPPMBuff>())
                buff.ResetLastProcs();
        }

        public List<T> GetEffectStack<T>() where T : class, IEffectStack
        {
            var effectStack = Buffs.Values.OfBaseType<T>()
                .OrderBy(e => e.GetType().GetCustomAttributes(false).OfType<EffectPriorityAttribute>().Select(p => p.Priority).DefaultIfEmpty(0).First())
                .ToList();

            var replacingStack = effectStack.OfBaseType<IReplacingEffectStack>().ToList();

            foreach(var replacingEffect in replacingStack)
            {
                var replacedEffect = effectStack.FirstOrDefault(b => b.GetType() == replacingEffect.ReplacedType);
                if (replacedEffect != null)
                {
                    replacingEffect.ReplacedEffect = replacedEffect;
                    effectStack.Remove(replacedEffect);
                }
            }

            return effectStack;
        }
    }
}
