using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class VampiricFangs:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Tank.Abilities.DeathKnight.VampiricBlood))
            {
                var buff = Result.TargetBuffsApplied.OfType<Tank.Buffs.DeathKnight.VampiricBlood>().FirstOrDefault();
                if(buff!=null)
                {
                    buff.PercentageGain += 0.10m * Stacks;
                }
            }
        }
    }
}
