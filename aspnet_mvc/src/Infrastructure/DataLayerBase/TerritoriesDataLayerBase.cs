using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for TerritoriesDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesDataLayer class
     /// </summary>
     public class TerritoriesDataLayerBase
     {
         // constructor
         public TerritoriesDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Territories SelectByPrimaryKey(string territoryID)
         {
              Territories objTerritories = null;
              string storedProcName = "[dbo].[Territories_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@territoryID", territoryID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTerritories = CreateTerritoriesFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objTerritories;
         }

         /// <summary>
         /// Gets the total number of records in the Territories table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Territories_GetRecordCount]", null, null, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Territories table by RegionID
         /// </summary>
         public static int GetRecordCountByRegionID(int regionID)
         {
             return GetRecordCountShared("[dbo].[Territories_GetRecordCountByRegionID]", "regionID", regionID, true, null);
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

                      // parameters
                      switch (param)
                      {
                          case "regionID":
                              command.Parameters.AddWithValue("@regionID", paramValue);
                              break;
                          default:
                              break;
                      }

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
         /// Gets the total number of records in the Territories table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Territories_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, territoryID, territoryDescription, regionID);

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
         /// Selects Territories records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Territories_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by RegionID as a collection (List) of Territories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeByRegionID(string sortByExpression, int startRowIndex, int rows, int regionID)
         {
             return SelectShared("[dbo].[Territories_SelectSkipAndTakeByRegionID]", "regionID", regionID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Territories records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeDynamicWhere(string territoryID, string territoryDescription, int? regionID, string sortByExpression, int startRowIndex, int rows)
         {
              TerritoriesCollection objTerritoriesCol = null;
              string storedProcName = "[dbo].[Territories_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, territoryID, territoryDescription, regionID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTerritoriesCol = new TerritoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Territories objTerritories = CreateTerritoriesFromDataRowShared(dr);
                                      objTerritoriesCol.Add(objTerritories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTerritoriesCol;
         }

         /// <summary>
         /// Selects all Territories
         /// </summary>
         public static TerritoriesCollection SelectAll()
         {
             return SelectShared("[dbo].[Territories_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Territories.
         /// </summary>
         public static TerritoriesCollection SelectAllDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
              TerritoriesCollection objTerritoriesCol = null;
              string storedProcName = "[dbo].[Territories_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, territoryID, territoryDescription, regionID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTerritoriesCol = new TerritoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Territories objTerritories = CreateTerritoriesFromDataRowShared(dr);
                                      objTerritoriesCol.Add(objTerritories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTerritoriesCol;
         }

         /// <summary>
         /// Selects all Territories by Region, related to column RegionID
         /// </summary>
         public static TerritoriesCollection SelectTerritoriesCollectionByRegionID(int regionID)
         {
             return SelectShared("[dbo].[Territories_SelectAllByRegionID]", "regionID", regionID);
         }

         /// <summary>
         /// Selects TerritoryID and TerritoryDescription columns for use with a DropDownList web control
         /// </summary>
         public static TerritoriesCollection SelectTerritoriesDropDownListData()
         {
              TerritoriesCollection objTerritoriesCol = null;
              string storedProcName = "[dbo].[Territories_SelectDropDownListData]";

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
                                  objTerritoriesCol = new TerritoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Territories objTerritories = new Territories();
                                      objTerritories.TerritoryID = (string)dr["TerritoryID"];
                                      objTerritories.TerritoryDescription = (string)(dr["TerritoryDescription"]);

                                      objTerritoriesCol.Add(objTerritories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTerritoriesCol;
         }

         public static TerritoriesCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              TerritoriesCollection objTerritoriesCol = null;

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

                      // parameters
                      switch (param)
                      {
                          case "regionID":
                              command.Parameters.AddWithValue("@regionID", paramValue);
                              break;
                          default:
                              break;
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objTerritoriesCol = new TerritoriesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Territories objTerritories = CreateTerritoriesFromDataRowShared(dr);
                                      objTerritoriesCol.Add(objTerritories);
                                  }
                              }
                          }
                      }
                  }
              }

              return objTerritoriesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static string Insert(Territories objTerritories)
         {
             string storedProcName = "[dbo].[Territories_Insert]";
             return InsertUpdate(objTerritories, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Territories objTerritories)
         {
             string storedProcName = "[dbo].[Territories_Update]";
             InsertUpdate(objTerritories, true, storedProcName);
         }

         private static string InsertUpdate(Territories objTerritories, bool isUpdate, string storedProcName)
         {
              string newlyCreatedTerritoryID = objTerritories.TerritoryID;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@territoryID", objTerritories.TerritoryID);
                      command.Parameters.AddWithValue("@territoryDescription", objTerritories.TerritoryDescription);
                      command.Parameters.AddWithValue("@regionID", objTerritories.RegionID);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedTerritoryID = (string)command.ExecuteScalar();
                  }
              }

              return newlyCreatedTerritoryID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string territoryID)
         {
              string storedProcName = "[dbo].[Territories_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@territoryID", territoryID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, string territoryID, string territoryDescription, int? regionID)
         {
              if(!String.IsNullOrEmpty(territoryID))
                  command.Parameters.AddWithValue("@territoryID", territoryID);
              else
                  command.Parameters.AddWithValue("@territoryID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(territoryDescription))
                  command.Parameters.AddWithValue("@territoryDescription", territoryDescription);
              else
                  command.Parameters.AddWithValue("@territoryDescription", System.DBNull.Value);

              if(regionID != null)
                  command.Parameters.AddWithValue("@regionID", regionID);
              else
                  command.Parameters.AddWithValue("@regionID", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Territories object from the passed data row
         /// </summary>
         private static Territories CreateTerritoriesFromDataRowShared(DataRow dr)
         {
             Territories objTerritories = new Territories();

             objTerritories.TerritoryID = dr["TerritoryID"].ToString();
             objTerritories.TerritoryDescription = dr["TerritoryDescription"].ToString();
             objTerritories.RegionID = (int)dr["RegionID"];

             return objTerritories;
         }
     }
}
