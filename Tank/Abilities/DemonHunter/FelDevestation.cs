using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Abilities.DemonHunter
{
    public class FelDevestation : Ability
    {
        public FelDevestation()
        {
            Cooldown = 60m;
        }

        public override AbilityResult GetAbilityResult(AttackResult Result, Actor Caster, Actor Target)
        {
            return new AbilityResult
            {
                ResourceCost = 30,
                TargetBuffsApplied = new List<Buff> { new FelDevestation_Buff((int)(11.5m * (Caster as Player).AttackPower), Caster as Player) }
            };
        }
    }

    public class FelDevestation_Buff : LeechOverTime
    {
        public FelDevestation_Buff(int leechPerTick, Player caster)
        {
            Caster = caster;
            LeechPerTick = leechPerTick;
            Tick = 1;
        }

        public override decimal Durration
        { get { return 2m; } }
    }
}
