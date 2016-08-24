using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Buffs;

namespace Tank.Abilities.DeathKnight
{
    public class DeathAndDecay : Ability
    {
        public DeathAndDecay()
        {
            Cooldown = 30m;
        }
        
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var runeCost = Caster.Buffs.GetBuff<Buffs.DeathKnight.CrimsonScourge>() != null ? 0 : 1;
            Caster.Buffs.ClearBuff<Buffs.DeathKnight.CrimsonScourge>();

            return new AbilityResult
            {
                CasterBuffsApplied = new[] { new Buffs.DeathKnight.DeathAndDecay() },
                SecondaryResourceCost = runeCost,
                ResourceCost = -10  //need to test this - does it still generate 10 power when free?
            };
        }
    }
}
