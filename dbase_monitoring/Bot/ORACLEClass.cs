using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;


namespace Bot
{
    class ORACLEClass
    {
        private OracleConnection objConn;
        private string m_ConnectionString;


        public ORACLEClass(string ConnectionString, out OracleException ExceptionObject)
        {
            try
            {
                m_ConnectionString = ConnectionString;
                objConn = new OracleConnection(ConnectionString);
                objConn.Open();

                ExceptionObject = null;
            }
            catch (OracleException ex)
            {
                objConn = null;
                ExceptionObject = ex;
            }
        }

        public DataTable GetDATATABLE(string SQLSTR)
        {
            OracleDataAdapter sqlAD = new OracleDataAdapter();
            DataTable sqlSET = new DataTable();
            OracleCommand sqlco = new OracleCommand();

            try
            {
                sqlco.CommandText = SQLSTR;
                sqlco.Connection = objConn;

                sqlAD.SelectCommand = sqlco;
                //sqlAD.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sqlAD.Fill(sqlSET);

                return sqlSET;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "OracleClass - GetDATASET");
                return null;
            }
            finally
            {
                sqlco.Dispose();
                sqlAD.Dispose();
                sqlSET.Dispose();
            }
        }

        public object ExecuteSQLScalar(string SQLSTR)
        {
            OracleCommand sqlco = new OracleCommand();
            try
            {
                sqlco.Connection = objConn;
                sqlco.CommandText = SQLSTR;
                return sqlco.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "OracleClass - ExecuteSQLScalar");
                return "";
            }
            finally
            {
                sqlco.Dispose();
            }
        }

        public void Close()
        {
            if ((objConn != null))
            {
                objConn.Close();
                objConn.Dispose();
            }
        }


    }
}
