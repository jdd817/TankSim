using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class QuickSip : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.IronskinBrew))
            {
                var stagger = tank.Buffs.GetBuff<Buffs.Monk.Stagger>();
                if (stagger != null)
                {
                    stagger.DamageDelayed = (int)(stagger.DamageDelayed * (0.95m));
                }
            }
            if (Ability.GetType() == typeof(Abilities.Monk.PurifyingBrew))
            {
                tank.Buffs.AddBuff(new Buffs.Monk.IronskinBrew() { TimeRemaining = 1.0m });
            }
        }
    }
}
