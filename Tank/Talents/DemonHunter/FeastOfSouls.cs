using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.DemonHunter
{
    [Talent(typeof(Classes.DemonHunter), 1, 2)]
    public class FeastOfSouls : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DemonHunter.SoulCleave))
                Result.CasterBuffsApplied.Add(new FeastOfSouls_Buff((int)(4.68m * tank.AttackPower * tank.HealthPercentage)));
        }
    }

    public class FeastOfSouls_Buff : HealOverTime
    {
        public FeastOfSouls_Buff(int HealPerTick)
        {
            TimeRemaining = Durration;
            HealingPerTick = HealPerTick;
            Tick = 1.0m;
        }

        public override decimal Durration { get { return 6.0m; } }
    }
}
