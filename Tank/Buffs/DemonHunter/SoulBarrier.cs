using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter
{
    public class SoulBarrier:AbsorbShield, IBuffFadedEffectStack
    {
        public SoulBarrier(Player tank, int fragmentsConsumed)
            :base(1)
        {
            AbsorbFloor = (int)(3 * tank.AttackPower * tank.HealthPercentage);
            DamageRemaining = (int)(22.5m * tank.AttackPower * tank.HealthPercentage
                + 2.5m * tank.AttackPower * tank.HealthPercentage * fragmentsConsumed);
        }

        public override decimal Durration { get { return 12m; } }

        public int AbsorbFloor { get; set; }

        public override int DamageRemaining
        {
            get { return base.DamageRemaining; }
            set
            {
                var dr = value;
                if (dr < AbsorbFloor)
                    dr = AbsorbFloor;
                base.DamageRemaining = dr;
            }
        }

        public void BuffFaded(Buff buff)
        {
            if (buff.GetType() == typeof(LesserSoulFragment))
            {
                var tank = Target as Player;
                DamageRemaining += (int)(2.5m * tank.AttackPower * tank.HealthPercentage * buff.Stacks);
            }
        }
    }
}
