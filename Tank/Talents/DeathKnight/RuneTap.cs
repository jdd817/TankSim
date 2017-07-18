using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 6, 2)]
    public class RuneTap : Abilities.Ability
    {
        public RuneTap()
        {
            MaxCharges = 2;
            Cooldown = 25m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                SecondaryResourceCost = 1,
                CasterBuffsApplied = new[] { new RuneTap_Buff() }
            };
        }
    }

    public class RuneTap_Buff : Buffs.Buff
    {
        public override decimal Durration { get { return 3; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.DamageReduction)
                return 0.40m;
            return 0;
        }
    }
}
