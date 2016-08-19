using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tank.Web.Models
{
    public class SimulationParameters
    {
        public int Seed { get; set; }
        public int RunCount { get; set; }
        public List<Tank> Tanks { get; set; }
        public List<Healer> Healers { get; set; }
        public List<Mob> Mobs { get; set; }
    }

    public class Tank
    {
        public string Class { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int MaxHealth { get; set; }
        public int Armor { get; set; }
        public int Mastery { get; set; }
        public int Crit { get; set; }
        public int Haste { get; set; }
        public int Versatility { get; set; }
        public int WeaponLowDamage { get; set; }
        public int WeaponHighDamage { get; set; }
        public decimal WeaponSpeed { get; set; }
    }

    public class Healer
    {
        public int HealAmount { get; set; }
        public decimal HealPeriod { get; set; }
    }

    public class Mob
    {
        public string Name { get; set; }
        public List<MobAttack> Attacks { get; set; }
    }

    public class MobAttack
    {
        public int Damage { get; set; }
        public decimal Period { get; set; }
    }
}