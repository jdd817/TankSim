using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Swipe : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var MangleReset = new List<CooldownReduction>(); ;
            var buffs = new List<Buffs.Buff>() { };

            if (RNG.NextDouble() <= 0.15)
            {
                MangleReset.Add(new CooldownReduction { Ability = typeof(Druid.Mangle), Amount = 0, ReductionType = ReductionType.To });
                buffs.Add(new Buffs.Druid.Gore());
            }

            return new AbilityResult
            {
                CasterBuffsApplied = buffs.ToArray(),
                CooldownReduction = MangleReset.ToArray()
            };
        }
    }
}
