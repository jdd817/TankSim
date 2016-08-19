using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public decimal TimeRemaining
        { get; set; }

        public abstract decimal Durration
        { get; }

        public int Stacks
        { get; set; }

        public abstract int MaxStacks
        { get; }

        public decimal Tick { get; set; }
        public decimal TickTimer { get; set; }

        /// <summary>
        /// Gets a buffs modification to RATING
        /// </summary>
        /// <param name="RatingType"></param>
        /// <returns></returns>
        public abstract int GetRatingModifier(StatType RatingType);

        /// <summary>
        /// Gets a buffs modification to a chance of something
        /// </summary>
        /// <param name="Stat"></param>
        /// <returns></returns>
        public abstract decimal GetPercentageModifier(StatType Stat);

        public virtual string Name
        { get { return this.GetType().Name; } }

        public virtual void Refresh(Buff NewBuff)
        {
            TimeRemaining = NewBuff.Durration;
            Stacks += NewBuff.Stacks;
            if (Stacks > MaxStacks)
                Stacks = MaxStacks;
        }

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
    }
}
