using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class DampenHarm : Ability
    {
        public DampenHarm()
        {
            Cooldown = 120m;
        }

        public override bool OnGCD { get { return false; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Monk.DampenHarm() }
            };
        }
    }
}
