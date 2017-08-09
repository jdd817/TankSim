using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class BrewStache : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Monk.IronskinBrew)
                || Ability.GetType() == typeof(Abilities.Monk.PurifyingBrew))
            {
                Result.CasterBuffsApplied.Add(new BrewStache_Buff());
            }
        }
    }

    public class BrewStache_Buff:Buff
    {
        public override decimal Durration { get { return 4.5m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Dodge)
                return 0.10m;
            return 0;
        }
    }
}
