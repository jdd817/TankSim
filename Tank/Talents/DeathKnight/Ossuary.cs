using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 3, 1)]
    public class Ossuary : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.DeathStrike))
            {
                var boneShield=tank.Buffs.GetBuff<Buffs.DeathKnight.BoneShield>();
                if(boneShield!=null && boneShield.Stacks>=5)
                {
                    Result.ResourceCost -= 5;
                }
            }
        }
    }
}
