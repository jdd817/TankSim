using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;
using Tank.Buffs;

namespace Tank.Legendaries.DeathKnight
{
    [Effect(Class = typeof(Classes.DeathKnight))]
    public class SkullflowersHaemostasis : PermanentBuff, IPlayerAbilityEffectStack
    {
        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.BloodBoil))
                tank.Buffs.AddBuff(new Haemostasis());
        }
    }

    public class Haemostasis : Buff, IPlayerAbilityEffectStack
    {
        public override decimal Durration { get { return 600m; } } //this is actually a permanant buff in-game, implimenting a long counter so its cleared on reset

        public override int MaxStacks{ get { return 5; } }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if (Ability.GetType() == typeof(Abilities.DeathKnight.DeathStrike))
            {
                Result.DamageDealt = (int)(Result.DamageDealt * (1m + Stacks * 0.20m));
                Result.SelfHealing = (int)(Result.SelfHealing * (1m + Stacks * 0.20m));
                Stacks = 0;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", this.GetType().Name, Stacks);
        }
    }
}
