using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior.Artifact
{
    public class NeltharionsThunder : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Warrior.ThunderClap))
                Result.TargetBuffsApplied.Add(new NeltharionsThunder_Buff());
        }
    }

    public class NeltharionsThunder_Buff : Buff
    {
        public override int MaxStacks { get { return 5; } }

        public override decimal Durration { get { return 10m; } }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if(Stat==StatType.DamageReduction)
                return Stacks * 0.01m;

            return 0;
        }
    }
}
