using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class RageOfTheSleeper : Ability
    {
        public RageOfTheSleeper()
        {
            Cooldown = 90m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Druid.RageOfTheSleeper() }
            };
        }
    }
}
