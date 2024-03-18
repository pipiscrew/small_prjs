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
     /// Base class for SuppliersApiController.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersApiController class
     /// </summary>
     public class SuppliersApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the SuppliersModel here.  Arrives as SuppliersFields which automatically strips the data annotations from the SuppliersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]SuppliersFields model)
         {
             return AddEditSuppliers(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the SuppliersModel
         /// </summary>
         /// <param name="model">Pass the SuppliersModel here.  Arrives as SuppliersFields which automatically strips the data annotations from the SuppliersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]SuppliersFields model)
         {
             return AddEditSuppliers(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">SupplierID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(int id)
         {
             Suppliers.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditSuppliers(SuppliersFields model, CrudOperation operation)
         {
             Suppliers objSuppliers;

             if (operation == CrudOperation.Add)
                objSuppliers = new Suppliers();
             else
                objSuppliers = Suppliers.SelectByPrimaryKey(model.SupplierID);

             objSuppliers.SupplierID = model.SupplierID;
             objSuppliers.CompanyName = model.CompanyName;
             objSuppliers.ContactName = model.ContactName;
             objSuppliers.ContactTitle = model.ContactTitle;
             objSuppliers.Address = model.Address;
             objSuppliers.City = model.City;
             objSuppliers.Region1 = model.Region1;
             objSuppliers.PostalCode = model.PostalCode;
             objSuppliers.Country = model.Country;
             objSuppliers.Phone = model.Phone;
             objSuppliers.Fax = model.Fax;
             objSuppliers.HomePage = model.HomePage;

             if (operation == CrudOperation.Add)
                objSuppliers.Insert();
             else
                objSuppliers.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private SuppliersCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     int? supplierID = null;
                     string companyName = String.Empty;
                     string contactName = String.Empty;
                     string contactTitle = String.Empty;
                     string address = String.Empty;
                     string city = String.Empty;
                     string region1 = String.Empty;
                     string postalCode = String.Empty;
                     string country = String.Empty;
                     string phone = String.Empty;
                     string fax = String.Empty;

                     foreach (string filter in filterArray)
                     {
                         string[] fieldsArray = Regex.Split(filter, ",");
                         fieldName.Add(fieldsArray[0].Replace("\"field\":", "").Replace("\"", "").ToLower().Trim());
                         data.Add(fieldsArray[2].Replace("\"data\":", "").Replace("\"", "").ToLower().Trim());
                     }

                     foreach (string item in fieldName)
                     {
                         if (item == "supplierid")
                             supplierID = Convert.ToInt32(data[ctr]);

                         if (item == "companyname")
                             companyName = data[ctr];

                         if (item == "contactname")
                             contactName = data[ctr];

                         if (item == "contacttitle")
                             contactTitle = data[ctr];

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

                         if (item == "phone")
                             phone = data[ctr];

                         if (item == "fax")
                             fax = data[ctr];

                         ctr++;
                     }

                     totalRecords = Suppliers.GetRecordCountDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
                     return Suppliers.SelectSkipAndTakeDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Suppliers.GetRecordCount();
             return Suppliers.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Suppliers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Suppliers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objSuppliersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objSuppliers in objSuppliersCol
                     select new
                     {
                         id = objSuppliers.SupplierID,
                         cell = new string[] { 
                             objSuppliers.SupplierID.ToString(),
                             objSuppliers.CompanyName,
                             objSuppliers.ContactName,
                             objSuppliers.ContactTitle,
                             objSuppliers.Address,
                             objSuppliers.City,
                             objSuppliers.Region1,
                             objSuppliers.PostalCode,
                             objSuppliers.Country,
                             objSuppliers.Phone,
                             objSuppliers.Fax,
                             objSuppliers.HomePage
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Suppliers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Suppliers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             SuppliersCollection objSuppliersCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objSuppliersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objSuppliers in objSuppliersCol
                     select new
                     {
                         id = objSuppliers.SupplierID,
                         cell = new string[] { 
                             objSuppliers.SupplierID.ToString(),
                             objSuppliers.CompanyName,
                             objSuppliers.ContactName,
                             objSuppliers.ContactTitle,
                             objSuppliers.Address,
                             objSuppliers.City,
                             objSuppliers.Region1,
                             objSuppliers.PostalCode,
                             objSuppliers.Country,
                             objSuppliers.Phone,
                             objSuppliers.Fax,
                             objSuppliers.HomePage
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Suppliers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Suppliers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objSuppliersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objSuppliers in objSuppliersCol
                     select new
                     {
                         id = objSuppliers.SupplierID,
                         cell = new string[] { 
                             objSuppliers.SupplierID.ToString(),
                             objSuppliers.CompanyName,
                             objSuppliers.ContactName,
                             objSuppliers.ContactTitle,
                             objSuppliers.Address,
                             objSuppliers.City,
                             objSuppliers.Region1,
                             objSuppliers.PostalCode,
                             objSuppliers.Country,
                             objSuppliers.Phone,
                             objSuppliers.Fax,
                             objSuppliers.HomePage
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">SupplierID</param>
         /// <returns>One serialized Suppliers record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(int id)
         {
             Suppliers objSuppliers = Suppliers.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 SupplierID = objSuppliers.SupplierID,
                 CompanyName = objSuppliers.CompanyName,
                 ContactName = objSuppliers.ContactName,
                 ContactTitle = objSuppliers.ContactTitle,
                 Address = objSuppliers.Address,
                 City = objSuppliers.City,
                 Region1 = objSuppliers.Region1,
                 PostalCode = objSuppliers.PostalCode,
                 Country = objSuppliers.Country,
                 Phone = objSuppliers.Phone,
                 Fax = objSuppliers.Fax,
                 HomePage = objSuppliers.HomePage
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Suppliers table
         /// </summary>
         /// <returns>Total number of records in the Suppliers table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Suppliers.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Suppliers table based on search parameters
         /// </summary>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="contactName">ContactName</param>
         /// <param name="contactTitle">ContactTitle</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="phone">Phone</param>
         /// <param name="fax">Fax</param>
         /// <returns>Total number of records in the Suppliers table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return Suppliers.GetRecordCountDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objSuppliersCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Suppliers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="contactName">ContactName</param>
         /// <param name="contactTitle">ContactTitle</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="phone">Phone</param>
         /// <param name="fax">Fax</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTakeDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objSuppliersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Suppliers
         /// </summary>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             SuppliersCollection objSuppliersCol = Suppliers.SelectAll();
             return GetJsonCollection(objSuppliersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Suppliers sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             SuppliersCollection objSuppliersCol = Suppliers.SelectAll(sortExpression);
             return GetJsonCollection(objSuppliersCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Suppliers.
         /// </summary>
         /// <param name="supplierID">SupplierID</param>
         /// <param name="companyName">CompanyName</param>
         /// <param name="contactName">ContactName</param>
         /// <param name="contactTitle">ContactTitle</param>
         /// <param name="address">Address</param>
         /// <param name="city">City</param>
         /// <param name="region1">Region1</param>
         /// <param name="postalCode">PostalCode</param>
         /// <param name="country">Country</param>
         /// <param name="phone">Phone</param>
         /// <param name="fax">Fax</param>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             SuppliersCollection objSuppliersCol = Suppliers.SelectAllDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             return GetJsonCollection(objSuppliersCol);
         }

         /// <summary>
         /// Selects SupplierID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Suppliers collection in json format</returns>
         [HttpGet]
         public object SelectSuppliersDropDownListData()
         {
             SuppliersCollection objSuppliersCol = Suppliers.SelectSuppliersDropDownListData();

             var jsonData = (from objSuppliers in objSuppliersCol
                 select new
                 {
                     SupplierID = objSuppliers.SupplierID,
                     CompanyName = objSuppliers.CompanyName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(SuppliersCollection objSuppliersCol)
         {
             if (objSuppliersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objSuppliers in objSuppliersCol
                 select new
                 {
                     SupplierID = objSuppliers.SupplierID,
                     CompanyName = objSuppliers.CompanyName,
                     ContactName = objSuppliers.ContactName,
                     ContactTitle = objSuppliers.ContactTitle,
                     Address = objSuppliers.Address,
                     City = objSuppliers.City,
                     Region1 = objSuppliers.Region1,
                     PostalCode = objSuppliers.PostalCode,
                     Country = objSuppliers.Country,
                     Phone = objSuppliers.Phone,
                     Fax = objSuppliers.Fax,
                     HomePage = objSuppliers.HomePage
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "SupplierID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
