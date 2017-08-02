using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class ShatterTheSouls : PermanentBuff
    {
        public override int MaxStacks
        { get { return 7; } }
        //implimented in the shear ability itself, as there's no way to directly change the soul fragment chance via effect stacks
    }
}
