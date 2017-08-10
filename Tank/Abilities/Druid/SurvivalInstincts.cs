using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class SurvivalInstincts : Ability
    {
        public SurvivalInstincts()
        {
            MaxCharges = 2;
            Cooldown = 180m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.Druid.SurvivalInstincts() }
            };
        }
    }
}
