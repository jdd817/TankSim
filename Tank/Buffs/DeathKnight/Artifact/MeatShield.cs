﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class MeatShield : PermanentBuff
    {
        public override int MaxStacks
        {
            get { return 7; }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Stamina)
                return Stacks * 0.01m;
            return 0;
        }
    }
}
