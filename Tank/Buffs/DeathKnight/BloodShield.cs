﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tank.DataLogging;

namespace Tank.Buffs.DeathKnight
{
    public class BloodShield:Buff, IDamageTakenEffectStack
    {
        public BloodShield(int DamageAbsorbed)
        {
            TimeRemaining = 60.0m;
            _damageRemaining = DamageAbsorbed;
        }

        public override decimal Durration { get { return 60.0m; } }

        public override int MaxStacks
        {
            get { return 1; }
        }

        private int _damageRemaining;

        public int DamageRemaining
        {
            get { return _damageRemaining; }
            set
            {
                _damageRemaining = value;
                if (_damageRemaining <= 0)
                    TimeRemaining = 0;
            }
        }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            DamageRemaining += (NewBuff as BloodShield).DamageRemaining;
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageRemaining,
                    TimeRemaining);
        }

        public void DamageTaken(decimal currentTime, DamageEvent damageEvent, Player tank)
        {
            int Absorbed = Math.Min(DamageRemaining, damageEvent.DamageTaken);
            damageEvent.DamageTaken = damageEvent.DamageTaken - Absorbed;
            damageEvent.DamageAbsorbed = Absorbed;
            DamageRemaining -= Absorbed;
        }
    }
}
