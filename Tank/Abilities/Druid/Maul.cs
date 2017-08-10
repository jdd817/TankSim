using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Druid
{
    public class Maul : Ability
    {
        private IRng _rng;

        public Maul (IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var buffs = new List<Buffs.Buff>() { new Buffs.Druid.GuardianOfElune() };

            if (_rng.NextDouble() <= 0.15)
                buffs.Add(new Buffs.Druid.Gore());

            return new AbilityResult
            {
                DamageDealt = (int)(7.5m * (Caster as Player).WeaponDamage),
                ResourceCost = 45,
                CasterBuffsApplied = buffs
            };
        }
    }
}
