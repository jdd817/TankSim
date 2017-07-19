using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 1, 4)]
    public class WarlordsChallenge : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Warrior.BerserkerRage))
            {
                Result.CooldownReduction.Add(
                    new CooldownReduction
                    {
                        Ability =typeof(Abilities.Warrior.BerserkerRage),
                        Amount=15,
                        ReductionType=ReductionType.By
                    });
            }
        }
    }
}
