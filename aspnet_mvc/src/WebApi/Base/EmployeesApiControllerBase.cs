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
     /// Base class for EmployeesApiController.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesApiController class
     /// </summary>
     public class EmployeesApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the EmployeesModel here.  Arrives as EmployeesFields which automatically strips the data annotations from the EmployeesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]EmployeesFields model)
         {
             return AddEditEmployees(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the EmployeesModel
         /// </summary>
         /// <param name="model">Pass the EmployeesModel here.  Arrives as EmployeesFields which automatically strips the data annotations from the EmployeesModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]EmployeesFields model)
         {
             return AddEditEmployees(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">EmployeeID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Employees.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditEmployees(EmployeesFields model, CrudOperation operation)
         {
             Employees objEmployees;

             if (operation == CrudOperation.Add)
                objEmployees = new Employees();
             else
                objEmployees = Employees.SelectByPrimaryKey(model.EmployeeID);

             objEmployees.EmployeeID = model.EmployeeID;
             objEmployees.LastName = model.LastName;
             objEmployees.FirstName = model.FirstName;
             objEmployees.Title = model.Title;
             objEmployees.TitleOfCourtesy = model.TitleOfCourtesy;
             objEmployees.BirthDate = model.BirthDate;
             objEmployees.HireDate = model.HireDate;
             objEmployees.Address = model.Address;
             objEmployees.City = model.City;
             objEmployees.Region1 = model.Region1;
             objEmployees.PostalCode = model.PostalCode;
             objEmployees.Country = model.Country;
             objEmployees.HomePhone = model.HomePhone;
             objEmployees.Extension = model.Extension;
             objEmployees.Notes = model.Notes;
             objEmployees.ReportsTo = model.ReportsTo;
             objEmployees.PhotoPath = model.PhotoPath;

             if (operation == CrudOperation.Add)
                objEmployees.Insert();
             else
                objEmployees.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private EmployeesCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? employeeID = null;
                     string lastName = String.Empty;
                     string firstName = String.Empty;
                     string title = String.Empty;
                     string titleOfCourtesy = String.Empty;
                     DateTime? birthDate = null;
                     DateTime? hireDate = null;
                     string address = String.Empty;
                     string city = String.Empty;
                     string region1 = String.Empty;
                     string postalCode = String.Empty;
                     string country = String.Empty;
                     string homePhone = String.Empty;
                     string extension = String.Empty;
                     int? reportsTo = null;
                     string photoPath = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "employeeid")
                             employeeID = Convert.ToInt32(data[ctr]);

                         if (item == "lastname")
                             lastName = data[ctr];

                         if (item == "firstname")
                             firstName = data[ctr];

                         if (item == "title")
                             title = data[ctr];

                         if (item == "titleofcourtesy")
                             titleOfCourtesy = data[ctr];

                         if (item == "birthdate")
                             birthDate = Convert.ToDateTime(data[ctr]);

                         if (item == "hiredate")
                             hireDate = Convert.ToDateTime(data[ctr]);

                         if (item == "address")
                             address = data[ctr];

                         if (item == "city")
                             city = data[ctr];

                         if (item == "region1")
                             region1 = data[ctr];

                         if (item == "postalcode")
                             postalCode = data[ctr];

                         if (item == "country")
                             country = data[ctr];

                         if (item == "homephone")
                             homePhone = data[ctr];

                         if (item == "extension")
                             extension = data[ctr];

                         if (item == "reportsto")
                             reportsTo = Convert.ToInt32(data[ctr]);

                         if (item == "photopath")
                             photoPath = data[ctr];

                         ctr++;
                     }

                     totalRecords = Employees.GetRecordCountDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
                     return Employees.SelectSkipAndTakeDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Employees.GetRecordCount();
             return Employees.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Employees sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Employees collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objEmployeesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objEmployees in objEmployeesCol
                     select new
                     {
                         id = objEmployees.EmployeeID,
                         cell = new string[] { 
                             objEmployees.EmployeeID.ToString(),
                             objEmployees.LastName,
                             objEmployees.FirstName,
                             objEmployees.Title,
                             objEmployees.TitleOfCourtesy,
                             objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : "",
                             objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : "",
                             objEmployees.Address,
                             objEmployees.City,
                             objEmployees.Region1,
                             objEmployees.PostalCode,
                             objEmployees.Country,
                             objEmployees.HomePhone,
                             objEmployees.Extension,
                             objEmployees.Notes,
                             objEmployees.ReportsTo.HasValue ? objEmployees.ReportsTo.Value.ToString() : "",
                             objEmployees.PhotoPath
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Employees sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Employees collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             EmployeesCollection objEmployeesCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objEmployeesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objEmployees in objEmployeesCol
                     select new
                     {
                         id = objEmployees.EmployeeID,
                         cell = new string[] { 
                             objEmployees.EmployeeID.ToString(),
                             objEmployees.LastName,
                             objEmployees.FirstName,
                             objEmployees.Title,
                             objEmployees.TitleOfCourtesy,
                             objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : "",
                             objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : "",
                             objEmployees.Address,
                             objEmployees.City,
                             objEmployees.Region1,
                             objEmployees.PostalCode,
                             objEmployees.Country,
                             objEmployees.HomePhone,
                             objEmployees.Extension,
                             objEmployees.Notes,
                             objEmployees.ReportsTo.HasValue ? objEmployees.ReportsTo.Value.ToString() : "",
                             objEmployees.PhotoPath
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Employees sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Employees collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objEmployeesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objEmployees in objEmployeesCol
                     select new
                     {
                         id = objEmployees.EmployeeID,
                         cell = new string[] { 
                             objEmployees.EmployeeID.ToString(),
                             objEmployees.LastName,
                             objEmployees.FirstName,
                             objEmployees.Title,
                             objEmployees.TitleOfCourtesy,
                             objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : "",
                             objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : "",
                             objEmployees.Address,
                             objEmployees.City,
                             objEmployees.Region1,
                             objEmployees.PostalCode,
                             objEmployees.Country,
                             objEmployees.HomePhone,
                             objEmployees.Extension,
                             objEmployees.Notes,
                             objEmployees.ReportsTo.HasValue ? objEmployees.ReportsTo.Value.ToString() : "",
                             objEmployees.PhotoPath
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Employees sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Employees collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeGroupedByReportsTo(string sidx, string sord, int page, int rows)
         {
             // using a groupField in the jqgrid passes that field
             // along with the field to sort, remove the groupField
             string groupBy = "LastName asc, ";
             sidx = sidx.Replace(groupBy, "");

             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objEmployeesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objEmployees in objEmployeesCol
                     select new
                     {
                         id = objEmployees.EmployeeID,
                         cell = new string[] { 
                             objEmployees.EmployeeID.ToString(),
                             objEmployees.LastName,
                             objEmployees.FirstName,
                             objEmployees.Title,
                             objEmployees.TitleOfCourtesy,
                             objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : "",
                             objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : "",
                             objEmployees.Address,
                             objEmployees.City,
                             objEmployees.Region1,
                             objEmployees.PostalCode,
                             objEmployees.Country,
                             objEmployees.HomePhone,
                             objEmployees.Extension,
                             objEmployees.Notes,
                             objEmployees.ReportsTo.HasValue ? objEmployees.ReportsTo.Value.ToString() : "",
                             objEmployees.PhotoPath,
                             objEmployees.ReportsTo == null ? "" : objEmployees.EmployeesReportsTo.Value.LastName

                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">EmployeeID</param>
         /// <returns>One serialized Employees record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Employees objEmployees = Employees.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 EmployeeID = objEmployees.EmployeeID,
                 LastName = objEmployees.LastName,
                 FirstName = objEmployees.FirstName,
                 Title = objEmployees.Title,
                 TitleOfCourtesy = objEmployees.TitleOfCourtesy,
                 BirthDate = objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : null,
                 HireDate = objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : null,
                 Address = objEmployees.Address,
                 City = objEmployees.City,
                 Region1 = objEmployees.Region1,
                 PostalCode = objEmployees.PostalCode,
                 Country = objEmployees.Country,
                 HomePhone = objEmployees.HomePhone,
                 Extension = objEmployees.Extension,
                 Notes = objEmployees.Notes,
                 ReportsTo = objEmployees.ReportsTo,
                 PhotoPath = objEmployees.PhotoPath
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Employees table
         /// </summary>
         /// <returns>Total number of records in the Employees table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Employees.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Employees table by ReportsTo
         /// </summary>
         /// <param name="id">reportsTo</param>
         /// <returns>Total number of records in the Employees table by reportsTo</returns>
         [HttpGet]
         public int GetRecordCountByReportsTo(int id)
         {
             return Employees.GetRecordCountByReportsTo(id);
         }

         /// <summary>
         /// Gets the total number of records in the Employees table based on search parameters
         /// </summary>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="lastName">LastName</param>
         /// <param name="firstName">FirstName</param>
         /// <param name="title">Title</param>
         /// <param name="titleOfCourtesy">TitleOfCourtesy</param>
         /// <param name="birthDate">BirthDate</param>
         /// <param name="hireDate">HireDate</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="homePhone">HomePhone</param>
         /// <param name="extension">Extension</param>
         /// <param name="reportsTo">ReportsTo</param>
         /// <param name="photoPath">PhotoPath</param>
         /// <returns>Total number of records in the Employees table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
             return Employees.GetRecordCountDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects records by ReportsTo as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <param name="reportsTo">ReportsTo</param>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeByReportsTo(int rows, int startRowIndex, string sortByExpression, int reportsTo)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTakeByReportsTo(rows, startRowIndex, sortByExpression, reportsTo);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Employees sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="lastName">LastName</param>
         /// <param name="firstName">FirstName</param>
         /// <param name="title">Title</param>
         /// <param name="titleOfCourtesy">TitleOfCourtesy</param>
         /// <param name="birthDate">BirthDate</param>
         /// <param name="hireDate">HireDate</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="homePhone">HomePhone</param>
         /// <param name="extension">Extension</param>
         /// <param name="reportsTo">ReportsTo</param>
         /// <param name="photoPath">PhotoPath</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTakeDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Employees
         /// </summary>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             EmployeesCollection objEmployeesCol = Employees.SelectAll();
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Employees sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             EmployeesCollection objEmployeesCol = Employees.SelectAll(sortExpression);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Employees.
         /// </summary>
         /// <param name="employeeID">EmployeeID</param>
         /// <param name="lastName">LastName</param>
         /// <param name="firstName">FirstName</param>
         /// <param name="title">Title</param>
         /// <param name="titleOfCourtesy">TitleOfCourtesy</param>
         /// <param name="birthDate">BirthDate</param>
         /// <param name="hireDate">HireDate</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="homePhone">HomePhone</param>
         /// <param name="extension">Extension</param>
         /// <param name="reportsTo">ReportsTo</param>
         /// <param name="photoPath">PhotoPath</param>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
             EmployeesCollection objEmployeesCol = Employees.SelectAllDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects all Employees by Employees, related to column ReportsTo
         /// </summary>
         /// <param name="id">employeeID</param>
         /// <returns>Total number of records in the Employees table by reportsTo</returns>
         [HttpGet]
         public object SelectEmployeesCollectionByReportsTo(int id)
         {
             EmployeesCollection objEmployeesCol = Employees.SelectEmployeesCollectionByReportsTo(id);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects all Employees by Employees, related to column ReportsTo, sorted by the sort expression
         /// </summary>
         /// <param name="id">employeeID</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Total number of records in the Employees table by reportsTo</returns>
         [HttpGet]
         public object SelectEmployeesCollectionByReportsTo(int id, string sortExpression)
         {
             EmployeesCollection objEmployeesCol = Employees.SortByExpression(Employees.SelectEmployeesCollectionByReportsTo(id), sortExpression);
             return GetJsonCollection(objEmployeesCol);
         }

         /// <summary>
         /// Selects EmployeeID and LastName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Employees collection in json format</returns>
         [HttpGet]
         public object SelectEmployeesDropDownListData()
         {
             EmployeesCollection objEmployeesCol = Employees.SelectEmployeesDropDownListData();

             var jsonData = (from objEmployees in objEmployeesCol
                 select new
                 {
                     EmployeeID = objEmployees.EmployeeID,
                     LastName = objEmployees.LastName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(EmployeesCollection objEmployeesCol)
         {
             if (objEmployeesCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objEmployees in objEmployeesCol
                 select new
                 {
                     EmployeeID = objEmployees.EmployeeID,
                     LastName = objEmployees.LastName,
                     FirstName = objEmployees.FirstName,
                     Title = objEmployees.Title,
                     TitleOfCourtesy = objEmployees.TitleOfCourtesy,
                     BirthDate = objEmployees.BirthDate.HasValue ? objEmployees.BirthDate.Value.ToShortDateString() : null,
                     HireDate = objEmployees.HireDate.HasValue ? objEmployees.HireDate.Value.ToShortDateString() : null,
                     Address = objEmployees.Address,
                     City = objEmployees.City,
                     Region1 = objEmployees.Region1,
                     PostalCode = objEmployees.PostalCode,
                     Country = objEmployees.Country,
                     HomePhone = objEmployees.HomePhone,
                     Extension = objEmployees.Extension,
                     Notes = objEmployees.Notes,
                     ReportsTo = objEmployees.ReportsTo,
                     PhotoPath = objEmployees.PhotoPath
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "EmployeeID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
