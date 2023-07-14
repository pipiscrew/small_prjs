﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouyei.DbFactory.DbSqlProvider.SqlKeywords
{
    public class OrderBy : WordsBase
    {
        public SortType SortType { get; private set; }

        public string[] ColumnNames { get; private set; }

        public OrderBy(SortType sortType,params string[] columnNames)
        {
            this.SortType = sortType;
            this.ColumnNames = columnNames;
        }

        public override string ToString()
        {
            return string.Format("Order By {0} {1} ",string.Join(",", ColumnNames), SortType.ToString());
        }
    }
}

namespace Bouyei.DbFactory
{
    public enum SortType : byte
    {
        Asc,
        Desc
    }
}
