using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Moonfire : Ability
    {
        private IRng _rng;

        public Moonfire(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var MangleReset = new List<CooldownReduction>(); ;
            var buffs = new List<Buffs.Buff>() { };

            if (_rng.NextDouble() <= 0.15)
            {
                MangleReset.Add(new CooldownReduction { Ability = typeof(Druid.Mangle), Amount = 0, ReductionType = ReductionType.To });
                buffs.Add(new Buffs.Druid.Gore());
            }

            var rageCost = Caster.Buffs.GetBuff<Buffs.Druid.Moonfire>() == null ? 0 : -15;
            Caster.Buffs.ClearBuff<Buffs.Druid.GalacticGuardian>();

            return new AbilityResult
            {
                ResourceCost = rageCost,
                TargetBuffsApplied = new[] { new Buffs.Druid.Moonfire() },
                CasterBuffsApplied = buffs.ToArray(),
                CooldownReduction = MangleReset.ToArray()
            };
        }
    }
}
