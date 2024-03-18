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
     /// Base class for TerritoriesApiController.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesApiController class
     /// </summary>
     public class TerritoriesApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the TerritoriesModel here.  Arrives as TerritoriesFields which automatically strips the data annotations from the TerritoriesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]TerritoriesFields model)
         {
             return AddEditTerritories(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the TerritoriesModel
         /// </summary>
         /// <param name="model">Pass the TerritoriesModel here.  Arrives as TerritoriesFields which automatically strips the data annotations from the TerritoriesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]TerritoriesFields model)
         {
             return AddEditTerritories(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">TerritoryID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(string id)
         {
             Territories.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditTerritories(TerritoriesFields model, CrudOperation operation)
         {
             Territories objTerritories;

             if (operation == CrudOperation.Add)
                objTerritories = new Territories();
             else
                objTerritories = Territories.SelectByPrimaryKey(model.TerritoryID);

             objTerritories.TerritoryID = model.TerritoryID;
             objTerritories.TerritoryDescription = model.TerritoryDescription;
             objTerritories.RegionID = model.RegionID;

             if (operation == CrudOperation.Add)
                objTerritories.Insert();
             else
                objTerritories.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private TerritoriesCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     string territoryID = String.Empty;
                     string territoryDescription = String.Empty;
                     int? regionID = null;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "territoryid")
                             territoryID = data[ctr];

                         if (item == "territorydescription")
                             territoryDescription = data[ctr];

                         if (item == "regionid")
                             regionID = Convert.ToInt32(data[ctr]);

                         ctr++;
                     }

                     totalRecords = Territories.GetRecordCountDynamicWhere(territoryID, territoryDescription, regionID);
                     return Territories.SelectSkipAndTakeDynamicWhere(territoryID, territoryDescription, regionID, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Territories.GetRecordCount();
             return Territories.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Territories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Territories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTerritoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTerritories in objTerritoriesCol
                     select new
                     {
                         id = objTerritories.TerritoryID,
                         cell = new string[] { 
                             objTerritories.TerritoryID,
                             objTerritories.TerritoryDescription,
                             objTerritories.RegionID.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Territories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Territories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TerritoriesCollection objTerritoriesCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTerritoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTerritories in objTerritoriesCol
                     select new
                     {
                         id = objTerritories.TerritoryID,
                         cell = new string[] { 
                             objTerritories.TerritoryID,
                             objTerritories.TerritoryDescription,
                             objTerritories.RegionID.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Territories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Territories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTerritoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTerritories in objTerritoriesCol
                     select new
                     {
                         id = objTerritories.TerritoryID,
                         cell = new string[] { 
                             objTerritories.TerritoryID,
                             objTerritories.TerritoryDescription,
                             objTerritories.RegionID.ToString()
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Territories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Territories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByRegionID(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "RegionDescription asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objTerritoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objTerritories in objTerritoriesCol
                     select new
                     {
                         id = objTerritories.TerritoryID,
                         cell = new string[] { 
                             objTerritories.TerritoryID,
                             objTerritories.TerritoryDescription,
                             objTerritories.RegionID.ToString(),
                             objTerritories.Region.Value.RegionDescription

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">TerritoryID</param>
         /// <returns>One serialized Territories record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(string id)
         {
             Territories objTerritories = Territories.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 TerritoryID = objTerritories.TerritoryID,
                 TerritoryDescription = objTerritories.TerritoryDescription,
                 RegionID = objTerritories.RegionID
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Territories table
         /// </summary>
         /// <returns>Total number of records in the Territories table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Territories.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Territories table by RegionID
         /// </summary>
         /// <param name="id">regionID</param>
         /// <returns>Total number of records in the Territories table by regionID</returns>
         [HttpGet]
         public int GetRecordCountByRegionID(int id)
         {
             return Territories.GetRecordCountByRegionID(id);
         }

         /// <summary>
         /// Gets the total number of records in the Territories table based on search parameters
         /// </summary>
         /// <param name="territoryID">TerritoryID</param>
         /// <param name="territoryDescription">TerritoryDescription</param>
         /// <param name="regionID">RegionID</param>
         /// <returns>Total number of records in the Territories table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
             return Territories.GetRecordCountDynamicWhere(territoryID, territoryDescription, regionID);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects records by RegionID as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="regionID">RegionID</param>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByRegionID(int rows, int startRowIndex, string sortByExpression, int regionID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTakeByRegionID(rows, startRowIndex, sortByExpression, regionID);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="territoryID">TerritoryID</param>
         /// <param name="territoryDescription">TerritoryDescription</param>
         /// <param name="regionID">RegionID</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(string territoryID, string territoryDescription, int? regionID, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTakeDynamicWhere(territoryID, territoryDescription, regionID, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Territories
         /// </summary>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             TerritoriesCollection objTerritoriesCol = Territories.SelectAll();
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Territories sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             TerritoriesCollection objTerritoriesCol = Territories.SelectAll(sortExpression);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Territories.
         /// </summary>
         /// <param name="territoryID">TerritoryID</param>
         /// <param name="territoryDescription">TerritoryDescription</param>
         /// <param name="regionID">RegionID</param>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
             TerritoriesCollection objTerritoriesCol = Territories.SelectAllDynamicWhere(territoryID, territoryDescription, regionID);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects all Territories by Region, related to column RegionID
         /// </summary>
         /// <param name="id">regionID</param>
         /// <returns>Total number of records in the Territories table by regionID</returns>
         [HttpGet]
         public object SelectTerritoriesCollectionByRegionID(int id)
         {
             TerritoriesCollection objTerritoriesCol = Territories.SelectTerritoriesCollectionByRegionID(id);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects all Territories by Region, related to column RegionID, sorted by the sort expression
         /// </summary>
         /// <param name="id">regionID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Territories table by regionID</returns>
         [HttpGet]
         public object SelectTerritoriesCollectionByRegionID(int id, string sortExpression)
         {
             TerritoriesCollection objTerritoriesCol = Territories.SortByExpression(Territories.SelectTerritoriesCollectionByRegionID(id), sortExpression);
             return GetJsonCollection(objTerritoriesCol);
         }

         /// <summary>
         /// Selects TerritoryID and TerritoryDescription columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Territories collection in json format</returns>
         [HttpGet]
         public object SelectTerritoriesDropDownListData()
         {
             TerritoriesCollection objTerritoriesCol = Territories.SelectTerritoriesDropDownListData();

             var jsonData = (from objTerritories in objTerritoriesCol
                 select new
                 {
                     TerritoryID = objTerritories.TerritoryID,
                     TerritoryDescription = objTerritories.TerritoryDescription
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(TerritoriesCollection objTerritoriesCol)
         {
             if (objTerritoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objTerritories in objTerritoriesCol
                 select new
                 {
                     TerritoryID = objTerritories.TerritoryID,
                     TerritoryDescription = objTerritories.TerritoryDescription,
                     RegionID = objTerritories.RegionID
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "TerritoryID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
