using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class Fracture : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 30,
                DamageDealt = (int)(13.48m * (Caster as Player).WeaponDamage),
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.LesserSoulFragment { Stacks = 2 } }
            };
        }
    }
}
