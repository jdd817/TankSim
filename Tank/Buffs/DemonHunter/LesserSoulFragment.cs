using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DemonHunter
{
    public class LesserSoulFragment:Buff
    {
        public LesserSoulFragment()
        {
            TimeRemaining = Durration;
            Stacks = 1;
        }

        public override decimal Durration { get { return 20.0m; } }

        public override int MaxStacks
        {
            get { return 5; }
        }

        public override void Refresh(Buff NewBuff)
        {
            if(NewBuff.Stacks + Stacks>MaxStacks)
            {
                var fragmentsConsumed = (NewBuff.Stacks + Stacks) - MaxStacks;
                var amountHealed = fragmentsConsumed * GetHealing();
                (Target as Player).ApplyHealing(fragmentsConsumed * GetHealing());
                DataLogging.DataLogManager.LogHeal(new DataLogging.HealingEvent
                {
                    Name = "LesserSoulFragment",
                    Amount = amountHealed,
                    Time = DataLogging.DataLogManager.CurrentTime
                });
            }
            base.Refresh(NewBuff);
        }

        public int GetHealing()
        {
            return (int)((Target as Player).AttackPower * 2.5m);
        }

        public int GetTotalHealing()
        {
            return GetHealing() * Stacks;
        }
    }
}
