using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DemonHunter
{
    public class Metamorphosis : Buff, IPlayerAbilityEffectStack
    {
        public Metamorphosis()
        {
            TickTimer = 1;
            HealthGain = 0.30m;
        }

        public override decimal Durration
        { get { return 15m; } }

        public override void Ticked()
        {
            (Target as Classes.DemonHunter).Pain += 7;
        }

        public decimal HealthGain { get; set; }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            if (Stat == StatType.MaxHealth)
                return HealthGain;
            if (Stat == StatType.Armor)
                return 1.0m;
            return 0;
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DemonHunter.Shear))
            {
                Result.DamageDealt = (int)(Result.DamageDealt * (406m / 340m));
                if (!Result.CasterBuffsApplied.Any(b => b.GetType() == typeof(LesserSoulFragment)))
                    Result.CasterBuffsApplied.Add(new LesserSoulFragment());
            }
        }

        public override void Refresh(Buff NewBuff)
        {
            if (NewBuff.TimeRemaining > TimeRemaining)
                TimeRemaining = NewBuff.TimeRemaining;
        }
    }
}
