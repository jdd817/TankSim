using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.DataLogging;

namespace Tank.Buffs.DeathKnight
{
    public class BoneShield:Buff, IDamageTakenEffectStack
    {
        //need to fix this shit
        private IRng _rng;
        private bool _hasSkeletalShattering;
        private decimal _critChance;

        public BoneShield(bool hasSkeletalShattering, decimal critChance, IRng rng)
        {
            TimeRemaining = Durration;
            _rng = rng;
            _hasSkeletalShattering = hasSkeletalShattering;
            _critChance = critChance;
            BaseDamageReduction = 0.16m;
        }

        public override decimal Durration { get { return 30.0m; } }

        public override int MaxStacks
        {
            get { return 10; }
        }

        public decimal BaseDamageReduction { get; set; }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Haste)
                return 0.10m;
            //if (Stat == StatType.MaxHealth)  //foul bulwark
            //    return 0.02m * Stacks;
            /*if (Stat == StatType.DamageReduction)
            {
                if (_hasSkeletalShattering && _rng.NextDouble() <= (double)_critChance)
                    return 0.24m;
                else
                    return 0.16m;
                
            }*/
            return 0;
        }

        decimal lastChargeUsed = -100;

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (Stacks <= 0)
                return;
            var damageReduction = GetDamageReduction(tank);
            damageEvent.DamageTaken = (int)(damageEvent.DamageTaken * (1m - damageReduction));

            if(currentTime>=lastChargeUsed+2m) //ICD on boneshield charges
            {
                Stacks--;
                lastChargeUsed = currentTime;
            }
        }

        internal decimal GetDamageReduction(Player tank)
        {
            if (_hasSkeletalShattering && _rng.NextDouble() <= (double)tank.CritChance)
                return BaseDamageReduction + 0.08m;
            else
                return BaseDamageReduction;
        }
    }
}
