using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities
{

    public abstract class Ability
    {
        public Ability()
        {
            Cooldown = 0;
            MaxCharges = 1;
        }

        public decimal Cooldown { get; set; }
        public int MaxCharges { get; set; }
        public virtual Type CooldownType
        {
            get { return this.GetType(); }
        }

        public virtual HitTableModifiers GetModifiers(IBuffManager CaseterBuffs, IBuffManager TargetBuffs)
        {
            return new HitTableModifiers();
        }

        public virtual bool OnGCD { get { return true; } }

        public abstract AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target);
    }
}
