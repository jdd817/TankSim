using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class SharpenedInstincts : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.SurvivalInstincts))
            {
                var buff = Result.CasterBuffsApplied.OfType<Buffs.Druid.SurvivalInstincts>().First();
                buff.DamageReduction += 0.0333m * Stacks;
            }
        }
    }
}
