using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class BreathOfFire : Ability
    {
        public BreathOfFire()
        {
            Cooldown = 15m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = (int)(1.8m * (Caster as Player).AttackPower),
                DamageType = DamageType.Fire
            };
        }
    }
}
