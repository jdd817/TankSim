using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid
{
    public class Gore : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration
        { get { return 30m; } }

        public override int MaxStacks
        { get { return 1; } }

        public override void Applied()
        {
            base.Applied();

            (Target as Player).Cooldowns.ReduceTimers(new Abilities.CooldownReduction
            {
                Ability = typeof(Abilities.Druid.Mangle),
                ReductionType = Abilities.ReductionType.To,
                Amount = 0
            });
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Mangle))
            {
                Result.ResourceCost -= 4;
                TimeRemaining = 0;
            }
        }
    }
}
