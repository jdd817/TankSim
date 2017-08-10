using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

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
            var buffs = new List<Buffs.Buff>() { };

            if (_rng.NextDouble() <= (Caster as Classes.Druid).GoreChance)
                buffs.Add(new Buffs.Druid.Gore());
            
            return new AbilityResult
            {
                DamageDealt=(int)(1.144m*(Caster as Player).AttackPower),
                DamageType=DamageType.Arcane,
                ResourceCost = 0,
                TargetBuffsApplied = new List<Buff> { new Buffs.Druid.Moonfire() },
                CasterBuffsApplied = buffs
            };
        }
    }
}
