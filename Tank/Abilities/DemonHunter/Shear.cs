using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class Shear : Ability
    {
        private IRng _rng;

        public Shear(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            Buff[] buffs;

            /*blue post:
            Currently, that chance is 
            (4%, 12%, 25%, 40%, 60%, 80%, 90%, 100%, based on how many Shears since you got a soul) 
            + (10% if Frail) , but that may change in tuning.
            */

            var dh = Caster as Classes.DemonHunter;

            var shearChance = GetShearChance(dh.ShearsSinceLastSoulFragment);

            CooldownReduction[] cdReduction;

            if (_rng.NextDouble() <= 0.10)
                cdReduction = new[] { new CooldownReduction { Ability = typeof(Abilities.DemonHunter.FelBlade), ReductionType = ReductionType.To, Amount = 0 } };
            else
                cdReduction = new CooldownReduction[0];

            if (_rng.NextDouble() <= shearChance)
            {
                buffs = new[] { new SoulFragment() };
                dh.ShearsSinceLastSoulFragment = 0;
            }
            else
            {
                buffs = new Buff[0];
                dh.ShearsSinceLastSoulFragment++;
            }

            return new AbilityResult
            {
                ResourceCost = -10,
                DamageDealt = 0,
                CasterBuffsApplied = buffs.ToList(),
                CooldownReduction = cdReduction.ToList()
            };
        }

        private double GetShearChance(int ShearsSinceLastSoulFragment)
        {
            switch (ShearsSinceLastSoulFragment)
            {
                case 0: return 0.04;
                case 1: return 0.12;
                case 2: return 0.25;
                case 3: return 0.40;
                case 4: return 0.60;
                case 5: return 0.80;
                case 6: return 0.90;
                default: return 1.00;
            }
        }
    }
}
