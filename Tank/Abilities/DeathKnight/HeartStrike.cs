using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class HeartStrike : Ability
    {
        private IRng _rng;

        public HeartStrike(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                SecondaryResourceCost = 1,
                ResourceCost = -15,
                DamageDealt = (int)((Caster as Player).WeaponDamage * 2.58m)
            };
        }
    }
}
