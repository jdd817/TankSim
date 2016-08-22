using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class PurifyingBrew : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
            };
        }

        public override Type CooldownType
        {
            get
            {
                return typeof(IronskinBrew);
            }
        }

        public override bool OnGCD { get { return false; } }
    }
}
