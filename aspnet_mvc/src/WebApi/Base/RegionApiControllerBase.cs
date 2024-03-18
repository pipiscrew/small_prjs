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
     /// Base class for RegionApiController.  Do not make changes to this class,
     /// instead, put additional code in the RegionApiController class
     /// </summary>
     public class RegionApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the RegionModel here.  Arrives as RegionFields which automatically strips the data annotations from the RegionModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]RegionFields model)
         {
             return AddEditRegion(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the RegionModel
         /// </summary>
         /// <param name="model">Pass the RegionModel here.  Arrives as RegionFields which automatically strips the data annotations from the RegionModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]RegionFields model)
         {
             return AddEditRegion(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">RegionID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             north.BusinessObject.Region.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditRegion(RegionFields model, CrudOperation operation)
         {
             north.BusinessObject.Region objRegion;

             if (operation == CrudOperation.Add)
                objRegion = new north.BusinessObject.Region();
             else
                objRegion = north.BusinessObject.Region.SelectByPrimaryKey(model.RegionID);

             objRegion.RegionID = model.RegionID;
             objRegion.RegionDescription = model.RegionDescription;

             if (operation == CrudOperation.Add)
                objRegion.Insert();
             else
                objRegion.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private RegionCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? regionID = null;
                     string regionDescription = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "regionid")
                             regionID = Convert.ToInt32(data[ctr]);

                         if (item == "regiondescription")
                             regionDescription = data[ctr];

                         ctr++;
                     }

                     totalRecords = north.BusinessObject.Region.GetRecordCountDynamicWhere(regionID, regionDescription);
                     return north.BusinessObject.Region.SelectSkipAndTakeDynamicWhere(regionID, regionDescription, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = north.BusinessObject.Region.GetRecordCount();
             return north.BusinessObject.Region.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Region sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized north.BusinessObject.Region collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objRegionCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objRegion in objRegionCol
                     select new
                     {
                         id = objRegion.RegionID,
                         cell = new string[] { 
                             objRegion.RegionID.ToString(),
                             objRegion.RegionDescription
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Region sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized north.BusinessObject.Region collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             RegionCollection objRegionCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objRegionCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objRegion in objRegionCol
                     select new
                     {
                         id = objRegion.RegionID,
                         cell = new string[] { 
                             objRegion.RegionID.ToString(),
                             objRegion.RegionDescription
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Region sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized north.BusinessObject.Region collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objRegionCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objRegion in objRegionCol
                     select new
                     {
                         id = objRegion.RegionID,
                         cell = new string[] { 
                             objRegion.RegionID.ToString(),
                             objRegion.RegionDescription
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">RegionID</param>
         /// <returns>One serialized Region record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             north.BusinessObject.Region objRegion = north.BusinessObject.Region.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 RegionID = objRegion.RegionID,
                 RegionDescription = objRegion.RegionDescription
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Region table
         /// </summary>
         /// <returns>Total number of records in the Region table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return north.BusinessObject.Region.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Region table based on search parameters
         /// </summary>
         /// <param name="regionID">RegionID</param>
         /// <param name="regionDescription">RegionDescription</param>
         /// <returns>Total number of records in the Region table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? regionID, string regionDescription)
         {
             return north.BusinessObject.Region.GetRecordCountDynamicWhere(regionID, regionDescription);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objRegionCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="regionID">RegionID</param>
         /// <param name="regionDescription">RegionDescription</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? regionID, string regionDescription, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectSkipAndTakeDynamicWhere(regionID, regionDescription, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objRegionCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Region
         /// </summary>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectAll();
             return GetJsonCollection(objRegionCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Region sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectAll(sortExpression);
             return GetJsonCollection(objRegionCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Region.
         /// </summary>
         /// <param name="regionID">RegionID</param>
         /// <param name="regionDescription">RegionDescription</param>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? regionID, string regionDescription)
         {
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectAllDynamicWhere(regionID, regionDescription);
             return GetJsonCollection(objRegionCol);
         }

         /// <summary>
         /// Selects RegionID and RegionDescription columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Region collection in json format</returns>
         [HttpGet]
         public object SelectRegionDropDownListData()
         {
             RegionCollection objRegionCol = north.BusinessObject.Region.SelectRegionDropDownListData();

             var jsonData = (from objRegion in objRegionCol
                 select new
                 {
                     RegionID = objRegion.RegionID,
                     RegionDescription = objRegion.RegionDescription
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(RegionCollection objRegionCol)
         {
             if (objRegionCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objRegion in objRegionCol
                 select new
                 {
                     RegionID = objRegion.RegionID,
                     RegionDescription = objRegion.RegionDescription
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "RegionID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
