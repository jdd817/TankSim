using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Ironfur : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var buff = new Buffs.Druid.Ironfur();

            if (Caster.Buffs.GetBuff<Buffs.Druid.GuardianOfElune>() != null)
            {
                buff.TimeRemaining += 2m;
                Caster.Buffs.ClearBuff<Buffs.Druid.GuardianOfElune>();
            }

            return new AbilityResult
            {
                ResourceCost = 45,
                CasterBuffsApplied = new[] { buff }
            };
        }
    }
}
