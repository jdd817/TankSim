using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 2, 7)]
    public class HeavyRepercussions : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.ShieldSlam))
            {
                var shieldBlock = tank.Buffs.GetBuff<Buffs.Warrior.ShieldBlock>();
                if (shieldBlock == null)
                    return;

                shieldBlock.TimeRemaining += 1;

                Result.DamageDealt = (int)(Result.DamageDealt * 1.30m);
            }
        }
    }
}
