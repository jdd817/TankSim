using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Thrash : Ability
    {
        private IRng _rng;

        public Thrash(IRng rng)
        {
            _rng = rng;
            Cooldown = 6m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var buffs = new List<Buffs.Buff>() { new Buffs.Druid.Thrash((int)(0.605m * (Caster as Player).AttackPower)) };

            if (_rng.NextDouble() <= (Caster as Classes.Druid).GoreChance)
                buffs.Add(new Buffs.Druid.Gore());

            return new AbilityResult
            {
                DamageDealt = (int)(0.553m * (Caster as Player).AttackPower),
                ResourceCost = -4,
                CasterBuffsApplied = buffs
            };
        }
    }
}
