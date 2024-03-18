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
     /// Base class for CategoriesApiController.  Do not make changes to this class,
     /// instead, put additional code in the CategoriesApiController class
     /// </summary>
     public class CategoriesApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the CategoriesModel here.  Arrives as CategoriesFields which automatically strips the data annotations from the CategoriesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]CategoriesFields model)
         {
             return AddEditCategories(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the CategoriesModel
         /// </summary>
         /// <param name="model">Pass the CategoriesModel here.  Arrives as CategoriesFields which automatically strips the data annotations from the CategoriesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]CategoriesFields model)
         {
             return AddEditCategories(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">CategoryID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Categories.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditCategories(CategoriesFields model, CrudOperation operation)
         {
             Categories objCategories;

             if (operation == CrudOperation.Add)
                objCategories = new Categories();
             else
                objCategories = Categories.SelectByPrimaryKey(model.CategoryID);

             objCategories.CategoryID = model.CategoryID;
             objCategories.CategoryName = model.CategoryName;
             objCategories.Description = model.Description;

             if (operation == CrudOperation.Add)
                objCategories.Insert();
             else
                objCategories.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private CategoriesCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? categoryID = null;
                     string categoryName = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "categoryid")
                             categoryID = Convert.ToInt32(data[ctr]);

                         if (item == "categoryname")
                             categoryName = data[ctr];

                         ctr++;
                     }

                     totalRecords = Categories.GetRecordCountDynamicWhere(categoryID, categoryName);
                     return Categories.SelectSkipAndTakeDynamicWhere(categoryID, categoryName, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Categories.GetRecordCount();
             return Categories.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Categories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Categories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CategoriesCollection objCategoriesCol = Categories.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCategoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCategories in objCategoriesCol
                     select new
                     {
                         id = objCategories.CategoryID,
                         cell = new string[] { 
                             objCategories.CategoryID.ToString(),
                             objCategories.CategoryName,
                             objCategories.Description
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Categories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Categories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CategoriesCollection objCategoriesCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCategoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCategories in objCategoriesCol
                     select new
                     {
                         id = objCategories.CategoryID,
                         cell = new string[] { 
                             objCategories.CategoryID.ToString(),
                             objCategories.CategoryName,
                             objCategories.Description
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Categories sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Categories collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CategoriesCollection objCategoriesCol = Categories.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCategoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCategories in objCategoriesCol
                     select new
                     {
                         id = objCategories.CategoryID,
                         cell = new string[] { 
                             objCategories.CategoryID.ToString(),
                             objCategories.CategoryName,
                             objCategories.Description
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">CategoryID</param>
         /// <returns>One serialized Categories record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Categories objCategories = Categories.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 CategoryID = objCategories.CategoryID,
                 CategoryName = objCategories.CategoryName,
                 Description = objCategories.Description
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Categories table
         /// </summary>
         /// <returns>Total number of records in the Categories table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Categories.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Categories table based on search parameters
         /// </summary>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="categoryName">CategoryName</param>
         /// <returns>Total number of records in the Categories table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? categoryID, string categoryName)
         {
             return Categories.GetRecordCountDynamicWhere(categoryID, categoryName);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CategoriesCollection objCategoriesCol = Categories.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCategoriesCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Categories sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="categoryName">CategoryName</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? categoryID, string categoryName, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CategoriesCollection objCategoriesCol = Categories.SelectSkipAndTakeDynamicWhere(categoryID, categoryName, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCategoriesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Categories
         /// </summary>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             CategoriesCollection objCategoriesCol = Categories.SelectAll();
             return GetJsonCollection(objCategoriesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Categories sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             CategoriesCollection objCategoriesCol = Categories.SelectAll(sortExpression);
             return GetJsonCollection(objCategoriesCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Categories.
         /// </summary>
         /// <param name="categoryID">CategoryID</param>
         /// <param name="categoryName">CategoryName</param>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? categoryID, string categoryName)
         {
             CategoriesCollection objCategoriesCol = Categories.SelectAllDynamicWhere(categoryID, categoryName);
             return GetJsonCollection(objCategoriesCol);
         }

         /// <summary>
         /// Selects CategoryID and CategoryName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Categories collection in json format</returns>
         [HttpGet]
         public object SelectCategoriesDropDownListData()
         {
             CategoriesCollection objCategoriesCol = Categories.SelectCategoriesDropDownListData();

             var jsonData = (from objCategories in objCategoriesCol
                 select new
                 {
                     CategoryID = objCategories.CategoryID,
                     CategoryName = objCategories.CategoryName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(CategoriesCollection objCategoriesCol)
         {
             if (objCategoriesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objCategories in objCategoriesCol
                 select new
                 {
                     CategoryID = objCategories.CategoryID,
                     CategoryName = objCategories.CategoryName,
                     Description = objCategories.Description
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CategoryID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
