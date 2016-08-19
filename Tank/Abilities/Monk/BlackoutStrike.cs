using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class BlackoutStrike : Ability
    {
        public override AbilityType AbilityType
        {
            get
            {
                return AbilityType.Melee;
            }
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult();
        }
    }
}
