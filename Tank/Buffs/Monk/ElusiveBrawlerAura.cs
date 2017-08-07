using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Buffs.Monk
{
    public class ElusiveBrawlerAura : PermanentBuff, IDamageTakenEffectStack, IPlayerAbilityEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            if (damageEvent.DamageTaken > 0)
                tank.Buffs.AddBuff(new Buffs.Monk.ElusiveBrawler(tank.Mastery));
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.AttackPower)
                return (Target as Player).Mastery;
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.BlackoutStrike)
                || Ability.GetType() == typeof(Abilities.Monk.BreathOfFire))
                tank.Buffs.AddBuff(new Buffs.Monk.ElusiveBrawler(tank.Mastery));
        }
    }
}
