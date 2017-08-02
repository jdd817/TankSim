using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class DemonicInfusion : Ability
    {
        public DemonicInfusion()
        {
            Cooldown = 90m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = -60,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.DemonSpikes(Caster as Player) },
                CooldownReduction = new List<CooldownReduction> {
                    new CooldownReduction
                    {
                        Ability =typeof(DemonSpikes),
                        ReductionType=ReductionType.Recharge
                    }
                }
            };
        }
    }
}
