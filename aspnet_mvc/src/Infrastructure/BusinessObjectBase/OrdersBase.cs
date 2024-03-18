using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Orders.  Do not make changes to this class,
     /// instead, put additional code in the Orders class
     /// </summary>
     public class OrdersBase : north.Models.OrdersModel
     {
         /// <summary>
         /// Gets or sets the Related Customers.  Related to column CustomerID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Customers> Customers
         {
             get
             {
                 if(!String.IsNullOrEmpty(CustomerID))
                     return new Lazy<Customers>(() => BusinessObject.Customers.SelectByPrimaryKey(CustomerID));
                 else
                     return null;
             }
             set{ }
         } 

         /// <summary>
         /// Gets or sets the Related Employees.  Related to column EmployeeID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Employees> Employees
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(EmployeeID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Employees>(() => BusinessObject.Employees.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 

         /// <summary>
         /// Gets or sets the Related Shippers.  Related to column ShipVia
         /// </summary>
         [ScriptIgnore]
         public Lazy<Shippers> Shippers
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ShipVia.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Shippers>(() => BusinessObject.Shippers.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 


         /// <summary>
         /// Constructor
         /// </summary>
         public OrdersBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Orders SelectByPrimaryKey(int orderID)
         {
             return OrdersDataLayer.SelectByPrimaryKey(orderID);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table
         /// </summary>
         public static int GetRecordCount()
         {
             return OrdersDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by CustomerID
         /// </summary>
         public static int GetRecordCountByCustomerID(string customerID)
         {
             return OrdersDataLayer.GetRecordCountByCustomerID(customerID);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by EmployeeID
         /// </summary>
         public static int GetRecordCountByEmployeeID(int employeeID)
         {
             return OrdersDataLayer.GetRecordCountByEmployeeID(employeeID);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by ShipVia
         /// </summary>
         public static int GetRecordCountByShipVia(int shipVia)
         {
             return OrdersDataLayer.GetRecordCountByShipVia(shipVia);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
             return OrdersDataLayer.GetRecordCountDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return OrdersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static OrdersCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by CustomerID as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByCustomerID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, string customerID)
         {
             totalRowCount = OrdersDataLayer.GetRecordCountByCustomerID(customerID);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByCustomerID(sortByExpression, startRowIndex, rows, customerID);
         }

         /// <summary>
         /// Selects records by CustomerID as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByCustomerID(int rows, int startRowIndex, string sortByExpression, string customerID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByCustomerID(sortByExpression, startRowIndex, rows, customerID);
         }

         /// <summary>
         /// Selects records by EmployeeID as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByEmployeeID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int employeeID)
         {
             totalRowCount = OrdersDataLayer.GetRecordCountByEmployeeID(employeeID);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByEmployeeID(sortByExpression, startRowIndex, rows, employeeID);
         }

         /// <summary>
         /// Selects records by EmployeeID as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByEmployeeID(int rows, int startRowIndex, string sortByExpression, int employeeID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByEmployeeID(sortByExpression, startRowIndex, rows, employeeID);
         }

         /// <summary>
         /// Selects records by ShipVia as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByShipVia(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int shipVia)
         {
             totalRowCount = OrdersDataLayer.GetRecordCountByShipVia(shipVia);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByShipVia(sortByExpression, startRowIndex, rows, shipVia);
         }

         /// <summary>
         /// Selects records by ShipVia as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeByShipVia(int rows, int startRowIndex, string sortByExpression, int shipVia)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeByShipVia(sortByExpression, startRowIndex, rows, shipVia);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static OrdersCollection SelectSkipAndTakeDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrdersDataLayer.SelectSkipAndTakeDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. FreightTotal
         /// </summary>
         public static Orders SelectTotals()
         {
             return OrdersDataLayer.SelectTotals();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Orders
         /// </summary>
         public static OrdersCollection SelectAll()
         {
             return OrdersDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Orders sorted by the sort expression
         /// </summary>
         public static OrdersCollection SelectAll(string sortExpression)
         {
             OrdersCollection objOrdersCol = OrdersDataLayer.SelectAll();
             return SortByExpression(objOrdersCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Orders.
         /// </summary>
         public static OrdersCollection SelectAllDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
             return OrdersDataLayer.SelectAllDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Orders sorted by the sort expression.
         /// </summary>
         public static OrdersCollection SelectAllDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, string sortExpression)
         {
             OrdersCollection objOrdersCol = OrdersDataLayer.SelectAllDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
             return SortByExpression(objOrdersCol, sortExpression);
         }

         /// <summary>
         /// Selects all Orders by Customers, related to column CustomerID
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByCustomerID(string customerID)
         {
             return OrdersDataLayer.SelectOrdersCollectionByCustomerID(customerID);
         }

         /// <summary>
         /// Selects all Orders by Customers, related to column CustomerID, sorted by the sort expression
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByCustomerID(string customerID, string sortExpression)
         {
             OrdersCollection objOrdersCol = OrdersDataLayer.SelectOrdersCollectionByCustomerID(customerID);
             return SortByExpression(objOrdersCol, sortExpression);
         }

         /// <summary>
         /// Selects all Orders by Employees, related to column EmployeeID
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByEmployeeID(int employeeID)
         {
             return OrdersDataLayer.SelectOrdersCollectionByEmployeeID(employeeID);
         }

         /// <summary>
         /// Selects all Orders by Employees, related to column EmployeeID, sorted by the sort expression
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByEmployeeID(int employeeID, string sortExpression)
         {
             OrdersCollection objOrdersCol = OrdersDataLayer.SelectOrdersCollectionByEmployeeID(employeeID);
             return SortByExpression(objOrdersCol, sortExpression);
         }

         /// <summary>
         /// Selects all Orders by Shippers, related to column ShipVia
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByShipVia(int shipperID)
         {
             return OrdersDataLayer.SelectOrdersCollectionByShipVia(shipperID);
         }

         /// <summary>
         /// Selects all Orders by Shippers, related to column ShipVia, sorted by the sort expression
         /// </summary>
         public static OrdersCollection SelectOrdersCollectionByShipVia(int shipperID, string sortExpression)
         {
             OrdersCollection objOrdersCol = OrdersDataLayer.SelectOrdersCollectionByShipVia(shipperID);
             return SortByExpression(objOrdersCol, sortExpression);
         }

         /// <summary>
         /// Selects OrderID and ShipName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static OrdersCollection SelectOrdersDropDownListData()
         {
             return OrdersDataLayer.SelectOrdersDropDownListData();
         }

         /// <summary>
         /// Sorts the OrdersCollection by sort expression
         /// </summary>
         public static OrdersCollection SortByExpression(OrdersCollection objOrdersCol, string sortExpression)
         {
             bool isSortDescending = sortExpression.ToLower().Contains(" desc");

             if (isSortDescending)
             {
                 sortExpression = sortExpression.Replace(" DESC", "");
                 sortExpression = sortExpression.Replace(" desc", "");
             }
             else
             {
                 sortExpression = sortExpression.Replace(" ASC", "");
                 sortExpression = sortExpression.Replace(" asc", "");
             }

             switch (sortExpression)
             {
                 case "OrderID":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByOrderID);
                     break;
                 case "CustomerID":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByCustomerID);
                     break;
                 case "EmployeeID":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByEmployeeID);
                     break;
                 case "OrderDate":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByOrderDate);
                     break;
                 case "RequiredDate":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByRequiredDate);
                     break;
                 case "ShippedDate":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShippedDate);
                     break;
                 case "ShipVia":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipVia);
                     break;
                 case "Freight":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByFreight);
                     break;
                 case "ShipName":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipName);
                     break;
                 case "ShipAddress":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipAddress);
                     break;
                 case "ShipCity":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipCity);
                     break;
                 case "ShipRegion":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipRegion);
                     break;
                 case "ShipPostalCode":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipPostalCode);
                     break;
                 case "ShipCountry":
                     objOrdersCol.Sort(north.BusinessObject.Orders.ByShipCountry);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objOrdersCol.Reverse();

             return objOrdersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Orders objOrders = (Orders)this;
             return OrdersDataLayer.Insert(objOrders);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Orders objOrders = (Orders)this;
             OrdersDataLayer.Update(objOrders);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int orderID)
         {
             OrdersDataLayer.Delete(orderID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "OrderID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares OrderID used for sorting
         /// </summary>
         public static Comparison<Orders> ByOrderID = delegate(Orders x, Orders y)
         {
             return x.OrderID.CompareTo(y.OrderID);
         };

         /// <summary>
         /// Compares CustomerID used for sorting
         /// </summary>
         public static Comparison<Orders> ByCustomerID = delegate(Orders x, Orders y)
         {
             string value1 = x.CustomerID ?? String.Empty;
             string value2 = y.CustomerID ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares EmployeeID used for sorting
         /// </summary>
         public static Comparison<Orders> ByEmployeeID = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.EmployeeID, y.EmployeeID);
         };

         /// <summary>
         /// Compares OrderDate used for sorting
         /// </summary>
         public static Comparison<Orders> ByOrderDate = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.OrderDate, y.OrderDate);
         };

         /// <summary>
         /// Compares RequiredDate used for sorting
         /// </summary>
         public static Comparison<Orders> ByRequiredDate = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.RequiredDate, y.RequiredDate);
         };

         /// <summary>
         /// Compares ShippedDate used for sorting
         /// </summary>
         public static Comparison<Orders> ByShippedDate = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.ShippedDate, y.ShippedDate);
         };

         /// <summary>
         /// Compares ShipVia used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipVia = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.ShipVia, y.ShipVia);
         };

         /// <summary>
         /// Compares Freight used for sorting
         /// </summary>
         public static Comparison<Orders> ByFreight = delegate(Orders x, Orders y)
         {
             return Nullable.Compare(x.Freight, y.Freight);
         };

         /// <summary>
         /// Compares ShipName used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipName = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipName ?? String.Empty;
             string value2 = y.ShipName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ShipAddress used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipAddress = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipAddress ?? String.Empty;
             string value2 = y.ShipAddress ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ShipCity used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipCity = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipCity ?? String.Empty;
             string value2 = y.ShipCity ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ShipRegion used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipRegion = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipRegion ?? String.Empty;
             string value2 = y.ShipRegion ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ShipPostalCode used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipPostalCode = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipPostalCode ?? String.Empty;
             string value2 = y.ShipPostalCode ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ShipCountry used for sorting
         /// </summary>
         public static Comparison<Orders> ByShipCountry = delegate(Orders x, Orders y)
         {
             string value1 = x.ShipCountry ?? String.Empty;
             string value2 = y.ShipCountry ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
