using System.Collections.Generic;
using System.Linq;

namespace App.Helpers
{
    public static class Extensions
    {
        public static string ToStrinX(this object value)
        {
            string retvalue = "";

            if (value != null)
                retvalue = value.ToString();

            return retvalue;
        }

        public static SortableBindingList<T> ToSortableBindingList<T>(this IEnumerable<T> source)
        {
            return new SortableBindingList<T>(source.ToList());
        }

    }
}
