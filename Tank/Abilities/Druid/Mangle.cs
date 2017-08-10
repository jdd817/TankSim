using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Mangle : Ability
    {
        public Mangle()
        {
            Cooldown = 6m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var isBleeding = Caster.Buffs.BuffActive<Buffs.Druid.Thrash>();

            return new AbilityResult
            {
                DamageDealt = (int)(3.87m * (Caster as Player).WeaponDamage * (isBleeding ? 1.2m : 1m)),
                ResourceCost = -5
            };
        }
    }
}
