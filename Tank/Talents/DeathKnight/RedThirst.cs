using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 4, 2)]
    public class RedThirst : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.ResourceCost>0)
            {
                tank.Cooldowns.ReduceTimers(new CooldownReduction()
                {
                    Ability = typeof(Abilities.DeathKnight.VampiricBlood),
                    Amount = Result.ResourceCost / 6m,
                    ReductionType = ReductionType.By
                });
            }
        }
    }
}
