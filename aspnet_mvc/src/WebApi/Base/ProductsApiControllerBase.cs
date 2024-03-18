using System;
using System.Text;
using System.Linq;
using System.Web.Http;
using north;
using north.BusinessObject;
using north.Models;
using north.ViewModels;
using north.Domain;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace north.ApiControllers.Base
{
     /// <summary>
     /// Base class for ProductsApiController.  Do not make changes to this class,
     /// instead, put additional code in the ProductsApiController class
     /// </summary>
     public class ProductsApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the ProductsModel here.  Arrives as ProductsFields which automatically strips the data annotations from the ProductsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]ProductsFields model)
         {
             return AddEditProducts(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the ProductsModel
         /// </summary>
         /// <param name="model">Pass the ProductsModel here.  Arrives as ProductsFields which automatically strips the data annotations from the ProductsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]ProductsFields model)
         {
             return AddEditProducts(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">ProductID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Products.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditProducts(ProductsFields model, CrudOperation operation)
         {
             Products objProducts;

             if (operation == CrudOperation.Add)
                objProducts = new Products();
             else
                objProducts = Products.SelectByPrimaryKey(model.ProductID);

             objProducts.ProductID = model.ProductID;
             objProducts.ProductName = model.ProductName;
             objProducts.SupplierID = model.SupplierID;
             objProducts.CategoryID = model.CategoryID;
             objProducts.QuantityPerUnit = model.QuantityPerUnit;
             objProducts.UnitPrice = model.UnitPrice;
             objProducts.UnitsInStock = model.UnitsInStock;
             objProducts.UnitsOnOrder = model.UnitsOnOrder;
             objProducts.ReorderLevel = model.ReorderLevel;
             objProducts.Discontinued = model.Discontinued;

             if (operation == CrudOperation.Add)
                objProducts.Insert();
             else
                objProducts.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private ProductsCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
         {
             if (!String.IsNullOrEmpty(filters))
             {
                 if (filters.Contains("field") && filters.Contains("op") && filters.Contains("data"))
                 {
                     filters = filters.Replace("{\"groupOp\":\"AND\",\"rules\":[{", "");
                     filters = filters.Replace("}]}", "");

                     string[] filterArray = Regex.Split(filters, "},{");
                     List<string> fieldName = new List<string>();
                     List<string> data = new List<string>();
                     int ctr = 0;
                     int? productID = null;
                     string productName = String.Empty;
                     int? supplierID = null;
                     int? categoryID = null;
                     string quantityPerUnit = String.Empty;
                     decimal? unitPrice = null;
                     Int16? unitsInStock = null;
                     Int16? unitsOnOrder = null;
                     Int16? reorderLevel = null;
                     bool? discontinued = null;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "productid")
                             productID = Convert.ToInt32(data[ctr]);

                         if (item == "productname")
                             productName = data[ctr];

                         if (item == "supplierid")
                             supplierID = Convert.ToInt32(data[ctr]);

                         if (item == "categoryid")
                             categoryID = Convert.ToInt32(data[ctr]);

                         if (item == "quantityperunit")
                             quantityPerUnit = data[ctr];

                         if (item == "unitprice")
                             unitPrice = Convert.ToDecimal(data[ctr]);

                         if (item == "unitsinstock")
                             unitsInStock = Convert.ToInt16(data[ctr]);

                         if (item == "unitsonorder")
                             unitsOnOrder = Convert.ToInt16(data[ctr]);

                         if (item == "reorderlevel")
                             reorderLevel = Convert.ToInt16(data[ctr]);

                         if (item == "discontinued")
                             discontinued = Convert.ToBoolean(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Products.GetRecordCountDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
                     return Products.SelectSkipAndTakeDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Products.GetRecordCount();
             return Products.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 userdata = new
                 {
                     UnitPrice = objProductsCol.Select(p => p.UnitPrice).Sum().ToString()
                 },
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedBySupplierID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString(),
                             objProducts.SupplierID == null ? "" : objProducts.Suppliers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByCategoryID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CategoryName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString(),
                             objProducts.CategoryID == null ? "" : objProducts.Categories.Value.CategoryName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedBySupplierID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString(),
                             objProducts.SupplierID == null ? "" : objProducts.Suppliers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Products sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Products collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByCategoryID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CategoryName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objProducts in objProductsCol
                     select new
                     {
                         id = objProducts.ProductID,
                         cell = new string[] { 
                             objProducts.ProductID.ToString(),
                             objProducts.ProductName,
                             objProducts.SupplierID.HasValue ? objProducts.SupplierID.Value.ToString() : "",
                             objProducts.CategoryID.HasValue ? objProducts.CategoryID.Value.ToString() : "",
                             objProducts.QuantityPerUnit,
                             objProducts.UnitPrice.HasValue ? objProducts.UnitPrice.Value.ToString() : "",
                             objProducts.UnitsInStock.HasValue ? objProducts.UnitsInStock.Value.ToString() : "",
                             objProducts.UnitsOnOrder.HasValue ? objProducts.UnitsOnOrder.Value.ToString() : "",
                             objProducts.ReorderLevel.HasValue ? objProducts.ReorderLevel.Value.ToString() : "",
                             objProducts.Discontinued.ToString(),
                             objProducts.CategoryID == null ? "" : objProducts.Categories.Value.CategoryName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">ProductID</param>
         /// <returns>One serialized Products record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Products objProducts = Products.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 ProductID = objProducts.ProductID,
                 ProductName = objProducts.ProductName,
                 SupplierID = objProducts.SupplierID,
                 CategoryID = objProducts.CategoryID,
                 QuantityPerUnit = objProducts.QuantityPerUnit,
                 UnitPrice = objProducts.UnitPrice,
                 UnitsInStock = objProducts.UnitsInStock,
                 UnitsOnOrder = objProducts.UnitsOnOrder,
                 ReorderLevel = objProducts.ReorderLevel,
                 Discontinued = objProducts.Discontinued
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Products table
         /// </summary>
         /// <returns>Total number of records in the Products table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Products.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Products table by SupplierID
         /// </summary>
         /// <param name="id">supplierID</param>
         /// <returns>Total number of records in the Products table by supplierID</returns>
         [HttpGet]
         public int GetRecordCountBySupplierID(int id)
         {
             return Products.GetRecordCountBySupplierID(id);
         }

         /// <summary>
         /// Gets the total number of records in the Products table by CategoryID
         /// </summary>
         /// <param name="id">categoryID</param>
         /// <returns>Total number of records in the Products table by categoryID</returns>
         [HttpGet]
         public int GetRecordCountByCategoryID(int id)
         {
             return Products.GetRecordCountByCategoryID(id);
         }

         /// <summary>
         /// Gets the total number of records in the Products table based on search parameters
         /// </summary>
         /// <param name="productID">ProductID</param>
         /// <param name="productName">ProductName</param>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="quantityPerUnit">QuantityPerUnit</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="unitsInStock">UnitsInStock</param>
         /// <param name="unitsOnOrder">UnitsOnOrder</param>
         /// <param name="reorderLevel">ReorderLevel</param>
         /// <param name="discontinued">Discontinued</param>
         /// <returns>Total number of records in the Products table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
             return Products.GetRecordCountDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ProductsCollection objProductsCol = Products.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects records by SupplierID as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="supplierID">SupplierID</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeBySupplierID(int rows, int startRowIndex, string sortByExpression, int supplierID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ProductsCollection objProductsCol = Products.SelectSkipAndTakeBySupplierID(rows, startRowIndex, sortByExpression, supplierID);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects records by CategoryID as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="categoryID">CategoryID</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByCategoryID(int rows, int startRowIndex, string sortByExpression, int categoryID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ProductsCollection objProductsCol = Products.SelectSkipAndTakeByCategoryID(rows, startRowIndex, sortByExpression, categoryID);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Products sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="productID">ProductID</param>
         /// <param name="productName">ProductName</param>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="quantityPerUnit">QuantityPerUnit</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="unitsInStock">UnitsInStock</param>
         /// <param name="unitsOnOrder">UnitsOnOrder</param>
         /// <param name="reorderLevel">ReorderLevel</param>
         /// <param name="discontinued">Discontinued</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ProductsCollection objProductsCol = Products.SelectSkipAndTakeDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. UnitPriceTotal
         /// </summary>
         [HttpGet]
         public object SelectTotals()
         {
             Products objProducts = Products.SelectTotals();

             var jsonData = new
             {
                 UnitPriceTotal = objProducts.UnitPriceTotal
             };

             return jsonData;
         }

         /// <summary>
         /// Selects all records as a collection (List) of Products
         /// </summary>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             ProductsCollection objProductsCol = Products.SelectAll();
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Products sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             ProductsCollection objProductsCol = Products.SelectAll(sortExpression);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Products.
         /// </summary>
         /// <param name="productID">ProductID</param>
         /// <param name="productName">ProductName</param>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="quantityPerUnit">QuantityPerUnit</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="unitsInStock">UnitsInStock</param>
         /// <param name="unitsOnOrder">UnitsOnOrder</param>
         /// <param name="reorderLevel">ReorderLevel</param>
         /// <param name="discontinued">Discontinued</param>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
             ProductsCollection objProductsCol = Products.SelectAllDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects all Products by Suppliers, related to column SupplierID
         /// </summary>
         /// <param name="id">supplierID</param>
         /// <returns>Total number of records in the Products table by supplierID</returns>
         [HttpGet]
         public object SelectProductsCollectionBySupplierID(int id)
         {
             ProductsCollection objProductsCol = Products.SelectProductsCollectionBySupplierID(id);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects all Products by Suppliers, related to column SupplierID, sorted by the sort expression
         /// </summary>
         /// <param name="id">supplierID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Products table by supplierID</returns>
         [HttpGet]
         public object SelectProductsCollectionBySupplierID(int id, string sortExpression)
         {
             ProductsCollection objProductsCol = Products.SortByExpression(Products.SelectProductsCollectionBySupplierID(id), sortExpression);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects all Products by Categories, related to column CategoryID
         /// </summary>
         /// <param name="id">categoryID</param>
         /// <returns>Total number of records in the Products table by categoryID</returns>
         [HttpGet]
         public object SelectProductsCollectionByCategoryID(int id)
         {
             ProductsCollection objProductsCol = Products.SelectProductsCollectionByCategoryID(id);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects all Products by Categories, related to column CategoryID, sorted by the sort expression
         /// </summary>
         /// <param name="id">categoryID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Products table by categoryID</returns>
         [HttpGet]
         public object SelectProductsCollectionByCategoryID(int id, string sortExpression)
         {
             ProductsCollection objProductsCol = Products.SortByExpression(Products.SelectProductsCollectionByCategoryID(id), sortExpression);
             return GetJsonCollection(objProductsCol);
         }

         /// <summary>
         /// Selects ProductID and ProductName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Products collection in json format</returns>
         [HttpGet]
         public object SelectProductsDropDownListData()
         {
             ProductsCollection objProductsCol = Products.SelectProductsDropDownListData();

             var jsonData = (from objProducts in objProductsCol
                 select new
                 {
                     ProductID = objProducts.ProductID,
                     ProductName = objProducts.ProductName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(ProductsCollection objProductsCol)
         {
             if (objProductsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objProducts in objProductsCol
                 select new
                 {
                     ProductID = objProducts.ProductID,
                     ProductName = objProducts.ProductName,
                     SupplierID = objProducts.SupplierID,
                     CategoryID = objProducts.CategoryID,
                     QuantityPerUnit = objProducts.QuantityPerUnit,
                     UnitPrice = objProducts.UnitPrice,
                     UnitsInStock = objProducts.UnitsInStock,
                     UnitsOnOrder = objProducts.UnitsOnOrder,
                     ReorderLevel = objProducts.ReorderLevel,
                     Discontinued = objProducts.Discontinued
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "ProductID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
