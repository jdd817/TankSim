using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DeathKnight
{
    public class Consumption:Ability
    {
        private IRng _rng;

        public Consumption(IRng rng)
        {
            _rng = rng;
        }

        public Consumption()
        {
            Cooldown = 45m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = 0,
                SelfHealing = (int)(Caster.Weapons[0].Damage(_rng)*2.5m)
            };
        }
    }
}
