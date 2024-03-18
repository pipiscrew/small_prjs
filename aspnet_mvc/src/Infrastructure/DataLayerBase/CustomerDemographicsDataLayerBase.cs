using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for CustomerDemographicsDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographicsDataLayer class
     /// </summary>
     public class CustomerDemographicsDataLayerBase
     {
         // constructor
         public CustomerDemographicsDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static CustomerDemographics SelectByPrimaryKey(string customerTypeID)
         {
              CustomerDemographics objCustomerDemographics = null;
              string storedProcName = "[dbo].[CustomerDemographics_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerTypeID", customerTypeID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomerDemographics = CreateCustomerDemographicsFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objCustomerDemographics;
         }

         /// <summary>
         /// Gets the total number of records in the CustomerDemographics table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[CustomerDemographics_GetRecordCount]", null, null, true, null);
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
         /// Gets the total number of records in the CustomerDemographics table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string customerTypeID)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[CustomerDemographics_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, customerTypeID);

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
         /// Selects CustomerDemographics records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[CustomerDemographics_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects CustomerDemographics records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTakeDynamicWhere(string customerTypeID, string sortByExpression, int startRowIndex, int rows)
         {
              CustomerDemographicsCollection objCustomerDemographicsCol = null;
              string storedProcName = "[dbo].[CustomerDemographics_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, customerTypeID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomerDemographicsCol = new CustomerDemographicsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      CustomerDemographics objCustomerDemographics = CreateCustomerDemographicsFromDataRowShared(dr);
                                      objCustomerDemographicsCol.Add(objCustomerDemographics);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomerDemographicsCol;
         }

         /// <summary>
         /// Selects all CustomerDemographics
         /// </summary>
         public static CustomerDemographicsCollection SelectAll()
         {
             return SelectShared("[dbo].[CustomerDemographics_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of CustomerDemographics.
         /// </summary>
         public static CustomerDemographicsCollection SelectAllDynamicWhere(string customerTypeID)
         {
              CustomerDemographicsCollection objCustomerDemographicsCol = null;
              string storedProcName = "[dbo].[CustomerDemographics_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, customerTypeID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomerDemographicsCol = new CustomerDemographicsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      CustomerDemographics objCustomerDemographics = CreateCustomerDemographicsFromDataRowShared(dr);
                                      objCustomerDemographicsCol.Add(objCustomerDemographics);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomerDemographicsCol;
         }

         /// <summary>
         /// Selects CustomerTypeID and CustomerDesc columns for use with a DropDownList web control
         /// </summary>
         public static CustomerDemographicsCollection SelectCustomerDemographicsDropDownListData()
         {
              CustomerDemographicsCollection objCustomerDemographicsCol = null;
              string storedProcName = "[dbo].[CustomerDemographics_SelectDropDownListData]";

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
                                  objCustomerDemographicsCol = new CustomerDemographicsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      CustomerDemographics objCustomerDemographics = new CustomerDemographics();
                                      objCustomerDemographics.CustomerTypeID = (string)dr["CustomerTypeID"];

                                      if (dr["CustomerDesc"] != System.DBNull.Value)
                                          objCustomerDemographics.CustomerDesc = (string)(dr["CustomerDesc"]);
                                      else
                                          objCustomerDemographics.CustomerDesc = null;

                                      objCustomerDemographicsCol.Add(objCustomerDemographics);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomerDemographicsCol;
         }

         public static CustomerDemographicsCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              CustomerDemographicsCollection objCustomerDemographicsCol = null;

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
                                  objCustomerDemographicsCol = new CustomerDemographicsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      CustomerDemographics objCustomerDemographics = CreateCustomerDemographicsFromDataRowShared(dr);
                                      objCustomerDemographicsCol.Add(objCustomerDemographics);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomerDemographicsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static string Insert(CustomerDemographics objCustomerDemographics)
         {
             string storedProcName = "[dbo].[CustomerDemographics_Insert]";
             return InsertUpdate(objCustomerDemographics, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(CustomerDemographics objCustomerDemographics)
         {
             string storedProcName = "[dbo].[CustomerDemographics_Update]";
             InsertUpdate(objCustomerDemographics, true, storedProcName);
         }

         private static string InsertUpdate(CustomerDemographics objCustomerDemographics, bool isUpdate, string storedProcName)
         {
              string newlyCreatedCustomerTypeID = objCustomerDemographics.CustomerTypeID;

              object customerDesc = objCustomerDemographics.CustomerDesc;

              if (String.IsNullOrEmpty(objCustomerDemographics.CustomerDesc))
                  customerDesc = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerTypeID", objCustomerDemographics.CustomerTypeID);
                      command.Parameters.AddWithValue("@customerDesc", customerDesc);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedCustomerTypeID = (string)command.ExecuteScalar();
                  }
              }

              return newlyCreatedCustomerTypeID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string customerTypeID)
         {
              string storedProcName = "[dbo].[CustomerDemographics_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerTypeID", customerTypeID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, string customerTypeID)
         {
              if(!String.IsNullOrEmpty(customerTypeID))
                  command.Parameters.AddWithValue("@customerTypeID", customerTypeID);
              else
                  command.Parameters.AddWithValue("@customerTypeID", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a CustomerDemographics object from the passed data row
         /// </summary>
         private static CustomerDemographics CreateCustomerDemographicsFromDataRowShared(DataRow dr)
         {
             CustomerDemographics objCustomerDemographics = new CustomerDemographics();

             objCustomerDemographics.CustomerTypeID = dr["CustomerTypeID"].ToString();

             if (dr["CustomerDesc"] != System.DBNull.Value)
                 objCustomerDemographics.CustomerDesc = dr["CustomerDesc"].ToString();
             else
                 objCustomerDemographics.CustomerDesc = null;

             return objCustomerDemographics;
         }
     }
}
