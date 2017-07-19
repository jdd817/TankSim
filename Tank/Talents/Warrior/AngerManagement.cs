using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 1, 7)]
    public class AngerManagement : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            var reduction = Result.ResourceCost / 10m;

            /*
             * //major cooldowns NYI
            var cooldownReductions = new[]
            {
                new CooldownReduction
                {
                    Ability=typeof(Abilities.Warrior.BattleCry),
                    Amount=reduction,
                    ReductionType=ReductionType.By
                },
                new CooldownReduction
                {
                    Ability=typeof(Abilities.Warrior.LastStand),
                    Amount=reduction,
                    ReductionType=ReductionType.By
                },
                new CooldownReduction
                {
                    Ability=typeof(Abilities.Warrior.ShieldWall),
                    Amount=reduction,
                    ReductionType=ReductionType.By
                },
                new CooldownReduction
                {
                    Ability=typeof(Abilities.Warrior.DemoralizingShout),
                    Amount=reduction,
                    ReductionType=ReductionType.By
                },
            };

            if (Result.CooldownReduction == null)
                Result.CooldownReduction = cooldownReductions;
            else
                Result.CooldownReduction = Result.CooldownReduction.Concat(cooldownReductions).ToArray();
            */
        }
    }
}
