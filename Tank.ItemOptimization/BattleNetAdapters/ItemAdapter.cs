using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleNetApi.DataObjects;

namespace Tank.ItemOptimization.BattleNetAdapters
{
    public class ItemAdapter : IAdapter<BattleNetApi.DataObjects.Item, Item>
    {
        private IAdapter<int, StatType> _statAdapter;

        public ItemAdapter(IAdapter<int, StatType> statAdapter)
        {
            _statAdapter = statAdapter;
        }

        public Item Convert(BattleNetApi.DataObjects.Item source)
        {
            return new Item
            {
                Id = source.id,
                Name = source.name,
                //Slot=source.sl
                Stats = source.stats.Select(s => new { Stat = _statAdapter.Convert(s.stat), Value = s.amount })
                    .GroupBy(s => s.Stat)
                    .ToDictionary(s => s.Key, s => s.Sum(x => x.Value))
            };
        }
    }
}
