using System.Collections.Generic;
using System.Linq;

namespace MVNormanNativeKit.Tools.Extensions
{
    public static class EnumerableExtensions
    {
     
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> array)
        {
            return array ?? Enumerable.Empty<T>();
        }
    }
}