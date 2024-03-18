using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for CustomerDemographics.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographics class
     /// </summary>
     public class CustomerDemographicsBase : north.Models.CustomerDemographicsModel
     {

         /// <summary>
         /// Constructor
         /// </summary>
         public CustomerDemographicsBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static CustomerDemographics SelectByPrimaryKey(string customerTypeID)
         {
             return CustomerDemographicsDataLayer.SelectByPrimaryKey(customerTypeID);
         }

         /// <summary>
         /// Gets the total number of records in the CustomerDemographics table
         /// </summary>
         public static int GetRecordCount()
         {
             return CustomerDemographicsDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the CustomerDemographics table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string customerTypeID)
         {
             return CustomerDemographicsDataLayer.GetRecordCountDynamicWhere(customerTypeID);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return CustomerDemographicsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomerDemographicsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTakeDynamicWhere(string customerTypeID, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(customerTypeID);
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomerDemographicsDataLayer.SelectSkipAndTakeDynamicWhere(customerTypeID, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CustomerDemographicsCollection SelectSkipAndTakeDynamicWhere(string customerTypeID, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomerDemographicsDataLayer.SelectSkipAndTakeDynamicWhere(customerTypeID, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of CustomerDemographics
         /// </summary>
         public static CustomerDemographicsCollection SelectAll()
         {
             return CustomerDemographicsDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of CustomerDemographics sorted by the sort expression
         /// </summary>
         public static CustomerDemographicsCollection SelectAll(string sortExpression)
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographicsDataLayer.SelectAll();
             return SortByExpression(objCustomerDemographicsCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of CustomerDemographics.
         /// </summary>
         public static CustomerDemographicsCollection SelectAllDynamicWhere(string customerTypeID)
         {
             return CustomerDemographicsDataLayer.SelectAllDynamicWhere(customerTypeID);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of CustomerDemographics sorted by the sort expression.
         /// </summary>
         public static CustomerDemographicsCollection SelectAllDynamicWhere(string customerTypeID, string sortExpression)
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographicsDataLayer.SelectAllDynamicWhere(customerTypeID);
             return SortByExpression(objCustomerDemographicsCol, sortExpression);
         }

         /// <summary>
         /// Selects CustomerTypeID and CustomerDesc columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static CustomerDemographicsCollection SelectCustomerDemographicsDropDownListData()
         {
             return CustomerDemographicsDataLayer.SelectCustomerDemographicsDropDownListData();
         }

         /// <summary>
         /// Sorts the CustomerDemographicsCollection by sort expression
         /// </summary>
         public static CustomerDemographicsCollection SortByExpression(CustomerDemographicsCollection objCustomerDemographicsCol, string sortExpression)
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
                 case "CustomerTypeID":
                     objCustomerDemographicsCol.Sort(north.BusinessObject.CustomerDemographics.ByCustomerTypeID);
                     break;
                 case "CustomerDesc":
                     objCustomerDemographicsCol.Sort(north.BusinessObject.CustomerDemographics.ByCustomerDesc);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objCustomerDemographicsCol.Reverse();

             return objCustomerDemographicsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public string Insert()
         {
             CustomerDemographics objCustomerDemographics = (CustomerDemographics)this;
             return CustomerDemographicsDataLayer.Insert(objCustomerDemographics);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             CustomerDemographics objCustomerDemographics = (CustomerDemographics)this;
             CustomerDemographicsDataLayer.Update(objCustomerDemographics);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string customerTypeID)
         {
             CustomerDemographicsDataLayer.Delete(customerTypeID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CustomerTypeID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares CustomerTypeID used for sorting
         /// </summary>
         public static Comparison<CustomerDemographics> ByCustomerTypeID = delegate(CustomerDemographics x, CustomerDemographics y)
         {
             string value1 = x.CustomerTypeID ?? String.Empty;
             string value2 = y.CustomerTypeID ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares CustomerDesc used for sorting
         /// </summary>
         public static Comparison<CustomerDemographics> ByCustomerDesc = delegate(CustomerDemographics x, CustomerDemographics y)
         {
             string value1 = x.CustomerDesc ?? String.Empty;
             string value2 = y.CustomerDesc ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
