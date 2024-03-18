using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for OrderDetails.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetails class
     /// </summary>
     public class OrderDetailsBase : north.Models.OrderDetailsModel
     {
         /// <summary>
         /// Gets or sets the Related Orders.  Related to column OrderID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Orders> Orders
         {
             get
             {
                 return new Lazy<Orders>(() => BusinessObject.Orders.SelectByPrimaryKey(OrderID));
             }
             set{ }
         } 

         /// <summary>
         /// Gets or sets the Related Products.  Related to column ProductID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Products> Products
         {
             get
             {
                 return new Lazy<Products>(() => BusinessObject.Products.SelectByPrimaryKey(ProductID));
             }
             set{ }
         } 


         /// <summary>
         /// Constructor
         /// </summary>
         public OrderDetailsBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static OrderDetails SelectByPrimaryKey(int orderID, int productID)
         {
             return OrderDetailsDataLayer.SelectByPrimaryKey(orderID, productID);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table
         /// </summary>
         public static int GetRecordCount()
         {
             return OrderDetailsDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by OrderID
         /// </summary>
         public static int GetRecordCountByOrderID(int orderID)
         {
             return OrderDetailsDataLayer.GetRecordCountByOrderID(orderID);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by ProductID
         /// </summary>
         public static int GetRecordCountByProductID(int productID)
         {
             return OrderDetailsDataLayer.GetRecordCountByProductID(productID);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
             return OrderDetailsDataLayer.GetRecordCountDynamicWhere(orderID, productID, unitPrice, quantity, discount);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return OrderDetailsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by OrderID as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByOrderID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int orderID)
         {
             totalRowCount = OrderDetailsDataLayer.GetRecordCountByOrderID(orderID);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeByOrderID(sortByExpression, startRowIndex, rows, orderID);
         }

         /// <summary>
         /// Selects records by OrderID as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByOrderID(int rows, int startRowIndex, string sortByExpression, int orderID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeByOrderID(sortByExpression, startRowIndex, rows, orderID);
         }

         /// <summary>
         /// Selects records by ProductID as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByProductID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int productID)
         {
             totalRowCount = OrderDetailsDataLayer.GetRecordCountByProductID(productID);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeByProductID(sortByExpression, startRowIndex, rows, productID);
         }

         /// <summary>
         /// Selects records by ProductID as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeByProductID(int rows, int startRowIndex, string sortByExpression, int productID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeByProductID(sortByExpression, startRowIndex, rows, productID);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(orderID, productID, unitPrice, quantity, discount);
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeDynamicWhere(orderID, productID, unitPrice, quantity, discount, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static OrderDetailsCollection SelectSkipAndTakeDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return OrderDetailsDataLayer.SelectSkipAndTakeDynamicWhere(orderID, productID, unitPrice, quantity, discount, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. UnitPriceTotal
         /// </summary>
         public static OrderDetails SelectTotals()
         {
             return OrderDetailsDataLayer.SelectTotals();
         }

         /// <summary>
         /// Selects all records as a collection (List) of OrderDetails
         /// </summary>
         public static OrderDetailsCollection SelectAll()
         {
             return OrderDetailsDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of OrderDetails sorted by the sort expression
         /// </summary>
         public static OrderDetailsCollection SelectAll(string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetailsDataLayer.SelectAll();
             return SortByExpression(objOrderDetailsCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of OrderDetails.
         /// </summary>
         public static OrderDetailsCollection SelectAllDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
             return OrderDetailsDataLayer.SelectAllDynamicWhere(orderID, productID, unitPrice, quantity, discount);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of OrderDetails sorted by the sort expression.
         /// </summary>
         public static OrderDetailsCollection SelectAllDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount, string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetailsDataLayer.SelectAllDynamicWhere(orderID, productID, unitPrice, quantity, discount);
             return SortByExpression(objOrderDetailsCol, sortExpression);
         }

         /// <summary>
         /// Selects all OrderDetails by Orders, related to column OrderID
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByOrderID(int orderID)
         {
             return OrderDetailsDataLayer.SelectOrderDetailsCollectionByOrderID(orderID);
         }

         /// <summary>
         /// Selects all OrderDetails by Orders, related to column OrderID, sorted by the sort expression
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByOrderID(int orderID, string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetailsDataLayer.SelectOrderDetailsCollectionByOrderID(orderID);
             return SortByExpression(objOrderDetailsCol, sortExpression);
         }

         /// <summary>
         /// Selects all OrderDetails by Products, related to column ProductID
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByProductID(int productID)
         {
             return OrderDetailsDataLayer.SelectOrderDetailsCollectionByProductID(productID);
         }

         /// <summary>
         /// Selects all OrderDetails by Products, related to column ProductID, sorted by the sort expression
         /// </summary>
         public static OrderDetailsCollection SelectOrderDetailsCollectionByProductID(int productID, string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetailsDataLayer.SelectOrderDetailsCollectionByProductID(productID);
             return SortByExpression(objOrderDetailsCol, sortExpression);
         }

         /// <summary>
         /// Sorts the OrderDetailsCollection by sort expression
         /// </summary>
         public static OrderDetailsCollection SortByExpression(OrderDetailsCollection objOrderDetailsCol, string sortExpression)
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
                     objOrderDetailsCol.Sort(north.BusinessObject.OrderDetails.ByOrderID);
                     break;
                 case "ProductID":
                     objOrderDetailsCol.Sort(north.BusinessObject.OrderDetails.ByProductID);
                     break;
                 case "UnitPrice":
                     objOrderDetailsCol.Sort(north.BusinessObject.OrderDetails.ByUnitPrice);
                     break;
                 case "Quantity":
                     objOrderDetailsCol.Sort(north.BusinessObject.OrderDetails.ByQuantity);
                     break;
                 case "Discount":
                     objOrderDetailsCol.Sort(north.BusinessObject.OrderDetails.ByDiscount);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objOrderDetailsCol.Reverse();

             return objOrderDetailsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public void Insert()
         {
             OrderDetails objOrderDetails = (OrderDetails)this;
             OrderDetailsDataLayer.Insert(objOrderDetails);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             OrderDetails objOrderDetails = (OrderDetails)this;
             OrderDetailsDataLayer.Update(objOrderDetails);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int orderID, int productID)
         {
             OrderDetailsDataLayer.Delete(orderID, productID);
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
         public static Comparison<OrderDetails> ByOrderID = delegate(OrderDetails x, OrderDetails y)
         {
             return x.OrderID.CompareTo(y.OrderID);
         };

         /// <summary>
         /// Compares ProductID used for sorting
         /// </summary>
         public static Comparison<OrderDetails> ByProductID = delegate(OrderDetails x, OrderDetails y)
         {
             return x.ProductID.CompareTo(y.ProductID);
         };

         /// <summary>
         /// Compares UnitPrice used for sorting
         /// </summary>
         public static Comparison<OrderDetails> ByUnitPrice = delegate(OrderDetails x, OrderDetails y)
         {
             return x.UnitPrice.CompareTo(y.UnitPrice);
         };

         /// <summary>
         /// Compares Quantity used for sorting
         /// </summary>
         public static Comparison<OrderDetails> ByQuantity = delegate(OrderDetails x, OrderDetails y)
         {
             return x.Quantity.CompareTo(y.Quantity);
         };

         /// <summary>
         /// Compares Discount used for sorting
         /// </summary>
         public static Comparison<OrderDetails> ByDiscount = delegate(OrderDetails x, OrderDetails y)
         {
             return x.Discount.CompareTo(y.Discount);
         };

     }
}
