using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

internal static class General
{
    internal static DialogResult Mes(string descr, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons butt = MessageBoxButtons.OK)
    {
        if (descr.Length > 0)
            return MessageBox.Show(descr, Application.ProductName, butt, icon);
        else
            return DialogResult.OK;
    }

    public static List<T> Query<T>(this IDbConnection objConn, string sql, object parameters = null) where T : class, new()
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

    public static int ExecuteModel<T>(this IDbConnection objConn, string sql, T model) where T : class
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

    public static object ExecuteScalar(this IDbConnection objConn, string sql)
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
}


