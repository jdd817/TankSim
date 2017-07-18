using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.DataLogging;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 7, 2)]
    public class BloodMirror : Abilities.Ability
    {
        public BloodMirror()
        {
            Cooldown = 120m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            throw new NotImplementedException();
        }
    }

    public class BloodMirror_Buff : Buffs.Buff, Buffs.IDamageTakenEffectStack
    {
        public override decimal Durration
        { get { return 10m; } }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            damageEvent.DamageTaken = (int)(damageEvent.DamageTaken * 0.80m);
        }
    }
}
