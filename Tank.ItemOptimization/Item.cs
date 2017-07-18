using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public class Item
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ItemLevel { get; set; }
        public int Quality { get; set; }
        public ItemType Slot { get; set; }
        public Dictionary<StatType, int> Stats { get; set; }
    }

    public enum ItemType
    {
        Head,
        Neck,
        Shoulder,
        Back,
        Chest,
        Wrist,
        Hands,
        Waist,
        Legs,
        Feet,
        Finger,
        Trinket,
        Weapon
    }

    public enum StatType
    {
        Other,
        Str,
        Stam,
        Haste,
        Vers,
        Crit,
        Mastery,
        Leech
    }

    public enum Slot
    {
        Head,
        Neck,
        Shoulder,
        Back,
        Chest,
        Wrist,
        Hands,
        Waist,
        Legs,
        Feet,
        Finger1,
        Finger2,
        Trinket1,
        Trinket2,
        MainHand,
        OffHand
    }
}
