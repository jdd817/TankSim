using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class Fortification : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.FortifyingBrew))
            {
                Result.CasterBuffsApplied.Add(new Fortification_Buff());
            }
        }
    }

    public class Fortification_Buff : Buff
    {
        public override decimal Durration { get { return 21m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.Dodge)
                return 0.25m;
            return 0;
        }
    }
}
