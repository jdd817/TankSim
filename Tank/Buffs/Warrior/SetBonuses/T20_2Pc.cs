using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Warrior.SetBonuses
{
    [Effect(Class = typeof(Classes.Warrior))]
    public class T20_2Pc : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.Warrior.BerserkerRage))
            {
                Result.ResourceCost = -20;
                Result.CasterBuffsApplied.Add(new T20_2Pc_Tick());
            }
        }
    }

    public class T20_2Pc_Tick:Buff
    {
        public T20_2Pc_Tick()
        {
            Tick = 1;
        }

        public override decimal Durration
        { get { return 6m; } }

        public override void Ticked()
        {
            (Target as Classes.Warrior).Rage += 10;
        }
    }
}
