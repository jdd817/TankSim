using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Netherlight.Light
{
    [Effect]
    public class SecureInTheLight : RPPMBuff, IPlayerAbilityEffectStack
    {
        public SecureInTheLight(IRng rng) : base(rng)
        {
            ProcsPerMinute = 3;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if ((Result.DamageDealt > 0 || Result.SelfHealing > 0 || Result.TartgetHealing > 0) && DidProc(CurrentTime, tank))
            {
                if (Result.DamageDealt > 0)
                    Result.DamageDealt += 135000;
                if (Result.SelfHealing > 0 || Result.TartgetHealing > 0)
                    tank.Buffs.AddBuff(new SecureInTheLight_Buff());
            }
        }
    }

    public class SecureInTheLight_Buff : AbsorbShield
    {
        public SecureInTheLight_Buff() : base(135000)
        {
        }

        public override decimal Durration { get { return 10m; } }
    }
}
