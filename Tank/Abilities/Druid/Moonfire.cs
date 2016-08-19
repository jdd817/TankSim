using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Moonfire : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = Caster.Buffs.GetBuff<Buffs.Druid.Moonfire>() == null ? 0 : -15,
                TargetBuffsApplied = new[] { new Buffs.Druid.Moonfire() }
            };
        }
    }
}
