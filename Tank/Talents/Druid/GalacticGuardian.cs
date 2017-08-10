using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 5, 3)]
    public class GalacticGuardian : PermanentBuff, IPlayerAbilityEffectStack
    {
        IRng _rng;

        public GalacticGuardian(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Result.DamageDealt>0)
            {
                if (_rng.NextDouble() < 0.07)
                {
                    Result.CasterBuffsApplied.Add(new GalacticGuardian_Buff());
                    Result.TargetBuffsApplied.Add(new Buffs.Druid.Moonfire());
                }
            }
        }
    }

    public class GalacticGuardian_Buff : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 60.0m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Moonfire))
            {
                Result.ResourceCost = -8;
                Result.DamageDealt *= 3;
                TimeRemaining = 0;
            }
        }
    }
}
