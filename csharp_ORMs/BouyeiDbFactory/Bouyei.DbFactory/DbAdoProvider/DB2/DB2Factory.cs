﻿using System;
using System.Data;
using System.Data.Common;

namespace Bouyei.DbFactory.DbAdoProvider.DB2
{

    internal class Db2Factory:BaseFactory
    {
        public Db2Factory(int timeout = 1800)
            : base( FactoryType.DB2, timeout)
        {

        }

        public Db2Factory(string ConnectionString,int timeout):
            base(FactoryType.DB2,timeout,ConnectionString)
        { }

        public Db2Factory(IDbConnection dbConnection, int timeout = 1800)
            : base(FactoryType.DB2, timeout)
        {

        }
  
        public static DbProviderFactory GetFactory()
        {
            return IBM.Data.DB2.DB2Factory.Instance;
        }

        public static DbParameter GetParameter(CmdParameter param)
        {
            var p = new IBM.Data.DB2.DB2Parameter(param.ParameterName, param.Value);
            p.DbType = param.DbType;
            p.Size = param.Size;
            p.Direction = param.Direction;
            p.SourceColumn = param.SourceColumn;
            p.SourceVersion = param.SourceVersion;
            p.SourceColumnNullMapping = param.SourceColumnNullMapping;
            return p;
        }
    }
}
