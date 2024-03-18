using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Suppliers.  Do not make changes to this class,
     /// instead, put additional code in the Suppliers class
     /// </summary>
     public class SuppliersBase : north.Models.SuppliersModel
     {
         /// <summary>
         /// Gets or sets the related Products(s) by SupplierID
         /// </summary>
         [ScriptIgnore]
         public Lazy<ProductsCollection> ProductsCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(SupplierID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<ProductsCollection>(() => north.BusinessObject.Products.SelectProductsCollectionBySupplierID(value));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public SuppliersBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Suppliers SelectByPrimaryKey(int supplierID)
         {
             return SuppliersDataLayer.SelectByPrimaryKey(supplierID);
         }

         /// <summary>
         /// Gets the total number of records in the Suppliers table
         /// </summary>
         public static int GetRecordCount()
         {
             return SuppliersDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Suppliers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return SuppliersDataLayer.GetRecordCountDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static SuppliersCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return SuppliersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static SuppliersCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return SuppliersDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static SuppliersCollection SelectSkipAndTakeDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             sortByExpression = GetSortExpression(sortByExpression);
             return SuppliersDataLayer.SelectSkipAndTakeDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static SuppliersCollection SelectSkipAndTakeDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return SuppliersDataLayer.SelectSkipAndTakeDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Suppliers
         /// </summary>
         public static SuppliersCollection SelectAll()
         {
             return SuppliersDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Suppliers sorted by the sort expression
         /// </summary>
         public static SuppliersCollection SelectAll(string sortExpression)
         {
             SuppliersCollection objSuppliersCol = SuppliersDataLayer.SelectAll();
             return SortByExpression(objSuppliersCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Suppliers.
         /// </summary>
         public static SuppliersCollection SelectAllDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return SuppliersDataLayer.SelectAllDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Suppliers sorted by the sort expression.
         /// </summary>
         public static SuppliersCollection SelectAllDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, string sortExpression)
         {
             SuppliersCollection objSuppliersCol = SuppliersDataLayer.SelectAllDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             return SortByExpression(objSuppliersCol, sortExpression);
         }

         /// <summary>
         /// Selects SupplierID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static SuppliersCollection SelectSuppliersDropDownListData()
         {
             return SuppliersDataLayer.SelectSuppliersDropDownListData();
         }

         /// <summary>
         /// Sorts the SuppliersCollection by sort expression
         /// </summary>
         public static SuppliersCollection SortByExpression(SuppliersCollection objSuppliersCol, string sortExpression)
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
                 case "SupplierID":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.BySupplierID);
                     break;
                 case "CompanyName":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByCompanyName);
                     break;
                 case "ContactName":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByContactName);
                     break;
                 case "ContactTitle":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByContactTitle);
                     break;
                 case "Address":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByAddress);
                     break;
                 case "City":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByCity);
                     break;
                 case "Region1":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByRegion1);
                     break;
                 case "PostalCode":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByPostalCode);
                     break;
                 case "Country":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByCountry);
                     break;
                 case "Phone":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByPhone);
                     break;
                 case "Fax":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByFax);
                     break;
                 case "HomePage":
                     objSuppliersCol.Sort(north.BusinessObject.Suppliers.ByHomePage);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objSuppliersCol.Reverse();

             return objSuppliersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Suppliers objSuppliers = (Suppliers)this;
             return SuppliersDataLayer.Insert(objSuppliers);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Suppliers objSuppliers = (Suppliers)this;
             SuppliersDataLayer.Update(objSuppliers);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int supplierID)
         {
             SuppliersDataLayer.Delete(supplierID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "SupplierID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares SupplierID used for sorting
         /// </summary>
         public static Comparison<Suppliers> BySupplierID = delegate(Suppliers x, Suppliers y)
         {
             return x.SupplierID.CompareTo(y.SupplierID);
         };

         /// <summary>
         /// Compares CompanyName used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByCompanyName = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.CompanyName ?? String.Empty;
             string value2 = y.CompanyName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ContactName used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByContactName = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.ContactName ?? String.Empty;
             string value2 = y.ContactName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ContactTitle used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByContactTitle = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.ContactTitle ?? String.Empty;
             string value2 = y.ContactTitle ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Address used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByAddress = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.Address ?? String.Empty;
             string value2 = y.Address ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares City used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByCity = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.City ?? String.Empty;
             string value2 = y.City ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Region1 used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByRegion1 = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.Region1 ?? String.Empty;
             string value2 = y.Region1 ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares PostalCode used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByPostalCode = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.PostalCode ?? String.Empty;
             string value2 = y.PostalCode ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Country used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByCountry = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.Country ?? String.Empty;
             string value2 = y.Country ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Phone used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByPhone = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.Phone ?? String.Empty;
             string value2 = y.Phone ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Fax used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByFax = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.Fax ?? String.Empty;
             string value2 = y.Fax ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares HomePage used for sorting
         /// </summary>
         public static Comparison<Suppliers> ByHomePage = delegate(Suppliers x, Suppliers y)
         {
             string value1 = x.HomePage ?? String.Empty;
             string value2 = y.HomePage ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
