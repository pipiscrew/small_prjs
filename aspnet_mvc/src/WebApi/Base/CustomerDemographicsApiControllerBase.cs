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
     /// Base class for CustomerDemographicsApiController.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographicsApiController class
     /// </summary>
     public class CustomerDemographicsApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the CustomerDemographicsModel here.  Arrives as CustomerDemographicsFields which automatically strips the data annotations from the CustomerDemographicsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]CustomerDemographicsFields model)
         {
             return AddEditCustomerDemographics(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the CustomerDemographicsModel
         /// </summary>
         /// <param name="model">Pass the CustomerDemographicsModel here.  Arrives as CustomerDemographicsFields which automatically strips the data annotations from the CustomerDemographicsModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]CustomerDemographicsFields model)
         {
             return AddEditCustomerDemographics(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">CustomerTypeID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(string id)
         {
             CustomerDemographics.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditCustomerDemographics(CustomerDemographicsFields model, CrudOperation operation)
         {
             CustomerDemographics objCustomerDemographics;

             if (operation == CrudOperation.Add)
                objCustomerDemographics = new CustomerDemographics();
             else
                objCustomerDemographics = CustomerDemographics.SelectByPrimaryKey(model.CustomerTypeID);

             objCustomerDemographics.CustomerTypeID = model.CustomerTypeID;
             objCustomerDemographics.CustomerDesc = model.CustomerDesc;

             if (operation == CrudOperation.Add)
                objCustomerDemographics.Insert();
             else
                objCustomerDemographics.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private CustomerDemographicsCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     string customerTypeID = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "customertypeid")
                             customerTypeID = data[ctr];

                         ctr++;
                     }

                     totalRecords = CustomerDemographics.GetRecordCountDynamicWhere(customerTypeID);
                     return CustomerDemographics.SelectSkipAndTakeDynamicWhere(customerTypeID, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = CustomerDemographics.GetRecordCount();
             return CustomerDemographics.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized CustomerDemographics collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomerDemographicsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomerDemographics in objCustomerDemographicsCol
                     select new
                     {
                         id = objCustomerDemographics.CustomerTypeID,
                         cell = new string[] { 
                             objCustomerDemographics.CustomerTypeID,
                             objCustomerDemographics.CustomerDesc
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized CustomerDemographics collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomerDemographicsCollection objCustomerDemographicsCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomerDemographicsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomerDemographics in objCustomerDemographicsCol
                     select new
                     {
                         id = objCustomerDemographics.CustomerTypeID,
                         cell = new string[] { 
                             objCustomerDemographics.CustomerTypeID,
                             objCustomerDemographics.CustomerDesc
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized CustomerDemographics collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomerDemographicsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomerDemographics in objCustomerDemographicsCol
                     select new
                     {
                         id = objCustomerDemographics.CustomerTypeID,
                         cell = new string[] { 
                             objCustomerDemographics.CustomerTypeID,
                             objCustomerDemographics.CustomerDesc
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">CustomerTypeID</param>
         /// <returns>One serialized CustomerDemographics record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(string id)
         {
             CustomerDemographics objCustomerDemographics = CustomerDemographics.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 CustomerTypeID = objCustomerDemographics.CustomerTypeID,
                 CustomerDesc = objCustomerDemographics.CustomerDesc
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the CustomerDemographics table
         /// </summary>
         /// <returns>Total number of records in the CustomerDemographics table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return CustomerDemographics.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the CustomerDemographics table based on search parameters
         /// </summary>
         /// <param name="customerTypeID">CustomerTypeID</param>
         /// <returns>Total number of records in the CustomerDemographics table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(string customerTypeID)
         {
             return CustomerDemographics.GetRecordCountDynamicWhere(customerTypeID);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCustomerDemographicsCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of CustomerDemographics sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="customerTypeID">CustomerTypeID</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(string customerTypeID, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTakeDynamicWhere(customerTypeID, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCustomerDemographicsCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of CustomerDemographics
         /// </summary>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectAll();
             return GetJsonCollection(objCustomerDemographicsCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of CustomerDemographics sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectAll(sortExpression);
             return GetJsonCollection(objCustomerDemographicsCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of CustomerDemographics.
         /// </summary>
         /// <param name="customerTypeID">CustomerTypeID</param>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(string customerTypeID)
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectAllDynamicWhere(customerTypeID);
             return GetJsonCollection(objCustomerDemographicsCol);
         }

         /// <summary>
         /// Selects CustomerTypeID and CustomerDesc columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized CustomerDemographics collection in json format</returns>
         [HttpGet]
         public object SelectCustomerDemographicsDropDownListData()
         {
             CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectCustomerDemographicsDropDownListData();

             var jsonData = (from objCustomerDemographics in objCustomerDemographicsCol
                 select new
                 {
                     CustomerTypeID = objCustomerDemographics.CustomerTypeID,
                     CustomerDesc = objCustomerDemographics.CustomerDesc
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(CustomerDemographicsCollection objCustomerDemographicsCol)
         {
             if (objCustomerDemographicsCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objCustomerDemographics in objCustomerDemographicsCol
                 select new
                 {
                     CustomerTypeID = objCustomerDemographics.CustomerTypeID,
                     CustomerDesc = objCustomerDemographics.CustomerDesc
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CustomerTypeID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
