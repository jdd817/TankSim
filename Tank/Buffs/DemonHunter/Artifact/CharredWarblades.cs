using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter.Artifact
{
    [EffectPriority(5)]
    public class CharredWarblades : PermanentBuff, IPlayerAbilityEffectStack, IBuffTickedEffectStack
    {
        public void BuffTicked(Buff buff)
        {
            var buffType = buff.GetType();
            if (buffType == typeof(ImmolationAura)
                || buffType == typeof(SigilOfFlame))
            {
                (Target as Player).ApplyHealing((int)((buff as DamageOverTime).DamagePerTick * 0.15m));
            }
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            //heal for 15% of all fire damage dealt
            /*var abilityType = Ability.GetType();
            if (abilityType == typeof(Abilities.DemonHunter.ImmolationAura)
                || abilityType == typeof(Abilities.DemonHunter.SigilOfFlame)
                || abilityType == typeof(Abilities.DemonHunter.SpiritBomb)
                || abilityType == typeof(Abilities.DemonHunter.FelDevestation)
                || abilityType == typeof(Abilities.DemonHunter.FelBlade))*/
            if (Result.DamageType == DamageType.Fire)
            {
                Result.SelfHealing += (int)(Result.DamageDealt * 0.15m);
            }
        }
    }
}
