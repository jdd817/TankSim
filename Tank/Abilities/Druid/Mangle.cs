using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Druid
{
    public class Mangle : Ability
    {
        public Mangle()
        {
            Cooldown = 6m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var rageGen = -6;
            if(Caster.Buffs.GetBuff<Buffs.Druid.Gore>()!=null)
            {
                rageGen = -10;
                Caster.Buffs.ClearBuff<Buffs.Druid.Gore>();
            }
            return new AbilityResult
            {
                ResourceCost = rageGen
            };
        }
    }
}
