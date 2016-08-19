using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class HealingElixer : Ability
    {
        public override AbilityType AbilityType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                SelfHealing = (int)(Caster.MaxHealth * 0.15m)
            };
        }
    }
}
