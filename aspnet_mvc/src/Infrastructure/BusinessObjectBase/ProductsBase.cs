using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Products.  Do not make changes to this class,
     /// instead, put additional code in the Products class
     /// </summary>
     public class ProductsBase : north.Models.ProductsModel
     {
         /// <summary>
         /// Gets or sets the Related Suppliers.  Related to column SupplierID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Suppliers> Suppliers
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(SupplierID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Suppliers>(() => BusinessObject.Suppliers.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 

         /// <summary>
         /// Gets or sets the Related Categories.  Related to column CategoryID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Categories> Categories
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(CategoryID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<Categories>(() => BusinessObject.Categories.SelectByPrimaryKey(value));
                 else
                     return null;
             }
             set{ }
         } 


         /// <summary>
         /// Constructor
         /// </summary>
         public ProductsBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Products SelectByPrimaryKey(int productID)
         {
             return ProductsDataLayer.SelectByPrimaryKey(productID);
         }

         /// <summary>
         /// Gets the total number of records in the Products table
         /// </summary>
         public static int GetRecordCount()
         {
             return ProductsDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Products table by SupplierID
         /// </summary>
         public static int GetRecordCountBySupplierID(int supplierID)
         {
             return ProductsDataLayer.GetRecordCountBySupplierID(supplierID);
         }

         /// <summary>
         /// Gets the total number of records in the Products table by CategoryID
         /// </summary>
         public static int GetRecordCountByCategoryID(int categoryID)
         {
             return ProductsDataLayer.GetRecordCountByCategoryID(categoryID);
         }

         /// <summary>
         /// Gets the total number of records in the Products table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
             return ProductsDataLayer.GetRecordCountDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return ProductsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static ProductsCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by SupplierID as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeBySupplierID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int supplierID)
         {
             totalRowCount = ProductsDataLayer.GetRecordCountBySupplierID(supplierID);
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeBySupplierID(sortByExpression, startRowIndex, rows, supplierID);
         }

         /// <summary>
         /// Selects records by SupplierID as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeBySupplierID(int rows, int startRowIndex, string sortByExpression, int supplierID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeBySupplierID(sortByExpression, startRowIndex, rows, supplierID);
         }

         /// <summary>
         /// Selects records by CategoryID as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeByCategoryID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int categoryID)
         {
             totalRowCount = ProductsDataLayer.GetRecordCountByCategoryID(categoryID);
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeByCategoryID(sortByExpression, startRowIndex, rows, categoryID);
         }

         /// <summary>
         /// Selects records by CategoryID as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeByCategoryID(int rows, int startRowIndex, string sortByExpression, int categoryID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeByCategoryID(sortByExpression, startRowIndex, rows, categoryID);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return ProductsDataLayer.SelectSkipAndTakeDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. UnitPriceTotal
         /// </summary>
         public static Products SelectTotals()
         {
             return ProductsDataLayer.SelectTotals();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Products
         /// </summary>
         public static ProductsCollection SelectAll()
         {
             return ProductsDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Products sorted by the sort expression
         /// </summary>
         public static ProductsCollection SelectAll(string sortExpression)
         {
             ProductsCollection objProductsCol = ProductsDataLayer.SelectAll();
             return SortByExpression(objProductsCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Products.
         /// </summary>
         public static ProductsCollection SelectAllDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
             return ProductsDataLayer.SelectAllDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Products sorted by the sort expression.
         /// </summary>
         public static ProductsCollection SelectAllDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued, string sortExpression)
         {
             ProductsCollection objProductsCol = ProductsDataLayer.SelectAllDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
             return SortByExpression(objProductsCol, sortExpression);
         }

         /// <summary>
         /// Selects all Products by Suppliers, related to column SupplierID
         /// </summary>
         public static ProductsCollection SelectProductsCollectionBySupplierID(int supplierID)
         {
             return ProductsDataLayer.SelectProductsCollectionBySupplierID(supplierID);
         }

         /// <summary>
         /// Selects all Products by Suppliers, related to column SupplierID, sorted by the sort expression
         /// </summary>
         public static ProductsCollection SelectProductsCollectionBySupplierID(int supplierID, string sortExpression)
         {
             ProductsCollection objProductsCol = ProductsDataLayer.SelectProductsCollectionBySupplierID(supplierID);
             return SortByExpression(objProductsCol, sortExpression);
         }

         /// <summary>
         /// Selects all Products by Categories, related to column CategoryID
         /// </summary>
         public static ProductsCollection SelectProductsCollectionByCategoryID(int categoryID)
         {
             return ProductsDataLayer.SelectProductsCollectionByCategoryID(categoryID);
         }

         /// <summary>
         /// Selects all Products by Categories, related to column CategoryID, sorted by the sort expression
         /// </summary>
         public static ProductsCollection SelectProductsCollectionByCategoryID(int categoryID, string sortExpression)
         {
             ProductsCollection objProductsCol = ProductsDataLayer.SelectProductsCollectionByCategoryID(categoryID);
             return SortByExpression(objProductsCol, sortExpression);
         }

         /// <summary>
         /// Selects ProductID and ProductName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static ProductsCollection SelectProductsDropDownListData()
         {
             return ProductsDataLayer.SelectProductsDropDownListData();
         }

         /// <summary>
         /// Sorts the ProductsCollection by sort expression
         /// </summary>
         public static ProductsCollection SortByExpression(ProductsCollection objProductsCol, string sortExpression)
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
                 case "ProductID":
                     objProductsCol.Sort(north.BusinessObject.Products.ByProductID);
                     break;
                 case "ProductName":
                     objProductsCol.Sort(north.BusinessObject.Products.ByProductName);
                     break;
                 case "SupplierID":
                     objProductsCol.Sort(north.BusinessObject.Products.BySupplierID);
                     break;
                 case "CategoryID":
                     objProductsCol.Sort(north.BusinessObject.Products.ByCategoryID);
                     break;
                 case "QuantityPerUnit":
                     objProductsCol.Sort(north.BusinessObject.Products.ByQuantityPerUnit);
                     break;
                 case "UnitPrice":
                     objProductsCol.Sort(north.BusinessObject.Products.ByUnitPrice);
                     break;
                 case "UnitsInStock":
                     objProductsCol.Sort(north.BusinessObject.Products.ByUnitsInStock);
                     break;
                 case "UnitsOnOrder":
                     objProductsCol.Sort(north.BusinessObject.Products.ByUnitsOnOrder);
                     break;
                 case "ReorderLevel":
                     objProductsCol.Sort(north.BusinessObject.Products.ByReorderLevel);
                     break;
                 case "Discontinued":
                     objProductsCol.Sort(north.BusinessObject.Products.ByDiscontinued);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objProductsCol.Reverse();

             return objProductsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Products objProducts = (Products)this;
             return ProductsDataLayer.Insert(objProducts);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Products objProducts = (Products)this;
             ProductsDataLayer.Update(objProducts);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int productID)
         {
             ProductsDataLayer.Delete(productID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "ProductID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares ProductID used for sorting
         /// </summary>
         public static Comparison<Products> ByProductID = delegate(Products x, Products y)
         {
             return x.ProductID.CompareTo(y.ProductID);
         };

         /// <summary>
         /// Compares ProductName used for sorting
         /// </summary>
         public static Comparison<Products> ByProductName = delegate(Products x, Products y)
         {
             string value1 = x.ProductName ?? String.Empty;
             string value2 = y.ProductName ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares SupplierID used for sorting
         /// </summary>
         public static Comparison<Products> BySupplierID = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.SupplierID, y.SupplierID);
         };

         /// <summary>
         /// Compares CategoryID used for sorting
         /// </summary>
         public static Comparison<Products> ByCategoryID = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.CategoryID, y.CategoryID);
         };

         /// <summary>
         /// Compares QuantityPerUnit used for sorting
         /// </summary>
         public static Comparison<Products> ByQuantityPerUnit = delegate(Products x, Products y)
         {
             string value1 = x.QuantityPerUnit ?? String.Empty;
             string value2 = y.QuantityPerUnit ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares UnitPrice used for sorting
         /// </summary>
         public static Comparison<Products> ByUnitPrice = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.UnitPrice, y.UnitPrice);
         };

         /// <summary>
         /// Compares UnitsInStock used for sorting
         /// </summary>
         public static Comparison<Products> ByUnitsInStock = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.UnitsInStock, y.UnitsInStock);
         };

         /// <summary>
         /// Compares UnitsOnOrder used for sorting
         /// </summary>
         public static Comparison<Products> ByUnitsOnOrder = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.UnitsOnOrder, y.UnitsOnOrder);
         };

         /// <summary>
         /// Compares ReorderLevel used for sorting
         /// </summary>
         public static Comparison<Products> ByReorderLevel = delegate(Products x, Products y)
         {
             return Nullable.Compare(x.ReorderLevel, y.ReorderLevel);
         };

         /// <summary>
         /// Compares Discontinued used for sorting
         /// </summary>
         public static Comparison<Products> ByDiscontinued = delegate(Products x, Products y)
         {
             return x.Discontinued.CompareTo(y.Discontinued);
         };

     }
}
