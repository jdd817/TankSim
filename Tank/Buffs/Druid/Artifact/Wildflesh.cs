using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class Wildflesh:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.FrenziedRegeneration))
            {
                var regen = Result.CasterBuffsApplied.OfType<Buffs.Druid.FrenziedRegeneration>().First();
                regen.HealingPerTick += (int)(regen.HealingPerTick * 0.05m * Stacks);
            }
        }
    }
}
