using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Warrior.Artifact
{
    public class DragonScales : PermanentBuff, IDamageTakenEffectStack
    {
        private IRng _rng;

        public DragonScales(IRng rng)
        {
            _rng = rng;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.Result==AttackResult.Block)
            {
                if(_rng.NextDouble()<=0.20)
                {

                }
            }
        }
    }

    public class DragonScales_Buff : Buff, IBuffAppliedEffectStack
    {
        public override decimal Durration
        { get { return 12m; } }

        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(IgnorePain))
            {
                var ignorePain = buff as IgnorePain;
                ignorePain.DamageRemaining = (int)(ignorePain.DamageRemaining * 1.40m);
            }
        }
    }
}
