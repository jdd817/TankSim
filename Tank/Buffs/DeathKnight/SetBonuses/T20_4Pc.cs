using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.SetBonuses
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class T20_4Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.DeathStrike))
            {
                if (tank.Buffs.GetBuff<GraveWarden>() != null)
                    Result.ResourceCost -= 5;
            }
        }
    }
}
