using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Tank.Classes.DeathKnight))]
    public class ServiceOfGorefiend : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.HeartStrike))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.DeathKnight.VampiricBlood),
                    Amount = 2.0m,
                    ReductionType = ReductionType.By
                });
            }
        }
    }
}
