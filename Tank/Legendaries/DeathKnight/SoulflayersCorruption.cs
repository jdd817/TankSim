using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class SoulflayersCorruption : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.BloodBoil))
            {
                var bp = Result.TargetBuffsApplied.OfType<Buffs.DeathKnight.BloodPlague>().First();
                bp.LeechPerTick *= 2;
            }
        }
    }
}
