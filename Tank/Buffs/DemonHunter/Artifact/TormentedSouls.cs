using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class TormentedSouls : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.SoulCleave))
                Result.DamageDealt = (int)(Result.DamageDealt * 1.1m);
        }
    }
}
