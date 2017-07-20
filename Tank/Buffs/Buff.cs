using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs
{
    public abstract class Buff
    {
        public Buff()
        {
            Stacks = 1;
            TimeRemaining = Durration;
            Tick = 0;
        }

        public Actor Target { get; set; }

        public decimal TimeRemaining
        { get; set; }

        public abstract decimal Durration
        { get; }

        public int Stacks
        { get; set; }

        public virtual int MaxStacks
        { get { return 1; } }

        public decimal Tick { get; set; }
        public decimal TickTimer { get; set; }

        /// <summary>
        /// Gets a buffs modification to RATING
        /// </summary>
        /// <param name="RatingType"></param>
        /// <returns></returns>
        public virtual int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        /// <summary>
        /// Gets a buffs modification to a chance of something
        /// </summary>
        /// <param name="Stat"></param>
        /// <returns></returns>
        public virtual decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public virtual string Name
        { get { return this.GetType().Name; } }

        public virtual void Refresh(Buff NewBuff)
        {
            TimeRemaining = NewBuff.Durration;
            Stacks += NewBuff.Stacks;
            if (Stacks > MaxStacks)
                Stacks = MaxStacks;
        }

        public virtual void TimerUpdated(decimal delta) { }

        public virtual void Ticked() { }

        public virtual bool Permanent { get { return false; } }

        public override string ToString()
        {
            if (MaxStacks > 1)
                return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    Stacks,
                    TimeRemaining);
            else
                return String.Format("{0} ({1:0.00})",
                    Name,
                    TimeRemaining);
        }
        
    }

    public abstract class PermanentBuff : Buff
    {
        public PermanentBuff()
        {
            TimeRemaining = 60;
        }

        public override bool Permanent { get { return true; } }
        public override decimal Durration
        {
            get
            {
                return 60;
            }
        }
    }

    public abstract class HealOverTime:Buff
    {
        public int HealingPerTick { get; set; }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    HealingPerTick,
                    TimeRemaining);
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }
    }

    public interface IEffectStack
    { }

    public interface IPlayerAbilityEffectStack:IEffectStack
    {
        void ProcessAbilityUsed(decimal CurrentTime, Abilities.Ability Ability, AbilityResult Result, Player tank, Mob mob);
    }

    public interface IHealingReceivedEffectStack:IEffectStack
    {
        void HealingReceived(DataLogging.HealingEvent healingEvent, Player tank, Ability ability);
    }

    public interface IDamageTakenEffectStack:IEffectStack
    {
        void DamageTaken(decimal currentTime, DataLogging.DamageEvent damageEvent, Player tank);
    }

    public interface IReplacingEffectStack:IEffectStack
    {
        Type ReplacedType { get; }
        IEffectStack ReplacedEffect { get; set; }
    }

    public interface IActionUponExpirationEffectStack:IEffectStack  //needs thought
    {
        AbilityResult GetAction();
    }
}
