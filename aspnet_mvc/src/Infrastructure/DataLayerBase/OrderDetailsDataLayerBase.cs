using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for OrderDetailsDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsDataLayer class
     /// </summary>
     public class OrderDetailsDataLayerBase
     {
         // constructor
         public OrderDetailsDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static OrderDetails SelectByPrimaryKey(int orderID, int productID)
         {
              OrderDetails objOrderDetails = null;
              string storedProcName = "[dbo].[OrderDetails_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@orderID", orderID);
                      command.Parameters.AddWithValue("@productID", productID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrderDetails = CreateOrderDetailsFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objOrderDetails;
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[OrderDetails_GetRecordCount]", null, null, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by OrderID
         /// </summary>
         public static int GetRecordCountByOrderID(int orderID)
         {
             return GetRecordCountShared("[dbo].[OrderDetails_GetRecordCountByOrderID]", "orderID", orderID, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by ProductID
         /// </summary>
         public static int GetRecordCountByProductID(int productID)
         {
             return GetRecordCountShared("[dbo].[OrderDetails_GetRecordCountByProductID]", "productID", productID, true, null);
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
                          case "orderID":
                              command.Parameters.AddWithValue("@orderID", paramValue);
                              break;
                          case "productID":
                              command.Parameters.AddWithValue("@productID", paramValue);
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
         /// Gets the total number of records in the OrderDetails table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[OrderDetails_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, orderID, productID, unitPrice, quantity, discount);

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
         /// Selects OrderDetails records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[OrderDetails_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by OrderID as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByOrderID(string sortByExpression, int startRowIndex, int rows, int orderID)
         {
             return SelectShared("[dbo].[OrderDetails_SelectSkipAndTakeByOrderID]", "orderID", orderID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by ProductID as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByProductID(string sortByExpression, int startRowIndex, int rows, int productID)
         {
             return SelectShared("[dbo].[OrderDetails_SelectSkipAndTakeByProductID]", "productID", productID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects OrderDetails records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount, string sortByExpression, int startRowIndex, int rows)
         {
              OrderDetailsCollection objOrderDetailsCol = null;
              string storedProcName = "[dbo].[OrderDetails_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, orderID, productID, unitPrice, quantity, discount);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrderDetailsCol = new OrderDetailsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      OrderDetails objOrderDetails = CreateOrderDetailsFromDataRowShared(dr);
                                      objOrderDetailsCol.Add(objOrderDetails);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrderDetailsCol;
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money of decimal data type
         /// </summary>
         public static OrderDetails SelectTotals()
         {
              OrderDetails objOrderDetails = null;
              string storedProcName = "[dbo].[OrderDetails_SelectTotals]";

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
                                  if(dt.Rows[0]["UnitPriceTotal"] != DBNull.Value)
                                       objOrderDetails.UnitPriceTotal = (decimal)dt.Rows[0]["UnitPriceTotal"];
                              }
                          }
                      }
                  }
              }

              return objOrderDetails;
         }

         /// <summary>
         /// Selects all OrderDetails
         /// </summary>
         public static OrderDetailsCollection SelectAll()
         {
             return SelectShared("[dbo].[OrderDetails_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of OrderDetails.
         /// </summary>
         public static OrderDetailsCollection SelectAllDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
              OrderDetailsCollection objOrderDetailsCol = null;
              string storedProcName = "[dbo].[OrderDetails_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, orderID, productID, unitPrice, quantity, discount);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrderDetailsCol = new OrderDetailsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      OrderDetails objOrderDetails = CreateOrderDetailsFromDataRowShared(dr);
                                      objOrderDetailsCol.Add(objOrderDetails);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrderDetailsCol;
         }

         /// <summary>
         /// Selects all OrderDetails by Orders, related to column OrderID
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByOrderID(int orderID)
         {
             return SelectShared("[dbo].[OrderDetails_SelectAllByOrderID]", "orderID", orderID);
         }

         /// <summary>
         /// Selects all OrderDetails by Products, related to column ProductID
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByProductID(int productID)
         {
             return SelectShared("[dbo].[OrderDetails_SelectAllByProductID]", "productID", productID);
         }

         public static OrderDetailsCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              OrderDetailsCollection objOrderDetailsCol = null;

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
                          case "orderID":
                              command.Parameters.AddWithValue("@orderID", paramValue);
                              break;
                          case "productID":
                              command.Parameters.AddWithValue("@productID", paramValue);
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
                                  objOrderDetailsCol = new OrderDetailsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      OrderDetails objOrderDetails = CreateOrderDetailsFromDataRowShared(dr);
                                      objOrderDetailsCol.Add(objOrderDetails);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrderDetailsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static void Insert(OrderDetails objOrderDetails)
         {
             string storedProcName = "[dbo].[OrderDetails_Insert]";
             InsertUpdate(objOrderDetails, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(OrderDetails objOrderDetails)
         {
             string storedProcName = "[dbo].[OrderDetails_Update]";
             InsertUpdate(objOrderDetails, true, storedProcName);
         }

         private static void InsertUpdate(OrderDetails objOrderDetails, bool isUpdate, string storedProcName)
         {
              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@orderID", objOrderDetails.OrderID);
                      command.Parameters.AddWithValue("@productID", objOrderDetails.ProductID);
                      command.Parameters.AddWithValue("@unitPrice", objOrderDetails.UnitPrice);
                      command.Parameters.AddWithValue("@quantity", objOrderDetails.Quantity);
                      command.Parameters.AddWithValue("@discount", objOrderDetails.Discount);

                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int orderID, int productID)
         {
              string storedProcName = "[dbo].[OrderDetails_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@orderID", orderID);
                      command.Parameters.AddWithValue("@productID", productID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
              if(orderID != null)
                  command.Parameters.AddWithValue("@orderID", orderID);
              else
                  command.Parameters.AddWithValue("@orderID", System.DBNull.Value);

              if(productID != null)
                  command.Parameters.AddWithValue("@productID", productID);
              else
                  command.Parameters.AddWithValue("@productID", System.DBNull.Value);

              if(unitPrice != null)
                  command.Parameters.AddWithValue("@unitPrice", unitPrice);
              else
                  command.Parameters.AddWithValue("@unitPrice", System.DBNull.Value);

              if(quantity != null)
                  command.Parameters.AddWithValue("@quantity", quantity);
              else
                  command.Parameters.AddWithValue("@quantity", System.DBNull.Value);

              if(discount != null)
                  command.Parameters.AddWithValue("@discount", discount);
              else
                  command.Parameters.AddWithValue("@discount", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a OrderDetails object from the passed data row
         /// </summary>
         private static OrderDetails CreateOrderDetailsFromDataRowShared(DataRow dr)
         {
             OrderDetails objOrderDetails = new OrderDetails();

             objOrderDetails.OrderID = (int)dr["OrderID"];
             objOrderDetails.ProductID = (int)dr["ProductID"];
             objOrderDetails.UnitPrice = (decimal)dr["UnitPrice"];
             objOrderDetails.Quantity = (Int16)dr["Quantity"];
             objOrderDetails.Discount = (Single)dr["Discount"];

             return objOrderDetails;
         }
     }
}
