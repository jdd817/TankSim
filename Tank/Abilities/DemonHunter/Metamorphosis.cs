using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class Metamorphosis : Ability
    {
        public Metamorphosis()
        {
            Cooldown = 180m;
        }

        public override bool OnGCD
        {
            get { return false; }
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.Metamorphosis() }
            };
        }
    }
}
