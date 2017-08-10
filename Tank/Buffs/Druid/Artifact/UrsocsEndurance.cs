using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class UrsocsEndurance:PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Barkskin) || Ability.GetType()==typeof(Abilities.Druid.Ironfur))
            {
                var buff = Result.CasterBuffsApplied.Where(b => b.GetType() == typeof(Buffs.Druid.Barkskin) || b.GetType() == typeof(Buffs.Druid.Ironfur)).First();
                buff.TimeRemaining += 0.5m * Stacks;
            }
        }
    }
}
