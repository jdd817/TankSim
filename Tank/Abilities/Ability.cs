using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities
{
    public enum AbilityType
    {
        Melee, Ranged, Spell, Self
    }

    public abstract class Ability
    {
        public Ability()
        {
            ResourceCost = 0;
            ResourceGain = 0;
            SecondaryResourceCost = 0;
        }

        public int ResourceCost
        { get; set; }

        public int ResourceGain
        { get; set; }

        public int SecondaryResourceCost
        { get; set; }

        public virtual HitTableModifiers GetModifiers(BuffManager CaseterBuffs, BuffManager TargetBuffs)
        {
            return new HitTableModifiers();
        }

        public virtual bool OnGCD { get { return true; } }

        public abstract AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target);
    }
}
