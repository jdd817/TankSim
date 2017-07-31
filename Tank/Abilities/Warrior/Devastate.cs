using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class Devastate : Ability
    {
        private IRng _rng;

        public Devastate(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CooldownReduction = GetCooldownRedutions().ToList()
            };
        }

        private IEnumerable<CooldownReduction> GetCooldownRedutions()
        {
            if (_rng.NextDouble() <= 0.30)
                yield return new CooldownReduction { Ability = typeof(Warrior.ShieldSlam), Amount = 0, ReductionType = ReductionType.To };
        }
    }
}
