﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class SanguinaryAffinity:PermanentBuff
    {
        public override int MaxStacks { get { return 1; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Damage)
                return 0.05m;
            return 0;
        }
    }
}
