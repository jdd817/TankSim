using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class KegSmash : Ability
    {
        public KegSmash()
        {
            Cooldown = 8m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult()
            {
                DamageDealt = (int)(6.70m * (Caster as Player).AttackPower),
                ResourceCost = 40,
                CooldownReduction = new List<CooldownReduction>() { new CooldownReduction { Ability = typeof(IronskinBrew), Amount = 4m, ReductionType = ReductionType.By } }
            };
        }
    }
}
