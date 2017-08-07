using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class ExplodingKeg : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = 9 * (Caster as Player).AttackPower,
                DamageType = DamageType.Fire,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Monk.ExplodingKeg() }
            };
        }
    }
}
