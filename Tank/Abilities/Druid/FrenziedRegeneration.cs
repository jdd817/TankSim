using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.Druid
{
    public class FrenziedRegeneration : Ability
    {
        public FrenziedRegeneration()
        {
            MaxCharges = 2;
            Cooldown = 24m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var amountHealed = Math.Max((int)(Caster.MaxHealth * 0.05m), HealAmount);

            return new AbilityResult
            {
                ResourceCost = 10,
                CasterBuffsApplied = new List<Buff>
                {
                    new Buffs.Druid.FrenziedRegeneration(amountHealed)
                }
            };
        }

        public static int HealAmount
        {
            get
            {
                return (int)(DataLogging.DataLogManager.DamageSince(DataLogging.DataLogManager.CurrentTime - 5.0m)
                        * 0.5m);
            }
        }

        public override bool OnGCD { get { return false; } }
    }
}
