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

        public void OptimizeGear(Character character, Dictionary<StatType, List<StatWeights>> statWeights)
        {
            bool madeSwap = true;

            while(madeSwap)
            {
                madeSwap = false;
                var currentStats = GetStats(character.EquippedItems);

                var swaps = new List<KeyValuePair<Slot, Item>>();

                foreach (var slot in character.EquippedItems)
                {
                    var slotRating = _rater.GetRating(slot.Value, AdjustStats(currentStats, slot.Value), statWeights);
                    Item winner = null;
                    foreach(var item in character.Inventory.Where(i=>i.Slot==slot.Value.Slot))
                    {
                        var itemRating = _rater.GetRating(item, AdjustStats(currentStats, slot.Value), statWeights);
                        if (itemRating > slotRating)
                            winner = item;
                    }
                    if(winner!=null)
                    {
                        madeSwap = true;
                        swaps.Add(new KeyValuePair<Slot, Item>(slot.Key, winner));
                        character.Inventory.Remove(winner);
                    }
                }

                foreach(var swap in swaps)
                {
                    var oldItem = character.EquippedItems[swap.Key];
                    
                    character.Inventory.Add(oldItem);
                    character.EquippedItems[swap.Key] = swap.Value;
                }
            }
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
