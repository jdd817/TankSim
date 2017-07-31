using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Warrior
{
    public class DemoralizingShout : Ability
    {
        public DemoralizingShout()
        {
            Cooldown = 90m;
        }

        public override bool OnGCD
        { get { return false; } }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                TargetBuffsApplied = new List<Buffs.Buff> { new Buffs.Warrior.DemoralizingShout() }
            };
        }
    }
}
