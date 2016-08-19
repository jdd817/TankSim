using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Artifacts
{
    public interface IArtifact
    {
        IEnumerable<Buff> GetArtifactBuffs();
    }
}
