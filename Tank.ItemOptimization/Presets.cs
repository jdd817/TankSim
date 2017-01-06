using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public static class Presets
    {
        public static Dictionary<StatType, List<StatWeights>> BloodDk
        {
            get
            {
                return new Dictionary<StatType, List<StatWeights>>
                {
                    {StatType.Stam,new List<StatWeights>{new StatWeights {Cap=-1, Weight=2.75m}}},
                    {StatType.Str,new List<StatWeights>{new StatWeights {Cap=-1, Weight=2.5m}}},
                    {StatType.Haste,new List<StatWeights>
                        {
                            new StatWeights {Cap=8125, Weight=3.6m},
                            new StatWeights {Cap=13000, Weight=2.8m},
                            new StatWeights {Cap=-1, Weight=0}
                        }
                    },
                    {StatType.Mastery,new List<StatWeights>
                        {
                            new StatWeights {Cap=3750, Weight=3.25m},
                            new StatWeights {Cap=4000, Weight=2.5m},
                            new StatWeights {Cap=-1, Weight=1.5m}
                        }
                    },
                    {StatType.Crit,new List<StatWeights>
                        {
                            new StatWeights {Cap=7000, Weight=3.2m},
                            new StatWeights {Cap=10000, Weight=2.8m},
                            new StatWeights {Cap=-1, Weight=2.6m}
                        }
                    },
                    {StatType.Vers,new List<StatWeights>{new StatWeights {Cap=-1, Weight=3.1m}}},
                };
            }
        }
    }
}
