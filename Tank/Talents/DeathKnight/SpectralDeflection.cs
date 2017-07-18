using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.DataLogging;

namespace Tank.Talents.DeathKnight
{
    [Talent(typeof(Classes.DeathKnight), 2, 3)]
    public class SpectralDeflection : Buffs.PermanentBuff, Buffs.IDamageTakenEffectStack
    {
        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            var boneShield = tank.Buffs.GetBuff<Buffs.DeathKnight.BoneShield>();
            if (boneShield != null && boneShield.Stacks > 0 && damageEvent.DamageTaken >= tank.MaxHealth * 0.25)
            {
                var damageReduction = boneShield.GetDamageReduction(tank);
                damageEvent.DamageTaken = (int)(damageEvent.DamageTaken * (1m - damageReduction));
                boneShield.Stacks--;  //Spectral Deflection ignores the boneshield charge ICD
            }
        }
    }
}
