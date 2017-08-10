using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Druid.Artifact
{
    public class EmbraceOfTheNightmare : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Druid.RageOfTheSleeper))
                Result.CasterBuffsApplied.Add(new EmbraceOfTheNightmare_Buff());
        }
    }

    public class EmbraceOfTheNightmare_Buff:Buff
    {
        public override decimal Durration { get { return 10m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Damage || Stat == StatType.Leech)
                return 0.25m;

            return 0;
        }
    }
}
