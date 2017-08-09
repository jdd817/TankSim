using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Abilities.Monk
{
    public class PurifyingBrew : Ability
    {
        public PurifyingBrew()
        {
            Cooldown = 21m;
            MaxCharges = 3;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            var stagger = Caster.Buffs.GetBuff<Buffs.Monk.Stagger>();
            if (stagger != null)
            {
                var staggerReduction = 0.40m + Caster.Buffs.GetStacks<Buffs.Monk.Artifact.StaggeringAround>() * 0.01m;
                
                stagger.DamageDelayed = (int)(stagger.DamageDelayed * (1-staggerReduction));
            }

            return new AbilityResult
            {
            };
        }

        public override Type CooldownType
        {
            get
            {
                return typeof(IronskinBrew);
            }
        }

        public override bool OnGCD { get { return false; } }
    }
}
