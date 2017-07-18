using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class SkeletalShattering:PermanentBuff//, IPlayerAbilityEffectStack, IReplacingEffectStack
    {
        public override int MaxStacks { get { return 1; } }

        public IEffectStack ReplacedEffect { get; set; }

        public Type ReplacedType
        {
            get
            {
                return typeof(Buffs.DeathKnight.BoneShield);
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            throw new NotImplementedException();
        }
    }
}
