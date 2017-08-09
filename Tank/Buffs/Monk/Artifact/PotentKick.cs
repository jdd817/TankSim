using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class PotentKick : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Monk.IronskinBrew))
            {
                var buff = Result.CasterBuffsApplied.OfType<Buffs.Monk.IronskinBrew>().First();
                buff.TimeRemaining += 0.5m * Stacks;
            }
        }
    }
}
