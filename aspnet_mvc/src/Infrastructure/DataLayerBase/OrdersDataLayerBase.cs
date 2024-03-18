using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for OrdersDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the OrdersDataLayer class
     /// </summary>
     public class OrdersDataLayerBase
     {
         // constructor
         public OrdersDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Orders SelectByPrimaryKey(int orderID)
         {
              Orders objOrders = null;
              string storedProcName = "[dbo].[Orders_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@orderID", orderID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrders = CreateOrdersFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objOrders;
         }

         /// <summary>
         /// Gets the total number of records in the Orders table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Orders_GetRecordCount]", null, null, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by CustomerID
         /// </summary>
         public static int GetRecordCountByCustomerID(string customerID)
         {
             return GetRecordCountShared("[dbo].[Orders_GetRecordCountByCustomerID]", "customerID", customerID, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by EmployeeID
         /// </summary>
         public static int GetRecordCountByEmployeeID(int employeeID)
         {
             return GetRecordCountShared("[dbo].[Orders_GetRecordCountByEmployeeID]", "employeeID", employeeID, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by ShipVia
         /// </summary>
         public static int GetRecordCountByShipVia(int shipVia)
         {
             return GetRecordCountShared("[dbo].[Orders_GetRecordCountByShipVia]", "shipVia", shipVia, true, null);
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
                          case "customerID":
                              command.Parameters.AddWithValue("@customerID", paramValue);
                              break;
                          case "employeeID":
                              command.Parameters.AddWithValue("@employeeID", paramValue);
                              break;
                          case "shipVia":
                              command.Parameters.AddWithValue("@shipVia", paramValue);
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
         /// Gets the total number of records in the Orders table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Orders_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);

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
         /// Selects Orders records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static OrdersCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Orders_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by CustomerID as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByCustomerID(string sortByExpression, int startRowIndex, int rows, string customerID)
         {
             return SelectShared("[dbo].[Orders_SelectSkipAndTakeByCustomerID]", "customerID", customerID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by EmployeeID as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByEmployeeID(string sortByExpression, int startRowIndex, int rows, int employeeID)
         {
             return SelectShared("[dbo].[Orders_SelectSkipAndTakeByEmployeeID]", "employeeID", employeeID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by ShipVia as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByShipVia(string sortByExpression, int startRowIndex, int rows, int shipVia)
         {
             return SelectShared("[dbo].[Orders_SelectSkipAndTakeByShipVia]", "shipVia", shipVia, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Orders records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, string sortByExpression, int startRowIndex, int rows)
         {
              OrdersCollection objOrdersCol = null;
              string storedProcName = "[dbo].[Orders_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrdersCol = new OrdersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Orders objOrders = CreateOrdersFromDataRowShared(dr);
                                      objOrdersCol.Add(objOrders);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrdersCol;
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money of decimal data type
         /// </summary>
         public static Orders SelectTotals()
         {
              Orders objOrders = null;
              string storedProcName = "[dbo].[Orders_SelectTotals]";

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
                                  if(dt.Rows[0]["FreightTotal"] != DBNull.Value)
                                       objOrders.FreightTotal = (decimal)dt.Rows[0]["FreightTotal"];
                              }
                          }
                      }
                  }
              }

              return objOrders;
         }

         /// <summary>
         /// Selects all Orders
         /// </summary>
         public static OrdersCollection SelectAll()
         {
             return SelectShared("[dbo].[Orders_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Orders.
         /// </summary>
         public static OrdersCollection SelectAllDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
              OrdersCollection objOrdersCol = null;
              string storedProcName = "[dbo].[Orders_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objOrdersCol = new OrdersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Orders objOrders = CreateOrdersFromDataRowShared(dr);
                                      objOrdersCol.Add(objOrders);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrdersCol;
         }

         /// <summary>
         /// Selects all Orders by Customers, related to column CustomerID
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByCustomerID(string customerID)
         {
             return SelectShared("[dbo].[Orders_SelectAllByCustomerID]", "customerID", customerID);
         }

         /// <summary>
         /// Selects all Orders by Employees, related to column EmployeeID
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByEmployeeID(int employeeID)
         {
             return SelectShared("[dbo].[Orders_SelectAllByEmployeeID]", "employeeID", employeeID);
         }

         /// <summary>
         /// Selects all Orders by Shippers, related to column ShipVia
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByShipVia(int shipperID)
         {
             return SelectShared("[dbo].[Orders_SelectAllByShipVia]", "shipVia", shipperID);
         }

         /// <summary>
         /// Selects OrderID and ShipName columns for use with a DropDownList web control
         /// </summary>
         public static OrdersCollection SelectOrdersDropDownListData()
         {
              OrdersCollection objOrdersCol = null;
              string storedProcName = "[dbo].[Orders_SelectDropDownListData]";

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
                                  objOrdersCol = new OrdersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Orders objOrders = new Orders();
                                      objOrders.OrderID = (int)dr["OrderID"];

                                      if (dr["ShipName"] != System.DBNull.Value)
                                          objOrders.ShipName = (string)(dr["ShipName"]);
                                      else
                                          objOrders.ShipName = null;

                                      objOrdersCol.Add(objOrders);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrdersCol;
         }

         public static OrdersCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              OrdersCollection objOrdersCol = null;

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
                          case "customerID":
                              command.Parameters.AddWithValue("@customerID", paramValue);
                              break;
                          case "employeeID":
                              command.Parameters.AddWithValue("@employeeID", paramValue);
                              break;
                          case "shipVia":
                              command.Parameters.AddWithValue("@shipVia", paramValue);
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
                                  objOrdersCol = new OrdersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Orders objOrders = CreateOrdersFromDataRowShared(dr);
                                      objOrdersCol.Add(objOrders);
                                  }
                              }
                          }
                      }
                  }
              }

              return objOrdersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Orders objOrders)
         {
             string storedProcName = "[dbo].[Orders_Insert]";
             return InsertUpdate(objOrders, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Orders objOrders)
         {
             string storedProcName = "[dbo].[Orders_Update]";
             InsertUpdate(objOrders, true, storedProcName);
         }

         private static int InsertUpdate(Orders objOrders, bool isUpdate, string storedProcName)
         {
              int newlyCreatedOrderID = objOrders.OrderID;

              object customerID = objOrders.CustomerID;
              object employeeID = objOrders.EmployeeID;
              object orderDate = objOrders.OrderDate;
              object requiredDate = objOrders.RequiredDate;
              object shippedDate = objOrders.ShippedDate;
              object shipVia = objOrders.ShipVia;
              object freight = objOrders.Freight;
              object shipName = objOrders.ShipName;
              object shipAddress = objOrders.ShipAddress;
              object shipCity = objOrders.ShipCity;
              object shipRegion = objOrders.ShipRegion;
              object shipPostalCode = objOrders.ShipPostalCode;
              object shipCountry = objOrders.ShipCountry;

              if (String.IsNullOrEmpty(objOrders.CustomerID))
                  customerID = System.DBNull.Value;

              if (objOrders.EmployeeID == null)
                  employeeID = System.DBNull.Value;

              if (objOrders.OrderDate == null)
                  orderDate = System.DBNull.Value;

              if (objOrders.RequiredDate == null)
                  requiredDate = System.DBNull.Value;

              if (objOrders.ShippedDate == null)
                  shippedDate = System.DBNull.Value;

              if (objOrders.ShipVia == null)
                  shipVia = System.DBNull.Value;

              if (objOrders.Freight == null)
                  freight = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipName))
                  shipName = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipAddress))
                  shipAddress = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipCity))
                  shipCity = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipRegion))
                  shipRegion = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipPostalCode))
                  shipPostalCode = System.DBNull.Value;

              if (String.IsNullOrEmpty(objOrders.ShipCountry))
                  shipCountry = System.DBNull.Value;

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
                          command.Parameters.AddWithValue("@orderID", objOrders.OrderID);
                      }

                      command.Parameters.AddWithValue("@customerID", customerID);
                      command.Parameters.AddWithValue("@employeeID", employeeID);
                      command.Parameters.AddWithValue("@orderDate", orderDate);
                      command.Parameters.AddWithValue("@requiredDate", requiredDate);
                      command.Parameters.AddWithValue("@shippedDate", shippedDate);
                      command.Parameters.AddWithValue("@shipVia", shipVia);
                      command.Parameters.AddWithValue("@freight", freight);
                      command.Parameters.AddWithValue("@shipName", shipName);
                      command.Parameters.AddWithValue("@shipAddress", shipAddress);
                      command.Parameters.AddWithValue("@shipCity", shipCity);
                      command.Parameters.AddWithValue("@shipRegion", shipRegion);
                      command.Parameters.AddWithValue("@shipPostalCode", shipPostalCode);
                      command.Parameters.AddWithValue("@shipCountry", shipCountry);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedOrderID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedOrderID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int orderID)
         {
              string storedProcName = "[dbo].[Orders_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@orderID", orderID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
              if(orderID != null)
                  command.Parameters.AddWithValue("@orderID", orderID);
              else
                  command.Parameters.AddWithValue("@orderID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(customerID))
                  command.Parameters.AddWithValue("@customerID", customerID);
              else
                  command.Parameters.AddWithValue("@customerID", System.DBNull.Value);

              if(employeeID != null)
                  command.Parameters.AddWithValue("@employeeID", employeeID);
              else
                  command.Parameters.AddWithValue("@employeeID", System.DBNull.Value);

              if(orderDate != null)
                  command.Parameters.AddWithValue("@orderDate", orderDate);
              else
                  command.Parameters.AddWithValue("@orderDate", System.DBNull.Value);

              if(requiredDate != null)
                  command.Parameters.AddWithValue("@requiredDate", requiredDate);
              else
                  command.Parameters.AddWithValue("@requiredDate", System.DBNull.Value);

              if(shippedDate != null)
                  command.Parameters.AddWithValue("@shippedDate", shippedDate);
              else
                  command.Parameters.AddWithValue("@shippedDate", System.DBNull.Value);

              if(shipVia != null)
                  command.Parameters.AddWithValue("@shipVia", shipVia);
              else
                  command.Parameters.AddWithValue("@shipVia", System.DBNull.Value);

              if(freight != null)
                  command.Parameters.AddWithValue("@freight", freight);
              else
                  command.Parameters.AddWithValue("@freight", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipName))
                  command.Parameters.AddWithValue("@shipName", shipName);
              else
                  command.Parameters.AddWithValue("@shipName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipAddress))
                  command.Parameters.AddWithValue("@shipAddress", shipAddress);
              else
                  command.Parameters.AddWithValue("@shipAddress", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipCity))
                  command.Parameters.AddWithValue("@shipCity", shipCity);
              else
                  command.Parameters.AddWithValue("@shipCity", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipRegion))
                  command.Parameters.AddWithValue("@shipRegion", shipRegion);
              else
                  command.Parameters.AddWithValue("@shipRegion", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipPostalCode))
                  command.Parameters.AddWithValue("@shipPostalCode", shipPostalCode);
              else
                  command.Parameters.AddWithValue("@shipPostalCode", System.DBNull.Value);

              if(!String.IsNullOrEmpty(shipCountry))
                  command.Parameters.AddWithValue("@shipCountry", shipCountry);
              else
                  command.Parameters.AddWithValue("@shipCountry", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Orders object from the passed data row
         /// </summary>
         private static Orders CreateOrdersFromDataRowShared(DataRow dr)
         {
             Orders objOrders = new Orders();

             objOrders.OrderID = (int)dr["OrderID"];

             if (dr["CustomerID"] != System.DBNull.Value)
                 objOrders.CustomerID = dr["CustomerID"].ToString();
             else
                 objOrders.CustomerID = null;


             if (dr["EmployeeID"] != System.DBNull.Value)
                 objOrders.EmployeeID = (int)dr["EmployeeID"];
             else
                 objOrders.EmployeeID = null;


             if (dr["OrderDate"] != System.DBNull.Value)
                 objOrders.OrderDate = (DateTime)dr["OrderDate"];
             else
                 objOrders.OrderDate = null;

             if (dr["RequiredDate"] != System.DBNull.Value)
                 objOrders.RequiredDate = (DateTime)dr["RequiredDate"];
             else
                 objOrders.RequiredDate = null;

             if (dr["ShippedDate"] != System.DBNull.Value)
                 objOrders.ShippedDate = (DateTime)dr["ShippedDate"];
             else
                 objOrders.ShippedDate = null;

             if (dr["ShipVia"] != System.DBNull.Value)
                 objOrders.ShipVia = (int)dr["ShipVia"];
             else
                 objOrders.ShipVia = null;


             if (dr["Freight"] != System.DBNull.Value)
                 objOrders.Freight = (decimal)dr["Freight"];
             else
                 objOrders.Freight = null;

             if (dr["ShipName"] != System.DBNull.Value)
                 objOrders.ShipName = dr["ShipName"].ToString();
             else
                 objOrders.ShipName = null;

             if (dr["ShipAddress"] != System.DBNull.Value)
                 objOrders.ShipAddress = dr["ShipAddress"].ToString();
             else
                 objOrders.ShipAddress = null;

             if (dr["ShipCity"] != System.DBNull.Value)
                 objOrders.ShipCity = dr["ShipCity"].ToString();
             else
                 objOrders.ShipCity = null;

             if (dr["ShipRegion"] != System.DBNull.Value)
                 objOrders.ShipRegion = dr["ShipRegion"].ToString();
             else
                 objOrders.ShipRegion = null;

             if (dr["ShipPostalCode"] != System.DBNull.Value)
                 objOrders.ShipPostalCode = dr["ShipPostalCode"].ToString();
             else
                 objOrders.ShipPostalCode = null;

             if (dr["ShipCountry"] != System.DBNull.Value)
                 objOrders.ShipCountry = dr["ShipCountry"].ToString();
             else
                 objOrders.ShipCountry = null;

             return objOrders;
         }
     }
}
