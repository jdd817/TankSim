using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class Stagger : Buff
    {
        public Stagger(int damageDelayed)
        {
            DamageDelayed = damageDelayed;
            Tick = 0.5m;
        }

        public override decimal Durration
        {
            get
            {
                return 10.0m;
            }
        }

        public override int MaxStacks
        {
            get
            {
                return 1;
            }
        }

        public int DamageDelayed { get; set; }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            DamageDelayed += (NewBuff as Stagger).DamageDelayed;
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageDelayed,
                    TimeRemaining);
        }
    }
}
