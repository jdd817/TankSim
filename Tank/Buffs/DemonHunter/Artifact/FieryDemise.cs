using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    public class FieryDemise:PermanentBuff, IPlayerAbilityEffectStack, IBuffAppliedEffectStack
    {
        public override int MaxStacks { get { return 7; } }

        public void BuffApplied(Buff buff)
        {
            var buffType = buff.GetType();
            if (buffType == typeof(ImmolationAura)
                || buffType == typeof(SigilOfFlame))
            {
                var dot = buff as DamageOverTime;
                dot.DamagePerTick = (int)(dot.DamagePerTick * (1 + 0.10m * Stacks));
            }
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            /*var abilityType = Ability.GetType();
            if (abilityType == typeof(Abilities.DemonHunter.ImmolationAura)
                || abilityType == typeof(Abilities.DemonHunter.SigilOfFlame)
                || abilityType == typeof(Abilities.DemonHunter.SpiritBomb)
                || abilityType == typeof(Abilities.DemonHunter.FelDevestation)
                || abilityType == typeof(Abilities.DemonHunter.FelBlade))*/
            if(Result.DamageType == DamageType.Fire)
            {
                Result.DamageDealt = (int)(Result.DamageDealt * (1 + 0.10m * Stacks));
            }
        }
    }
}
