using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Monk.Artifact
{
    public class FacePalm:PermanentBuff, IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public FacePalm(IRng rng)
        {
            _rng = rng;
        }

        public override int MaxStacks { get { return 7; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Monk.TigerPalm))
            {
                if (_rng.NextDouble() < 0.10 * Stacks)
                {
                    Result.DamageDealt *= 3;
                    Result.CooldownReduction.Add(
                        new CooldownReduction
                        {
                            Ability = typeof(Abilities.Monk.IronskinBrew),
                            ReductionType = ReductionType.By,
                            Amount = 1
                        });
                }
            }
        }
    }
}
