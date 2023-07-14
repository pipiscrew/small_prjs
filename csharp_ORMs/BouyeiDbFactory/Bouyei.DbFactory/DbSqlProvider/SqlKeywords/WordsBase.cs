using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Bouyei.DbFactory.DbSqlProvider.SqlKeywords
{
    public class WordsBase
    {
        private const string numericReg = @"^[+-]?\d*[.]?\d*$";

        public string SqlString { get; set; }

        protected Type type = null;
        protected AttributeType attrType = AttributeType.None;

        public WordsBase() { }

        public WordsBase(Type type)
        {
            this.type = type;
        }
        public WordsBase(AttributeType attrType)
        {
            this.attrType = attrType;
        }

        public WordsBase(Type type, AttributeType attrType)
       : this(attrType)
        {
            this.type = type;
        }

        public virtual string ToString(string[] columnNames)
        {
            return string.Join(",", columnNames);
        }

        public virtual string ToString(IEnumerable<string> columnNames)
        {
            return string.Join(",", columnNames);
        }

        public virtual string GetTableName()
        {
            string tabName = GetMappedAttributeName();
            if (tabName == string.Empty) tabName = type.Name;

            return tabName;
        }

        public virtual IEnumerable<string> GetColumns()
        {
            return GetProperties().Select(x => x.Name);
        }

        public virtual IEnumerable<PropertyInfo> GetProperties()
        {
            var items = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return items.Where(x => ExistIgnoreAttribute(x) == false);
        }

        protected Dictionary<string, string> GetColumnsKeyValue<T, R>(Func<T, R> selector)
        {
            var r = selector.Invoke(Activator.CreateInstance<T>());
            var pros = r.GetType().GetProperties();
            Dictionary<string, string> kv = new Dictionary<string, string>(pros.Length);

            foreach (var pro in pros)
            {
                var val = pro.GetValue(r, null);
                kv.Add(pro.Name, ObjectValueFormat(val));
            }
            return kv;
        }

        protected List<string> GetColumns<T, R>(Func<T, R> selector)
        {
            var r = selector.Invoke(Activator.CreateInstance<T>());
            var pros = r.GetType().GetProperties();
            var cols = pros.Select(x => x.Name).ToList();
            return cols;
        }

        protected string ParameterFormat(PropertyInfo[] pInfo, object value)
        {
            var pInfos = pInfo;// typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            List<string> values = new List<string>(pInfos.Length);

            foreach (var pi in pInfos)
            {
                object val = pi.GetValue(value, null);
                values.Add(ObjectValueFormat(val));
            }
            return string.Join(",", values);
        }

        protected string ObjectValueFormat(object val)
        {
            if (val == null || val == DBNull.Value)
            {
                return "NULL";
            }
            else if (val is Enum)
            {
                return Convert.ToInt32(val).ToString();
            }
            else if (val is DateTime)
            {
                var v = Convert.ToDateTime(val);
                return "'" + v.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else
            {
                if (IsDigital(val)) return val.ToString();
                else return "'" + val + "'";
            }
        }

        protected bool IsDigital(object value)
        {
            if (value == null) return false;

            if (value is int
                || value is double
                || value is float
                || value is long
                || value is byte) return true;

            var val = value.ToString();
            if (val == string.Empty) return false;

            return Regex.IsMatch(val, numericReg);
        }

        protected string GetMappedAttributeName()
        {
            var attr = type.GetCustomAttributes<MappedNameAttribute>(false).FirstOrDefault();

            if (attr == null) return string.Empty;

            return attr.Name;
        }

        protected bool ExistIgnoreAttribute(PropertyInfo pInfo)
        {
            if (attrType == AttributeType.None)
                return false;

            var attrs = pInfo.GetCustomAttributes();
            foreach (var attr in attrs)
            {
                if (attr is IgnoreAttribute ignore)
                {
                    var _ignore = attrType & AttributeType.Ignore;
                    if (_ignore == AttributeType.Ignore &&
                        (ignore.AttrType & AttributeType.Ignore) == AttributeType.Ignore)
                    {
                        return true;
                    }

                    var _read = attrType & AttributeType.IgnoreRead;
                    if (_read == AttributeType.IgnoreRead
                        && (ignore.AttrType & AttributeType.IgnoreRead) == AttributeType.IgnoreRead)
                    {
                        return true;
                    }

                    var _write = attrType & AttributeType.IgnoreWrite;
                    if (_write == AttributeType.IgnoreWrite
                        && (ignore.AttrType & AttributeType.IgnoreWrite) == AttributeType.IgnoreWrite)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
