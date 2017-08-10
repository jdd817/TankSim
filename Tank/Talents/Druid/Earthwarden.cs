using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;
using Tank.DataLogging;

namespace Tank.Talents.Druid
{
    [Talent(typeof(Classes.Druid), 6, 1)]
    public class Earthwarden : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Druid.Thrash))
                Result.CasterBuffsApplied.Add(new Earthwarden_Buff());
        }
    }

    public class Earthwarden_Buff:Buff, IDamageTakenEffectStack
    {
        public override decimal Durration { get { return 60m; } }

        public override int MaxStacks { get { return 3; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            damageEvent.DamageTaken -= (int)(damageEvent.DamageTaken * 0.30m);

            Stacks--;
        }
    }
}
