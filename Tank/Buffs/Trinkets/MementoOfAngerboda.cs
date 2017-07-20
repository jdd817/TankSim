using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Trinkets
{
    [Effect]
    public class MementoOfAngerboda : RPPMBuff, IPlayerAbilityEffectStack
    {
        private int _amount;
        private IRng _rng;

        public MementoOfAngerboda(IRng rng, int amount)
            : base(rng)
        {
            _rng = rng;
            _amount = amount;

            ProcsPerMinute = 1.5m;
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
            if(Ability is Attack && DidProc(CurrentTime, tank))
            {
                switch(_rng.Next(1,3))
                {
                    case 1:
                        tank.Buffs.AddBuff(new DirgeOfAngerboda(_amount));
                        break;
                    case 2:
                        tank.Buffs.AddBuff(new HowlOfIngvar(_amount));
                        break;
                    case 3:
                        tank.Buffs.AddBuff(new WailOfSvala(_amount));
                        break;
                }
            }
        }
    }

    public class DirgeOfAngerboda : Buff
    {
        private int _amount;

        public DirgeOfAngerboda(int amount)
        {
            _amount = amount;
        }
        

        public override decimal Durration
        {
            get
            {
                return 8;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Mastery)
                return _amount;
            return 0;
        }
    }

    public class HowlOfIngvar : Buff
    {
        private int _amount;

        public HowlOfIngvar(int amount)
        {
            _amount = amount;
        }


        public override decimal Durration
        {
            get
            {
                return 8;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Crit)
                return _amount;
            return 0;
        }
    }

    public class WailOfSvala : Buff
    {
        private int _amount;

        public WailOfSvala(int amount)
        {
            _amount = amount;
        }


        public override decimal Durration
        {
            get
            {
                return 8;
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
