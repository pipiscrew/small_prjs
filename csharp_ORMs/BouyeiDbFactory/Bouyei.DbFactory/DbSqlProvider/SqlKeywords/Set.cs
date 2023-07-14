using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouyei.DbFactory.DbSqlProvider.SqlKeywords
{
    public class Set : WordsBase
    {
        public Set() { }

        public string ToString(Dictionary<string, object> NameValues)
        {
            StringBuilder builder = new StringBuilder("Set ");
            int c = NameValues.Count;

            foreach (var item in NameValues)
            {
                builder.AppendFormat("{0}={1}{2}",
                    item.Key, ObjectValueFormat(item.Value), (--c) > 0 ? "," : "");
            }
            return builder.Append(" ").ToString();
        }
    }

    public class Set<T> : WordsBase
    {
        public Set():base(typeof(T),AttributeType.IgnoreWrite| AttributeType.Ignore) { }

        public string ToString(T value)
        {
            var items = GetProperties();
            List<string> tmp = new List<string>(items.Count());

            foreach (var item in items)
            {
                var rVal = item.GetValue(value, null);
                tmp.Add(string.Format("{0}={1}", item.Name, ObjectValueFormat(rVal)));
            }
            return "Set " + string.Join(",", tmp) + " ";
        }

        public string ToString<R>(Func<T, R> selector)
        {
            var dict = GetColumnsKeyValue(selector);
            var tmp = dict.Select(x => x.Key + "=" + x.Value);

            return "Set " + string.Join(",", tmp) + " ";
        }
    }
}
