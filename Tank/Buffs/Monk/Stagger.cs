using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.Buffs.Monk
{
    public class Stagger : Buff
    {
        public Stagger(int damageDelayed)
        {
            DamageDelayed = damageDelayed;
            Tick = 0.5m;
        }

        public override decimal Durration { get { return 10.0m; } } 

        public int DamageDelayed { get; set; }

        public override void Refresh(Buff NewBuff)
        {
            base.Refresh(NewBuff);
            DamageDelayed += (NewBuff as Stagger).DamageDelayed;
        }

        public override void Ticked()
        {
            int damageTaken = TickDamage;
            Target.CurrentHealth -= damageTaken;
            DamageDelayed -= damageTaken;
            DataLogging.DataLogManager.LogEvent(new DataLogging.DamageEvent
            {
                Name = "Stagger",
                Time = DataLogging.DataLogManager.CurrentTime,
                DamageTaken = damageTaken,
                Result = AttackResult.Hit
            });
        }

        public int TickDamage
        {
            get
            {
                return (TimeRemaining > 0)
                    ? (int)(DamageDelayed / TimeRemaining * Tick)
                    : DamageDelayed;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}<{1}> ({2:0.00})",
                    Name,
                    DamageDelayed,
                    TimeRemaining);
        }
    }
}
