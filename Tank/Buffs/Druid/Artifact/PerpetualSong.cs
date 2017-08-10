using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class PerpetualSong : PermanentBuff, IPlayerAbilityEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Barkskin))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Druid.Barkskin),
                    ReductionType = ReductionType.By,
                    Amount = Ability.Cooldown * 0.03m * Stacks
                });
            }
        }
    }
}
