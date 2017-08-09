using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk.Artifact
{
    public class StaggeringAround:PermanentBuff
    {
        public override int MaxStacks { get { return 7; } }

        //implimented in PurifyingBrew
    }
}
