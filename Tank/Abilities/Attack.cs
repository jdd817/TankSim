using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank.Abilities
{
    public class Attack : Ability
    {
        public Attack(int damage)
        {
            Damage = damage;
        }
        
        public int Damage
        { get; set; }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = Damage
            };
        }
    }
}
