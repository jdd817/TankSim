using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.Warrior
{
    [Effect(Class = typeof(Classes.Warrior))]
    public class KakushansStormscaleGauntlets : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Attack) && (Result.AttackResult == AttackResult.Hit || Result.AttackResult == AttackResult.Crit))
            {
                tank.Buffs.AddBuff(new KakushansStormscaleGauntlets_Buff());
            }
        }
    }

    public class KakushansStormscaleGauntlets_Buff : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 10m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Warrior.ShieldSlam) || Ability.GetType() == typeof(Abilities.Warrior.ThunderClap))
            {
                Result.ResourceCost = (int)(Result.ResourceCost * 1.2m);
                TimeRemaining = 0;
            }
        }
    }
}
