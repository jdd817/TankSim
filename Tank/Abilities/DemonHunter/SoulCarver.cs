using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.DemonHunter
{
    public class SoulCarver : Ability
    {
        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                DamageDealt = (int)(10.14m * (Caster as Player).WeaponDamage),
                CasterBuffsApplied = new List<Buffs.Buff> { new Buffs.DemonHunter.LesserSoulFragment { Stacks = 2 }, new SoulCarver_Buff() }
            };
        }
    }

    public class SoulCarver_Buff:Buffs.Buff
    {
        public SoulCarver_Buff()
        {
            TickTimer = 1m;
        }

        public override decimal Durration { get { return 3m; } }

        public override void Ticked()
        {
            Target.Buffs.AddBuff(new Buffs.DemonHunter.LesserSoulFragment());
        }
    }
}
