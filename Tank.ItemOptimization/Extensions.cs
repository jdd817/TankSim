using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank.ItemOptimization
{
    public static class Extensions
    {
        public static T Set<T>(this T obj, Action<T> action) where T:class
        {
            action(obj);
            return obj;
        }
    }
}
