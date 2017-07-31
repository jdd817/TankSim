using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Warrior.Artifact
{
    public class ScalesOfEarth : PermanentBuff, IDamageTakenEffectStack
    {
        private IRng _rng;

        public ScalesOfEarth(IRng rng)
        {
            _rng = rng;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(damageEvent.Result==AttackResult.Block)
            {
                //we dont have a result for crit blocks, so look at the damage blocked.. it should be more than unblocked for crits, less for non-crits
                if (damageEvent.DamageBlocked> (damageEvent.DamageTaken+damageEvent.DamageAbsorbed) && _rng.NextDouble()<0.25)
                {
                    tank.Buffs.AddBuff(new ScalesOfEarth_Buff());
                }
            }
        }
    }

    public class ScalesOfEarth_Buff : Buff
    {
        public override decimal Durration { get { return 6; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Armor)
                return 0.30m;
            return 0;
        }
    }
}
