using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tank
{
    public class Mob:Actor
    {
        private IRng _rng;

        public Mob(IRng rng)
            :this()
        {
            _rng = rng;
        }

        public Mob()
        {
            Buffs = new BuffManager();
            Buffs.Target = this;
            Weapons = new List<Weapon>();
        }

        public string Name
        { get; set; }

        #region Actor Members

        public int MaxHealth { get; set; }

        public int CurrentHealth { get; set; }

        public decimal DodgeChance
        { get; set; }

        public decimal ParryChance
        { get; set; }

        public decimal BlockChance
        { get; set; }

        public decimal MissChance
        { get; set; }

        public decimal CritChance
        { get; set; }

        [XmlIgnore]
        public IBuffManager Buffs
        { get; set; }

        public Abilities.Attack GetAttack()
        {
            foreach (Weapon W in Weapons)
            {
                if (W.SwingTimer <= 0)
                {
                    W.SwingTimer += W.Speed;
                    return new Abilities.Attack(W.Damage);
                }
            }
            return null;
        }

        public void UpdateTimeElapsed(decimal DeltaTime)
        {
            Buffs.DecrementBuffTimers(DeltaTime);
            foreach (Weapon W in Weapons)
                W.SwingTimer -= DeltaTime;
        }

        public void Reset()
        {
            Buffs.ClearAll();
            foreach (var w in Weapons)
                w.SwingTimer = w.Speed;
        }

        public List<Weapon> Weapons
        { get; set; }

        #endregion
    }
}
