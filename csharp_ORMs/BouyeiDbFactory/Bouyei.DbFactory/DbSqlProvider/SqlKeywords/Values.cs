using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bouyei.DbFactory.DbSqlProvider.SqlKeywords
{
    public class Values : WordsBase
    {
        public Values() : base(AttributeType.IgnoreWrite | AttributeType.Ignore)
        {

        }

        public string ToString(Dictionary<string, object> columns)
        {
            List<string> array = new List<string>(columns.Count);
            foreach (KeyValuePair<string, object> kv in columns)
            {
                array.Add(ObjectValueFormat(kv.Value));
            }

            return "Values (" + string.Join(",", array) + ") ";
        }
    }

    public class Values<T> : WordsBase
    {
        public Values() : base(typeof(T), AttributeType.IgnoreWrite | AttributeType.Ignore)
        {

        }

        public string ToString(params T[] value)
        {
            StringBuilder builder = new StringBuilder("Values ");
            var pros = GetProperties().ToArray();

            for (int i = 0; i < value.Length; ++i)
            {
                builder.AppendFormat("({0}){1}",
                    base.ParameterFormat(pros, value[i]), i < value.Length - 1 ? "," : "");
            }
            return builder.Append(" ").ToString();
        }

        public string ToString(Dictionary<string, object> columns)
        {
            List<string> array = new List<string>(columns.Count);
            foreach (KeyValuePair<string, object> kv in columns)
            {
                array.Add(ObjectValueFormat(kv.Value));
            }

            return "Values (" + string.Join(",", array) + ") ";
        }

        public string ToString<R>(Func<T,R> selector)
        {
            var columns = GetColumnsKeyValue(selector);
            var tmp = columns.Select(x => x.Value);

            return "Values (" + string.Join(",", tmp) + ") ";
        }
    }
}
