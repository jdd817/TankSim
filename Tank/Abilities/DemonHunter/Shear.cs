﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class Shear : Ability
    {
        private static int ShearsSinceLastSoulFragment = 0;

        public Shear()
        {
            ResourceCost = -10;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            Buff[] buffs;

            /*blue post:
            Currently, that chance is 
            (4%, 12%, 25%, 40%, 60%, 80%, 90%, 100%, based on how many Shears since you got a soul) 
            + (10% if Frail) , but that may change in tuning.
            */

            var shearChance = GetShearChance();

            if (RNG.NextDouble() <= shearChance)
            {
                buffs = new[] { new SoulFragment() };
                ShearsSinceLastSoulFragment = 0;
            }
            else
            {
                buffs = new Buff[0];
                ShearsSinceLastSoulFragment++;
            }

            return new AbilityResult
            {
                ResourceCost = -10,
                DamageDealt = 0,
                CasterBuffsApplied = buffs
            };
        }

        private double GetShearChance()
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
