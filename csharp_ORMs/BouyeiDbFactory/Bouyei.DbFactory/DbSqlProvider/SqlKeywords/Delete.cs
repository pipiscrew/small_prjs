﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouyei.DbFactory.DbSqlProvider.SqlKeywords
{
   public class Delete:WordsBase
    {
        public Delete() { }

        public override string ToString()
        {
            return string.Format("Delete ");
        }
    }
}
