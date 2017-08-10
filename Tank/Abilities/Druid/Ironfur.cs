using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Druid
{
    public class Ironfur : Ability
    {
        public Ironfur()
        {
            Cooldown = 0.5m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 45,
                CasterBuffsApplied = new List<Buff> { new Buffs.Druid.Ironfur() }
            };
        }

        public override bool OnGCD { get { return false; } }
    }
}
