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
     /// Base class for ShippersApiController.  Do not make changes to this class,
     /// instead, put additional code in the ShippersApiController class
     /// </summary>
     public class ShippersApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the ShippersModel here.  Arrives as ShippersFields which automatically strips the data annotations from the ShippersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]ShippersFields model)
         {
             return AddEditShippers(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the ShippersModel
         /// </summary>
         /// <param name="model">Pass the ShippersModel here.  Arrives as ShippersFields which automatically strips the data annotations from the ShippersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]ShippersFields model)
         {
             return AddEditShippers(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">ShipperID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Shippers.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditShippers(ShippersFields model, CrudOperation operation)
         {
             Shippers objShippers;

             if (operation == CrudOperation.Add)
                objShippers = new Shippers();
             else
                objShippers = Shippers.SelectByPrimaryKey(model.ShipperID);

             objShippers.ShipperID = model.ShipperID;
             objShippers.CompanyName = model.CompanyName;
             objShippers.Phone = model.Phone;

             if (operation == CrudOperation.Add)
                objShippers.Insert();
             else
                objShippers.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private ShippersCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? shipperID = null;
                     string companyName = String.Empty;
                     string phone = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "shipperid")
                             shipperID = Convert.ToInt32(data[ctr]);

                         if (item == "companyname")
                             companyName = data[ctr];

                         if (item == "phone")
                             phone = data[ctr];

                         ctr++;
                     }

                     totalRecords = Shippers.GetRecordCountDynamicWhere(shipperID, companyName, phone);
                     return Shippers.SelectSkipAndTakeDynamicWhere(shipperID, companyName, phone, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Shippers.GetRecordCount();
             return Shippers.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Shippers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Shippers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ShippersCollection objShippersCol = Shippers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objShippersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objShippers in objShippersCol
                     select new
                     {
                         id = objShippers.ShipperID,
                         cell = new string[] { 
                             objShippers.ShipperID.ToString(),
                             objShippers.CompanyName,
                             objShippers.Phone
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Shippers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Shippers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ShippersCollection objShippersCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objShippersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objShippers in objShippersCol
                     select new
                     {
                         id = objShippers.ShipperID,
                         cell = new string[] { 
                             objShippers.ShipperID.ToString(),
                             objShippers.CompanyName,
                             objShippers.Phone
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Shippers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Shippers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             ShippersCollection objShippersCol = Shippers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objShippersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objShippers in objShippersCol
                     select new
                     {
                         id = objShippers.ShipperID,
                         cell = new string[] { 
                             objShippers.ShipperID.ToString(),
                             objShippers.CompanyName,
                             objShippers.Phone
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">ShipperID</param>
         /// <returns>One serialized Shippers record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Shippers objShippers = Shippers.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 ShipperID = objShippers.ShipperID,
                 CompanyName = objShippers.CompanyName,
                 Phone = objShippers.Phone
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table
         /// </summary>
         /// <returns>Total number of records in the Shippers table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Shippers.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Shippers table based on search parameters
         /// </summary>
         /// <param name="shipperID">ShipperID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="phone">Phone</param>
         /// <returns>Total number of records in the Shippers table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? shipperID, string companyName, string phone)
         {
             return Shippers.GetRecordCountDynamicWhere(shipperID, companyName, phone);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ShippersCollection objShippersCol = Shippers.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objShippersCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Shippers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="shipperID">ShipperID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="phone">Phone</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? shipperID, string companyName, string phone, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             ShippersCollection objShippersCol = Shippers.SelectSkipAndTakeDynamicWhere(shipperID, companyName, phone, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objShippersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Shippers
         /// </summary>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             ShippersCollection objShippersCol = Shippers.SelectAll();
             return GetJsonCollection(objShippersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Shippers sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             ShippersCollection objShippersCol = Shippers.SelectAll(sortExpression);
             return GetJsonCollection(objShippersCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Shippers.
         /// </summary>
         /// <param name="shipperID">ShipperID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="phone">Phone</param>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? shipperID, string companyName, string phone)
         {
             ShippersCollection objShippersCol = Shippers.SelectAllDynamicWhere(shipperID, companyName, phone);
             return GetJsonCollection(objShippersCol);
         }

         /// <summary>
         /// Selects ShipperID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Shippers collection in json format</returns>
         [HttpGet]
         public object SelectShippersDropDownListData()
         {
             ShippersCollection objShippersCol = Shippers.SelectShippersDropDownListData();

             var jsonData = (from objShippers in objShippersCol
                 select new
                 {
                     ShipperID = objShippers.ShipperID,
                     CompanyName = objShippers.CompanyName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(ShippersCollection objShippersCol)
         {
             if (objShippersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objShippers in objShippersCol
                 select new
                 {
                     ShipperID = objShippers.ShipperID,
                     CompanyName = objShippers.CompanyName,
                     Phone = objShippers.Phone
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "ShipperID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
