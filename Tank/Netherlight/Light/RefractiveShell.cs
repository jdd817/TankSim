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
    public class RefractiveShell : RPPMBuff, IPlayerAbilityEffectStack
    {
        public RefractiveShell(IRng rng) : base(rng)
        {
            ProcsPerMinute = 2;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(DidProc(CurrentTime,tank))
            {
                tank.Buffs.AddBuff(new RefractiveShell_Buff());
            }
        }
    }

    public class RefractiveShell_Buff:AbsorbShield
    {
        public RefractiveShell_Buff() : base(300000)
        {
        }

        public override decimal Durration { get { return 10m; } }
    }
}
