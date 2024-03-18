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
     /// Base class for OrderDetailsApiController.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsApiController class
     /// </summary>
     public class OrderDetailsApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the OrderDetailsModel here.  Arrives as OrderDetailsFields which automatically strips the data annotations from the OrderDetailsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]OrderDetailsFields model)
         {
             return AddEditOrderDetails(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the OrderDetailsModel
         /// </summary>
         /// <param name="model">Pass the OrderDetailsModel here.  Arrives as OrderDetailsFields which automatically strips the data annotations from the OrderDetailsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]OrderDetailsFields model)
         {
             return AddEditOrderDetails(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="productID">ProductID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int orderID, int productID)
         {
             OrderDetails.Delete(orderID, productID);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditOrderDetails(OrderDetailsFields model, CrudOperation operation)
         {
             OrderDetails objOrderDetails;

             if (operation == CrudOperation.Add)
                objOrderDetails = new OrderDetails();
             else
                objOrderDetails = OrderDetails.SelectByPrimaryKey(model.OrderID, model.ProductID);

             objOrderDetails.OrderID = model.OrderID;
             objOrderDetails.ProductID = model.ProductID;
             objOrderDetails.UnitPrice = model.UnitPrice;
             objOrderDetails.Quantity = model.Quantity;
             objOrderDetails.Discount = model.Discount;

             if (operation == CrudOperation.Add)
                objOrderDetails.Insert();
             else
                objOrderDetails.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private OrderDetailsCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? orderID = null;
                     int? productID = null;
                     decimal? unitPrice = null;
                     Int16? quantity = null;
                     Single? discount = null;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "orderid")
                             orderID = Convert.ToInt32(data[ctr]);

                         if (item == "productid")
                             productID = Convert.ToInt32(data[ctr]);

                         if (item == "unitprice")
                             unitPrice = Convert.ToDecimal(data[ctr]);

                         if (item == "quantity")
                             quantity = Convert.ToInt16(data[ctr]);

                         if (item == "discount")
                             discount = Convert.ToSingle(data[ctr]);

                         ctr++;
                     }

                     totalRecords = OrderDetails.GetRecordCountDynamicWhere(orderID, productID, unitPrice, quantity, discount);
                     return OrderDetails.SelectSkipAndTakeDynamicWhere(orderID, productID, unitPrice, quantity, discount, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = OrderDetails.GetRecordCount();
             return OrderDetails.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 userdata = new
                 {
                     UnitPrice = objOrderDetailsCol.Select(o => o.UnitPrice).Sum().ToString()
                 },
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByOrderID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "ShipName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString(),
                             objOrderDetails.Orders.Value.ShipName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByProductID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "ProductName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString(),
                             objOrderDetails.Products.Value.ProductName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByOrderID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "ShipName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString(),
                             objOrderDetails.Orders.Value.ShipName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of OrderDetails sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized OrderDetails collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByProductID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "ProductName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrderDetails in objOrderDetailsCol
                     select new
                     {
                         id = objOrderDetails.OrderID.ToString() + objOrderDetails.ProductID.ToString(),
                         cell = new string[] { 
                             objOrderDetails.OrderID.ToString(),
                             objOrderDetails.ProductID.ToString(),
                             objOrderDetails.UnitPrice.ToString(),
                             objOrderDetails.Quantity.ToString(),
                             objOrderDetails.Discount.ToString(),
                             objOrderDetails.Products.Value.ProductName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="productID">ProductID</param>
         /// <returns>One serialized OrderDetails record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int orderID, int productID)
         {
             OrderDetails objOrderDetails = OrderDetails.SelectByPrimaryKey(orderID, productID);

             var jsonData = new
             {
                 OrderID = objOrderDetails.OrderID,
                 ProductID = objOrderDetails.ProductID,
                 UnitPrice = objOrderDetails.UnitPrice,
                 Quantity = objOrderDetails.Quantity,
                 Discount = objOrderDetails.Discount
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table
         /// </summary>
         /// <returns>Total number of records in the OrderDetails table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return OrderDetails.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by OrderID
         /// </summary>
         /// <param name="id">orderID</param>
         /// <returns>Total number of records in the OrderDetails table by orderID</returns>
         [HttpGet]
         public int GetRecordCountByOrderID(int id)
         {
             return OrderDetails.GetRecordCountByOrderID(id);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table by ProductID
         /// </summary>
         /// <param name="id">productID</param>
         /// <returns>Total number of records in the OrderDetails table by productID</returns>
         [HttpGet]
         public int GetRecordCountByProductID(int id)
         {
             return OrderDetails.GetRecordCountByProductID(id);
         }

         /// <summary>
         /// Gets the total number of records in the OrderDetails table based on search parameters
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="productID">ProductID</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="quantity">Quantity</param>
         /// <param name="discount">Discount</param>
         /// <returns>Total number of records in the OrderDetails table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
             return OrderDetails.GetRecordCountDynamicWhere(orderID, productID, unitPrice, quantity, discount);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects records by OrderID as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="orderID">OrderID</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByOrderID(int rows, int startRowIndex, string sortByExpression, int orderID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeByOrderID(rows, startRowIndex, sortByExpression, orderID);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects records by ProductID as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="productID">ProductID</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByProductID(int rows, int startRowIndex, string sortByExpression, int productID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeByProductID(rows, startRowIndex, sortByExpression, productID);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of OrderDetails sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="productID">ProductID</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="quantity">Quantity</param>
         /// <param name="discount">Discount</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeDynamicWhere(orderID, productID, unitPrice, quantity, discount, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. UnitPriceTotal
         /// </summary>
         [HttpGet]
         public object SelectTotals()
         {
             OrderDetails objOrderDetails = OrderDetails.SelectTotals();

             var jsonData = new
             {
                 UnitPriceTotal = objOrderDetails.UnitPriceTotal
             };

             return jsonData;
         }

         /// <summary>
         /// Selects all records as a collection (List) of OrderDetails
         /// </summary>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectAll();
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of OrderDetails sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectAll(sortExpression);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of OrderDetails.
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="productID">ProductID</param>
         /// <param name="unitPrice">UnitPrice</param>
         /// <param name="quantity">Quantity</param>
         /// <param name="discount">Discount</param>
         /// <returns>Serialized OrderDetails collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? orderID, int? productID, decimal? unitPrice, Int16? quantity, Single? discount)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectAllDynamicWhere(orderID, productID, unitPrice, quantity, discount);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects all OrderDetails by Orders, related to column OrderID
         /// </summary>
         /// <param name="id">orderID</param>
         /// <returns>Total number of records in the OrderDetails table by orderID</returns>
         [HttpGet]
         public object SelectOrderDetailsCollectionByOrderID(int id)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectOrderDetailsCollectionByOrderID(id);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects all OrderDetails by Orders, related to column OrderID, sorted by the sort expression
         /// </summary>
         /// <param name="id">orderID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the OrderDetails table by orderID</returns>
         [HttpGet]
         public object SelectOrderDetailsCollectionByOrderID(int id, string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SortByExpression(OrderDetails.SelectOrderDetailsCollectionByOrderID(id), sortExpression);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects all OrderDetails by Products, related to column ProductID
         /// </summary>
         /// <param name="id">productID</param>
         /// <returns>Total number of records in the OrderDetails table by productID</returns>
         [HttpGet]
         public object SelectOrderDetailsCollectionByProductID(int id)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectOrderDetailsCollectionByProductID(id);
             return GetJsonCollection(objOrderDetailsCol);
         }

         /// <summary>
         /// Selects all OrderDetails by Products, related to column ProductID, sorted by the sort expression
         /// </summary>
         /// <param name="id">productID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the OrderDetails table by productID</returns>
         [HttpGet]
         public object SelectOrderDetailsCollectionByProductID(int id, string sortExpression)
         {
             OrderDetailsCollection objOrderDetailsCol = OrderDetails.SortByExpression(OrderDetails.SelectOrderDetailsCollectionByProductID(id), sortExpression);
             return GetJsonCollection(objOrderDetailsCol);
         }

         private object GetJsonCollection(OrderDetailsCollection objOrderDetailsCol)
         {
             if (objOrderDetailsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objOrderDetails in objOrderDetailsCol
                 select new
                 {
                     OrderID = objOrderDetails.OrderID,
                     ProductID = objOrderDetails.ProductID,
                     UnitPrice = objOrderDetails.UnitPrice,
                     Quantity = objOrderDetails.Quantity,
                     Discount = objOrderDetails.Discount
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "OrderID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
