using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class DragonfireBrew : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.BreathOfFire))
            {
                Result.DamageDealt += (int)(Result.DamageDealt * 1.26m);
                Result.CasterBuffsApplied.OfType<Buffs.Monk.BreathOfFire>().First().TimeRemaining *= 2;
            }
        }
    }
}
