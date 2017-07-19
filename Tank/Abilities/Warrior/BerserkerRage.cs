using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Warrior
{
    public class BerserkerRage : Ability
    {
        public BerserkerRage()
        {
            Cooldown = 60m;
        }

        public override bool OnGCD
        { get { return false; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult();
        }
    }
}
