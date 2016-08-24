using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    public static class RatingConverter
    {
        /// <summary>
        /// Returns the value of the rating as a fraction. (ie, 10% = 0.10)
        /// </summary>
        /// <param name="ratingType"></param>
        /// <param name="Rating"></param>
        /// <returns></returns>
        public static decimal GetRating(StatType ratingType, int Rating)
        {
            switch (ratingType)
            {
                case StatType.Mastery:
                    return Rating / 7335.0m;
                case StatType.Crit:
                    return Rating / 11000.0m;
                case StatType.Haste:
                    return Rating / 10000.0m;
                case StatType.Versatility:
                    return Rating / 13000.0m;
                case StatType.Leech:
                    return Rating / 80.0m;
                case StatType.Parry:
                case StatType.Dodge:
                    return Rating / 16200.0m;
                case StatType.Armor:
                    return Rating / (Rating + 3609.9m);
                default:
                    return 0;
                    throw new InvalidOperationException("Unknown rating");
            }
        }
    }
}
