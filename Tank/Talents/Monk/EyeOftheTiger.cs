using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Monk
{
    [Talent(typeof(Classes.Monk), 1, 2)]
    public class EyeOftheTiger : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.TigerPalm))
                Result.CasterBuffsApplied.Add(new EyeOfTheTiger_Buff((int)(1.72m * tank.AttackPower / 8m)));
        }
    }

    public class EyeOfTheTiger_Buff:HealOverTime
    {
        public EyeOfTheTiger_Buff(int healingPerTick)
        {
            Tick = 1m;
            HealingPerTick = healingPerTick;
        }

        public override decimal Durration { get { return 8m; } }
    }
}
