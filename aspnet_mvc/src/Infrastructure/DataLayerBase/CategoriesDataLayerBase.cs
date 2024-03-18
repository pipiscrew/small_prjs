using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for CategoriesDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the CategoriesDataLayer class
     /// </summary>
     public class CategoriesDataLayerBase
     {
         // constructor
         public CategoriesDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Categories SelectByPrimaryKey(int categoryID)
         {
              Categories objCategories = null;
              string storedProcName = "[dbo].[Categories_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@categoryID", categoryID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCategories = CreateCategoriesFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objCategories;
         }

         /// <summary>
         /// Gets the total number of records in the Categories table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Categories_GetRecordCount]", null, null, true, null);
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
         /// Gets the total number of records in the Categories table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? categoryID, string categoryName)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Categories_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, categoryID, categoryName);

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
         /// Selects Categories records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static CategoriesCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Categories_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Categories records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static CategoriesCollection SelectSkipAndTakeDynamicWhere(int? categoryID, string categoryName, string sortByExpression, int startRowIndex, int rows)
         {
              CategoriesCollection objCategoriesCol = null;
              string storedProcName = "[dbo].[Categories_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, categoryID, categoryName);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCategoriesCol = new CategoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Categories objCategories = CreateCategoriesFromDataRowShared(dr);
                                      objCategoriesCol.Add(objCategories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCategoriesCol;
         }

         /// <summary>
         /// Selects all Categories
         /// </summary>
         public static CategoriesCollection SelectAll()
         {
             return SelectShared("[dbo].[Categories_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Categories.
         /// </summary>
         public static CategoriesCollection SelectAllDynamicWhere(int? categoryID, string categoryName)
         {
              CategoriesCollection objCategoriesCol = null;
              string storedProcName = "[dbo].[Categories_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, categoryID, categoryName);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCategoriesCol = new CategoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Categories objCategories = CreateCategoriesFromDataRowShared(dr);
                                      objCategoriesCol.Add(objCategories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCategoriesCol;
         }

         /// <summary>
         /// Selects CategoryID and CategoryName columns for use with a DropDownList web control
         /// </summary>
         public static CategoriesCollection SelectCategoriesDropDownListData()
         {
              CategoriesCollection objCategoriesCol = null;
              string storedProcName = "[dbo].[Categories_SelectDropDownListData]";

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
                                  objCategoriesCol = new CategoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Categories objCategories = new Categories();
                                      objCategories.CategoryID = (int)dr["CategoryID"];
                                      objCategories.CategoryName = (string)(dr["CategoryName"]);

                                      objCategoriesCol.Add(objCategories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCategoriesCol;
         }

         public static CategoriesCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              CategoriesCollection objCategoriesCol = null;

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
                                  objCategoriesCol = new CategoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Categories objCategories = CreateCategoriesFromDataRowShared(dr);
                                      objCategoriesCol.Add(objCategories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCategoriesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Categories objCategories)
         {
             string storedProcName = "[dbo].[Categories_Insert]";
             return InsertUpdate(objCategories, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Categories objCategories)
         {
             string storedProcName = "[dbo].[Categories_Update]";
             InsertUpdate(objCategories, true, storedProcName);
         }

         private static int InsertUpdate(Categories objCategories, bool isUpdate, string storedProcName)
         {
              int newlyCreatedCategoryID = objCategories.CategoryID;

              object description = objCategories.Description;

              if (String.IsNullOrEmpty(objCategories.Description))
                  description = System.DBNull.Value;

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
                          command.Parameters.AddWithValue("@categoryID", objCategories.CategoryID);
                      }

                      command.Parameters.AddWithValue("@categoryName", objCategories.CategoryName);
                      command.Parameters.AddWithValue("@description", description);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedCategoryID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedCategoryID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int categoryID)
         {
              string storedProcName = "[dbo].[Categories_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@categoryID", categoryID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? categoryID, string categoryName)
         {
              if(categoryID != null)
                  command.Parameters.AddWithValue("@categoryID", categoryID);
              else
                  command.Parameters.AddWithValue("@categoryID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(categoryName))
                  command.Parameters.AddWithValue("@categoryName", categoryName);
              else
                  command.Parameters.AddWithValue("@categoryName", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Categories object from the passed data row
         /// </summary>
         private static Categories CreateCategoriesFromDataRowShared(DataRow dr)
         {
             Categories objCategories = new Categories();

             objCategories.CategoryID = (int)dr["CategoryID"];
             objCategories.CategoryName = dr["CategoryName"].ToString();

             if (dr["Description"] != System.DBNull.Value)
                 objCategories.Description = dr["Description"].ToString();
             else
                 objCategories.Description = null;

             return objCategories;
         }
     }
}
