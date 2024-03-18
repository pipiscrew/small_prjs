using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Shippers.  Do not make changes to this class,
     /// instead, put additional code in the Shippers class
     /// </summary>
     public class ShippersBase : north.Models.ShippersModel
     {
         /// <summary>
         /// Gets or sets the related Orders(s) by ShipperID
         /// </summary>
         [ScriptIgnore]
         public Lazy<OrdersCollection> OrdersCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ShipperID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<OrdersCollection>(() => north.BusinessObject.Orders.SelectOrdersCollectionByShipVia(value));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public ShippersBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Shippers SelectByPrimaryKey(int shipperID)
         {
             return ShippersDataLayer.SelectByPrimaryKey(shipperID);
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table
         /// </summary>
         public static int GetRecordCount()
         {
             return ShippersDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? shipperID, string companyName, string phone)
         {
             return ShippersDataLayer.GetRecordCountDynamicWhere(shipperID, companyName, phone);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ShippersCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return ShippersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static ShippersCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ShippersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static ShippersCollection SelectSkipAndTakeDynamicWhere(int? shipperID, string companyName, string phone, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(shipperID, companyName, phone);
             sortByExpression = GetSortExpression(sortByExpression);
             return ShippersDataLayer.SelectSkipAndTakeDynamicWhere(shipperID, companyName, phone, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static ShippersCollection SelectSkipAndTakeDynamicWhere(int? shipperID, string companyName, string phone, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ShippersDataLayer.SelectSkipAndTakeDynamicWhere(shipperID, companyName, phone, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Shippers
         /// </summary>
         public static ShippersCollection SelectAll()
         {
             return ShippersDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Shippers sorted by the sort expression
         /// </summary>
         public static ShippersCollection SelectAll(string sortExpression)
         {
             ShippersCollection objShippersCol = ShippersDataLayer.SelectAll();
             return SortByExpression(objShippersCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Shippers.
         /// </summary>
         public static ShippersCollection SelectAllDynamicWhere(int? shipperID, string companyName, string phone)
         {
             return ShippersDataLayer.SelectAllDynamicWhere(shipperID, companyName, phone);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Shippers sorted by the sort expression.
         /// </summary>
         public static ShippersCollection SelectAllDynamicWhere(int? shipperID, string companyName, string phone, string sortExpression)
         {
             ShippersCollection objShippersCol = ShippersDataLayer.SelectAllDynamicWhere(shipperID, companyName, phone);
             return SortByExpression(objShippersCol, sortExpression);
         }

         /// <summary>
         /// Selects ShipperID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static ShippersCollection SelectShippersDropDownListData()
         {
             return ShippersDataLayer.SelectShippersDropDownListData();
         }

         /// <summary>
         /// Sorts the ShippersCollection by sort expression
         /// </summary>
         public static ShippersCollection SortByExpression(ShippersCollection objShippersCol, string sortExpression)
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
                 case "ShipperID":
                     objShippersCol.Sort(north.BusinessObject.Shippers.ByShipperID);
                     break;
                 case "CompanyName":
                     objShippersCol.Sort(north.BusinessObject.Shippers.ByCompanyName);
                     break;
                 case "Phone":
                     objShippersCol.Sort(north.BusinessObject.Shippers.ByPhone);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objShippersCol.Reverse();

             return objShippersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Shippers objShippers = (Shippers)this;
             return ShippersDataLayer.Insert(objShippers);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Shippers objShippers = (Shippers)this;
             ShippersDataLayer.Update(objShippers);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int shipperID)
         {
             ShippersDataLayer.Delete(shipperID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "ShipperID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares ShipperID used for sorting
         /// </summary>
         public static Comparison<Shippers> ByShipperID = delegate(Shippers x, Shippers y)
         {
             return x.ShipperID.CompareTo(y.ShipperID);
         };

         /// <summary>
         /// Compares CompanyName used for sorting
         /// </summary>
         public static Comparison<Shippers> ByCompanyName = delegate(Shippers x, Shippers y)
         {
             string value1 = x.CompanyName ?? String.Empty;
             string value2 = y.CompanyName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Phone used for sorting
         /// </summary>
         public static Comparison<Shippers> ByPhone = delegate(Shippers x, Shippers y)
         {
             string value1 = x.Phone ?? String.Empty;
             string value2 = y.Phone ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
