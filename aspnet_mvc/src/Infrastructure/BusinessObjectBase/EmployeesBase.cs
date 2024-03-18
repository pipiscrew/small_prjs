using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Employees.  Do not make changes to this class,
     /// instead, put additional code in the Employees class
     /// </summary>
     public class EmployeesBase : north.Models.EmployeesModel
     {
         /// <summary>
         /// Gets or sets the Related Employees.  Related to column ReportsTo
         /// </summary>
         [ScriptIgnore]
         public Lazy<Employees> EmployeesReportsTo
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(ReportsTo.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Employees>(() => BusinessObject.Employees.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 

         /// <summary>
         /// Gets or sets the related Employees(s) by EmployeeID
         /// </summary>
         [ScriptIgnore]
         public Lazy<EmployeesCollection> EmployeesCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(EmployeeID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<EmployeesCollection>(() => north.BusinessObject.Employees.SelectEmployeesCollectionByReportsTo(value));
                 else
                     return null;
             }
             set { }
         }

         /// <summary>
         /// Gets or sets the related Orders(s) by EmployeeID
         /// </summary>
         [ScriptIgnore]
         public Lazy<OrdersCollection> OrdersCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(EmployeeID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<OrdersCollection>(() => north.BusinessObject.Orders.SelectOrdersCollectionByEmployeeID(value));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public EmployeesBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Employees SelectByPrimaryKey(int employeeID)
         {
             return EmployeesDataLayer.SelectByPrimaryKey(employeeID);
         }

         /// <summary>
         /// Gets the total number of records in the Employees table
         /// </summary>
         public static int GetRecordCount()
         {
             return EmployeesDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Employees table by ReportsTo
         /// </summary>
         public static int GetRecordCountByReportsTo(int reportsTo)
         {
             return EmployeesDataLayer.GetRecordCountByReportsTo(reportsTo);
         }

         /// <summary>
         /// Gets the total number of records in the Employees table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
             return EmployeesDataLayer.GetRecordCountDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static EmployeesCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return EmployeesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static EmployeesCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return EmployeesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by ReportsTo as a collection (List) of Employees sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeByReportsTo(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int reportsTo)
         {
             totalRowCount = EmployeesDataLayer.GetRecordCountByReportsTo(reportsTo);
             sortByExpression = GetSortExpression(sortByExpression);
             return EmployeesDataLayer.SelectSkipAndTakeByReportsTo(sortByExpression, startRowIndex, rows, reportsTo);
         }

         /// <summary>
         /// Selects records by ReportsTo as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeByReportsTo(int rows, int startRowIndex, string sortByExpression, int reportsTo)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return EmployeesDataLayer.SelectSkipAndTakeByReportsTo(sortByExpression, startRowIndex, rows, reportsTo);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
             sortByExpression = GetSortExpression(sortByExpression);
             return EmployeesDataLayer.SelectSkipAndTakeDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return EmployeesDataLayer.SelectSkipAndTakeDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Employees
         /// </summary>
         public static EmployeesCollection SelectAll()
         {
             return EmployeesDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Employees sorted by the sort expression
         /// </summary>
         public static EmployeesCollection SelectAll(string sortExpression)
         {
             EmployeesCollection objEmployeesCol = EmployeesDataLayer.SelectAll();
             return SortByExpression(objEmployeesCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Employees.
         /// </summary>
         public static EmployeesCollection SelectAllDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
             return EmployeesDataLayer.SelectAllDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Employees sorted by the sort expression.
         /// </summary>
         public static EmployeesCollection SelectAllDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath, string sortExpression)
         {
             EmployeesCollection objEmployeesCol = EmployeesDataLayer.SelectAllDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
             return SortByExpression(objEmployeesCol, sortExpression);
         }

         /// <summary>
         /// Selects all Employees by Employees, related to column ReportsTo
         /// </summary>
         public static EmployeesCollection SelectEmployeesCollectionByReportsTo(int employeeID)
         {
             return EmployeesDataLayer.SelectEmployeesCollectionByReportsTo(employeeID);
         }

         /// <summary>
         /// Selects all Employees by Employees, related to column ReportsTo, sorted by the sort expression
         /// </summary>
         public static EmployeesCollection SelectEmployeesCollectionByReportsTo(int employeeID, string sortExpression)
         {
             EmployeesCollection objEmployeesCol = EmployeesDataLayer.SelectEmployeesCollectionByReportsTo(employeeID);
             return SortByExpression(objEmployeesCol, sortExpression);
         }

         /// <summary>
         /// Selects EmployeeID and LastName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static EmployeesCollection SelectEmployeesDropDownListData()
         {
             return EmployeesDataLayer.SelectEmployeesDropDownListData();
         }

         /// <summary>
         /// Sorts the EmployeesCollection by sort expression
         /// </summary>
         public static EmployeesCollection SortByExpression(EmployeesCollection objEmployeesCol, string sortExpression)
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
                 case "EmployeeID":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByEmployeeID);
                     break;
                 case "LastName":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByLastName);
                     break;
                 case "FirstName":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByFirstName);
                     break;
                 case "Title":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByTitle);
                     break;
                 case "TitleOfCourtesy":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByTitleOfCourtesy);
                     break;
                 case "BirthDate":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByBirthDate);
                     break;
                 case "HireDate":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByHireDate);
                     break;
                 case "Address":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByAddress);
                     break;
                 case "City":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByCity);
                     break;
                 case "Region1":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByRegion1);
                     break;
                 case "PostalCode":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByPostalCode);
                     break;
                 case "Country":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByCountry);
                     break;
                 case "HomePhone":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByHomePhone);
                     break;
                 case "Extension":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByExtension);
                     break;
                 case "Notes":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByNotes);
                     break;
                 case "ReportsTo":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByReportsTo);
                     break;
                 case "PhotoPath":
                     objEmployeesCol.Sort(north.BusinessObject.Employees.ByPhotoPath);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objEmployeesCol.Reverse();

             return objEmployeesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Employees objEmployees = (Employees)this;
             return EmployeesDataLayer.Insert(objEmployees);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Employees objEmployees = (Employees)this;
             EmployeesDataLayer.Update(objEmployees);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int employeeID)
         {
             EmployeesDataLayer.Delete(employeeID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "EmployeeID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares EmployeeID used for sorting
         /// </summary>
         public static Comparison<Employees> ByEmployeeID = delegate(Employees x, Employees y)
         {
             return x.EmployeeID.CompareTo(y.EmployeeID);
         };

         /// <summary>
         /// Compares LastName used for sorting
         /// </summary>
         public static Comparison<Employees> ByLastName = delegate(Employees x, Employees y)
         {
             string value1 = x.LastName ?? String.Empty;
             string value2 = y.LastName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares FirstName used for sorting
         /// </summary>
         public static Comparison<Employees> ByFirstName = delegate(Employees x, Employees y)
         {
             string value1 = x.FirstName ?? String.Empty;
             string value2 = y.FirstName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Title used for sorting
         /// </summary>
         public static Comparison<Employees> ByTitle = delegate(Employees x, Employees y)
         {
             string value1 = x.Title ?? String.Empty;
             string value2 = y.Title ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares TitleOfCourtesy used for sorting
         /// </summary>
         public static Comparison<Employees> ByTitleOfCourtesy = delegate(Employees x, Employees y)
         {
             string value1 = x.TitleOfCourtesy ?? String.Empty;
             string value2 = y.TitleOfCourtesy ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares BirthDate used for sorting
         /// </summary>
         public static Comparison<Employees> ByBirthDate = delegate(Employees x, Employees y)
         {
             return Nullable.Compare(x.BirthDate, y.BirthDate);
         };

         /// <summary>
         /// Compares HireDate used for sorting
         /// </summary>
         public static Comparison<Employees> ByHireDate = delegate(Employees x, Employees y)
         {
             return Nullable.Compare(x.HireDate, y.HireDate);
         };

         /// <summary>
         /// Compares Address used for sorting
         /// </summary>
         public static Comparison<Employees> ByAddress = delegate(Employees x, Employees y)
         {
             string value1 = x.Address ?? String.Empty;
             string value2 = y.Address ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares City used for sorting
         /// </summary>
         public static Comparison<Employees> ByCity = delegate(Employees x, Employees y)
         {
             string value1 = x.City ?? String.Empty;
             string value2 = y.City ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Region1 used for sorting
         /// </summary>
         public static Comparison<Employees> ByRegion1 = delegate(Employees x, Employees y)
         {
             string value1 = x.Region1 ?? String.Empty;
             string value2 = y.Region1 ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares PostalCode used for sorting
         /// </summary>
         public static Comparison<Employees> ByPostalCode = delegate(Employees x, Employees y)
         {
             string value1 = x.PostalCode ?? String.Empty;
             string value2 = y.PostalCode ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Country used for sorting
         /// </summary>
         public static Comparison<Employees> ByCountry = delegate(Employees x, Employees y)
         {
             string value1 = x.Country ?? String.Empty;
             string value2 = y.Country ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares HomePhone used for sorting
         /// </summary>
         public static Comparison<Employees> ByHomePhone = delegate(Employees x, Employees y)
         {
             string value1 = x.HomePhone ?? String.Empty;
             string value2 = y.HomePhone ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Extension used for sorting
         /// </summary>
         public static Comparison<Employees> ByExtension = delegate(Employees x, Employees y)
         {
             string value1 = x.Extension ?? String.Empty;
             string value2 = y.Extension ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Notes used for sorting
         /// </summary>
         public static Comparison<Employees> ByNotes = delegate(Employees x, Employees y)
         {
             string value1 = x.Notes ?? String.Empty;
             string value2 = y.Notes ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares ReportsTo used for sorting
         /// </summary>
         public static Comparison<Employees> ByReportsTo = delegate(Employees x, Employees y)
         {
             return Nullable.Compare(x.ReportsTo, y.ReportsTo);
         };

         /// <summary>
         /// Compares PhotoPath used for sorting
         /// </summary>
         public static Comparison<Employees> ByPhotoPath = delegate(Employees x, Employees y)
         {
             string value1 = x.PhotoPath ?? String.Empty;
             string value2 = y.PhotoPath ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
