using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight
{
    public class BloodPlague: LeechOverTime
    {
        public BloodPlague(int leechPerTick, Player caster)
        {
            TimeRemaining = Durration;
            LeechPerTick = leechPerTick;
            Tick = 1.0m;
            Caster = caster;
        }

        public override decimal Durration { get { return 24.0m; } }
    }
}
