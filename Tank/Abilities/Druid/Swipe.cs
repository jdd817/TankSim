using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Swipe : Ability
    {
        private IRng _rng;

        public Swipe(IRng rng)
        {
            _rng = rng;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var buffs = new List<Buffs.Buff>() { };

            if (_rng.NextDouble() <= (Caster as Classes.Druid).GoreChance)
                buffs.Add(new Buffs.Druid.Gore());

            return new AbilityResult
            {
                DamageDealt = (int)(1.46m * (Caster as Player).WeaponDamage),
                CasterBuffsApplied = buffs
            };
        }
    }
}
