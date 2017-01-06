using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public class ItemRater : IItemRater
    {
        public decimal GetRating(Item item, Dictionary<StatType, int> currentStats, Dictionary<StatType, List<StatWeights>> statWeights)
        {
            var value = 0m;
            foreach (var stat in item.Stats)
            {
                if (!statWeights.ContainsKey(stat.Key))
                    continue;
                var currentStat = currentStats[stat.Key];
                var statValue = stat.Value;

                foreach (var weight in statWeights[stat.Key])
                {
                    var statAllocation = 0;
                    if (weight.Cap == -1)
                        statAllocation = statValue;
                    else if (statValue > 0 && currentStat <= weight.Cap)
                        statAllocation = Math.Min(statValue, weight.Cap - currentStat);

                    statValue -= statAllocation;
                    currentStat += statAllocation;
                    value += statAllocation * weight.Weight;
                }
            }

            return value;
        }
    }
}
