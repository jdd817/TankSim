using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Trinkets
{
    [Effect]
    public class ChronoShard : RPPMBuff, IPlayerAbilityEffectStack
    {
        private int _amount;
        private IRng _rng;

        public ChronoShard(IRng rng, int ilvl)
            : base(rng)
        {
            _rng = rng;
            
            ProcsPerMinute = 1m;
            _amount = 5020 + 19 * ilvl;

            /*switch (ilvl)
            {
                case 845: _amount = 5020;break;
                case 885: _amount = 9388; break;
                default: _amount = 9388; break;
            }*/
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (!(Ability is Attack) && DidProc(CurrentTime, tank))
            {
                tank.Buffs.AddBuff(new Acceleration(_amount));
            }
        }
    }

    public class Acceleration : Buff
    {
        private int _amount;

        public Acceleration(int amount)
        {
            _amount = amount;
        }

        public override decimal Durration
        {
            get
            {
                return 10m; ;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Haste)
                return _amount;
            return 0;
        }
    }
}
