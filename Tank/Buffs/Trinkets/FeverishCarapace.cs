using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Buffs.Trinkets
{
    [Effect]
    public class FeverishCarapace : RPPMBuff, IDamageTakenEffectStack
    {
        public FeverishCarapace(IRng rng) : base(rng)
        {
            ProcsPerMinute = 1.5m;
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if(DidProc(currentTime, tank))
            {
                tank.Buffs.AddBuff(new InfernalSkin());
            }
        }
    }

    public class InfernalSkin:Buff, IDamageTakenEffectStack
    {
        public InfernalSkin()
        {
        }

        public override decimal Durration
        { get { return 10; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            //reflect (but don't prevent) damage
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Armor)
                return 5297;
            return 0;
        }
    }
}
