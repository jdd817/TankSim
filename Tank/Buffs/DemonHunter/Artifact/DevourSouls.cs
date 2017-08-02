using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class DevourSouls : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.SoulCleave))
                Result.SelfHealing = (int)(Result.SelfHealing * 0.03m * Stacks);
        }
    }
}
