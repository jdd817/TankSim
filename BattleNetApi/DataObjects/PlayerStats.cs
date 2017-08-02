using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetApi.DataObjects
{
    public class PlayerStats
    {
        public int health { get; set; }
        public string powerType { get; set; }
        public int power { get; set; }
        public int str { get; set; }
        public int agi { get; set; }
        public int @int { get; set; }
        public int sta { get; set; }
        public decimal speedRating { get; set; }
        public decimal speedRatingBonus { get; set; }
        public decimal crit { get; set; }
        public int critRating { get; set; }
        public decimal haste { get; set; }
        public int hasteRating { get; set; }
        public decimal mastery { get; set; }
        public int masteryRating { get; set; }
        public decimal leech { get; set; }
        public decimal leechRating { get; set; }
        public decimal leechRatingBonus { get; set; }
        public int versatility { get; set; }
        public decimal versatilityDamageDoneBonus { get; set; }
        public decimal versatilityHealingDoneBonus { get; set; }
        public decimal versatilityDamageTakenBonus { get; set; }
        public decimal avoidancdeRating { get; set; }
        public decimal avoidanceRatingBonus { get; set; }
        public int spellPen { get; set; }
        public decimal spellCrit { get; set; }
        public decimal mana5 { get; set; }
        public decimal mana5combat { get; set; }
        public int armor { get; set; }
        public decimal dodge { get; set; }
        public int dodgeRating { get; set; }
        public decimal parry { get; set; }
        public int parryRating { get; set; }
        public decimal block { get; set; }
        public int blockRating { get; set; }
        public decimal mainHandDmgMin { get; set; }
        public decimal mainHandDmgMax { get; set; }
        public decimal mainHandSpeed { get; set; }
        public decimal mainHandDps { get; set; }
        public decimal offHandDmgMin { get; set; }
        public decimal offHandDmgMax { get; set; }
        public decimal offHandSpeed { get; set; }
        public decimal offHandDps { get; set; }
        public decimal rangedDmgMin { get; set; }
        public decimal rangedDmgMax { get; set; }
        public decimal rangedSpeed { get; set; }
        public decimal rangedDps { get; set; }
    }
}
