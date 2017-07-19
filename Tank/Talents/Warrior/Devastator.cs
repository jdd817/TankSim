using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 5, 1)]
    public class Devastator : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        private IRng _rng;

        public Devastator(IRng rng)
        {
            _rng = rng;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Attack))
            {
                var extraDamage = (int)(2.78m * tank.WeaponDamage);  //this may not be accurate
                Result.DamageDealt += extraDamage;
                Result.ResourceCost = -5;

                if(_rng.NextDouble()<=0.30)
                {
                    Result.CooldownReduction.Add(
                        new CooldownReduction
                        {
                            Ability = typeof(Abilities.Warrior.ShieldSlam),
                            Amount = 0,
                            ReductionType = ReductionType.To
                        });
                }
            }
        }
    }
}
