using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class FelDevestation : Ability
    {
        public FelDevestation()
        {
            Cooldown = 60m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 30,
                SelfHealing = (Caster as Classes.DemonHunter).AttackPower * 25
            };
        }
    }
}
