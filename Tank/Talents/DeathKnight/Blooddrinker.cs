using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 1, 3)]
    public class Blooddrinker : Abilities.Ability
    {
        public Blooddrinker()
        {
            Cooldown = 30m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = -10,
                SecondaryResourceCost = 1,
                ChannelLength = 3,
                TargetBuffsApplied = new[] { new Blooddrinker_Buff((int)(13.5m * (Caster as Player).AttackPower / 3m)) }
            };
        }
    }

    public class Blooddrinker_Buff : Buffs.HealOverTime
    {
        public Blooddrinker_Buff(int healingAmount)
        {
            HealingPerTick = healingAmount;
            Tick = 1;
        }

        public override decimal Durration
        { get { return 3; } }
    }
}
