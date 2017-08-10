using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 6, 2)]
    public class GuardianOfElune : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Druid.Mangle))
                Result.CasterBuffsApplied.Add(new GuardianOfElune_Buff());
        }
    }

    public class GuardianOfElune_Buff:Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 60m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Druid.Ironfur))
            {
                Result.CasterBuffsApplied.OfType<Buffs.Druid.Ironfur>().First().TimeRemaining += 2;
                TimeRemaining = 0;
            }
            if (Ability.GetType() == typeof(Abilities.Druid.FrenziedRegeneration))
            {
                var regen = Result.CasterBuffsApplied.OfType<Buffs.Druid.FrenziedRegeneration>().First();
                regen.HealingPerTick += (int)(regen.HealingPerTick * 0.20m);
                TimeRemaining = 0;
            }
        }
    }
}
