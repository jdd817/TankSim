using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class PawsitiveOutlook : PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public PawsitiveOutlook(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Thrash))
            {
                if(_rng.NextDouble()<0.15)
                {
                    var buff = Result.CasterBuffsApplied.OfBaseType<Buffs.Druid.Thrash>().First();

                    Result.CasterBuffsApplied.Add(new Thrash(buff.DamagePerTick));
                }
            }
        }
    }
}
