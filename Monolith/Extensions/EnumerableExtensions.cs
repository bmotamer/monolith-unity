using System.Collections.Generic;
using System.Linq;

namespace Monolith.Extensions
{
    
    public static class EnumerableExtensions
    {

        public static bool HasNulls<T>(this IEnumerable<T> list)
        {
            bool result = false;
            
            foreach (T item in list)
            {
                result = item.Equals(null);

                if (result) break;
            }

            return result;
        }

        private static IList<T> ToIList<T>(this IEnumerable<T> enumerable)
        {
            IList<T> result;
            
            if (enumerable is IList<T> list)
            {
                result = list;
            }
            else
            {
                result = enumerable.ToArray();
            }

            return result;
        }

        public static bool HasDuplicates<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer = null)
        {
            if (comparer is null) comparer = EqualityComparer<T>.Default;
            
            bool result = false;

            IList<T> list = enumerable.ToIList();
            
            for (int i = 0; i < list.Count; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    result = comparer.Equals(list[i], list[j]);
                    
                    if (result) goto End;
                }
            }
            
            End:
            return result;
        }
        
    }
    
}