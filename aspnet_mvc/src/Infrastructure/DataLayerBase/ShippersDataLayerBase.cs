using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for ShippersDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the ShippersDataLayer class
     /// </summary>
     public class ShippersDataLayerBase
     {
         // constructor
         public ShippersDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Shippers SelectByPrimaryKey(int shipperID)
         {
              Shippers objShippers = null;
              string storedProcName = "[dbo].[Shippers_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@shipperID", shipperID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objShippers = CreateShippersFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objShippers;
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Shippers_GetRecordCount]", null, null, true, null);
         }

         public static int GetRecordCountShared(string storedProcName = null, string param = null, object paramValue = null, bool isUseStoredProc = true, string dynamicSqlScript = null)
         {
              int recordCount = 0;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? shipperID, string companyName, string phone)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Shippers_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, shipperID, companyName, phone);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

         /// <summary>
         /// Selects Shippers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static ShippersCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Shippers_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Shippers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static ShippersCollection SelectSkipAndTakeDynamicWhere(int? shipperID, string companyName, string phone, string sortByExpression, int startRowIndex, int rows)
         {
              ShippersCollection objShippersCol = null;
              string storedProcName = "[dbo].[Shippers_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // select, skip, take, sort parameters
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

                      // search parameters
                      AddSearchCommandParamsShared(command, shipperID, companyName, phone);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objShippersCol = new ShippersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Shippers objShippers = CreateShippersFromDataRowShared(dr);
                                      objShippersCol.Add(objShippers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objShippersCol;
         }

         /// <summary>
         /// Selects all Shippers
         /// </summary>
         public static ShippersCollection SelectAll()
         {
             return SelectShared("[dbo].[Shippers_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Shippers.
         /// </summary>
         public static ShippersCollection SelectAllDynamicWhere(int? shipperID, string companyName, string phone)
         {
              ShippersCollection objShippersCol = null;
              string storedProcName = "[dbo].[Shippers_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, shipperID, companyName, phone);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objShippersCol = new ShippersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Shippers objShippers = CreateShippersFromDataRowShared(dr);
                                      objShippersCol.Add(objShippers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objShippersCol;
         }

         /// <summary>
         /// Selects ShipperID and CompanyName columns for use with a DropDownList web control
         /// </summary>
         public static ShippersCollection SelectShippersDropDownListData()
         {
              ShippersCollection objShippersCol = null;
              string storedProcName = "[dbo].[Shippers_SelectDropDownListData]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objShippersCol = new ShippersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Shippers objShippers = new Shippers();
                                      objShippers.ShipperID = (int)dr["ShipperID"];
                                      objShippers.CompanyName = (string)(dr["CompanyName"]);

                                      objShippersCol.Add(objShippers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objShippersCol;
         }

         public static ShippersCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              ShippersCollection objShippersCol = null;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // select, skip, take, sort parameters
                      if (!String.IsNullOrEmpty(sortByExpression) && startRowIndex != null && rows != null)
                      {
                          command.Parameters.AddWithValue("@start", startRowIndex.Value);
                          command.Parameters.AddWithValue("@numberOfRows", rows.Value);
                          command.Parameters.AddWithValue("@sortByExpression", sortByExpression);
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objShippersCol = new ShippersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Shippers objShippers = CreateShippersFromDataRowShared(dr);
                                      objShippersCol.Add(objShippers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objShippersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Shippers objShippers)
         {
             string storedProcName = "[dbo].[Shippers_Insert]";
             return InsertUpdate(objShippers, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Shippers objShippers)
         {
             string storedProcName = "[dbo].[Shippers_Update]";
             InsertUpdate(objShippers, true, storedProcName);
         }

         private static int InsertUpdate(Shippers objShippers, bool isUpdate, string storedProcName)
         {
              int newlyCreatedShipperID = objShippers.ShipperID;

              object phone = objShippers.Phone;

              if (String.IsNullOrEmpty(objShippers.Phone))
                  phone = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      if (isUpdate)
                      {
                          // for update only
                          command.Parameters.AddWithValue("@shipperID", objShippers.ShipperID);
                      }

                      command.Parameters.AddWithValue("@companyName", objShippers.CompanyName);
                      command.Parameters.AddWithValue("@phone", phone);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedShipperID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedShipperID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int shipperID)
         {
              string storedProcName = "[dbo].[Shippers_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@shipperID", shipperID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? shipperID, string companyName, string phone)
         {
              if(shipperID != null)
                  command.Parameters.AddWithValue("@shipperID", shipperID);
              else
                  command.Parameters.AddWithValue("@shipperID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(companyName))
                  command.Parameters.AddWithValue("@companyName", companyName);
              else
                  command.Parameters.AddWithValue("@companyName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(phone))
                  command.Parameters.AddWithValue("@phone", phone);
              else
                  command.Parameters.AddWithValue("@phone", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Shippers object from the passed data row
         /// </summary>
         private static Shippers CreateShippersFromDataRowShared(DataRow dr)
         {
             Shippers objShippers = new Shippers();

             objShippers.ShipperID = (int)dr["ShipperID"];
             objShippers.CompanyName = dr["CompanyName"].ToString();

             if (dr["Phone"] != System.DBNull.Value)
                 objShippers.Phone = dr["Phone"].ToString();
             else
                 objShippers.Phone = null;

             return objShippers;
         }
     }
}
