using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight
{
    public class BoneShield:Buff
    {
        //need to fix this shit
        private IRng _rng;
        private bool _hasSkeletalShattering;
        private decimal _critChance;

        public BoneShield(bool hasSkeletalShattering, decimal critChance, IRng rng)
        {
            TimeRemaining = Durration;
            rng = _rng;
            _hasSkeletalShattering = hasSkeletalShattering;
            _critChance = critChance;
        }

        public override decimal Durration { get { return 30.0m; } }

        public override int MaxStacks
        {
            get { return 10; }
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Haste)
                return 0.10m;
            if (Stat == StatType.Stamina)
                return 0.02m * Stacks;
            if (Stat == StatType.DamageReduction)
            {
                if (_hasSkeletalShattering && _rng.NextDouble() <= (double)_critChance)
                    return 0.24m;
                else
                    return 0.16m;
                
            }
            return 0;
        }
    }
}
