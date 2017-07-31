using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior.Artifact
{
    public class RumblingVoice : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.DemoralizingShout))
            {
                var demoShout = Result.TargetBuffsApplied.OfType<DemoralizingShout>().First();
                demoShout.TimeRemaining *= 1.5m;
            }
        }
    }
}
