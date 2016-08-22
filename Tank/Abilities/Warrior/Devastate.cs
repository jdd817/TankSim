using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    class Devastate : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CooldownReduction = GetCooldownRedutions().ToArray()
            };
        }

        public IEnumerable<CooldownReduction> GetCooldownRedutions()
        {
            if (RNG.NextDouble() <= 0.30)
                yield return new CooldownReduction { Ability = typeof(Warrior.ShieldBlock), Amount = 0, ReductionType = ReductionType.To };
        }
    }
}
