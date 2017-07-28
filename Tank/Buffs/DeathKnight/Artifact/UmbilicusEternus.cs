using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.DeathKnight.Artifact
{
    public class UmbilicusEternus : PermanentBuff,IBuffAppliedEffectStack, IBuffTickedEffectStack, IBuffFadedEffectStack, IPlayerAbilityEffectStack
    {
        private int BloodPlagueDamage = 0;

        public void BuffApplied(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.DeathKnight.VampiricBlood))
            {
                BloodPlagueDamage = 0;
            }
        }

        public void BuffFaded(Buff buff)
        {
            if (buff.GetType() == typeof(Buffs.DeathKnight.VampiricBlood))
            {
                buff.Target.Buffs.AddBuff(new UmbilicusEternusSheild(BloodPlagueDamage * 5));
            }
        }

        public void BuffTicked(Buff buff)
        {
            if(buff.GetType()==typeof(Buffs.DeathKnight.BloodPlague))
            {
                var plague = buff as BloodPlague;
                BloodPlagueDamage += plague.LeechPerTick;
            }
        }

        public void ProcessAbilityUsed(decimal CurrentTime, Ability Ability, AbilityResult Result, Player tank, Mob mob)
        {
            if(Ability.GetType()==typeof(Abilities.DeathKnight.BloodBoil))
            {
                if (mob.Buffs.GetBuff<UmbilicusEternus>() == null)
                    mob.Buffs.AddBuff(this);
            }
        }
    }

    public class UmbilicusEternusSheild:AbsorbShield
    {
        public UmbilicusEternusSheild(int DamageAbsorbed) : base(DamageAbsorbed)
        {
        }

        public override decimal Durration { get { return 10m; } }
    }
}
