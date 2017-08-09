using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Talents.Monk
{
    [Talent(typeof(Classes.Monk), 7, 2)]
    public class BlackoutCombo : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Monk.BlackoutStrike))
                Result.TargetBuffsApplied.Add(new BlackoutCombo_Buff());
        }
    }

    public class BlackoutCombo_Buff:Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 60m; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Monk.TigerPalm))
            {
                Result.DamageDealt *= 2;
                TimeRemaining = 0;
            }
            if(Ability.GetType()==typeof(Abilities.Monk.BreathOfFire))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Monk.BreathOfFire),
                    Amount = 3,
                    ReductionType = ReductionType.By
                });
                TimeRemaining = 0;
            }
            if(Ability.GetType()==typeof(Abilities.Monk.KegSmash))
            {
                Result.CooldownReduction.Add(new CooldownReduction
                {
                    Ability = typeof(Abilities.Monk.IronskinBrew),
                    ReductionType = ReductionType.By,
                    Amount = 2
                });
                TimeRemaining = 0;
            }
            if(Ability.GetType()==typeof(Abilities.Monk.IronskinBrew))
            {
                var stagger = tank.Buffs.GetBuff<Buffs.Monk.Stagger>();
                if(stagger!=null)
                {
                    stagger.TickTimer += 3;
                }
                TimeRemaining = 0;
            }
            if(Ability.GetType()==typeof(Abilities.Monk.PurifyingBrew))
            {
                Result.CasterBuffsApplied.Add(new Buffs.Monk.ElusiveBrawler(tank.Mastery));
                TimeRemaining = 0;
            }
        }
    }
}
