using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class LunarBeam : Ability
    {
        public LunarBeam()
        {
            Cooldown = 80m;
        }

        public override bool OnGCD
        {
            get
            {
                return false;
            }
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = (int)(7.2m * (Caster as Player).AttackPower),
                DamageType = DamageType.Arcane,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Druid.LunarBeam(3 * (Caster as Player).AttackPower) }
            };
        }
    }
}
