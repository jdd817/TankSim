using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    [EffectPriority(5)]
    public class StaveOff : PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public StaveOff(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Monk.KegSmash))
            {
                Result.DamageDealt *= 2;
                Result.CooldownReduction = Result.CooldownReduction.Concat(Result.CooldownReduction).ToList();
                Result.CasterBuffsApplied = Result.CasterBuffsApplied.Concat(Result.CasterBuffsApplied).ToList();
            }
        }
    }
}
