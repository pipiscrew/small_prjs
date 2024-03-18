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
     /// Base class for CustomersApiController.  Do not make changes to this class,
     /// instead, put additional code in the CustomersApiController class
     /// </summary>
     public class CustomersApiControllerBase : ApiController
     {
         /// <summary>
         /// Inserts/Adds/Creates a new record in the database
         /// </summary>
         /// <param name="model">Pass the CustomersModel here.  Arrives as CustomersFields which automatically strips the data annotations from the CustomersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Insert([FromBody]CustomersFields model)
         {
             return AddEditCustomers(model, CrudOperation.Add);
         }

         /// <summary>
         /// Updates an existing record in the database by primary key.  Pass the primary key in the CustomersModel
         /// </summary>
         /// <param name="model">Pass the CustomersModel here.  Arrives as CustomersFields which automatically strips the data annotations from the CustomersModel.</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpPost]
         public HttpResponseMessage Update([FromBody]CustomersFields model)
         {
             return AddEditCustomers(model, CrudOperation.Update);
         }

         /// <summary>
         /// Deletes an existing record by primary key
         /// </summary>
         /// <param name="id">CustomerID</param>
         /// <returns>HttpResponseMessage</returns>
         [HttpDelete]
         public HttpResponseMessage Delete(string id)
         {
             Customers.Delete(id);
             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private HttpResponseMessage AddEditCustomers(CustomersFields model, CrudOperation operation)
         {
             Customers objCustomers;

             if (operation == CrudOperation.Add)
                objCustomers = new Customers();
             else
                objCustomers = Customers.SelectByPrimaryKey(model.CustomerID);

             objCustomers.CustomerID = model.CustomerID;
             objCustomers.CompanyName = model.CompanyName;
             objCustomers.ContactName = model.ContactName;
             objCustomers.ContactTitle = model.ContactTitle;
             objCustomers.Address = model.Address;
             objCustomers.City = model.City;
             objCustomers.Region1 = model.Region1;
             objCustomers.PostalCode = model.PostalCode;
             objCustomers.Country = model.Country;
             objCustomers.Phone = model.Phone;
             objCustomers.Fax = model.Fax;

             if (operation == CrudOperation.Add)
                objCustomers.Insert();
             else
                objCustomers.Update();

             return Request.CreateResponse(HttpStatusCode.OK);
         }

         private CustomersCollection GetFilteredData(string sidx, string sord, string filters, out int totalRecords, int rows, int startRowIndex, string sortExpression)
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
                     string customerID = String.Empty;
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
                         if (item == "customerid")
                             customerID = data[ctr];

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

                     totalRecords = Customers.GetRecordCountDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
                     return Customers.SelectSkipAndTakeDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, rows, startRowIndex, sortExpression);
                 }
             }

             totalRecords = Customers.GetRecordCount();
             return Customers.SelectSkipAndTake(rows, startRowIndex, sortExpression);
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Customers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Customers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTake(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomersCollection objCustomersCol = Customers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomers in objCustomersCol
                     select new
                     {
                         id = objCustomers.CustomerID,
                         cell = new string[] { 
                             objCustomers.CustomerID,
                             objCustomers.CompanyName,
                             objCustomers.ContactName,
                             objCustomers.ContactTitle,
                             objCustomers.Address,
                             objCustomers.City,
                             objCustomers.Region1,
                             objCustomers.PostalCode,
                             objCustomers.Country,
                             objCustomers.Phone,
                             objCustomers.Fax
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Customers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records based on the search filters.
         /// </summary>
         /// <param name="_search">true or false</param>
         /// <param name="nd">nd</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="page">Current page</param>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="filters">Optional.  Filters used in search</param>
         /// <returns>Serialized Customers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithFilters(string _search, string nd, int rows, int page, string sidx, string sord, string filters = "")
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomersCollection objCustomersCol = this.GetFilteredData(sidx, sord, filters, out totalRecords, rows, startRowIndex, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomers in objCustomersCol
                     select new
                     {
                         id = objCustomers.CustomerID,
                         cell = new string[] { 
                             objCustomers.CustomerID,
                             objCustomers.CompanyName,
                             objCustomers.ContactName,
                             objCustomers.ContactTitle,
                             objCustomers.Address,
                             objCustomers.City,
                             objCustomers.Region1,
                             objCustomers.PostalCode,
                             objCustomers.Country,
                             objCustomers.Phone,
                             objCustomers.Fax
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Use in a JQGrid plugin.  Selects records as a collection (List) of Customers sorted by the sortByExpression.
         /// Also returns total pages, current page, and total records.
         /// </summary>
         /// <param name="sidx">Field to sort.  Can be an empty string.</param>
         /// <param name="sord">asc or an empty string = ascending.  desc = descending</param>
         /// <param name="page">Current page</param>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <returns>Serialized Customers collection in json format for use in a JQGrid plugin</returns>
         [HttpGet]
         public object SelectSkipTakeWithTotals(string sidx, string sord, int page, int rows)
         {
             int totalRecords = 0;
             int startRowIndex = ((page * rows) - rows) + 1;
             CustomersCollection objCustomersCol = Customers.SelectSkipAndTake(rows, startRowIndex, out totalRecords, sidx + " " + sord);
             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

             if (objCustomersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = new
             {
                 total = totalPages,
                 page,
                 records = totalRecords,
                 rows = (
                     from objCustomers in objCustomersCol
                     select new
                     {
                         id = objCustomers.CustomerID,
                         cell = new string[] { 
                             objCustomers.CustomerID,
                             objCustomers.CompanyName,
                             objCustomers.ContactName,
                             objCustomers.ContactTitle,
                             objCustomers.Address,
                             objCustomers.City,
                             objCustomers.Region1,
                             objCustomers.PostalCode,
                             objCustomers.Country,
                             objCustomers.Phone,
                             objCustomers.Fax
                         }
                     }).ToArray()
             };

             return jsonData;
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         /// <param name="id">CustomerID</param>
         /// <returns>One serialized Customers record in json format</returns>
         [HttpGet]
         public object SelectByPrimaryKey(string id)
         {
             Customers objCustomers = Customers.SelectByPrimaryKey(id);

             var jsonData = new
             {
                 CustomerID = objCustomers.CustomerID,
                 CompanyName = objCustomers.CompanyName,
                 ContactName = objCustomers.ContactName,
                 ContactTitle = objCustomers.ContactTitle,
                 Address = objCustomers.Address,
                 City = objCustomers.City,
                 Region1 = objCustomers.Region1,
                 PostalCode = objCustomers.PostalCode,
                 Country = objCustomers.Country,
                 Phone = objCustomers.Phone,
                 Fax = objCustomers.Fax
             };

             return jsonData;
         }

         /// <summary>
         /// Gets the total number of records in the Customers table
         /// </summary>
         /// <returns>Total number of records in the Customers table</returns>
         [HttpGet]
         public int GetRecordCount()
         {
             return Customers.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Customers table based on search parameters
         /// </summary>
         /// <param name="customerID">CustomerID</param>
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
         /// <returns>Total number of records in the Customers table based on the search parameters</returns>
         [HttpGet]
         public int GetRecordCountDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             return Customers.GetRecordCountDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         /// <param name="rows">Number of rows to retrieve</param>
         /// <param name="startRowIndex">Zero-based.  Row index where to start taking rows from</param>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CustomersCollection objCustomersCol = Customers.SelectSkipAndTake(rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCustomersCol);
         }

         /// <summary>
         /// Selects records as a collection (List) of Customers sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         /// <param name="customerID">CustomerID</param>
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
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectSkipAndTakeDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             CustomersCollection objCustomersCol = Customers.SelectSkipAndTakeDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, rows, startRowIndex, sortByExpression);
             return GetJsonCollection(objCustomersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Customers
         /// </summary>
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectAll()
         {
             CustomersCollection objCustomersCol = Customers.SelectAll();
             return GetJsonCollection(objCustomersCol);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Customers sorted by the sort expression
         /// </summary>
         /// <param name="sortByExpression">Field to sort and sort direction.  E.g. "FieldName asc" or "FieldName desc"</param>
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectAll(string sortExpression)
         {
             CustomersCollection objCustomersCol = Customers.SelectAll(sortExpression);
             return GetJsonCollection(objCustomersCol);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Customers.
         /// </summary>
         /// <param name="customerID">CustomerID</param>
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
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectAllDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
             CustomersCollection objCustomersCol = Customers.SelectAllDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);
             return GetJsonCollection(objCustomersCol);
         }

         /// <summary>
         /// Selects CustomerID and CompanyName columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         /// <returns>Serialized Customers collection in json format</returns>
         [HttpGet]
         public object SelectCustomersDropDownListData()
         {
             CustomersCollection objCustomersCol = Customers.SelectCustomersDropDownListData();

             var jsonData = (from objCustomers in objCustomersCol
                 select new
                 {
                     CustomerID = objCustomers.CustomerID,
                     CompanyName = objCustomers.CompanyName
                 }).ToArray();

             return jsonData;
         }

         private object GetJsonCollection(CustomersCollection objCustomersCol)
         {
             if (objCustomersCol == null)
                 return "{ total = 0, page = 0, records = 0, rows = null }";

             var jsonData = (from objCustomers in objCustomersCol
                 select new
                 {
                     CustomerID = objCustomers.CustomerID,
                     CompanyName = objCustomers.CompanyName,
                     ContactName = objCustomers.ContactName,
                     ContactTitle = objCustomers.ContactTitle,
                     Address = objCustomers.Address,
                     City = objCustomers.City,
                     Region1 = objCustomers.Region1,
                     PostalCode = objCustomers.PostalCode,
                     Country = objCustomers.Country,
                     Phone = objCustomers.Phone,
                     Fax = objCustomers.Fax
                 }).ToArray();

             return jsonData;
         }

         private string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "CustomerID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }
     }
}
