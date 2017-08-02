using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class DefensiveSpikes : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.DemonSpikes))
                Result.CasterBuffsApplied.Add(new DefensiveSpikes_Buff());
        }
    }

    public class DefensiveSpikes_Buff:Buff
    {
        public override decimal Durration { get { return 3m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Parry)
                return 0.10m;
            return 0;
        }
    }
}
