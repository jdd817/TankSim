using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class LingeringOrdeal:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DemonHunter.Metamorphosis))
            {
                var meta = Result.CasterBuffsApplied.OfType<Metamorphosis>().First();
                meta.TimeRemaining += 0.5m * Stacks;
            }
        }
    }
}
