using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Customers.  Do not make changes to this class,
     /// instead, put additional code in the Customers class
     /// </summary>
     public class CustomersBase : north.Models.CustomersModel
     {
         /// <summary>
         /// Gets or sets the related Orders(s) by CustomerID
         /// </summary>
         [ScriptIgnore]
         public Lazy<OrdersCollection> OrdersCollection
         {
             get
             {
                 if(!String.IsNullOrEmpty(CustomerID))
                     return new Lazy<OrdersCollection>(() => north.BusinessObject.Orders.SelectOrdersCollectionByCustomerID(CustomerID));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public CustomersBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Customers SelectByPrimaryKey(string customerID)
         {
             return CustomersDataLayer.SelectByPrimaryKey(customerID);
         }

         /// <summary>
         /// Gets the total number of records in the Customers table
         /// </summary>
         public static int GetRecordCount()
         {
             return CustomersDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Customers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return CustomersDataLayer.GetRecordCountDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static CustomersCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return CustomersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static CustomersCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CustomersCollection SelectSkipAndTakeDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomersDataLayer.SelectSkipAndTakeDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CustomersCollection SelectSkipAndTakeDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CustomersDataLayer.SelectSkipAndTakeDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Customers
         /// </summary>
         public static CustomersCollection SelectAll()
         {
             return CustomersDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Customers sorted by the sort expression
         /// </summary>
         public static CustomersCollection SelectAll(string sortExpression)
         {
             CustomersCollection objCustomersCol = CustomersDataLayer.SelectAll();
             return SortByExpression(objCustomersCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Customers.
         /// </summary>
         public static CustomersCollection SelectAllDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return CustomersDataLayer.SelectAllDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Customers sorted by the sort expression.
         /// </summary>
         public static CustomersCollection SelectAllDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, string sortExpression)
         {
             CustomersCollection objCustomersCol = CustomersDataLayer.SelectAllDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             return SortByExpression(objCustomersCol, sortExpression);
         }

         /// <summary>
         /// Selects CustomerID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static CustomersCollection SelectCustomersDropDownListData()
         {
             return CustomersDataLayer.SelectCustomersDropDownListData();
         }

         /// <summary>
         /// Sorts the CustomersCollection by sort expression
         /// </summary>
         public static CustomersCollection SortByExpression(CustomersCollection objCustomersCol, string sortExpression)
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
                 case "CustomerID":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByCustomerID);
                     break;
                 case "CompanyName":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByCompanyName);
                     break;
                 case "ContactName":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByContactName);
                     break;
                 case "ContactTitle":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByContactTitle);
                     break;
                 case "Address":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByAddress);
                     break;
                 case "City":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByCity);
                     break;
                 case "Region1":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByRegion1);
                     break;
                 case "PostalCode":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByPostalCode);
                     break;
                 case "Country":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByCountry);
                     break;
                 case "Phone":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByPhone);
                     break;
                 case "Fax":
                     objCustomersCol.Sort(north.BusinessObject.Customers.ByFax);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objCustomersCol.Reverse();

             return objCustomersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public string Insert()
         {
             Customers objCustomers = (Customers)this;
             return CustomersDataLayer.Insert(objCustomers);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Customers objCustomers = (Customers)this;
             CustomersDataLayer.Update(objCustomers);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string customerID)
         {
             CustomersDataLayer.Delete(customerID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CustomerID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares CustomerID used for sorting
         /// </summary>
         public static Comparison<Customers> ByCustomerID = delegate(Customers x, Customers y)
         {
             string value1 = x.CustomerID ?? String.Empty;
             string value2 = y.CustomerID ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares CompanyName used for sorting
         /// </summary>
         public static Comparison<Customers> ByCompanyName = delegate(Customers x, Customers y)
         {
             string value1 = x.CompanyName ?? String.Empty;
             string value2 = y.CompanyName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ContactName used for sorting
         /// </summary>
         public static Comparison<Customers> ByContactName = delegate(Customers x, Customers y)
         {
             string value1 = x.ContactName ?? String.Empty;
             string value2 = y.ContactName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ContactTitle used for sorting
         /// </summary>
         public static Comparison<Customers> ByContactTitle = delegate(Customers x, Customers y)
         {
             string value1 = x.ContactTitle ?? String.Empty;
             string value2 = y.ContactTitle ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Address used for sorting
         /// </summary>
         public static Comparison<Customers> ByAddress = delegate(Customers x, Customers y)
         {
             string value1 = x.Address ?? String.Empty;
             string value2 = y.Address ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares City used for sorting
         /// </summary>
         public static Comparison<Customers> ByCity = delegate(Customers x, Customers y)
         {
             string value1 = x.City ?? String.Empty;
             string value2 = y.City ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Region1 used for sorting
         /// </summary>
         public static Comparison<Customers> ByRegion1 = delegate(Customers x, Customers y)
         {
             string value1 = x.Region1 ?? String.Empty;
             string value2 = y.Region1 ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares PostalCode used for sorting
         /// </summary>
         public static Comparison<Customers> ByPostalCode = delegate(Customers x, Customers y)
         {
             string value1 = x.PostalCode ?? String.Empty;
             string value2 = y.PostalCode ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Country used for sorting
         /// </summary>
         public static Comparison<Customers> ByCountry = delegate(Customers x, Customers y)
         {
             string value1 = x.Country ?? String.Empty;
             string value2 = y.Country ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Phone used for sorting
         /// </summary>
         public static Comparison<Customers> ByPhone = delegate(Customers x, Customers y)
         {
             string value1 = x.Phone ?? String.Empty;
             string value2 = y.Phone ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Fax used for sorting
         /// </summary>
         public static Comparison<Customers> ByFax = delegate(Customers x, Customers y)
         {
             string value1 = x.Fax ?? String.Empty;
             string value2 = y.Fax ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
