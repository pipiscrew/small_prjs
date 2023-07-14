﻿/*-------------------------------------------------------------
 *auth:bouyei
 *date:2016/4/26 9:19:56
 *contact:qq453840293
 *machinename:BOUYEI-PC
 *company/organization:Microsoft
 *profile:www.openthinking.cn
---------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bouyei.DbFactory.DbAdoProvider
{
    public interface IDbProvider: IDisposable
    {
        string ConnectionString { get; set; }
        FactoryType FactoryType { get; set; }
        DbResult<bool, string> Connect(string connectionString = "");
        DbResult<DataTable, string> QuerySchema();
        DbResult<DataTable, string> Query(Parameter dbParameter);
        DbResult<List<T>, string> Query<T>(Parameter dbParameter);
        DbResult<DataSet, string> Querys(Parameter dbParameter);
        DbResult<int, string> Query<T>(Parameter dbParameter, Func<T, bool> rowAction);
        DbResult<int, string> Query(Parameter dbParameter, Func<IDataReader, bool> rowAction);
        DbResult<int, string> Query(Parameter dbParameter, Func<object[],DataColumn[], bool> rowAction);
        DbResult<IDataReader, string> QueryToReader(Parameter dbParameter);
        DbResult<int, string> QueryChanged(Parameter dbParameter, Func<DataTable,bool> action);
        DbResult<int, string> QueryToTable(Parameter dbParameter, DataTable dstTable);
        DbResult<int, string> ExecuteCmd(Parameter dbParameter);
        DbResult<int, string> ExecuteTransaction(Parameter dbParameter);
        DbResult<int, string> ExecuteTransaction(string[] CommandTexts,
            int timeout = 1800, Func<int, bool> rowAction=null);

        DbResult<T, string> ExecuteScalar<T>(Parameter dbParameter);

        DbResult<int, string> BulkCopy(BulkParameter dbParameter);

        DbResult<int, string> BulkCopy<T>(CopyParameter<T> dbParameter);
    }
}
