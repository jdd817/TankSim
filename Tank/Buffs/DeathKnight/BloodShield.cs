using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight
{
    public class BloodShield:Buff
    {
        public BloodShield(int DamageAbsorbed)
        {
            TimeRemaining = 60.0m;
            _damageRemaining = DamageAbsorbed;
        }

        public override decimal Durration { get { return 60.0m; } }

        public override int MaxStacks
        {
            get { return 1; }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.AttackPower)
                return 0.08m;            
            return 0;
        }

        private int _damageRemaining;

        public int DamageRemaining
        {
            get { return _damageRemaining; }
            set
            {
                _damageRemaining = value;
                if (_damageRemaining <= 0)
                    TimeRemaining = 0;
            }
        }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            DamageRemaining += (NewBuff as BloodShield).DamageRemaining;
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageRemaining,
                    TimeRemaining);
        }
    }
}
