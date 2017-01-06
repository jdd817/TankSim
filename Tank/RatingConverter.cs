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
                    return Rating / (70000m / 3m);
                case StatType.Crit:
                    return Rating / 35000m;  //increase to 40000 in 7.1.5
                case StatType.Haste:
                    return Rating / 32500m;  //increase to 37500 in 7.1.5
                case StatType.Versatility:
                    return Rating / 40000m;  //increase to 47500 in 7.1.5
                case StatType.Leech:
                    return Rating / 22950m;
                case StatType.Parry:
                case StatType.Dodge:
                    return Rating / 51500m;
                case StatType.PrimaryAvoidance:
                    return Rating / 231500m;
                case StatType.Armor:
                    return Rating / (Rating + 3609.9m);
                default:
                    return 0;
                    throw new InvalidOperationException("Unknown rating");
            }
        }
    }
}
