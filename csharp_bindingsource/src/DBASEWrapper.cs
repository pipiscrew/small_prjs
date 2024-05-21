using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

public class DBASEWrapper : IDisposable
{
    private IDbConnection objConn;

    public DBASEWrapper(IDbConnection connection)
    {
        try
        {
            this.objConn = connection;
            this.objConn.Open();
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Get the rows for the sql passed. Warning this is not return the table schema (column definitions and shits), if such the case use GetDataTableWithSchema function.
    /// </summary>
    public DataTable GetDataTable(string sql)
    {  //this is faster than GetDataTableWithSchema (datatable.Load) -- comparison on 5k records having 18 fields
        DataTable dT = new DataTable();

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                using (IDataReader reader = sqlco.ExecuteReader())
                {
                    SubClass dataAdapter = new SubClass();
                    dataAdapter.FillDatatable(dT, reader);
                }

                return dT;
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
            return null;
        }
        finally
        {
            dT.Dispose();
        }
    }

    public DataTable GetDataTableWithSchema(string sql)
    {
        DataTable dT = new DataTable();

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                dT.Load(sqlco.ExecuteReader());

                return dT;
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);

            return null;
        }
        finally
        {
            dT.Dispose();
        }
    }

    /// <summary>
    /// Returns a datareader for the sql, user must close the datareader on his side.
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public IDataReader GetDataReader(string sql)
    {
        IDataReader sqlread = null;

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.Connection = objConn;
                sqlco.CommandText = sql;

                sqlread = sqlco.ExecuteReader();

                return sqlread;
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);

            return null;
        }
    }

    public object ExecuteScalar(string sql)
    {
        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                return sqlco.ExecuteScalar();
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
            return string.Empty;
        }
    }

    public int Execute(string sql)
    {
        int returnValue = 0;

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                return sqlco.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
            returnValue = 0;
        }

        return returnValue;
    }


    /// <summary>
    /// Execule SQL wuth the user specified paramters array.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public int ExecuteParams(string sql, IDbDataParameter[] parameters)
    {
        int returnValue = 0;

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                if (parameters != null)
                {
                    foreach (IDbDataParameter parameter in parameters)
                    {
                        sqlco.Parameters.Add(parameter);
                    }
                }

                return sqlco.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
            returnValue = 0;
        }

        return returnValue;
    }


    #region " Model functions "

    /// <summary>
    /// Executes a query and retun a List<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public List<T> Query<T>(string sql, object parameters = null) where T : class, new()
    {
        List<T> models = new List<T>();

        try
        {
            using (var command = objConn.CreateCommand())
            {
                command.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var parameter in parameters.GetType().GetProperties())
                    {
                        IDbDataParameter param = command.CreateParameter();
                        param.ParameterName = "@" + parameter.Name;
                        param.Value = parameter.GetValue(parameters);
                        command.Parameters.Add(param);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T model = new T();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (reader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(model, reader[property.Name]);
                            }
                        }
                        models.Add(model);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
        }

        return models;
    }

    /// <summary>
    /// Executes the query and return the first record as T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public T QueryFirst<T>(string sql, object parameters = null) where T : class, new()
    {
        try
        {
            using (var command = objConn.CreateCommand())
            {
                command.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var parameter in parameters.GetType().GetProperties())
                    {
                        IDbDataParameter param = command.CreateParameter();
                        param.ParameterName = "@" + parameter.Name;
                        param.Value = parameter.GetValue(parameters);
                        command.Parameters.Add(param);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        T model = new T();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (reader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(model, reader[property.Name]);
                            }
                        }
                        return model;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
        }

        return null;
    }

    /// <summary>
    /// Execute SQL and convert Model properties as parameters.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    public int ExecuteModel<T>(string sql, T model) where T : class
    {
        int returnValue = 0;

        try
        {
            using (var sqlco = objConn.CreateCommand())
            {
                sqlco.CommandText = sql;

                foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
                {
                    IDbDataParameter parameter = sqlco.CreateParameter();
                    parameter.ParameterName = property.Name;
                    parameter.Value = property.GetValue(model);
                    sqlco.Parameters.Add(parameter);
                }

                return sqlco.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
            returnValue = 0;
        }

        return returnValue;
    }

    #endregion


    public bool IsConnected
    {
        get
        {
            if (objConn == null | objConn.State != ConnectionState.Open)
                return false;
            else
                return true;
        }
    }

    public IDbConnection GetConnection()
    {
        return objConn;
    }

    public void ConnectionClose()
    {
        if ((objConn != null))
        {
            objConn.Close();
            objConn.Dispose();
        }
    }

    public void Dispose()
    {
        if ((objConn != null))
        {
            objConn.Close();
            objConn.Dispose();
        }
    }

    private class SubClass : DataAdapter
    {  //subclass #System.Data.Common.DataAdapter# to access direct the Fill method (as is protected). All sqladapters and datatable.load() endup there..
        public SubClass() : base() { }

        /// <summary>
        /// THe only disadvange is not return the table schema (column definitions and shits)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
        public int FillDatatable(DataTable dt, IDataReader dr)
        {
            return Fill(dt, dr);
        }
    }
}


//public static class DBASEWrapperExtensions
//{
//    public static IDbDataParameter AddWithValue(this IDbCommand command, string parameterName, object value)
//    {
//        IDbDataParameter parameter = command.CreateParameter();
//        parameter.ParameterName = parameterName;
//        parameter.Value = value;
//        command.Parameters.Add(parameter);
//        return parameter;
//    }
//
//implementation over IDbConnection as Dapper / NHibernate - tested & working - versus use the ExecuteModel^ which is inside the Wrapper ;)
//    public static int Execute<T>(this IDbConnection connection, string sql, T model) where T : class
//    {
//        int returnValue = 0;

//        try
//        {
//            using (var sqlco = connection.CreateCommand())
//            {
//                sqlco.CommandText = sql;

//                foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
//                {
//                    IDbDataParameter parameter = sqlco.CreateParameter();
//                    parameter.ParameterName = property.Name;
//                    parameter.Value = property.GetValue(model);
//                    sqlco.Parameters.Add(parameter);
//                }

//                return sqlco.ExecuteNonQuery();
//            }
//        }
//        catch (Exception ex)
//        {
//            General.Mes(ex.Message + "\r\n\r\n" + (ex.InnerException == null ? "" : ex.InnerException.Message), System.Windows.Forms.MessageBoxIcon.Error);
//            returnValue = 0;
//        }

//        return returnValue;
//    }
//}
