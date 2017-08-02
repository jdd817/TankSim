using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs.DemonHunter;

namespace Tank.Abilities.DemonHunter
{
    public class SpiritBomb : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var dh = Caster as Player;

            var healingDone = 0;
            var damageDone = 0;

            var soulFragments = Caster.Buffs.GetBuff<LesserSoulFragment>();
            healingDone += soulFragments.GetTotalHealing();
            damageDone = 2 * dh.AttackPower * soulFragments.Stacks;
            Caster.Buffs.ClearBuff(typeof(LesserSoulFragment));

            return new AbilityResult
            {
                ResourceCost = 0,
                DamageDealt = damageDone,
                SelfHealing = healingDone,
            };
        }
    }
}
