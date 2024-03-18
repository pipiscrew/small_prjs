using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for RegionDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the RegionDataLayer class
     /// </summary>
     public class RegionDataLayerBase
     {
         // constructor
         public RegionDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Region SelectByPrimaryKey(int regionID)
         {
              Region objRegion = null;
              string storedProcName = "[dbo].[Region_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@regionID", regionID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objRegion = CreateRegionFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objRegion;
         }

         /// <summary>
         /// Gets the total number of records in the Region table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Region_GetRecordCount]", null, null, true, null);
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
         /// Gets the total number of records in the Region table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? regionID, string regionDescription)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Region_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, regionID, regionDescription);

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
         /// Selects Region records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static RegionCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Region_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Region records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static RegionCollection SelectSkipAndTakeDynamicWhere(int? regionID, string regionDescription, string sortByExpression, int startRowIndex, int rows)
         {
              RegionCollection objRegionCol = null;
              string storedProcName = "[dbo].[Region_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, regionID, regionDescription);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objRegionCol = new RegionCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Region objRegion = CreateRegionFromDataRowShared(dr);
                                      objRegionCol.Add(objRegion);
                                  }
                              }
                          }
                      }
                  }
              }

              return objRegionCol;
         }

         /// <summary>
         /// Selects all Region
         /// </summary>
         public static RegionCollection SelectAll()
         {
             return SelectShared("[dbo].[Region_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Region.
         /// </summary>
         public static RegionCollection SelectAllDynamicWhere(int? regionID, string regionDescription)
         {
              RegionCollection objRegionCol = null;
              string storedProcName = "[dbo].[Region_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, regionID, regionDescription);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objRegionCol = new RegionCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Region objRegion = CreateRegionFromDataRowShared(dr);
                                      objRegionCol.Add(objRegion);
                                  }
                              }
                          }
                      }
                  }
              }

              return objRegionCol;
         }

         /// <summary>
         /// Selects RegionID and RegionDescription columns for use with a DropDownList web control
         /// </summary>
         public static RegionCollection SelectRegionDropDownListData()
         {
              RegionCollection objRegionCol = null;
              string storedProcName = "[dbo].[Region_SelectDropDownListData]";

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
                                  objRegionCol = new RegionCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Region objRegion = new Region();
                                      objRegion.RegionID = (int)dr["RegionID"];
                                      objRegion.RegionDescription = (string)(dr["RegionDescription"]);

                                      objRegionCol.Add(objRegion);
                                  }
                              }
                          }
                      }
                  }
              }

              return objRegionCol;
         }

         public static RegionCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              RegionCollection objRegionCol = null;

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
                                  objRegionCol = new RegionCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Region objRegion = CreateRegionFromDataRowShared(dr);
                                      objRegionCol.Add(objRegion);
                                  }
                              }
                          }
                      }
                  }
              }

              return objRegionCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Region objRegion)
         {
             string storedProcName = "[dbo].[Region_Insert]";
             return InsertUpdate(objRegion, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Region objRegion)
         {
             string storedProcName = "[dbo].[Region_Update]";
             InsertUpdate(objRegion, true, storedProcName);
         }

         private static int InsertUpdate(Region objRegion, bool isUpdate, string storedProcName)
         {
              int newlyCreatedRegionID = objRegion.RegionID;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@regionID", objRegion.RegionID);
                      command.Parameters.AddWithValue("@regionDescription", objRegion.RegionDescription);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedRegionID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedRegionID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int regionID)
         {
              string storedProcName = "[dbo].[Region_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@regionID", regionID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? regionID, string regionDescription)
         {
              if(regionID != null)
                  command.Parameters.AddWithValue("@regionID", regionID);
              else
                  command.Parameters.AddWithValue("@regionID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(regionDescription))
                  command.Parameters.AddWithValue("@regionDescription", regionDescription);
              else
                  command.Parameters.AddWithValue("@regionDescription", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Region object from the passed data row
         /// </summary>
         private static Region CreateRegionFromDataRowShared(DataRow dr)
         {
             Region objRegion = new Region();

             objRegion.RegionID = (int)dr["RegionID"];
             objRegion.RegionDescription = dr["RegionDescription"].ToString();

             return objRegion;
         }
     }
}
