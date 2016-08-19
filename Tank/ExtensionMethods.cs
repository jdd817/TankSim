using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> OfBaseType<T>(this IEnumerable source) where T :class
        {
            foreach (var element in source)
                if (typeof(T).IsAssignableFrom(element.GetType()))
                    yield return element as T;
        }
    }
}
