using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class GoryFur : PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public GoryFur(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Mangle))
            {
                if (_rng.NextDouble() < 0.15)
                    Result.CasterBuffsApplied.Add(new GoryFur_Buff());
            }
        }
    }

    public class GoryFur_Buff:Buff,IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 60m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Buffs.Druid.Ironfur))
            {
                Result.ResourceCost -= (int)(Result.ResourceCost * .025m);
                TimeRemaining = 0;
            }
        }
    }
}
