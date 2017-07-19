using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class TigerPalm : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 50,
                CooldownReduction = new List<CooldownReduction>() { new CooldownReduction { Ability = typeof(IronskinBrew), Amount = 1m, ReductionType = ReductionType.By } }
            };
        }
    }
}
