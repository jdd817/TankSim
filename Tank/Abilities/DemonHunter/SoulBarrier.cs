using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SoulBarrier : Ability
    {
        public SoulBarrier()
        {
            Cooldown = 30m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;
            
            var soulFragments = Caster.Buffs.GetBuff<LesserSoulFragment>();
            var healingDone = soulFragments != null ? soulFragments.GetTotalHealing() : 0;
            Caster.Buffs.ClearBuff(typeof(LesserSoulFragment));

            return new AbilityResult
            {
                ResourceCost = 10,
                DamageDealt = 0,
                SelfHealing = healingDone,
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.SoulBarrier(dh, soulFragments != null ? soulFragments.Stacks : 0) }
            };
        }
    }
}
