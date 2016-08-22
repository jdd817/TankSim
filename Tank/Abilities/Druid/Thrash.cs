using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Thrash : Ability
    {
        public Thrash()
        {
            Cooldown = 6m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var MangleReset = new List<CooldownReduction>(); ;
            var buffs = new List<Buffs.Buff>() { new Buffs.Druid.RendAndTear() };

            if (RNG.NextDouble() <= 0.15)
            {
                MangleReset.Add(new CooldownReduction { Ability = typeof(Druid.Mangle), Amount = 0, ReductionType = ReductionType.To });
                buffs.Add(new Buffs.Druid.Gore());
            }

            return new AbilityResult
            {
                ResourceCost = -4,
                CasterBuffsApplied = buffs.ToArray(),
                CooldownReduction = MangleReset.ToArray()
            };
        }
    }
}
