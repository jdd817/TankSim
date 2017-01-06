using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public class ItemOptimizer
    {
        private IItemRater _rater;

        public ItemOptimizer(
            IItemRater rater)
        {
            _rater = rater;
        }

        public decimal RateInventory(Dictionary<Slot, Item> equippedGear, Dictionary<StatType, List<StatWeights>> statWeights)
        {
            var value = 0m;

            var currentStats = GetStats(equippedGear);

            foreach (var equipment in equippedGear)
                value += _rater.GetRating(equipment.Value, AdjustStats(currentStats, equipment.Value), statWeights);

            return value;
        }

        public Dictionary<StatType, int> GetStats(Dictionary<Slot, Item> equippedGear)
        {
            var stats = new Dictionary<StatType, int>();
            foreach (var equipment in equippedGear)
            {
                foreach (var stat in equipment.Value.Stats)
                {
                    if (!stats.ContainsKey(stat.Key))
                        stats.Add(stat.Key, 0);
                    stats[stat.Key] += stat.Value;
                }
            }

            return stats;
        }

        public Dictionary<StatType, int> AdjustStats(Dictionary<StatType, int> currentStats, Item equipment)
        {
            var stats = new Dictionary<StatType, int>();
            foreach (var stat in currentStats)
            {
                var value = 0;
                if (equipment.Stats.ContainsKey(stat.Key))
                    value = equipment.Stats[stat.Key];
                stats.Add(stat.Key, stat.Value - value);
            }

            return stats;
        }
    }
}
