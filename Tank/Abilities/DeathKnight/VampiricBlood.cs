using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class VampiricBlood : Ability
    {
        public override AbilityType AbilityType { get { return AbilityType.Spell; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new[] { new Buffs.DeathKnight.VampiricBlood() }
            };
        }
    }
}
