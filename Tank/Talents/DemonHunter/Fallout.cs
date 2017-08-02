using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.DemonHunter
{
    [Talent(typeof(Classes.DemonHunter), 2, 2)]
    public class Fallout : PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public Fallout(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DemonHunter.ImmolationAura))
            {
                if (_rng.NextDouble() < 0.70)
                    Result.CasterBuffsApplied.Add(new Buffs.DemonHunter.LesserSoulFragment());
            }
        }
    }
}
