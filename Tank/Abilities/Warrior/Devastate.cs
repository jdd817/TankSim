using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities.Warrior
{
    class Devastate : Ability
    {
        public override AbilityType AbilityType { get { return AbilityType.Melee; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = GetCasterBuffs().ToArray()
            };
        }

        public IEnumerable<Buffs.Buff> GetCasterBuffs()
        {
            if (RNG.NextDouble() <= 0.30)
                yield return new Buffs.Warrior.SwordAndBoard();
        }
    }
}
