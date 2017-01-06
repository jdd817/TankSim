using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization.BattleNetAdapters
{
    public interface IAdapter<TFrom, TTo>
    {
        TTo Convert(TFrom source);
    }
}
