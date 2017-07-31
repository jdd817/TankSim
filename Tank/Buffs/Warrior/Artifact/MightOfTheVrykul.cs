using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior.Artifact
{
    public class MightOfTheVrykul : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Warrior.ShieldSlam) || Ability.GetType() == typeof(Abilities.Warrior.ThunderClap))
            {
                if (tank.Buffs.GetBuff<DemoralizingShout>() != null)
                {
                    Result.ResourceCost = (int)(Result.ResourceCost * 1.5m);
                }
            }
        }
    }
}
