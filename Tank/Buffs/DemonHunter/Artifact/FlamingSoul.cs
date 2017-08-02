using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class FlamingSoul : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DemonHunter.SoulCarver) || Ability.GetType()==typeof(Abilities.DemonHunter.ImmolationAura))
            {
                var fieryBrand = tank.Buffs.GetBuff<FieryBrand>();
                if (fieryBrand != null)
                    fieryBrand.TimeRemaining += 0.5m;
            }
        }
    }
}
