using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Talents.Warrior
{
    [Talent(typeof(Classes.Warrior), 1, 6)]
    public class Vengeance : Buffs.PermanentBuff, Buffs.IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            Buffs.Buff vengBuff = null;
            if (Ability.GetType() == typeof(Abilities.Warrior.IgnorePain))
                vengBuff = new Vengeance_Revenge();
            if (Ability.GetType() == typeof(Abilities.Warrior.Revenge))
                vengBuff = new Vengeance_IgnorePain();

            if (vengBuff != null)
            {
                Result.CasterBuffsApplied.Add(vengBuff);
            }
        }
    }

    public class Vengeance_IgnorePain : Buffs.Buff, Buffs.IPlayerAbilityEffectStack
    {
        public override decimal Durration
        { get { return 15; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.IgnorePain))
            {
                Result.ResourceCost = (int)(Result.ResourceCost * 0.65m);
                this.TimeRemaining = 0;
            }
        }
    }

    public class Vengeance_Revenge : Buffs.Buff, Buffs.IPlayerAbilityEffectStack
    {
        public override decimal Durration
        { get { return 15; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.Warrior.Revenge))
            {
                Result.ResourceCost = (int)(Result.ResourceCost * 0.65m);
                this.TimeRemaining = 0;
            }
        }
    }
}
