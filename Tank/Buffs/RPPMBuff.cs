using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs
{
    public abstract class RPPMBuff:PermanentBuff
    {
        private IRng _rng;

        public RPPMBuff(IRng rng)
        {
            _rng = rng;
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public decimal ProcsPerMinute { get; set; }

        private decimal _lastProc = -600;
        private decimal _lastChanceToProc = -600;

        internal void ResetLastProcs()
        {
            _lastProc = -600;
            _lastChanceToProc = -600;
        }

        protected bool DidProc(decimal currentTime, Player tank)
        {
            //base chance  https://us.battle.net/forums/en/wow/topic/6893549789#1
            var procChance = ProcsPerMinute * (1m + tank.Haste) * Math.Min(10m, currentTime - _lastChanceToProc) / 60m;

            //add bad luck protection  https://us.battle.net/forums/en/wow/topic/8197741003
            procChance = procChance * Math.Max(1m, 1 + (Math.Min(1000m, currentTime - _lastProc) / (60m / ProcsPerMinute) - 1.5m) * 3);

            _lastChanceToProc = currentTime;

            if(_rng.NextDouble()<(double)procChance)
            {
                _lastProc = currentTime;
                return true;
            }
            return false;
        }
    }
}
