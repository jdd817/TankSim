using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    public class Revenge:Ability
    {
        private IRng _rng;

        public Revenge(IRng rng)
        {
            _rng = rng;
        }

        public Revenge()
        {
            Cooldown = 3m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 30,
                CooldownReduction = GetCooldownRedutions().ToList()
            };
        }

        private IEnumerable<CooldownReduction> GetCooldownRedutions()
        {
            if (_rng.NextDouble() <= 0.30)
                yield return new CooldownReduction { Ability = typeof(Warrior.ShieldBlock), Amount = 0, ReductionType = ReductionType.To };
        }
    }
}
