using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 2, 5)]
    public class NeverSurrender : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.IgnorePain))
            {
                var ignorePain = Result.CasterBuffsApplied.OfType<Buffs.Warrior.IgnorePain>().FirstOrDefault();
                ignorePain.DamageRemaining = (int)(ignorePain.DamageRemaining * (2m - tank.HealthPercentage));
            }
        }
    }
}
