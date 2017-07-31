using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Buffs;

namespace Tank.Legendaries.Common
{
    [Effect]
    public class Prydaz:PermanentBuff
    {
        public Prydaz()
        {
            Tick = 30m;
        }

        public override void Ticked()
        {
            Target.Buffs.AddBuff(new PrydazShield((int)(Target.MaxHealth * 0.15m)));
        }
    }

    public class PrydazShield : AbsorbShield
    {
        public PrydazShield(int DamageAbsorbed) : base(DamageAbsorbed)
        {
        }

        public override decimal Durration { get { return 30m; } }
    }
}
