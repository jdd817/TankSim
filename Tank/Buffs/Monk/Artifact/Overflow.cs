using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk.Artifact
{
    public class Overflow:PermanentBuff, IBuffAppliedEffectStack
    {
        private IRng _rng;

        public Overflow(IRng rng)
        {
            _rng = rng;
        }

        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            //implimenting this as simply another healing orb added rather than create a greater orb
            if (buff.GetType() == typeof(HealingOrb))
            {
                if (!(buff as HealingOrb).GreaterOrb && _rng.NextDouble() < 0.05 * Stacks)
                    Target.Buffs.AddBuff(new HealingOrb() { GreaterOrb = true });
            }
        }
    }
}
