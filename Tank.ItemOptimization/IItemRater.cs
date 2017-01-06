using System.Collections.Generic;

namespace Tank.ItemOptimization
{
    public interface IItemRater
    {
        decimal GetRating(Item item, Dictionary<StatType, int> currentStats, Dictionary<StatType, List<StatWeights>> statWeights);
    }
}