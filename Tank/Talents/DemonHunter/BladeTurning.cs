using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Talents.DemonHunter
{
    [Talent(typeof(Classes.DemonHunter), 6,2)]
    public class BladeTurning : PermanentBuff, IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.Result==AttackResult.Parry)
            {
                tank.Buffs.AddBuff(new BladeTurning_Buff());
            }
        }
    }

    public class BladeTurning_Buff : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 5m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Result.ResourceCost < 0)
                Result.ResourceCost = (int)(Result.ResourceCost * 1.20m);
        }
    }
}
