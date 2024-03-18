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
     /// Base class for OrdersApiController.  Do not make changes to this class,
     /// instead, put additional code in the OrdersApiController class
     /// </summary>
     public class OrdersApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the OrdersModel here.  Arrives as OrdersFields which automatically strips the data annotations from the OrdersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]OrdersFields model)
         {
             return AddEditOrders(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the OrdersModel
         /// </summary>
         /// <param name="model">Pass the OrdersModel here.  Arrives as OrdersFields which automatically strips the data annotations from the OrdersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]OrdersFields model)
         {
             return AddEditOrders(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">OrderID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Orders.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditOrders(OrdersFields model, CrudOperation operation)
         {
             Orders objOrders;

             if (operation == CrudOperation.Add)
                objOrders = new Orders();
             else
                objOrders = Orders.SelectByPrimaryKey(model.OrderID);

             objOrders.OrderID = model.OrderID;
             objOrders.CustomerID = model.CustomerID;
             objOrders.EmployeeID = model.EmployeeID;
             objOrders.OrderDate = model.OrderDate;
             objOrders.RequiredDate = model.RequiredDate;
             objOrders.ShippedDate = model.ShippedDate;
             objOrders.ShipVia = model.ShipVia;
             objOrders.Freight = model.Freight;
             objOrders.ShipName = model.ShipName;
             objOrders.ShipAddress = model.ShipAddress;
             objOrders.ShipCity = model.ShipCity;
             objOrders.ShipRegion = model.ShipRegion;
             objOrders.ShipPostalCode = model.ShipPostalCode;
             objOrders.ShipCountry = model.ShipCountry;

             if (operation == CrudOperation.Add)
                objOrders.Insert();
             else
                objOrders.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private OrdersCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     string customerID = String.Empty;
                     int? employeeID = null;
                     DateTime? orderDate = null;
                     DateTime? requiredDate = null;
                     DateTime? shippedDate = null;
                     int? shipVia = null;
                     decimal? freight = null;
                     string shipName = String.Empty;
                     string shipAddress = String.Empty;
                     string shipCity = String.Empty;
                     string shipRegion = String.Empty;
                     string shipPostalCode = String.Empty;
                     string shipCountry = String.Empty;

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

                         if (item == "customerid")
                             customerID = data[ctr];

                         if (item == "employeeid")
                             employeeID = Convert.ToInt32(data[ctr]);

                         if (item == "orderdate")
                             orderDate = Convert.ToDateTime(data[ctr]);

                         if (item == "requireddate")
                             requiredDate = Convert.ToDateTime(data[ctr]);

                         if (item == "shippeddate")
                             shippedDate = Convert.ToDateTime(data[ctr]);

                         if (item == "shipvia")
                             shipVia = Convert.ToInt32(data[ctr]);

                         if (item == "freight")
                             freight = Convert.ToDecimal(data[ctr]);

                         if (item == "shipname")
                             shipName = data[ctr];

                         if (item == "shipaddress")
                             shipAddress = data[ctr];

                         if (item == "shipcity")
                             shipCity = data[ctr];

                         if (item == "shipregion")
                             shipRegion = data[ctr];

                         if (item == "shippostalcode")
                             shipPostalCode = data[ctr];

                         if (item == "shipcountry")
                             shipCountry = data[ctr];

                         ctr++;
                     }

                     totalRecords = Orders.GetRecordCountDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
                     return Orders.SelectSkipAndTakeDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Orders.GetRecordCount();
             return Orders.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 userdata = new
                 {
                     Freight = objOrdersCol.Select(o => o.Freight).Sum().ToString()
                 },
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByCustomerID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.CustomerID == null ? "" : objOrders.Customers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByEmployeeID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "LastName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.EmployeeID == null ? "" : objOrders.Employees.Value.LastName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByShipVia(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.ShipVia == null ? "" : objOrders.Shippers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByCustomerID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.CustomerID == null ? "" : objOrders.Customers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByEmployeeID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "LastName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.EmployeeID == null ? "" : objOrders.Employees.Value.LastName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Orders sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Orders collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeTotalsGroupedByShipVia(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "CompanyName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objOrders in objOrdersCol
                     select new
                     {
                         id = objOrders.OrderID,
                         cell = new string[] { 
                             objOrders.OrderID.ToString(),
                             !String.IsNullOrEmpty(objOrders.CustomerID) ? objOrders.CustomerID : "",
                             objOrders.EmployeeID.HasValue ? objOrders.EmployeeID.Value.ToString() : "",
                             objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : "",
                             objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : "",
                             objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : "",
                             objOrders.ShipVia.HasValue ? objOrders.ShipVia.Value.ToString() : "",
                             objOrders.Freight.HasValue ? objOrders.Freight.Value.ToString() : "",
                             objOrders.ShipName,
                             objOrders.ShipAddress,
                             objOrders.ShipCity,
                             objOrders.ShipRegion,
                             objOrders.ShipPostalCode,
                             objOrders.ShipCountry,
                             objOrders.ShipVia == null ? "" : objOrders.Shippers.Value.CompanyName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">OrderID</param>
         /// <returns>One serialized Orders record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Orders objOrders = Orders.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 OrderID = objOrders.OrderID,
                 CustomerID = objOrders.CustomerID,
                 EmployeeID = objOrders.EmployeeID,
                 OrderDate = objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : null,
                 RequiredDate = objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : null,
                 ShippedDate = objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : null,
                 ShipVia = objOrders.ShipVia,
                 Freight = objOrders.Freight,
                 ShipName = objOrders.ShipName,
                 ShipAddress = objOrders.ShipAddress,
                 ShipCity = objOrders.ShipCity,
                 ShipRegion = objOrders.ShipRegion,
                 ShipPostalCode = objOrders.ShipPostalCode,
                 ShipCountry = objOrders.ShipCountry
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Orders table
         /// </summary>
         /// <returns>Total number of records in the Orders table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Orders.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by CustomerID
         /// </summary>
         /// <param name="id">customerID</param>
         /// <returns>Total number of records in the Orders table by customerID</returns>
         [HttpGet]
         public int GetRecordCountByCustomerID(string id)
         {
             return Orders.GetRecordCountByCustomerID(id);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by EmployeeID
         /// </summary>
         /// <param name="id">employeeID</param>
         /// <returns>Total number of records in the Orders table by employeeID</returns>
         [HttpGet]
         public int GetRecordCountByEmployeeID(int id)
         {
             return Orders.GetRecordCountByEmployeeID(id);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table by ShipVia
         /// </summary>
         /// <param name="id">shipVia</param>
         /// <returns>Total number of records in the Orders table by shipVia</returns>
         [HttpGet]
         public int GetRecordCountByShipVia(int id)
         {
             return Orders.GetRecordCountByShipVia(id);
         }

         /// <summary>
         /// Gets the total number of records in the Orders table based on search parameters
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="customerID">CustomerID</param>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="orderDate">OrderDate</param>
         /// <param name="requiredDate">RequiredDate</param>
         /// <param name="shippedDate">ShippedDate</param>
         /// <param name="shipVia">ShipVia</param>
         /// <param name="freight">Freight</param>
         /// <param name="shipName">ShipName</param>
         /// <param name="shipAddress">ShipAddress</param>
         /// <param name="shipCity">ShipCity</param>
         /// <param name="shipRegion">ShipRegion</param>
         /// <param name="shipPostalCode">ShipPostalCode</param>
         /// <param name="shipCountry">ShipCountry</param>
         /// <returns>Total number of records in the Orders table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
             return Orders.GetRecordCountDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects records by CustomerID as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="customerID">CustomerID</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByCustomerID(int rows, int startRowIndex, string sortByExpression, string customerID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByCustomerID(rows, startRowIndex, sortByExpression, customerID);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects records by EmployeeID as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="employeeID">EmployeeID</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByEmployeeID(int rows, int startRowIndex, string sortByExpression, int employeeID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByEmployeeID(rows, startRowIndex, sortByExpression, employeeID);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects records by ShipVia as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="shipVia">ShipVia</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByShipVia(int rows, int startRowIndex, string sortByExpression, int shipVia)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByShipVia(rows, startRowIndex, sortByExpression, shipVia);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Orders sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="customerID">CustomerID</param>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="orderDate">OrderDate</param>
         /// <param name="requiredDate">RequiredDate</param>
         /// <param name="shippedDate">ShippedDate</param>
         /// <param name="shipVia">ShipVia</param>
         /// <param name="freight">Freight</param>
         /// <param name="shipName">ShipName</param>
         /// <param name="shipAddress">ShipAddress</param>
         /// <param name="shipCity">ShipCity</param>
         /// <param name="shipRegion">ShipRegion</param>
         /// <param name="shipPostalCode">ShipPostalCode</param>
         /// <param name="shipCountry">ShipCountry</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money or decimal data type.  E.g. FreightTotal
         /// </summary>
         [HttpGet]
         public object SelectTotals()
         {
             Orders objOrders = Orders.SelectTotals();

             var jsonData = new
             {
                 FreightTotal = objOrders.FreightTotal
             };

             return jsonData;
         }

         /// <summary>
         /// Selects all records as a collection (List) of Orders
         /// </summary>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             OrdersCollection objOrdersCol = Orders.SelectAll();
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Orders sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             OrdersCollection objOrdersCol = Orders.SelectAll(sortExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Orders.
         /// </summary>
         /// <param name="orderID">OrderID</param>
         /// <param name="customerID">CustomerID</param>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="orderDate">OrderDate</param>
         /// <param name="requiredDate">RequiredDate</param>
         /// <param name="shippedDate">ShippedDate</param>
         /// <param name="shipVia">ShipVia</param>
         /// <param name="freight">Freight</param>
         /// <param name="shipName">ShipName</param>
         /// <param name="shipAddress">ShipAddress</param>
         /// <param name="shipCity">ShipCity</param>
         /// <param name="shipRegion">ShipRegion</param>
         /// <param name="shipPostalCode">ShipPostalCode</param>
         /// <param name="shipCountry">ShipCountry</param>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? orderID, string customerID, int? employeeID, DateTime? orderDate, DateTime? requiredDate, DateTime? shippedDate, int? shipVia, decimal? freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry)
         {
             OrdersCollection objOrdersCol = Orders.SelectAllDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Customers, related to column CustomerID
         /// </summary>
         /// <param name="id">customerID</param>
         /// <returns>Total number of records in the Orders table by customerID</returns>
         [HttpGet]
         public object SelectOrdersCollectionByCustomerID(string id)
         {
             OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByCustomerID(id);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Customers, related to column CustomerID, sorted by the sort expression
         /// </summary>
         /// <param name="id">customerID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Orders table by customerID</returns>
         [HttpGet]
         public object SelectOrdersCollectionByCustomerID(string id, string sortExpression)
         {
             OrdersCollection objOrdersCol = Orders.SortByExpression(Orders.SelectOrdersCollectionByCustomerID(id), sortExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Employees, related to column EmployeeID
         /// </summary>
         /// <param name="id">employeeID</param>
         /// <returns>Total number of records in the Orders table by employeeID</returns>
         [HttpGet]
         public object SelectOrdersCollectionByEmployeeID(int id)
         {
             OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByEmployeeID(id);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Employees, related to column EmployeeID, sorted by the sort expression
         /// </summary>
         /// <param name="id">employeeID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Orders table by employeeID</returns>
         [HttpGet]
         public object SelectOrdersCollectionByEmployeeID(int id, string sortExpression)
         {
             OrdersCollection objOrdersCol = Orders.SortByExpression(Orders.SelectOrdersCollectionByEmployeeID(id), sortExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Shippers, related to column ShipVia
         /// </summary>
         /// <param name="id">shipperID</param>
         /// <returns>Total number of records in the Orders table by shipVia</returns>
         [HttpGet]
         public object SelectOrdersCollectionByShipVia(int id)
         {
             OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByShipVia(id);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects all Orders by Shippers, related to column ShipVia, sorted by the sort expression
         /// </summary>
         /// <param name="id">shipperID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Orders table by shipVia</returns>
         [HttpGet]
         public object SelectOrdersCollectionByShipVia(int id, string sortExpression)
         {
             OrdersCollection objOrdersCol = Orders.SortByExpression(Orders.SelectOrdersCollectionByShipVia(id), sortExpression);
             return GetJsonCollection(objOrdersCol);
         }

         /// <summary>
         /// Selects OrderID and ShipName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Orders collection in json format</returns>
         [HttpGet]
         public object SelectOrdersDropDownListData()
         {
             OrdersCollection objOrdersCol = Orders.SelectOrdersDropDownListData();

             var jsonData = (from objOrders in objOrdersCol
                 select new
                 {
                     OrderID = objOrders.OrderID,
                     ShipName = objOrders.ShipName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(OrdersCollection objOrdersCol)
         {
             if (objOrdersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objOrders in objOrdersCol
                 select new
                 {
                     OrderID = objOrders.OrderID,
                     CustomerID = objOrders.CustomerID,
                     EmployeeID = objOrders.EmployeeID,
                     OrderDate = objOrders.OrderDate.HasValue ? objOrders.OrderDate.Value.ToShortDateString() : null,
                     RequiredDate = objOrders.RequiredDate.HasValue ? objOrders.RequiredDate.Value.ToShortDateString() : null,
                     ShippedDate = objOrders.ShippedDate.HasValue ? objOrders.ShippedDate.Value.ToShortDateString() : null,
                     ShipVia = objOrders.ShipVia,
                     Freight = objOrders.Freight,
                     ShipName = objOrders.ShipName,
                     ShipAddress = objOrders.ShipAddress,
                     ShipCity = objOrders.ShipCity,
                     ShipRegion = objOrders.ShipRegion,
                     ShipPostalCode = objOrders.ShipPostalCode,
                     ShipCountry = objOrders.ShipCountry
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
