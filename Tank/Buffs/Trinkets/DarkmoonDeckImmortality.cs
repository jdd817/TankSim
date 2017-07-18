using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank.Abilities;

namespace Tank.Buffs.Trinkets
{
    [Effect]
    public class DarkmoonDeckImmortality : PermanentBuff
    {
        private IRng _rng;

        public DarkmoonDeckImmortality(IRng rng)
        {
            Tick = 20;
            _rng = rng;
        }

        public int BonusArmor { get; set; }

        public override decimal Durration
        {
            get
            {
                return 20;
            }
        }

        public override decimal GetPercentageModifier(StatType Stat)
        {
            return 0;
        }

        public override int GetRatingModifier(StatType RatingType)
        {
            if (RatingType == StatType.Armor)
                return BonusArmor;
            return 0;
        }

        public override void Ticked() {
            BonusArmor = GetNewArmor();
        }

        private int GetNewArmor()
        {
            var card = _rng.Next(1, 8);
            switch (card)
            {
                case 1: return 970;
                case 2: return 1214;
                case 3: return 1455;
                case 4: return 1699;
                case 5: return 1940;
                case 6: return 2183;
                case 7: return 2425;
                case 8: return 2912;
            }
            return 0;
        }
    }
}
