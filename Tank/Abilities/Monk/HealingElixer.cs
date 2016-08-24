using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class HealingElixer : Ability
    {
        public HealingElixer()
        {
            Cooldown = 30m;
            MaxCharges = 2;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                SelfHealing = (int)(Caster.MaxHealth * 0.15m)
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
