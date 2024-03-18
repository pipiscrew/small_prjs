using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Categories.  Do not make changes to this class,
     /// instead, put additional code in the Categories class
     /// </summary>
     public class CategoriesBase : north.Models.CategoriesModel
     {
         /// <summary>
         /// Gets or sets the related Products(s) by CategoryID
         /// </summary>
         [ScriptIgnore]
         public Lazy<ProductsCollection> ProductsCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(CategoryID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<ProductsCollection>(() => north.BusinessObject.Products.SelectProductsCollectionByCategoryID(value));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public CategoriesBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Categories SelectByPrimaryKey(int categoryID)
         {
             return CategoriesDataLayer.SelectByPrimaryKey(categoryID);
         }

         /// <summary>
         /// Gets the total number of records in the Categories table
         /// </summary>
         public static int GetRecordCount()
         {
             return CategoriesDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Categories table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? categoryID, string categoryName)
         {
             return CategoriesDataLayer.GetRecordCountDynamicWhere(categoryID, categoryName);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static CategoriesCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return CategoriesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static CategoriesCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CategoriesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CategoriesCollection SelectSkipAndTakeDynamicWhere(int? categoryID, string categoryName, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(categoryID, categoryName);
             sortByExpression = GetSortExpression(sortByExpression);
             return CategoriesDataLayer.SelectSkipAndTakeDynamicWhere(categoryID, categoryName, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static CategoriesCollection SelectSkipAndTakeDynamicWhere(int? categoryID, string categoryName, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return CategoriesDataLayer.SelectSkipAndTakeDynamicWhere(categoryID, categoryName, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Categories
         /// </summary>
         public static CategoriesCollection SelectAll()
         {
             return CategoriesDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Categories sorted by the sort expression
         /// </summary>
         public static CategoriesCollection SelectAll(string sortExpression)
         {
             CategoriesCollection objCategoriesCol = CategoriesDataLayer.SelectAll();
             return SortByExpression(objCategoriesCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Categories.
         /// </summary>
         public static CategoriesCollection SelectAllDynamicWhere(int? categoryID, string categoryName)
         {
             return CategoriesDataLayer.SelectAllDynamicWhere(categoryID, categoryName);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Categories sorted by the sort expression.
         /// </summary>
         public static CategoriesCollection SelectAllDynamicWhere(int? categoryID, string categoryName, string sortExpression)
         {
             CategoriesCollection objCategoriesCol = CategoriesDataLayer.SelectAllDynamicWhere(categoryID, categoryName);
             return SortByExpression(objCategoriesCol, sortExpression);
         }

         /// <summary>
         /// Selects CategoryID and CategoryName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static CategoriesCollection SelectCategoriesDropDownListData()
         {
             return CategoriesDataLayer.SelectCategoriesDropDownListData();
         }

         /// <summary>
         /// Sorts the CategoriesCollection by sort expression
         /// </summary>
         public static CategoriesCollection SortByExpression(CategoriesCollection objCategoriesCol, string sortExpression)
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
                 case "CategoryID":
                     objCategoriesCol.Sort(north.BusinessObject.Categories.ByCategoryID);
                     break;
                 case "CategoryName":
                     objCategoriesCol.Sort(north.BusinessObject.Categories.ByCategoryName);
                     break;
                 case "Description":
                     objCategoriesCol.Sort(north.BusinessObject.Categories.ByDescription);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objCategoriesCol.Reverse();

             return objCategoriesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Categories objCategories = (Categories)this;
             return CategoriesDataLayer.Insert(objCategories);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Categories objCategories = (Categories)this;
             CategoriesDataLayer.Update(objCategories);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int categoryID)
         {
             CategoriesDataLayer.Delete(categoryID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CategoryID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares CategoryID used for sorting
         /// </summary>
         public static Comparison<Categories> ByCategoryID = delegate(Categories x, Categories y)
         {
             return x.CategoryID.CompareTo(y.CategoryID);
         };

         /// <summary>
         /// Compares CategoryName used for sorting
         /// </summary>
         public static Comparison<Categories> ByCategoryName = delegate(Categories x, Categories y)
         {
             string value1 = x.CategoryName ?? String.Empty;
             string value2 = y.CategoryName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares Description used for sorting
         /// </summary>
         public static Comparison<Categories> ByDescription = delegate(Categories x, Categories y)
         {
             string value1 = x.Description ?? String.Empty;
             string value2 = y.Description ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}
